using System;
using System.Collections.Generic;
using System.Text;

using Jcw.Search.Interfaces;

using LuceneAnalyzer = Lucene.Net.Analysis;
using LuceneIndex = Lucene.Net.Index;
using LuceneQueryParsers = Lucene.Net.QueryParsers;
using LuceneSearch = Lucene.Net.Search;
using LuceneStore = Lucene.Net.Store;

namespace Jcw.Search
{
    public abstract class SearcherBase : ISearchIndex<LuceneSearch.TopDocs>, IDisposable
    {
        #region ISearchIndex Implementation

        public TimeSpan IndexSearchTime { get; private set; }
        public IIndexQueryDefinition IndexQueryDefinition { get; set; }
        public LuceneSearch.TopDocs QueryHits { get; private set; }

        public void RunSearch()
        {
            BeginRunSearch ();
            try
            {
                LuceneQueryParsers.QueryParser parser = CreateQueryParser ();
                LuceneSearch.Query q = parser.Parse (IndexQueryDefinition.QueryText);
                QueryHits = IndexSearcher.Search (q, IndexQueryDefinition.TopDocsToFind);
            }
            finally
            {
                EndRunSearch ();
            }
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose (true);

            // Use SupressFinalize in case a subclass of this type implements a finalizer.
            GC.SuppressFinalize (this);
        }

        protected bool disposed = false;
        private readonly object padlock = new object ();
        protected void Dispose(bool disposing)
        {
            lock (padlock)
            {
                if (disposed == false)
                {
                    if (disposing)
                    {
                        if (IndexSearcher != null)
                            IndexSearcher.Dispose ();
                    }

                    // Indicate that the instance has been disposed.
                    IndexSearcher = null;
                    disposed = true;
                }
            }
        }

        #endregion

        #region Properties

        protected DateTime IndexSearchStartTime { get; set; }
        protected DateTime IndexSearchEndTime { get; set; }
        public LuceneSearch.IndexSearcher IndexSearcher { get; set; }
        public LuceneAnalyzer.Analyzer IndexAnalyzer { get; set; }

        #endregion

        #region Abstract Methods

        protected abstract LuceneQueryParsers.QueryParser CreateQueryParser();

        #endregion

        #region Virtual Methods

        protected virtual void BeginRunSearch()
        {
            IndexSearchStartTime = DateTime.Now;
        }

        protected virtual void EndRunSearch()
        {
            IndexSearchEndTime = DateTime.Now;

            IndexSearchTime = IndexSearchEndTime - IndexSearchStartTime;
        }

        #endregion
    }

    public class IndexQueryDefinition : IIndexQueryDefinition
    {
        #region IIndexQueryDefinition Implementation

        public string QueryText { get; set; }

        private int topDocsToFind = 20;
        public int TopDocsToFind
        {
            get { return topDocsToFind; }
            set { topDocsToFind = value; }
        }

        #endregion
    }
}
