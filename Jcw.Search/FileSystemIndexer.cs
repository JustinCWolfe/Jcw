using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using Jcw.Search.Interfaces;

using LuceneAnalysis = Lucene.Net.Analysis;
using LuceneDocuments = Lucene.Net.Documents;
using LuceneIndex = Lucene.Net.Index;
using LuceneStore = Lucene.Net.Store;

namespace Jcw.Search
{
    [Serializable]
    public class FileSystemItemToIndex
    {
        public string Path { get; set; }
        public string SearchPattern { get; set; }
        public SearchOption SearchOptions { get; set; }
    }

    [Serializable]
    public class FileSystemItemsToIndex : IIndexedItems<FileSystemItemToIndex>
    {
        public HashSet<FileSystemItemToIndex> Items { get; set; }
    }

    public class FileSystemIndexer : IndexerBase<FileSystemItemsToIndex, FileSystemDocumentCreator>
    {
        #region Overrides

        public override LuceneAnalysis.Analyzer GetAnalyzer ()
        {
            return new LuceneAnalysis.Standard.StandardAnalyzer ();
        }

        protected override LuceneIndex.IndexWriter GetIndexWriter ( LuceneStore.Directory indexDirectory,
            LuceneAnalysis.Analyzer analyzer, bool create )
        {
            return new LuceneIndex.IndexWriter (
                indexDirectory,
                analyzer,
                create,
                LuceneIndex.IndexWriter.MaxFieldLength.UNLIMITED );
        }

        protected override void AddDocumentsToIndex ()
        {
            foreach ( FileSystemItemToIndex itemToIndex in ItemsToIndex.Items )
            {
                string[] files = Directory.GetFiles ( itemToIndex.Path, itemToIndex.SearchPattern, itemToIndex.SearchOptions );
                foreach ( string filename in files )
                {
                    if ( Directory.Exists ( filename ) == false && File.Exists ( filename ) )
                    {
                        FileInfo fi = new FileInfo ( filename );
                        Debug.WriteLine ( "Indexing file: " + fi.FullName );
                        LuceneDocuments.Document doc = DocumentCreator.GetDocument ( fi );
                        IndexWriter.AddDocument ( doc );
                    }
                }
            }
        }

        #endregion
    }

    public class FileSystemDocumentCreator : DocumentManagerBase<FileInfo>
    {
        #region Enumerations

        new public enum SearchableFields
        {
            CreationTimeUtc,
            DirectoryName,
            Extension,
            FullName,
            LastWriteTimeUtc,
            Length,
            Name,
            Contents,
        }

        #endregion

        #region Fields

        private readonly HashSet<string> knownTextFileExtensions = new HashSet<string> () 
        { 
            ".config",
            ".cs",
            ".resx",
            ".sln",
            ".vcproj",
            ".txt",
            ".ini",
            ".bat",
            ".log",
        };

        #endregion

        #region IGetDocument Members

        public override LuceneDocuments.Document GetDocument ( FileInfo fileToIndex )
        {
            LuceneDocuments.Document doc = new LuceneDocuments.Document ();

            /// Index but do not tokenize the creation time
            doc.Add ( new LuceneDocuments.Field ( SearchableFields.CreationTimeUtc.ToString (), fileToIndex.CreationTimeUtc.ToString (),
                LuceneDocuments.Field.Store.YES, LuceneDocuments.Field.Index.NOT_ANALYZED ) );

            doc.Add ( new LuceneDocuments.Field ( SearchableFields.DirectoryName.ToString (), fileToIndex.DirectoryName,
                LuceneDocuments.Field.Store.YES, LuceneDocuments.Field.Index.ANALYZED ) );

            /// Index but do not tokenize the file extension
            doc.Add ( new LuceneDocuments.Field ( SearchableFields.Extension.ToString (), fileToIndex.Extension,
                LuceneDocuments.Field.Store.YES, LuceneDocuments.Field.Index.NOT_ANALYZED ) );

            /// Don't index the full name because we are indexing the path and filename already. We should 
            /// store the full name though so it can be displayed in search results
            doc.Add ( new LuceneDocuments.Field ( SearchableFields.FullName.ToString (), fileToIndex.FullName,
                LuceneDocuments.Field.Store.YES, LuceneDocuments.Field.Index.NO ) );

            /// Index but do not tokenize the last write time
            doc.Add ( new LuceneDocuments.Field ( SearchableFields.LastWriteTimeUtc.ToString (), fileToIndex.LastWriteTimeUtc.ToString (),
                LuceneDocuments.Field.Store.YES, LuceneDocuments.Field.Index.NOT_ANALYZED ) );

            /// Index but do not tokenize the file length
            doc.Add ( new LuceneDocuments.Field ( SearchableFields.Length.ToString (), fileToIndex.Length.ToString (),
                LuceneDocuments.Field.Store.YES, LuceneDocuments.Field.Index.NOT_ANALYZED ) );

            doc.Add ( new LuceneDocuments.Field ( SearchableFields.Name.ToString (), fileToIndex.Name,
                LuceneDocuments.Field.Store.YES, LuceneDocuments.Field.Index.ANALYZED ) );

            try
            {
                /// All "known" text file extensions are in lower case so make sure we call 
                /// ToLower on file extension before doing exists check
                if ( knownTextFileExtensions.Contains ( fileToIndex.Extension.ToLower () ) )
                {
                    doc.Add ( new LuceneDocuments.Field (
                        SearchableFields.Contents.ToString (),
                        new StreamReader ( fileToIndex.FullName ) ) );
                }
            }
            catch ( IOException )
            {
            }

            return doc;
        }

        #endregion
    }
}
