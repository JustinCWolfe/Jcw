using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;

namespace DataTablePerformanceTester
{
    public partial class DataTablePerformanceTesterFrm : Form
    {
        #region Fields

        private string m_columnPrefix = "Column";
        private int m_testIterations = 50000;
        private int m_columnCount = 10;
        private int m_rowCount = 100;

        private ColumnOrdinalMap m_columnMapper = new ColumnOrdinalMap ();
        private DataTable m_performanceTesterTable = new DataTable ("Performance Tester");

        #endregion

        #region Constructors

        public DataTablePerformanceTesterFrm ()
        {
            InitializeComponent ();
        }

        #endregion

        #region Event Handlers

        private void DataTablePerformanceTesterFrm_Load (object sender, EventArgs e)
        {
            this.NumberOfIterationsTextEdit.EditValue = m_testIterations;

            m_performanceTesterTable.BeginInit ();

            Dictionary<string, int> columnNamesAndOrdinals = new Dictionary<string, int> ();
            for (int i = 0; i < m_columnCount; i++)
            {
                DataColumn dc = new DataColumn ();
                dc.Caption = m_columnPrefix + i.ToString ();
                dc.DataType = typeof (DateTime);
                dc.ColumnName = m_columnPrefix + i.ToString ();
                m_performanceTesterTable.Columns.Add (dc);

                columnNamesAndOrdinals[m_columnPrefix + i.ToString ()] = i;
            }

            ColumnOrdinalMap com = new ColumnOrdinalMap ();

            m_performanceTesterTable.BeginLoadData ();
            for (int i = 0; i < m_rowCount; i++)
            {
                DataRow dr = m_performanceTesterTable.NewRow ();
                for (int j = 0; j < m_columnCount; j++)
                    dr[j] = DateTime.Now;
                m_performanceTesterTable.Rows.Add (dr);
            }
            m_performanceTesterTable.EndLoadData ();
            m_performanceTesterTable.AcceptChanges ();

            m_performanceTesterTable.EndInit ();
        }

        private void RunColumnOrdinalsTestButton_Click (object sender, EventArgs e)
        {
            this.ColumnOrdinalsOutputListBox.Items.Clear ();
            DateTime startTime = DateTime.Now;
            this.ColumnOrdinalsOutputListBox.Items.Add ("Start time: " + startTime.ToString ());

            if (string.IsNullOrEmpty (NumberOfIterationsTextEdit.Text) == false)
            {
                for (int i = 0; i < Convert.ToInt32 (NumberOfIterationsTextEdit.EditValue); i++)
                {
                    foreach (DataRow row in m_performanceTesterTable.Rows)
                    {
                        DateTime val0 = Convert.ToDateTime (row[0]);
                        DateTime val1 = Convert.ToDateTime (row[1]);
                        DateTime val2 = Convert.ToDateTime (row[2]);
                        DateTime val3 = Convert.ToDateTime (row[3]);
                        DateTime val4 = Convert.ToDateTime (row[4]);
                        DateTime val5 = Convert.ToDateTime (row[5]);
                        DateTime val6 = Convert.ToDateTime (row[6]);
                        DateTime val7 = Convert.ToDateTime (row[7]);
                        DateTime val8 = Convert.ToDateTime (row[8]);
                        DateTime val9 = Convert.ToDateTime (row[9]);
                    }
                }
            }
            DateTime endTime = DateTime.Now;

            this.ColumnOrdinalsOutputListBox.Items.Add ("End time: " + endTime.ToString ());
            this.ColumnOrdinalsOutputListBox.Items.Add ("Duration: " + (endTime - startTime).ToString ());
        }

