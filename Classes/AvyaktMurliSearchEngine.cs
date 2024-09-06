using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

using Lucene.Net.Analysis.Hi;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;

using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

using static MurliAnveshan.Classes.Enums;

using Document = Lucene.Net.Documents.Document;

namespace MurliAnveshan.Classes
{
    internal class AvyaktMurliSearchEngine : MurliSearchEngineBase
    {
        #region Private Fields

        private readonly string indexFolderPath;
        private readonly string murlisFolderPath;

        #endregion Private Fields

        #region Public Constructors

        public AvyaktMurliSearchEngine() : base()
        {
            //Create an analyzer to process the text
            analyzer = new HindiAnalyzer(luceneVersion);// StandardAnalyzer(luceneVersion);
            //Create an index writer
            indexConfig = new IndexWriterConfig(luceneVersion, analyzer);

            indexName = "Avyakt Murli Index";

            indexFolderPath = ConfigurationManager.ConnectionStrings["IndexPath"].ConnectionString;

            indexPath = Path.Combine(indexFolderPath, "Hindi", indexName);

            indexDir = FSDirectory.Open(indexPath);
            indexConfig.OpenMode = OpenMode.CREATE;     // create/overwrite index
            indxWriter = new IndexWriter(indexDir, indexConfig);

            murlisFolderPath = ConfigurationManager.ConnectionStrings["MurlisPath"].ConnectionString;
        }

        #endregion Public Constructors

        #region Public Methods

