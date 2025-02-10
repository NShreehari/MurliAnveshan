using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Anotar.NLog;

using Lucene.Net.Analysis.Hi;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;

using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;
using static MurliAnveshan.Classes.Enums;

using Document = Lucene.Net.Documents.Document;

namespace MurliAnveshan.Classes
{
    internal class AvyaktMurliSearchEngine : MurliSearchEngineBase
    {
        #region Private Fields

        private readonly string indexFolderPath;
        private readonly string hindiAVMurlisPath;

        public readonly int pageSize;

        #endregion Private Fields

        #region Public Constructors

        public AvyaktMurliSearchEngine() : base()
        {
            this.pageSize = Convert.ToInt16(ConfigurationManager.AppSettings.Get("PageSize"));

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

            IndxWriter = new IndexWriter(indexDir, indexConfig);


            hindiAVMurlisPath = ConfigurationManager.ConnectionStrings["HindiAVMurlisPath"].ConnectionString;

            //InitializeIndexSearcher();

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
                        new StringField(nameof(AvyaktMurliDetails.MurliDate), _murliDetails.MurliDate, Field.Store.YES),
                        new TextField(nameof(AvyaktMurliDetails.MurliTitle), _murliDetails.MurliTitle, Field.Store.YES),
                        new TextField(nameof(AvyaktMurliDetails.FileName), _murliDetails.FileName, Field.Store.YES)
                    };
                    //document.Add(new TextField(nameof(MurliDetails.MurliLines), string.Join(" ", _murliDetails.MurliLines), Field.Store.YES));

                    foreach (var line in _murliDetails.MurliLines)
                    {
                        document.Add(new TextField(nameof(AvyaktMurliDetails.MurliLines), line, Field.Store.YES));
                    }

                    //document.Add(new TextField(nameof(MurliDetails.MurliLines), string.Join(" ", _murliDetails.MurliLines), Field.Store.YES));