        private void RunColumnOrdinalMapperTestButton_Click (object sender, EventArgs e)
        {
            this.ColumnOrdinalMapperOutputListBox.Items.Clear ();
            DateTime startTime = DateTime.Now;
            this.ColumnOrdinalMapperOutputListBox.Items.Add ("Start time: " + startTime.ToString ());

            if (string.IsNullOrEmpty (NumberOfIterationsTextEdit.Text) == false)
            {
                for (int i = 0; i < Convert.ToInt32 (NumberOfIterationsTextEdit.EditValue); i++)
                {
                    foreach (DataRow row in m_performanceTesterTable.Rows)
                    {
                        DateTime val0 = Convert.ToDateTime (row[m_columnMapper.column0]);
                        DateTime val1 = Convert.ToDateTime (row[m_columnMapper.column1]);
                        DateTime val2 = Convert.ToDateTime (row[m_columnMapper.column2]);
                        DateTime val3 = Convert.ToDateTime (row[m_columnMapper.column3]);
                        DateTime val4 = Convert.ToDateTime (row[m_columnMapper.column4]);
                        DateTime val5 = Convert.ToDateTime (row[m_columnMapper.column5]);
                        DateTime val6 = Convert.ToDateTime (row[m_columnMapper.column6]);
                        DateTime val7 = Convert.ToDateTime (row[m_columnMapper.column7]);
                        DateTime val8 = Convert.ToDateTime (row[m_columnMapper.column8]);
                        DateTime val9 = Convert.ToDateTime (row[m_columnMapper.column9]);
                    }
                }
            }
            DateTime endTime = DateTime.Now;

            this.ColumnOrdinalMapperOutputListBox.Items.Add ("End time: " + endTime.ToString ());
            this.ColumnOrdinalMapperOutputListBox.Items.Add ("Duration: " + (endTime - startTime).ToString ());
        }

        private void RunColumnOrdinalMapperPropertiesTestButton_Click (object sender, EventArgs e)
        {
            this.ColumnOrdinalMapperPropertiesOutputListBox.Items.Clear ();
            DateTime startTime = DateTime.Now;
            this.ColumnOrdinalMapperPropertiesOutputListBox.Items.Add ("Start time: " + startTime.ToString ());

            if (string.IsNullOrEmpty (NumberOfIterationsTextEdit.Text) == false)
            {
                for (int i = 0; i < Convert.ToInt32 (NumberOfIterationsTextEdit.EditValue); i++)
                {
                    foreach (DataRow row in m_performanceTesterTable.Rows)
                    {
                        // using column mapper properties
                        DateTime val0 = Convert.ToDateTime (row[m_columnMapper.Column0]);
                        DateTime val1 = Convert.ToDateTime (row[m_columnMapper.Column1]);
                        DateTime val2 = Convert.ToDateTime (row[m_columnMapper.Column2]);
                        DateTime val3 = Convert.ToDateTime (row[m_columnMapper.Column3]);
                        DateTime val4 = Convert.ToDateTime (row[m_columnMapper.Column4]);
                        DateTime val5 = Convert.ToDateTime (row[m_columnMapper.Column5]);
                        DateTime val6 = Convert.ToDateTime (row[m_columnMapper.Column6]);
                        DateTime val7 = Convert.ToDateTime (row[m_columnMapper.Column7]);
                        DateTime val8 = Convert.ToDateTime (row[m_columnMapper.Column8]);
                        DateTime val9 = Convert.ToDateTime (row[m_columnMapper.Column9]);
                    }
                }
            }
            DateTime endTime = DateTime.Now;

            this.ColumnOrdinalMapperPropertiesOutputListBox.Items.Add ("End time: " + endTime.ToString ());
            this.ColumnOrdinalMapperPropertiesOutputListBox.Items.Add ("Duration: " + (endTime - startTime).ToString ());
        }

        private void RunColumnNamesTestButton_Click (object sender, EventArgs e)
        {
            this.ColumnNamesOutputListBox.Items.Clear ();
            DateTime startTime = DateTime.Now;
            this.ColumnNamesOutputListBox.Items.Add ("Start time: " + startTime.ToString ());

            if (string.IsNullOrEmpty (NumberOfIterationsTextEdit.Text) == false)
            {
                for (int i = 0; i < Convert.ToInt32 (NumberOfIterationsTextEdit.EditValue); i++)
                {
                    foreach (DataRow row in m_performanceTesterTable.Rows)
                    {
                        DateTime val0 = Convert.ToDateTime (row[m_columnPrefix + "0"]);
                        DateTime val1 = Convert.ToDateTime (row[m_columnPrefix + "1"]);
                        DateTime val2 = Convert.ToDateTime (row[m_columnPrefix + "2"]);
                        DateTime val3 = Convert.ToDateTime (row[m_columnPrefix + "3"]);
                        DateTime val4 = Convert.ToDateTime (row[m_columnPrefix + "4"]);
                        DateTime val5 = Convert.ToDateTime (row[m_columnPrefix + "5"]);
                        DateTime val6 = Convert.ToDateTime (row[m_columnPrefix + "6"]);
                        DateTime val7 = Convert.ToDateTime (row[m_columnPrefix + "7"]);
                        DateTime val8 = Convert.ToDateTime (row[m_columnPrefix + "8"]);
                        DateTime val9 = Convert.ToDateTime (row[m_columnPrefix + "9"]);
                    }
                }
            }
            DateTime endTime = DateTime.Now;

            this.ColumnNamesOutputListBox.Items.Add ("End time: " + endTime.ToString ());
            this.ColumnNamesOutputListBox.Items.Add ("Duration: " + (endTime - startTime).ToString ());
        }