        public override bool AddMurliDetailsToIndex(IEnumerable<AvyaktMurliDetails> murliDetails)
        {
            try
            {
                foreach (var _murliDetails in murliDetails)
                {
                    var document = new Document();
                    document.Add(new TextField(nameof(MurliDetailsBase.MurliDate), _murliDetails.MurliDate, Field.Store.YES));
                    document.Add(new TextField(nameof(MurliDetailsBase.MurliTitle), _murliDetails.MurliTitle, Field.Store.YES));
                    //document.Add(new TextField(nameof(MurliDetails.MurliLines), string.Join(" ", _murliDetails.MurliLines), Field.Store.YES));

                    foreach (var line in _murliDetails.MurliLines)
                    {
                        document.Add(new TextField(nameof(MurliDetailsBase.MurliLines), line, Field.Store.YES));
                    }

                    //document.Add(new TextField(nameof(MurliDetails.MurliLines), string.Join(" ", _murliDetails.MurliLines), Field.Store.YES));

                    //document.Add(new TextField(nameof(MurliDetails.FileName), _murliDetails.FileName, Field.Store.YES));
                    indxWriter.AddDocument(document);
                }
                indxWriter.Commit();
                indxWriter.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public override bool BuildIndex()
        {
            string fileName = "1969.docx";

            List<AvyaktMurliDetails> murliDetailsList = ExtractDetailsFromDocx(Path.Combine(murlisFolderPath, @"Hindi Murlis\Avyakt Murlis\", fileName));

            if (AddMurliDetailsToIndex(murliDetailsList))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<AvyaktMurliDetails> ExtractDetailsFromDocx(string docxPath)
        {
            List<AvyaktMurliDetails> murliDetails = new List<AvyaktMurliDetails>();
            AvyaktMurliDetails currentChapter = null;
            //StringBuilder currentTitleBuilder = new StringBuilder();
            StringBuilder fullParagraphText = new StringBuilder();
            string fullParagraph;

            using (WordDocument document = new WordDocument(docxPath, FormatType.Docx))
            {
                foreach (IWSection section in document.Sections)
                {
                    foreach (IWParagraph paragraph in section.Body.Paragraphs)
                    {
                        fullParagraphText.Clear();// To accumulate the full line

                        int appendCount = 0;
                        foreach (IWTextRange textRange in paragraph.ChildEntities.OfType<IWTextRange>())
                        {
                            fullParagraphText.Append(textRange.Text); // Concatenate text from all runs

                            appendCount++;

                            if (appendCount != paragraph.ChildEntities.Count)
                                continue;

                            //if (string.IsNullOrEmpty(fullParagraphText)) continue;

                            var format = textRange.CharacterFormat;
                            fullParagraph = fullParagraphText.ToString();

                            // Check for Date (Noto Sans, Size 14, Red color)
                            if (IsDate(format))
                            {
                                if (currentChapter != null)
                                    murliDetails.Add(currentChapter);

                                currentChapter = new AvyaktMurliDetails { MurliDate = fullParagraph.Split(' ')[0] };
                            }

                            // Check for Title (Noto Sans, Size 16, Double quoted)
                            else if (IsTitle(format, fullParagraph))
                            {
                                if (currentChapter != null)
                                {
                                    currentChapter.MurliTitle = fullParagraph;
                                }
                            }

                            // Body (Tiro Devanagari Hindi, Size 13)
                            else if (IsBodyText(format))
                            {
                                currentChapter?.MurliLines.Add(fullParagraph);
                            }
                        }
                    }
                }

                if (currentChapter != null) murliDetails.Add(currentChapter); // Add last chapter
            }

            return murliDetails;
        }

        public override IEnumerable<MurliDetailsBase> SearchIndex(string searchTerm, SearchLocation searchLocation)
        {
            var directoryReader = DirectoryReader.Open(indexDir);
            var indexSearcher = new IndexSearcher(directoryReader);

            Query query = CreateQueryBasedOnSearchLocation(searchTerm, searchLocation);

            var hits = indexSearcher.Search(query, 10).ScoreDocs;

            var murlidetails = new List<MurliDetailsBase>();
            MurliDetailsBase murli;

            foreach (var hit in hits)
            {
                var document = indexSearcher.Doc(hit.Doc);

                murli = new AvyaktMurliDetails();

                murli.MurliDate = document.Get(nameof(MurliDetailsBase.MurliDate));
                murli.MurliTitle = document.Get(nameof(MurliDetailsBase.MurliTitle));

                //murlis.//MurliLines = document.Get("MurliLines")?.Split(new[] { "" }, StringSplitOptions.RemoveEmptyEntries).ToList(), //MurliLinesContainingSearchTerm. Add(document.Get("MurliLinesContainingSearchTerm")),
                //murlis.// Working// MurliLines = document.GetValues(nameof(MurliDetails.MurliLines)).ToList<string>(), //MurliLinesContainingSearchTerm. Add(document.Get("MurliLinesContainingSearchTerm")),

                if (searchLocation == SearchLocation.All)
                {
                    IEnumerable<string> relevantSentences = GetSurroundingSentences(document.GetValues(nameof(MurliDetailsBase.MurliLines)).ToList<string>(), searchTerm);

                    foreach (var sentence in relevantSentences)
                    {
                        murli.MurliLines.Add(sentence);
                    }
                }

                murli.SearchTerm = document.Get(nameof(MurliDetailsBase.SearchTerm));
                murli.FileName = document.Get(nameof(MurliDetailsBase.FileName));

                murlidetails.Add(murli);
            }

            return murlidetails;
        }

        #endregion Public Methods

        #region Private Methods

        private Query CreateQueryBasedOnSearchLocation(string searchTerm, SearchLocation searchLocation)
        {
            QueryParser queryParser = null;
            Query query = null;

            if (searchLocation == SearchLocation.All)
            {
                string[] fields = { nameof(MurliDetailsBase.MurliDate), nameof(MurliDetailsBase.MurliTitle), nameof(MurliDetailsBase.MurliLines) }; //"FileName", "SearchTerm" };
                queryParser = new MultiFieldQueryParser(luceneVersion, fields, analyzer);
            }
            else if (searchLocation == SearchLocation.TitleOnly)
            {
                //string[] fields = { "MurliDate", "MurliTitle", "MurliLines", "PageNumber" }; //"FileName", "SearchTerm" };
                queryParser = new QueryParser(luceneVersion, nameof(MurliDetailsBase.MurliTitle), analyzer);
            }
            //else if (searchLocation == SearchLocation.ContentOnly)
            //{
            //    queryParser = new QueryParser(luceneVersion, nameof(MurliDetails.MurliLines), _standardAnalyzer);
            //    //query2 = new TermQuery(new Term("MurliLines", searchTerm));
            //    //var hits2 = indexSearcher.Search(query2, 10).ScoreDocs;
            //}
            try
            {
                return query = queryParser?.Parse(searchTerm);
            }
            catch (Lucene.Net.QueryParsers.Classic.ParseException e)
            {
                // Handle parsing exception
                return query;
            }
        }

        private IEnumerable<string> GetSurroundingSentences(List<string> content, string searchTerm, int contextSize = 2)
        {
            var result = new List<string>();

            foreach (var item in content)
            {
                var sentences = item.Split(new[] { "।", "|" }, StringSplitOptions.RemoveEmptyEntries);

                // Find the index of the sentence containing the search term
                var searchTermIndex = -1;
                for (int i = 0; i < sentences.Length; i++)
                {
                    if (sentences[i].IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        searchTermIndex = i;

                        // Get the start and end index for the surrounding sentences
                        int start = Math.Max(0, searchTermIndex - contextSize);
                        int end = Math.Min(sentences.Length - 1, searchTermIndex + contextSize);

                        // Extract the surrounding sentences
                        for (int n = start; n <= end; n++)
                        {
                            if (result.Contains(sentences[n]))
                                continue;

                            result.Add(sentences[n]);
                        }

                        result.Add("\n");
                    }
                }
            }

            return result;
        }

        private bool IsBodyText(WCharacterFormat format)
        {
            return format.FontName == "Tiro Devanagari Hindi" || format.FontName == "Noto Sans" && format.FontSize == 13;
        }

        private bool IsDate(WCharacterFormat format)
        {
            return format.FontName == "Noto Sans" && format.FontSize == 14 && format.Bold && format.TextColor == Color.FromArgb(255, 255, 0, 0);
        }

        private bool IsTitle(WCharacterFormat format, string text)
        {
            return format.FontName == "Noto Sans" && format.FontSize == 16 && format.Bold; // && text.StartsWith("\"") && text.EndsWith("\"");
        }

        #endregion Private Methods
    }
}