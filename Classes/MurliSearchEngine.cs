using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Util;
using Lucene.Net.Store;
using System.IO;

using Lucene.Net.Documents;
using Lucene.Net.Search;
using System.Diagnostics;
using LuceneDirectory = Lucene.Net.Store.Directory;
//using OpenMode = Lucene.Net.Index.OpenMode;
using Document = Lucene.Net.Documents.Document;
using Lucene.Net.QueryParsers.Classic;
using System.Xml.Linq;
using Lucene.Net.Index.Extensions;
using Lucene.Net.Analysis.Hi;
using J2N.Text;
using Syncfusion.DocIO.DLS;

namespace MurliAnveshan.Classes
{
    internal class MurliSearchEngine
    {
        private readonly Analyzer _standardAnalyzer;
        private readonly IndexWriterConfig _indexConfig;
        private readonly IndexWriter _writer;
        private readonly LuceneDirectory _indexDir;
        // Specify the compatibility version we want

        private const LuceneVersion luceneVersion = LuceneVersion.LUCENE_48;
        public string indexPath;

        public MurliSearchEngine()
        {
            //Create an analyzer to process the text 
            _standardAnalyzer = new HindiAnalyzer(luceneVersion);// StandardAnalyzer(luceneVersion);
            //Create an index writer
            _indexConfig = new IndexWriterConfig(luceneVersion, _standardAnalyzer);

            //Open the Directory using a Lucene Directory class
            string indexName = "example_index";
            indexPath = Path.Combine(Environment.CurrentDirectory, indexName);

            _indexDir = FSDirectory.Open(indexPath);
            _indexConfig.OpenMode = OpenMode.CREATE;     // create/overwrite index
            //_indexConfig.SetCodec(new SimpleTextCodec());
            _writer = new IndexWriter(_indexDir, _indexConfig);
        }


        public void AddMurliDetailsToIndex(IEnumerable<MurliDetails> murliDetails)
        {
            try
            {
                foreach (var _murliDetails in murliDetails)
                {
                    var document = new Document();
                    document.Add(new TextField(nameof(MurliDetails.MurliDate), _murliDetails.MurliDate, Field.Store.YES));
                    document.Add(new TextField(nameof(MurliDetails.MurliTitle), _murliDetails.MurliTitle, Field.Store.YES));
                    //document.Add(new TextField(nameof(MurliDetails.MurliLines), string.Join(" ", _murliDetails.MurliLines), Field.Store.YES));

                    foreach (var line in _murliDetails.MurliLines)
                    {
                        document.Add(new TextField(nameof(MurliDetails.MurliLines), line, Field.Store.YES));
                    }

                    //document.Add(new TextField(nameof(MurliDetails.MurliLines), string.Join(" ", _murliDetails.MurliLines), Field.Store.YES));

                    document.Add(new TextField(nameof(MurliDetails.PageNumber), _murliDetails.PageNumber.ToString(), Field.Store.YES));
                    //document.Add(new TextField(nameof(MurliDetails.SearchTerm), _murliDetails.SearchTerm, Field.Store.YES));
                    //document.Add(new TextField(nameof(MurliDetails.FileName), _murliDetails.FileName, Field.Store.YES));
                    _writer.AddDocument(document);
                }
                _writer.Commit();
                _writer.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        public IEnumerable<MurliDetails> SearchIndex(string searchTerm, SearchLocation searchLocation)
        {
            var directoryReader = DirectoryReader.Open(_indexDir);
            var indexSearcher = new IndexSearcher(directoryReader);
            
            Query query = CreateQueryBasedOnSearchLocation(searchTerm, searchLocation);

            var hits = indexSearcher.Search(query, 10).ScoreDocs;

            var murlidetails = new List<MurliDetails>();
            MurliDetails murli;

            foreach (var hit in hits)
            {
                var document = indexSearcher.Doc(hit.Doc);

                murli = new MurliDetails();

                murli.MurliDate = document.Get(nameof(MurliDetails.MurliDate));
                murli.MurliTitle = document.Get(nameof(MurliDetails.MurliTitle));

                //murlis.//MurliLines = document.Get("MurliLines")?.Split(new[] { "" }, StringSplitOptions.RemoveEmptyEntries).ToList(), //MurliLinesContainingSearchTerm. Add(document.Get("MurliLinesContainingSearchTerm")),
                //murlis.// Working// MurliLines = document.GetValues(nameof(MurliDetails.MurliLines)).ToList<string>(), //MurliLinesContainingSearchTerm. Add(document.Get("MurliLinesContainingSearchTerm")),

                if (searchLocation == SearchLocation.All)
                {
                    IEnumerable<string> relevantSentences = GetSurroundingSentences(document.GetValues(nameof(MurliDetails.MurliLines)).ToList<string>(), searchTerm);

                    foreach (var sentence in relevantSentences)
                    {
                        murli.MurliLines.Add(sentence);
                    }
                }

                murli.PageNumber = Convert.ToInt32(document.Get(nameof(MurliDetails.PageNumber)));
                murli.SearchTerm = document.Get(nameof(MurliDetails.SearchTerm));
                murli.FileName = document.Get(nameof(MurliDetails.FileName));

                murlidetails.Add(murli);
            }

            return murlidetails;
        }

        private Query CreateQueryBasedOnSearchLocation(string searchTerm, SearchLocation searchLocation)
        {
            QueryParser queryParser = null;
            Query query = null;

            if (searchLocation == SearchLocation.All)
            {
                string[] fields = { nameof(MurliDetails.MurliDate), nameof(MurliDetails.MurliTitle), nameof(MurliDetails.MurliLines), nameof(MurliDetails.PageNumber) }; //"FileName", "SearchTerm" };
                queryParser = new MultiFieldQueryParser(luceneVersion, fields, _standardAnalyzer);
            }
            else if (searchLocation == SearchLocation.TitleOnly)
            {
                //string[] fields = { "MurliDate", "MurliTitle", "MurliLines", "PageNumber" }; //"FileName", "SearchTerm" };
                queryParser = new QueryParser(luceneVersion, nameof(MurliDetails.MurliTitle), _standardAnalyzer);
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

        public IEnumerable<string> GetSurroundingSentences(List<string> content, string searchTerm, int contextSize = 2)
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


        public enum SearchLocation
        {
            All,
            TitleOnly
        };


        ~MurliSearchEngine()
        {
            _writer?.Dispose();
            _indexDir?.Dispose();
            _standardAnalyzer?.Dispose();
        }
    }
}