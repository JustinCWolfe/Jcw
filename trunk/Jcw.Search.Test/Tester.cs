using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;

using LuceneDocuments = Lucene.Net.Documents;
using LuceneSearch = Lucene.Net.Search;

using Jcw.Search;
using Jcw.Search.Interfaces;

namespace Jcw.Search.Test
{
    class Tester
    {
        public static int Main ( string[] args )
        {
            TestLuceneDotNet ( true );
            TestLuceneDotNet ( false );

            TestFileSystemIndexer ();
            TestDatabaseIndexer ();

            return 0;
        }

        private static void TestDatabaseIndexer ()
        {
            /// Make sure we are using the database items to index filename
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            appSettings["ItemsToIndexFilename"] = "DatabaseItemsToIndex.xml";
            appSettings["IndexFileSystemLocation"] = "D:/TEMP/LuceneDatabaseIndex";

            using ( DatabaseIndexer dbi = new DatabaseIndexer () )
            {
                dbi.RunLoadIndex ();
                Console.WriteLine ( "Items in index: " + dbi.IndexItemCount );
                Console.WriteLine ( "Time to load index: " + dbi.IndexLoadTime );

                int docsToFind = 100;
                string queryText = "Wolfe";
                using ( DatabaseSearcher searcher = new DatabaseSearcher ()
                {
                    IndexAnalyzer = dbi.GetAnalyzer (),
                    IndexSearcher = dbi.GetIndexSearcher (),
                    IndexQueryDefinition = new IndexQueryDefinition () { QueryText = queryText, TopDocsToFind = docsToFind }
                } )
                {
                    Console.WriteLine ( "Searching for: " + queryText );
                    searcher.RunSearch ();
                    Console.WriteLine ( "Time to search index: " + searcher.IndexSearchTime );
                    Console.WriteLine ( "Top " + docsToFind.ToString () + " of found hits: " + searcher.QueryHits.totalHits +
                        " matching query '" + searcher.IndexQueryDefinition.QueryText + "'" );
                    DisplayDatabaseHitResults ( searcher, searcher.QueryHits.scoreDocs );
                    Console.WriteLine ();
                }

                queryText = "United";
                using ( DatabaseSearcher searcher = new DatabaseSearcher ()
                {
                    IndexAnalyzer = dbi.GetAnalyzer (),
                    IndexSearcher = dbi.GetIndexSearcher (),
                    IndexQueryDefinition = new IndexQueryDefinition () { QueryText = queryText, TopDocsToFind = docsToFind }
                } )
                {
                    Console.WriteLine ( "Searching for: " + queryText );
                    searcher.RunSearch ();
                    Console.WriteLine ( "Time to search index: " + searcher.IndexSearchTime );
                    Console.WriteLine ( "Top " + docsToFind.ToString () + " of found hits: " + searcher.QueryHits.totalHits +
                        " matching query '" + searcher.IndexQueryDefinition.QueryText + "'" );
                    DisplayDatabaseHitResults ( searcher, searcher.QueryHits.scoreDocs );
                    Console.WriteLine ();
                }
            }
        }

        private static void DisplayDatabaseHitResults ( DatabaseSearcher searcher, LuceneSearch.ScoreDoc[] scoreDocs )
        {
            foreach ( LuceneSearch.ScoreDoc sc in searcher.QueryHits.scoreDocs )
            {
                LuceneDocuments.Document doc = searcher.IndexSearcher.Doc ( sc.doc );
                Console.WriteLine (
                    "Table: " + doc.Get ( DatabaseDocumentCreator.SearchableFields.TableName.ToString () ) +
                    ", CategCode: " + doc.Get ( DatabaseDocumentCreator.SearchableFields.CategCode.ToString () ) +
                    ", CategText: " + doc.Get ( DatabaseDocumentCreator.SearchableFields.CategText.ToString () ) +
                    ", AttrCode: " + doc.Get ( DatabaseDocumentCreator.SearchableFields.AttrCode.ToString () ) +
                    ", AttrText: " + doc.Get ( DatabaseDocumentCreator.SearchableFields.AttrText.ToString () ) +
                    ". Hit number: " + sc.doc.ToString () +
                    ". Hit score: " + sc.score.ToString () );
            }
        }

