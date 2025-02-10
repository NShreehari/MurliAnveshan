using System.Collections.Generic;

using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Util;

using static MurliAnveshan.Classes.Enums;

using LuceneDirectory = Lucene.Net.Store.Directory;

namespace MurliAnveshan.Classes
{
    public abstract class MurliSearchEngineBase
    {
        #region Protected Fields

        protected const LuceneVersion luceneVersion = LuceneVersion.LUCENE_48;
        protected static Analyzer analyzer;
        protected IndexWriterConfig indexConfig;
        protected LuceneDirectory indexDir;
        protected string indexName;
        protected string indexPath;
        private IndexWriter indxWriter;

        #endregion Protected Fields

        #region Public Constructors

        public MurliSearchEngineBase()
        {
        }

        #endregion Public Constructors

        #region Private Destructors

        ~MurliSearchEngineBase()
        {
            IndxWriter?.Dispose();
            indexDir?.Dispose();
            analyzer?.Dispose();
        }

        protected IndexWriter IndxWriter { get => indxWriter; set => indxWriter = value; }

        #endregion Private Destructors

        #region Public Methods

        public abstract bool AddMurliDetailsToIndex(IEnumerable<AvyaktMurliDetails> murliDetails);

        public abstract bool BuildIndex();

        public abstract PagedResults<MurliDetailsBase> SearchIndex(string searchTerm, SearchLocation searchLocation, int currentPage = 1);

        #endregion Public Methods
    }
}