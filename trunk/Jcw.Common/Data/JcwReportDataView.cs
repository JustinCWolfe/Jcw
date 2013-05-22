using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Jcw.Common.Report;

namespace Jcw.Common.Data
{
    public class JcwReportDataView : DataView
    {
        #region Fields

        private Dictionary<string, JcwReportLayoutColumn> m_layoutColumns = new Dictionary<string, JcwReportLayoutColumn> ();

        #endregion

        #region Static Methods

        public static object GetField (DataRowView row, string fieldName)
        {
            object fieldValue = row[fieldName];
            if (fieldValue != DBNull.Value)
                return fieldValue;

            return null;
        }

        #endregion

        #region Public Methods

        public Dictionary<int, string[]> GetReportGroupingOptions ()
        {
            Dictionary<int, string[]> groupingOptions = new Dictionary<int, string[]> ();

            foreach (DataColumn column in this.Table.Columns)
            {
                JcwReportLayoutColumn layoutColumn = GetLayoutColumn (column.ColumnName);

                if (layoutColumn.IsGroupColumn)
                {
                    Dictionary<string, bool> groupMemberSet = new Dictionary<string, bool> ();

                    // get the of distinct group members for each grouping column
                    for (int rowIndex = 0 ; rowIndex < this.Count ; rowIndex++)
                    {
                        string detailText = JcwReportLayoutColumn.GetString (JcwReportDataView.GetField (this[rowIndex], column.ColumnName));

                        if (!groupMemberSet.ContainsKey (detailText))
                            groupMemberSet.Add (detailText, true);
                    }

                    string[] groupMemberList = new string[groupMemberSet.Count];
                    groupMemberSet.Keys.CopyTo (groupMemberList, 0);
                    groupingOptions.Add (column.Ordinal, groupMemberList);
                }
            }

            return groupingOptions;
        }

        public void AddLayoutColumn (string columnName, JcwReportLayoutColumn layoutColumn)
        {
            if (m_layoutColumns.ContainsKey (columnName))
                m_layoutColumns[columnName] = layoutColumn;
            else
                m_layoutColumns.Add (columnName, layoutColumn);
        }

        public JcwReportLayoutColumn GetLayoutColumn (string columnName)
        {
            JcwReportLayoutColumn layoutColumn = null;
            m_layoutColumns.TryGetValue (columnName, out layoutColumn);
            return layoutColumn;
        }

        #endregion
    }
}