        private static void TestFileSystemIndexer ()
        {
            /// Make sure we are using the database items to index filename
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            appSettings["ItemsToIndexFilename"] = "FileSystemItemsToIndex.xml";
            appSettings["IndexFileSystemLocation"] = "D:/TEMP/LuceneFileSystemIndex";

            using ( FileSystemIndexer fsi = new FileSystemIndexer () )
            {
                fsi.RunLoadIndex ();
                Console.WriteLine ( "Items in index: " + fsi.IndexItemCount );
                Console.WriteLine ( "Time to load index: " + fsi.IndexLoadTime );

                int docsToFind = 100;
                string queryText = "Copyright";
                using ( FileSystemSearcher searcher = new FileSystemSearcher ()
                {
                    IndexAnalyzer = fsi.GetAnalyzer (),
                    IndexSearcher = fsi.GetIndexSearcher (),
                    IndexQueryDefinition = new IndexQueryDefinition () { QueryText = queryText, TopDocsToFind = docsToFind }
                } )
                {
                    Console.WriteLine ( "Searching for: " + queryText );
                    searcher.RunSearch ();
                    Console.WriteLine ( "Time to search index: " + searcher.IndexSearchTime );
                    Console.WriteLine ( "Top " + docsToFind.ToString () + " of found hits: " + searcher.QueryHits.totalHits +
                        " matching query '" + searcher.IndexQueryDefinition.QueryText + "'" );
                    DisplayFileSystemHitResults ( searcher, searcher.QueryHits.scoreDocs );
                    Console.WriteLine ();
                }

                queryText = "GCogBmx*";
                using ( FileSystemSearcher searcher = new FileSystemSearcher ()
                {
                    IndexAnalyzer = fsi.GetAnalyzer (),
                    IndexSearcher = fsi.GetIndexSearcher (),
                    IndexQueryDefinition = new IndexQueryDefinition () { QueryText = queryText, TopDocsToFind = docsToFind }
                } )
                {
                    Console.WriteLine ( "Searching for: " + queryText );
                    searcher.RunSearch ();
                    Console.WriteLine ( "Time to search index: " + searcher.IndexSearchTime );
                    Console.WriteLine ( "Top " + docsToFind.ToString () + " of found hits: " + searcher.QueryHits.totalHits +
                        " matching query '" + searcher.IndexQueryDefinition.QueryText + "'" );
                    DisplayFileSystemHitResults ( searcher, searcher.QueryHits.scoreDocs );
                    Console.WriteLine ();
                }

                queryText = "JcwUserControl";
                using ( FileSystemSearcher searcher = new FileSystemSearcher ()
                {
                    IndexAnalyzer = fsi.GetAnalyzer (),
                    IndexSearcher = fsi.GetIndexSearcher (),
                    IndexQueryDefinition = new IndexQueryDefinition () { QueryText = queryText, TopDocsToFind = docsToFind }
                } )
                {
                    Console.WriteLine ( "Searching for: " + queryText );
                    searcher.RunSearch ();
                    Console.WriteLine ( "Time to search index: " + searcher.IndexSearchTime );
                    Console.WriteLine ( "Top " + docsToFind.ToString () + " of found hits: " + searcher.QueryHits.totalHits +
                        " matching query '" + searcher.IndexQueryDefinition.QueryText + "'" );
                    DisplayFileSystemHitResults ( searcher, searcher.QueryHits.scoreDocs );
                    Console.WriteLine ();
                }

                queryText = "Config.txt";
                using ( FileSystemSearcher searcher = new FileSystemSearcher ()
                {
                    IndexAnalyzer = fsi.GetAnalyzer (),
                    IndexSearcher = fsi.GetIndexSearcher (),
                    IndexQueryDefinition = new IndexQueryDefinition () { QueryText = queryText, TopDocsToFind = docsToFind }
                } )
                {
                    Console.WriteLine ( "Searching for: " + queryText );
                    searcher.RunSearch ();
                    Console.WriteLine ( "Time to search index: " + searcher.IndexSearchTime );
                    Console.WriteLine ( "Top " + docsToFind.ToString () + " of found hits: " + searcher.QueryHits.totalHits +
                        " matching query '" + searcher.IndexQueryDefinition.QueryText + "'" );
                    DisplayFileSystemHitResults ( searcher, searcher.QueryHits.scoreDocs );
                    Console.WriteLine ();
                }
            }
        }