        private void RunLinqTestButton_Click (object sender, EventArgs e)
        {
            this.LinqOutputListBox.Items.Clear ();
            DateTime startTime = DateTime.Now;
            this.LinqOutputListBox.Items.Add ("Start time: " + startTime.ToString ());

            if (string.IsNullOrEmpty (NumberOfIterationsTextEdit.Text) == false)
            {
                for (int i = 0; i < Convert.ToInt32 (NumberOfIterationsTextEdit.EditValue); i++)
                {
                    var rowData = from row in m_performanceTesterTable.AsEnumerable ()
                                  select new
                                  {
                                      Column0 = row[m_columnPrefix + "0"],
                                      Column1 = row[m_columnPrefix + "1"],
                                      Column2 = row[m_columnPrefix + "2"],
                                      Column3 = row[m_columnPrefix + "3"],
                                      Column4 = row[m_columnPrefix + "4"],
                                      Column5 = row[m_columnPrefix + "5"],
                                      Column6 = row[m_columnPrefix + "6"],
                                      Column7 = row[m_columnPrefix + "7"],
                                      Column8 = row[m_columnPrefix + "8"],
                                      Column9 = row[m_columnPrefix + "9"],
                                  };

                    foreach (var row in rowData)
                    {
                        DateTime val0 = Convert.ToDateTime (row.Column0);
                        DateTime val1 = Convert.ToDateTime (row.Column1);
                        DateTime val2 = Convert.ToDateTime (row.Column2);
                        DateTime val3 = Convert.ToDateTime (row.Column3);
                        DateTime val4 = Convert.ToDateTime (row.Column4);
                        DateTime val5 = Convert.ToDateTime (row.Column5);
                        DateTime val6 = Convert.ToDateTime (row.Column6);
                        DateTime val7 = Convert.ToDateTime (row.Column7);
                        DateTime val8 = Convert.ToDateTime (row.Column8);
                        DateTime val9 = Convert.ToDateTime (row.Column9);
                    }
                }
            }
            DateTime endTime = DateTime.Now;

            this.LinqOutputListBox.Items.Add ("End time: " + endTime.ToString ());
            this.LinqOutputListBox.Items.Add ("Duration: " + (endTime - startTime).ToString ());
        }

        #endregion
    }

    class ColumnOrdinalMap
    {
        #region Fields

        public int column0;
        public int column1;
        public int column2;
        public int column3;
        public int column4;
        public int column5;
        public int column6;
        public int column7;
        public int column8;
        public int column9;

        #endregion

        #region Properties

        public int Column0 { get; private set; }
        public int Column1 { get; private set; }
        public int Column2 { get; private set; }
        public int Column3 { get; private set; }
        public int Column4 { get; private set; }
        public int Column5 { get; private set; }
        public int Column6 { get; private set; }
        public int Column7 { get; private set; }
        public int Column8 { get; private set; }
        public int Column9 { get; private set; }

        #endregion

        #region Constructors

        public ColumnOrdinalMap ()
        {
            column0 = 0;
            column1 = 1;
            column2 = 2;
            column3 = 3;
            column4 = 4;
            column5 = 5;
            column6 = 6;
            column7 = 7;
            column8 = 8;
            column9 = 9;

            Column0 = 0;
            Column1 = 1;
            Column2 = 2;
            Column3 = 3;
            Column4 = 4;
            Column5 = 5;
            Column6 = 6;
            Column7 = 7;
            Column8 = 8;
            Column9 = 9;
        }

        #endregion
    }
}
