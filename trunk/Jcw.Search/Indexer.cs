using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

using Jcw.Common;
using Jcw.Common.Interfaces;
using Jcw.Search.Interfaces;

using LuceneAnalysis = Lucene.Net.Analysis;
using LuceneDocuments = Lucene.Net.Documents;
using LuceneIndex = Lucene.Net.Index;
using LuceneSearch = Lucene.Net.Search;
using LuceneStore = Lucene.Net.Store;

namespace Jcw.Search
{
    public abstract class IndexerBase<T, U> : IManageIndex<T, U>,
        ISearchIndexAdapter<LuceneAnalysis.Analyzer, LuceneSearch.IndexSearcher>,
        IDisposable
    {
        #region IManageIndex Implementation

        public bool InDebug { get; private set; }
        public int IndexItemCount { get; private set; }
        public TimeSpan IndexLoadTime { get; private set; }

        public T ItemsToIndex { get; protected set; }
        public U DocumentCreator { get; protected set; }

        public void RunLoadIndex ()
        {
            BeginRunLoadIndex ();
            try
            {
                LoadIndexerConfiguration ();
                bool createIndex = LuceneIndex.IndexReader.IndexExists ( IndexDirectory ) == false;
                IndexWriter = GetIndexWriter ( IndexDirectory, GetAnalyzer (), createIndex );
                if ( createIndex )
                {
                    AddDocumentsToIndex ();
                    IndexWriter.Optimize ( true );
                }
            }
            finally
            {
                EndRunLoadIndex ();
            }
        }

        public void RunUpdateIndex ()
        {
        }

        #endregion

        #region ISearchIndexAdapter Implementation

        public abstract LuceneAnalysis.Analyzer GetAnalyzer ();

        private LuceneSearch.IndexSearcher indexSearcher = null;
        public LuceneSearch.IndexSearcher GetIndexSearcher ()
        {
            return indexSearcher = new LuceneSearch.IndexSearcher ( IndexDirectory );
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose ()
        {
            Dispose ( true );

            /// Use SupressFinalize in case a subclass of this type implements a finalizer.
            GC.SuppressFinalize ( this );
        }

        protected bool disposed = false;
        private readonly object padlock = new object ();
        protected virtual void Dispose ( bool disposing )
        {
            lock ( padlock )
            {
                if ( disposed == false )
                {
                    SerializeItemsToIndex ();

                    if ( disposing )
                    {
                        if ( indexSearcher != null )
                            indexSearcher.Close ();
                        if ( IndexWriter != null )
                            IndexWriter.Close ();
                        if ( IndexDirectory != null )
                            IndexDirectory.Close ();
                    }

                    /// Indicate that the instance has been disposed.
                    IndexWriter = null;
                    indexSearcher = null;
                    IndexDirectory = null;
                    disposed = true;
                }
            }
        }

        #endregion

        #region Enumerations

        private enum ConfigFilePropertyDescriptors
        {
            Type,
            Value,
        }

        #endregion

        #region Properties

        private string ItemsToIndexFilename { get; set; }
        private LuceneStore.Directory IndexDirectory { get; set; }
        private DateTime IndexLoadStartTime { get; set; }
        private DateTime IndexLoadEndTime { get; set; }
        protected LuceneIndex.IndexWriter IndexWriter { get; private set; }

        #endregion

        #region Abstract Methods

        protected abstract void AddDocumentsToIndex ();
        protected abstract LuceneIndex.IndexWriter GetIndexWriter ( LuceneStore.Directory indexDirectory, LuceneAnalysis.Analyzer analyzer, bool create );

        #endregion

        #region Virtual Methods

        protected virtual void LoadIndexerConfiguration ()
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;

            InDebug = false;
            string inDebug = appSettings["InDebug"];
            if ( string.IsNullOrEmpty ( inDebug ) == false )
                InDebug = Convert.ToBoolean ( inDebug );

            string indexFileSystemLocation = appSettings["IndexFileSystemLocation"];
            /// Store the index on disk
            if ( string.IsNullOrEmpty ( indexFileSystemLocation ) == false )
                IndexDirectory = LuceneStore.FSDirectory.GetDirectory ( new FileInfo ( indexFileSystemLocation ) );
            /// Store the index in memory
            else
                IndexDirectory = new LuceneStore.RAMDirectory ();

            string itemsToIndexFilename = appSettings["ItemsToIndexFilename"];
            if ( string.IsNullOrEmpty ( itemsToIndexFilename ) == false )
            {
                ItemsToIndexFilename = itemsToIndexFilename;
                DeserializeItemsToIndex ();
            }

            DocumentCreator = Factory.Instance.Create<U> ();
        }

        protected virtual void BeginRunLoadIndex ()
        {
            IndexLoadStartTime = DateTime.Now;
        }

        protected virtual void EndRunLoadIndex ()
        {
            IndexItemCount = IndexWriter.NumDocs ();
            IndexWriter.Close ();
            IndexLoadEndTime = DateTime.Now;

            IndexLoadTime = IndexLoadEndTime - IndexLoadStartTime;
        }

        #endregion

        #region Private Methods

        private void SerializeItemsToIndex ()
        {
            using ( StreamWriter writer = new StreamWriter ( ItemsToIndexFilename ) )
            {
                XmlSerializer serializer = new XmlSerializer ( typeof ( T ) );
                serializer.Serialize ( writer.BaseStream, ItemsToIndex );
            }
        }

        private void DeserializeItemsToIndex ()
        {
            using ( StreamReader itemsToIndexReader = new StreamReader ( ItemsToIndexFilename ) )
            {
                XmlSerializer serializer = new XmlSerializer ( typeof ( T ) );
                ItemsToIndex = (T) serializer.Deserialize ( itemsToIndexReader.BaseStream );
            }
        }

        #endregion
    }

    public abstract class DocumentManagerBase<T> : IGetDocument<T, LuceneDocuments.Document>
    {
        public enum SearchableFields { }
        public abstract LuceneDocuments.Document GetDocument ( T elementToIndex );
    }
}