        private static void DisplayFileSystemHitResults ( FileSystemSearcher searcher, LuceneSearch.ScoreDoc[] scoreDocs )
        {
            foreach ( LuceneSearch.ScoreDoc sc in searcher.QueryHits.scoreDocs )
            {
                LuceneDocuments.Document doc = searcher.IndexSearcher.Doc ( sc.doc );
                Console.WriteLine (
                    "Name: " + doc.Get ( FileSystemDocumentCreator.SearchableFields.Name.ToString () ) +
                    ", LastWriteTimeUtc: " + doc.Get ( FileSystemDocumentCreator.SearchableFields.LastWriteTimeUtc.ToString () ) +
                    ". Hit number: " + sc.doc.ToString () +
                    ". Hit score: " + sc.score.ToString () );
            }
        }

        /// <summary>
        /// This is straight from the Lucene 2.4.0 help system
        /// </summary>
        private static void TestLuceneDotNet ( bool inMemory )
        {
            Console.WriteLine ( "Storing index in memory = " + inMemory.ToString () );

            Lucene.Net.Store.Directory directory = null;
            if ( inMemory )
                /// Store the index in memory
                directory = new Lucene.Net.Store.RAMDirectory ();
            else
                ///To store an index on disk, use this instead
                directory = Lucene.Net.Store.FSDirectory.GetDirectory ( "D:/TEMP/LuceneTestIndex" );

            Lucene.Net.Analysis.Standard.StandardAnalyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer ();

            Lucene.Net.Index.IndexWriter iwriter = null;
            if ( inMemory == false && Lucene.Net.Index.IndexReader.IndexExists ( directory ) )
                iwriter = new Lucene.Net.Index.IndexWriter ( directory, analyzer, false, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED );
            else
            {
                Console.WriteLine ( "Creating fresh index" );
                iwriter = new Lucene.Net.Index.IndexWriter ( directory, analyzer, true, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED );

                Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document ();
                string text = "This is the text to be indexed.";
                doc.Add ( new Lucene.Net.Documents.Field ( "fieldname", text, Lucene.Net.Documents.Field.Store.YES,
                    Lucene.Net.Documents.Field.Index.TOKENIZED ) );
                iwriter.AddDocument ( doc );

                text = "Some more text to be indexed.";
                doc = new Lucene.Net.Documents.Document ();
                doc.Add ( new Lucene.Net.Documents.Field ( "fieldname", text, Lucene.Net.Documents.Field.Store.YES,
                    Lucene.Net.Documents.Field.Index.TOKENIZED ) );
                iwriter.AddDocument ( doc );

                text = "Even more text to be indexed.";
                doc = new Lucene.Net.Documents.Document ();
                doc.Add ( new Lucene.Net.Documents.Field ( "fieldname", text, Lucene.Net.Documents.Field.Store.YES,
                    Lucene.Net.Documents.Field.Index.TOKENIZED ) );
                iwriter.AddDocument ( doc );

                text = "Not valid.";
                doc = new Lucene.Net.Documents.Document ();
                doc.Add ( new Lucene.Net.Documents.Field ( "fieldname", text, Lucene.Net.Documents.Field.Store.YES,
                    Lucene.Net.Documents.Field.Index.TOKENIZED ) );
                iwriter.AddDocument ( doc );
            }

            iwriter.Optimize ();
            iwriter.Close ();

            // Now search the index:
            Lucene.Net.Search.IndexSearcher isearcher = new Lucene.Net.Search.IndexSearcher ( directory );

            Console.WriteLine ( "Max Doc: " + isearcher.MaxDoc () );

            // Parse a simple query that searches for "text":
            Lucene.Net.QueryParsers.QueryParser parser = new Lucene.Net.QueryParsers.QueryParser ( "fieldname", analyzer );
            Lucene.Net.Search.Query query = parser.Parse ( "text" );
            /*
            Lucene.Net.Search.Hits hits = isearcher.Search ( query );

            // Iterate through the results:
            for ( int i = 0 ; i < hits.Length () ; i++ )
            {
                Lucene.Net.Documents.Document hitDoc = hits.Doc ( i );
                Console.WriteLine ( "Hit " + i.ToString () + ": " + hitDoc.Get ( "fieldname" ) );
            }
             */

            isearcher.Close ();
            directory.Close ();
            Console.WriteLine ();
        }
    }
}
