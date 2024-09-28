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
        private readonly string hindiAVMurlisPath;

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

            bool indexExists = DirectoryReader.IndexExists(indexDir);

            if (indexExists)
            {
                // If the index exists, open it in append mode
                indexConfig.OpenMode = OpenMode.APPEND;  // Open for reading and appending
            }
            else
            {
                // If the index does not exist, create it
                indexConfig.OpenMode = OpenMode.CREATE;  // Create a new index
            }

            indxWriter = new IndexWriter(indexDir, indexConfig);

            hindiAVMurlisPath = ConfigurationManager.ConnectionStrings["HindiAVMurlisPath"].ConnectionString;
        }

        #endregion Public Constructors

        #region Public Methods

        public override bool AddMurliDetailsToIndex(IEnumerable<AvyaktMurliDetails> murliDetails)
        {
            try
            {
                foreach (var _murliDetails in murliDetails)
                {
                    var document = new Document
                    {
                        new TextField(nameof(AvyaktMurliDetails.MurliDate), _murliDetails.MurliDate, Field.Store.YES),
                        new TextField(nameof(AvyaktMurliDetails.MurliTitle), _murliDetails.MurliTitle, Field.Store.YES),
                        new TextField(nameof(AvyaktMurliDetails.FileName), _murliDetails.FileName, Field.Store.YES)
                    };
                    //document.Add(new TextField(nameof(MurliDetails.MurliLines), string.Join(" ", _murliDetails.MurliLines), Field.Store.YES));

                    foreach (var line in _murliDetails.MurliLines)
                    {
                        document.Add(new TextField(nameof(AvyaktMurliDetails.MurliLines), line, Field.Store.YES));
                    }

                    //document.Add(new TextField(nameof(MurliDetails.MurliLines), string.Join(" ", _murliDetails.MurliLines), Field.Store.YES));

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

            List<AvyaktMurliDetails> murliDetailsList = ExtractDetailsFromDocx(Path.Combine(hindiAVMurlisPath, fileName));

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
            var murliDetails = new List<AvyaktMurliDetails>();
            AvyaktMurliDetails currentChapter = null;
            StringBuilder fullParagraphText = new StringBuilder();

            try
            {
                using (WordDocument document = new WordDocument(docxPath, FormatType.Docx))
                {
                    foreach (IWSection section in document.Sections)
                    {
                        foreach (IWParagraph paragraph in section.Body.Paragraphs)
                        {
                            // Clear the StringBuilder to accumulate the full line
                            fullParagraphText.Clear();

                            // Combine all text ranges into a single paragraph text
                            foreach (IWTextRange textRange in paragraph.ChildEntities.OfType<IWTextRange>())
                            {
                                fullParagraphText.Append(textRange.Text);
                            }

                            string fullParagraph = fullParagraphText.ToString().Trim();

                            // Skip processing if the paragraph is empty
                            if (string.IsNullOrEmpty(fullParagraph)) continue;

                            var format = paragraph.ChildEntities.OfType<IWTextRange>().FirstOrDefault()?.CharacterFormat;

                            // Check for Date (Noto Sans, Size 14, Red color)
                            if (IsDate(format))
                            {
                                if (currentChapter != null)
                                    murliDetails.Add(currentChapter);

                                currentChapter = new AvyaktMurliDetails
                                {
                                    FileName = Path.GetFileName(docxPath),
                                    MurliDate = fullParagraph.Split(' ')[0]
                                };
                            }
                            // Check for Title (Noto Sans, Size 16, Double quoted)
                            else if (IsTitle(format))
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

                    // Add the last chapter if it exists
                    if (currentChapter != null)
                        murliDetails.Add(currentChapter);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return murliDetails;
        }

        /*
        public override IEnumerable<MurliDetailsBase> SearchIndex(string searchTerm, SearchLocation searchLocation)
        {
            var directoryReader = DirectoryReader.Open(indexDir);
            var indexSearcher = new IndexSearcher(directoryReader);

            Query query = CreateQueryBasedOnSearchLocation(searchTerm, searchLocation);


            int pageSize = 10;  // Number of results per page
            int currentPage = 1; // Track the current page

            // Set start index based on current page
            int startIndex = (currentPage - 1) * pageSize;

            // Perform the search
            TopDocs hits = indexSearcher.Search(query, startIndex + pageSize);

            var murlidetails = new List<MurliDetailsBase>();
            MurliDetailsBase murli;

            for (int i = startIndex; i < startIndex + pageSize && i < hits.ScoreDocs.Length; i++)
            {
                var document = indexSearcher.Doc(hits.ScoreDocs[i].Doc);

                murli = new AvyaktMurliDetails
                {
                    MurliDate = document.Get(nameof(MurliDetailsBase.MurliDate)),
                    MurliTitle = document.Get(nameof(MurliDetailsBase.MurliTitle)),
                    SearchTerm = document.Get(nameof(MurliDetailsBase.SearchTerm)),
                    FileName = document.Get(nameof(MurliDetailsBase.FileName))
                };

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

                murlidetails.Add(murli);
            }

            return murlidetails;
        }
        */

        public override IEnumerable<MurliDetailsBase> SearchIndex(string searchTerm, SearchLocation searchLocation, int currentPage = 1, int pageSize = 10)
        {
            var murliDetailsList = new List<MurliDetailsBase>();

            using (var directoryReader = DirectoryReader.Open(indexDir))
            {
                var indexSearcher = new IndexSearcher(directoryReader);

                Query query = CreateQueryBasedOnSearchLocation(searchTerm, searchLocation);

                // Set the start index based on current page
                int startIndex = (currentPage - 1) * pageSize;

                // Perform the search and get TopDocs (fetch more results than pageSize to handle pagination smoothly)
                TopDocs hits = indexSearcher.Search(query, startIndex + pageSize);

                // Prepare a list to hold the results

                // Iterate over the ScoreDocs from the startIndex to retrieve the required number of results (page size)
                for (int i = startIndex; i < Math.Min(hits.ScoreDocs.Length, startIndex + pageSize); i++)
                {
                    // Fetch the document from the index
                    var doc = indexSearcher.Doc(hits.ScoreDocs[i].Doc);

                    // Create a MurliDetails object and populate it with the relevant fields from the document
                    var murliDetail = new AvyaktMurliDetails
                    {
                        MurliDate = doc.Get(nameof(MurliDetailsBase.MurliDate)),
                        MurliTitle = doc.Get(nameof(MurliDetailsBase.MurliTitle)),
                        SearchTerm = doc.Get(nameof(MurliDetailsBase.SearchTerm)),
                        FileName = doc.Get(nameof(MurliDetailsBase.FileName))
                    };

                    // If searching within the body text, retrieve surrounding sentences
                    if (searchLocation == SearchLocation.All)
                    {
                        var murliLines = doc.GetValues(nameof(MurliDetailsBase.MurliLines))?.ToList();
                        if (murliLines != null)
                        {
                            // Extract relevant sentences that match the search term
                            var relevantSentences = GetSurroundingSentences(murliLines, searchTerm);
                            murliDetail.MurliLines.AddRange(relevantSentences);
                        }
                    }

                    // Add the result to the list
                    murliDetailsList.Add(murliDetail);
                }
            }
            return murliDetailsList;
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
            bool contentAdded = false;

            foreach (var item in content)
            {
                var sentences = item.Split(new[] { "।", "|" }, StringSplitOptions.RemoveEmptyEntries);

                // Iterate through each sentence to find occurrences of the search term
                for (int i = 0; i < sentences.Length; i++)
                {
                    if (sentences[i].IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Get the start and end index for the surrounding sentences
                        int start = Math.Max(0, i - contextSize);
                        int end = Math.Min(sentences.Length - 1, i + contextSize);

                        // Add the indicator and surrounding sentences
                        if (!contentAdded)
                        {
                            result.Add("\t >   ");
                            contentAdded = true;
                        }

                        for (int n = start; n <= end; n++)
                        {
                            // Add sentence if not already in the result
                            if (!result.Contains(sentences[n]))
                            {
                                result.Add(sentences[n]);
                            }
                        }

                        result.Add("\n\t>   ");
                    }
                }
            }

            // Remove the trailing indicator if it's the last entry and no content was added after it
            if (result.Count > 0 && result[result.Count - 1].Trim() == ">")
            {
                result.RemoveAt(result.Count - 1);
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

        private bool IsTitle(WCharacterFormat format)
        {
            return format.FontName == "Noto Sans" && format.FontSize == 16 && format.Bold; // && text.StartsWith("\"") && text.EndsWith("\"");
        }

        #endregion Private Methods
    }
}