                    IndxWriter.AddDocument(document);
                }
                IndxWriter.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public override bool BuildIndex()
        {
            //string fileName = "1969.docm";

            var AVMurliFolderPath = ConfigurationManager.ConnectionStrings["HindiAVMurlisPath"].ConnectionString;

            if (!System.IO.Directory.Exists(AVMurliFolderPath)) return false;

            var dirFile = new DirectoryInfo(AVMurliFolderPath);

            foreach (var file in dirFile.GetFiles("*.doc*"))
            {
                try
                {
                    //TODO Read All .doc/.Docx files from a selected Folder.
                    List<AvyaktMurliDetails> murliDetailsList = ExtractDetailsFromDocx(Path.Combine(hindiAVMurlisPath, file.Name));

                    if (murliDetailsList == null)
                    {
                        return false;
                    }
                    else
                    {
                        AddMurliDetailsToIndex(murliDetailsList);
                    }
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            return true;
        }

        public List<AvyaktMurliDetails> ExtractDetailsFromDocx(string docxPath)
        {
            var murliDetails = new List<AvyaktMurliDetails>();
            AvyaktMurliDetails currentChapter = null;
            StringBuilder fullParagraphText = new StringBuilder();

            bool sectionBreakFound = false;

            try
            {
                using (WordDocument document = new WordDocument(docxPath, FormatType.Docx))
                {
                    foreach (IWSection section in document.Sections)
                    {
                        // Check if we have encountered the first section break
                        if (!sectionBreakFound)
                        {
                            sectionBreakFound = true; // Mark the first section as found
                            continue; // Skip processing the first section
                        }

                        // Process sections after the first section break
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
                                    MurliDate = fullParagraph.Split(' ', '\t')[0]
                                };
                            }
                            // Check for Title (Noto Sans, Size 16, Double quoted)
                            else if (IsTitle(format) && (currentChapter != null))
                            {
                                char[] charsToTrim = { '"', '“', '”' };
                                currentChapter.MurliTitle = fullParagraph.Trim(charsToTrim);

                                //currentChapter.MurliTitle = fullParagraph;
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
            catch (IOException ioe) when (ioe.Message.Contains("used by another process"))
            {
                string errorMessage = $"The file '{docxPath}' is being used by another process. Please close any apps using the file and try again.";
                LogTo.ErrorException(errorMessage, ioe);
                SelfMessageBoxWrapper.ShowErrorMessage(errorMessage, docxPath);
                return null;
            }
            catch (Exception ex)
            {
                LogTo.ErrorException($"An unexpected error occurred while processing '{docxPath}'", ex);
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

        //IndexSearcher indexSearcher;

        //private void InitializeIndexSearcher()
        //{
        //    using (var directoryReader = DirectoryReader.Open(indexDir))
        //    {
        //        indexSearcher = new IndexSearcher(directoryReader);
        //    }
        //}


        public override PagedResults<MurliDetailsBase> SearchIndex(string searchTerm, SearchLocation searchLocation, int currentPage = 1)
        {
            using (var directoryReader = DirectoryReader.Open(indexDir))
            {
                var indexSearcher = new IndexSearcher(directoryReader);

                Query query = CreateQueryBasedOnSearchLocation(searchTerm, searchLocation);

                // Calculate the starting point for the page
                int startIndex = (currentPage - 1) * pageSize;

                /*
                // Create a filter query, e.g., exclude documents with a score below 0.1
                Query filterQuery = NumericRangeQuery.NewDoubleRange("score", 0.1, double.MaxValue, true, true);
                Filter filter = new QueryWrapperFilter(filterQuery);


                // Execute the search to get enough results for pagination
                TopDocs topDocs = indexSearcher.Search(query, filter, startIndex + pageSize);
                */

                TopDocs topDocs = indexSearcher.Search(query, startIndex + pageSize);

                //var filteredResults = topDocs.ScoreDocs
                //    .Where(sd => sd.Score >= 0.1) // Filter out scores below 0.1
                //    .Select(sd => indexSearcher.Doc(sd.Doc))
                //    .ToList();

                // Create a list to hold the results
                var murliDetailsList = new List<MurliDetailsBase>();

                // Fetch documents in the current page
                for (int i = startIndex; i < Math.Min(topDocs.ScoreDocs.Length, startIndex + pageSize); i++)
                {
                    // Fetch the document from the index
                    var doc = indexSearcher.Doc(topDocs.ScoreDocs[i].Doc);

                    //var doc = filteredResults[i];
                    //float score = topDocs.ScoreDocs[i].Score;

                    //float scoreThreshold = 0.1f;
                    //// Check if the score meets the threshold
                    //if (score < scoreThreshold)
                    //{
                    //    continue; // Skip this result if the score is below the threshold
                    //}

                    //Explanation explanation = indexSearcher.Explain(query, topDocs.ScoreDocs[i].Doc);

                    //Console.WriteLine($"Document ID: {topDocs.ScoreDocs[i].Doc}");
                    //Console.WriteLine($"Explanation: {explanation}");

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
                        if (murliLines?.Count > 0)
                        {
                            // Extract relevant sentences that match the search term
                            var relevantSentences = GetSurroundingSentences(murliLines, searchTerm);
                            murliDetail.MurliLines.AddRange(relevantSentences);
                        }
                    }

                    // Add the result to the list
                    murliDetailsList.Add(murliDetail);
                }

                // Return the paged results
                return new PagedResults<MurliDetailsBase>
                {
                    CurrentPage = currentPage,
                    TotalHits = topDocs.TotalHits,
                    Results = murliDetailsList
                };
            }
        }

        //this Method is used when User wish to export the search results to a file.
        //In that scenario we need to fetch all the results at once instead of pagewise hence the Method.
        public List<MurliDetailsBase> SearchAllIndex(string searchTerm, SearchLocation searchLocation)
        {
            var murliDetailsList = new List<MurliDetailsBase>();

            using (var directoryReader = DirectoryReader.Open(indexDir))
            {
                var indexSearcher = new IndexSearcher(directoryReader);

                Query query = CreateQueryBasedOnSearchLocation(searchTerm, searchLocation);

                TopDocs topDocs = indexSearcher.Search(query, int.MaxValue);

                foreach (var scoreDoc in topDocs.ScoreDocs)
                {
                    var doc = indexSearcher.Doc(scoreDoc.Doc);
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


        //public List<string> SearchWordsForSuggestion(string prefix)
        //{
        //    //using var reader = DirectoryReader.Open(luceneIndex);
        //    //var searcher = new IndexSearcher(reader);
        //    var query = new PrefixQuery(new Term("word", prefix));
        //    var hits = indexSearcher.Search(query, 10).ScoreDocs;

        //    return hits.Select(hit => indexSearcher.Doc(hit.Doc).Get("word")).ToList();
        //}


        #endregion Public Methods

        #region Private Methods

        internal static Query CreateQueryBasedOnSearchLocation(string searchTerm, SearchLocation searchLocation)
        {
            Query query = null;

            try
            {
                if (searchLocation == SearchLocation.All)
                {
                    if (IsDate(searchTerm))
                    {
                        //var DatequeryParser = new QueryParser(luceneVersion, nameof(MurliDetailsBase.MurliDate), analyzer);
                        //query = DatequeryParser.Parse(QueryParser.Escape(searchTerm));

                        // Create an exact match for the date using TermQuery
                        query = new TermQuery(new Term(nameof(AvyaktMurliDetails.MurliDate), searchTerm));
                    }
                    else
                    {
                        string[] fields = { nameof(AvyaktMurliDetails.MurliTitle), nameof(AvyaktMurliDetails.MurliLines) }; //"FileName", "SearchTerm" };
                        var multiFieldQueryParser = new MultiFieldQueryParser(luceneVersion, fields, analyzer);
                        query = multiFieldQueryParser.Parse(MultiFieldQueryParser.Escape(searchTerm));

                        /*
                        // For multi-field search, use PhraseQuery for exact matches
                        var fields = new[] { nameof(MurliDetailsBase.MurliTitle), nameof(MurliDetailsBase.MurliLines) };
                        var booleanQuery = new BooleanQuery();

                        // Create PhraseQuery for each field to ensure terms are together
                        foreach (var field in fields)
                        {
                            var phraseQuery = new PhraseQuery();
                            var terms = searchTerm.Split(' '); // Split the search term into individual words

                            foreach (var term in terms)
                            {
                                phraseQuery.Add(new Term(field, term));
                            }

                            // Add the phrase query to the boolean query
                            booleanQuery.Add(phraseQuery, Occur.SHOULD);
                        }

                        query = booleanQuery;
                        */
                    }
                }
                else if (searchLocation == SearchLocation.TitleOnly)
                {
                    //string[] fields = { "MurliDate", "MurliTitle", "MurliLines"}; // "PageNumber" }; "FileName", "SearchTerm" };
                    var titleQueryParser = new QueryParser(luceneVersion, nameof(AvyaktMurliDetails.MurliTitle), analyzer);
                    query = titleQueryParser.Parse(QueryParser.Escape(searchTerm));
                }
            }
            catch (Lucene.Net.QueryParsers.Classic.ParseException ex)
            {
                // Log and handle parsing exceptions (e.g., return a default query or log for debugging)
                Console.WriteLine($"Query parsing exception: {ex.Message}");
                // Use a query that matches no documents
                query = new BooleanQuery
                {
                    { new TermQuery(new Term("nonexistentfield", "value")), Occur.MUST }
                };
            }
            return query;
        }

        private static bool IsDate(string searchTerm)
        {
            const string pattern = @"^\d{2}-\d{2}-\d{4}$";

            if (Regex.IsMatch(searchTerm, pattern))
            {
                // Try to parse the date using the exact format
                if (DateTime.TryParseExact(searchTerm, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    return true;
                }
            }

            return false;
        }

        private IEnumerable<string> GetSurroundingSentences(List<string> content, string searchTerm, int contextSize = 2)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || content == null || content.Count == 0)
                yield break;

            //var result = new List<string>();
            bool contentAdded = false;
            string lastYielded = null; // Track last yielded value

            foreach (var item in content)
            {
                var sentences = item.Split(new[] { "।", "|" }, StringSplitOptions.RemoveEmptyEntries);

                var searchTermWithOneCharLess = searchTerm.Length > 1 ? searchTerm.Remove(searchTerm.Length - 2) : searchTerm;


                // Iterate through each sentence to find occurrences of the search term
                for (int i = 0; i < sentences.Length; i++)
                {
                    if ((sentences[i].IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0) || 
                        (sentences[i].IndexOf(searchTermWithOneCharLess, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        // Get the start and end index for the surrounding sentences
                        int start = Math.Max(0, i - contextSize);
                        int end = Math.Min(sentences.Length - 1, i + contextSize);

                        // Add the indicator and surrounding sentences
                        if (!contentAdded)
                        {
                            yield return "\t >   ";
                            lastYielded = "\t >   ";
                            contentAdded = true;
                        }

                        for (int n = start; n <= end; n++)
                        {
                            yield return sentences[n];
                            lastYielded = sentences[n];
                        }

                        yield return "\n\t>   ";
                        lastYielded = "\n\t>   ";
                    }
                }
            }

            // Remove trailing indicator if it's the last yielded value
            if (lastYielded == "\n\t>   ")
            {
                yield return "\b"; // Backspace or some no-op replacement, or just ignore this case
            }
        }

        private static bool IsBodyText(WCharacterFormat format)
        {
            return (format.FontSize == 13 && format.FontName == "Noto Sans Devanagari") || format.FontName == "Noto Sans";
        }

        private static bool IsDate(WCharacterFormat format)
        {
            return format.FontName == "Noto Sans" && format.FontSize == 14 && format.Bold && format.TextColor == Color.FromArgb(255, 255, 0, 0);
        }

        private static bool IsTitle(WCharacterFormat format)
        {
            return format.FontName == "Noto Sans Devanagari" && format.FontSize == 16 && format.Bold; // && text.StartsWith("\"") && text.EndsWith("\"");
        }

        #endregion Private Methods
    }
}