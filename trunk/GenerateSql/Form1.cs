using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenerateSql
{
    public partial class Form1 : Form
    {
        public Form1 ()
        {
            InitializeComponent ();

            KeyColumnTextBox.Text = "view_id";
            DatabaseNameTextBox.Text = "justin_v9";
            DatabaseServerTextBox.Text = "devwin27";
            InsertTableNameTextBox.Text = "cs_view";
        }

        private void RunButton_Click ( object sender, EventArgs e )
        {
            ResultSetRichTextBox.Clear ();

            if ( string.IsNullOrEmpty ( DatabaseNameTextBox.Text ) == false &&
                string.IsNullOrEmpty ( DatabaseServerTextBox.Text ) == false &&
                string.IsNullOrEmpty ( SqlQueryRichTextBox.Text ) == false )
            {
                string connectionString = "Data Source={0};Initial Catalog={1};User Id=tm_dev;Password=tm_dev";
                using ( SqlConnection connection = new SqlConnection ( string.Format ( connectionString, DatabaseServerTextBox.Text, DatabaseNameTextBox.Text ) ) )
                {
                    SqlCommand command = new SqlCommand ( SqlQueryRichTextBox.Text, connection );
                    connection.Open ();
                    SqlDataReader reader = null;

                    try
                    {
                        int? rowCount = null;
                        if ( string.IsNullOrEmpty ( KeyStartValueTextBox.Text ) == false )
                            rowCount = Convert.ToInt32 ( KeyStartValueTextBox.Text );

                        StringBuilder columns = new StringBuilder ( "(" );

                        string keyColumn = KeyColumnTextBox.Text;
                        if ( rowCount != null && string.IsNullOrEmpty ( keyColumn ) == false )
                        {
                            columns.Append ( keyColumn );
                            columns.Append ( "," );
                        }

                        reader = command.ExecuteReader ();

                        while ( reader.Read () )
                        {
                            for ( int index = 0 ; index < reader.FieldCount ; index++ )
                            {
                                columns.Append ( reader.GetName ( index ) );
                                columns.Append ( "," );
                            }
                            break;
                        }
                        columns.Remove ( columns.Length - 1, 1 );
                        columns.Append ( ")" );

                        reader.Close ();
                        reader = command.ExecuteReader ();

                        while ( reader.Read () )
                        {
                            if ( rowCount == null )
                            {
                                ResultSetRichTextBox.AppendText ( string.Format ( "insert into {0} {1} values (",
                                    InsertTableNameTextBox.Text, columns.ToString () ) );
                            }
                            else
                            {
                                ResultSetRichTextBox.AppendText ( string.Format ( "insert into {0} {1} values ({2},",
                                    InsertTableNameTextBox.Text, columns.ToString (), rowCount ) );
                            }

                            for ( int index = 0 ; index < reader.FieldCount ; index++ )
                            {
                                if ( reader.GetProviderSpecificFieldType ( index ) == typeof ( SqlString ) ||
                                    reader.GetProviderSpecificFieldType ( index ) == typeof ( SqlDateTime ) )
                                {
                                    string sqlText = reader.GetProviderSpecificFieldType ( index ) == typeof ( SqlDateTime ) ?
                                        reader.GetSqlDateTime ( index ).ToString () :
                                        reader.GetSqlString ( index ).ToString ();

                                    // if the sql text contains a single character, we need to escape it
                                    sqlText = sqlText.Replace ( "'", "''" );

                                    if ( sqlText == "Null" )
                                        ResultSetRichTextBox.AppendText ( "null" );
                                    else
                                        ResultSetRichTextBox.AppendText ( "'" + sqlText + "'" );
                                }
                                else if ( reader.GetProviderSpecificFieldType ( index ) == typeof ( SqlDecimal ) )
                                    ResultSetRichTextBox.AppendText ( reader.GetSqlDecimal ( index ).ToString () );
                                else if ( reader.GetProviderSpecificFieldType ( index ) == typeof ( SqlInt32 ) )
                                    ResultSetRichTextBox.AppendText ( reader.GetSqlInt32 ( index ).ToString () );
                                else if ( reader.GetProviderSpecificFieldType ( index ) == typeof ( SqlInt64 ) )
                                    ResultSetRichTextBox.AppendText ( reader.GetSqlInt64 ( index ).ToString () );
                                else if ( reader.GetProviderSpecificFieldType ( index ) == typeof ( SqlBoolean ) )
                                    ResultSetRichTextBox.AppendText ( reader.GetSqlBoolean ( index ).ToString () );
                                else if ( reader.GetProviderSpecificFieldType ( index ) == typeof ( SqlDouble ) )
                                    ResultSetRichTextBox.AppendText ( reader.GetSqlDouble ( index ).ToString () );
                                else
                                {
                                    Type t = reader.GetProviderSpecificFieldType ( index );
                                    throw new Exception ( "Unsupported type: " + t.ToString () );
                                }

                                ResultSetRichTextBox.AppendText ( "," );
                            }

                            ResultSetRichTextBox.Text = ResultSetRichTextBox.Text.Remove ( ResultSetRichTextBox.Text.Length - 1, 1 );
                            ResultSetRichTextBox.AppendText ( ")\n" );
                            rowCount++;
                        }
                    }
                    catch ( Exception ex )
                    {
                        MessageBox.Show ( ex.ToString (), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    }
                    finally
                    {
                        if ( reader != null )
                            reader.Close ();
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged ( object sender, EventArgs e )
        {
        }
    }
}
