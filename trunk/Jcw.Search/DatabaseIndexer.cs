using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Jcw.Search.Interfaces;

using LuceneAnalysis = Lucene.Net.Analysis;
using LuceneDocuments = Lucene.Net.Documents;
using LuceneIndex = Lucene.Net.Index;
using LuceneStore = Lucene.Net.Store;

namespace Jcw.Search
{
    [Serializable]
    public class DatabaseItemToIndex
    {
        public string TableName { get; set; }
        public string WhereClause { get; set; }
        public List<string> Columns { get; set; }
    }

    [Serializable]
    public class DatabaseItemsToIndex : IIndexedItems<DatabaseItemToIndex>
    {
        public string ConnectionString { get; set; }
        public HashSet<DatabaseItemToIndex> Items { get; set; }
    }

    public class DatabaseIndexer : IndexerBase<DatabaseItemsToIndex, DatabaseDocumentCreator>
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
            using ( SqlConnection connection = new SqlConnection ( ItemsToIndex.ConnectionString ) )
            {
                connection.Open ();

                foreach ( DatabaseItemToIndex itemToIndex in ItemsToIndex.Items )
                {
                    string columns = string.Empty;
                    itemToIndex.Columns.ForEach ( columnName => columns += columnName + "," );
                    columns = columns.Remove ( columns.Length - 1 );

                    string query = "select " + columns + " from " + itemToIndex.TableName;
                    if ( string.IsNullOrEmpty ( itemToIndex.WhereClause ) == false )
                        query += " where " + itemToIndex.WhereClause;

                    SqlCommand selectCommand = new SqlCommand ( query, connection );

                    SqlDataAdapter adapter = new SqlDataAdapter ();
                    adapter.SelectCommand = selectCommand;
                    DataTable table = new DataTable ( itemToIndex.TableName );
                    adapter.Fill ( table );

                    /// Get the primary keys for the data table we are querying and set them in our detached
                    /// ADO.NET datatable because this will be used when creating our search index document
                    List<DataColumn> keyColumns = new List<DataColumn> ();
                    using ( SqlDataReader primaryKeyReader = selectCommand.ExecuteReader ( CommandBehavior.KeyInfo ) )
                    {
                        DataTable schemaTable = primaryKeyReader.GetSchemaTable ();
                        primaryKeyReader.Close ();
                        foreach ( DataRow row in schemaTable.Rows )
                        {
                            if ( Convert.ToBoolean ( row["IsKey"] ) )
                            {
                                string keyColumnName = row["ColumnName"] as string;
                                keyColumns.Add ( table.Columns[keyColumnName] );
                            }
                        }
                        table.PrimaryKey = keyColumns.ToArray ();

                        foreach ( DataRow row in table.Rows )
                        {
                            LuceneDocuments.Document doc = DocumentCreator.GetDocument ( row );
                            IndexWriter.AddDocument ( doc );
                        }
                    }
                }
            }
        }

        #endregion
    }

    public class DatabaseDocumentCreator : DocumentManagerBase<DataRow>
    {
        #region Enumerations

        new public enum SearchableFields
        {
            TableName,
            CategCode,
            CategText,
            AttrCode,
            AttrText,
        }

        #endregion

        #region IGetDocument Implementation

        public override LuceneDocuments.Document GetDocument ( DataRow elementToIndex )
        {
            LuceneDocuments.Document doc = new LuceneDocuments.Document ();

            /// Store the table name for this row in the document so we would know where to lookup
            /// this record when searching
            doc.Add ( new LuceneDocuments.Field ( "TableName", elementToIndex.Table.TableName,
                LuceneDocuments.Field.Store.YES, LuceneDocuments.Field.Index.NOT_ANALYZED ) );

            foreach ( DataColumn columnn in elementToIndex.Table.Columns )
            {
                /// Do not index database ids but store them so that they can be accessed as part of 
                /// search results (for retrieval)
                if ( columnn.ColumnName.EndsWith ( "Id" ) )
                {
                    doc.Add ( new LuceneDocuments.Field ( columnn.ColumnName, Convert.ToString ( elementToIndex[columnn] ),
                        LuceneDocuments.Field.Store.YES, LuceneDocuments.Field.Index.NO ) );
                    continue;
                }

                /// After special handling for Id columns, all other columns should be listed in the searchable fields enumeration
                if ( Enum.GetNames ( typeof ( SearchableFields ) ).Contains ( columnn.ColumnName ) == false )
                    throw new Exception ( "Column name " + columnn.ColumnName + " must be added as a 'Searchable Field'" );

                /// Index (but do not analyze) and store values from database primary key columns
                if ( elementToIndex.Table.PrimaryKey.Contains ( columnn ) )
                {
                    doc.Add ( new LuceneDocuments.Field ( columnn.ColumnName, Convert.ToString ( elementToIndex[columnn] ),
                        LuceneDocuments.Field.Store.YES, LuceneDocuments.Field.Index.NOT_ANALYZED ) );
                }
                else
                {
                    doc.Add ( new LuceneDocuments.Field ( columnn.ColumnName, Convert.ToString ( elementToIndex[columnn] ),
                        LuceneDocuments.Field.Store.YES, LuceneDocuments.Field.Index.ANALYZED ) );
                }
            }

            return doc;
        }

        #endregion
    }
}
