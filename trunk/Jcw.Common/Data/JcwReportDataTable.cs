using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Jcw.Common.Data
{
    public class JcwReportDataTable : DataTable, IEnumerable
    {
        #region Constructors

        public JcwReportDataTable ( string tableName )
            : base ( tableName )
        {
        }

        #endregion

        #region Public Methods

        public void AddJcwReportDataRow ( JcwReportDataRow row )
        {
            this.Rows.Add ( row );
        }

        public JcwReportDataRow NewJcwReportDataRow ()
        {
            return ( (JcwReportDataRow) ( this.NewRow () ) );
        }

        public JcwReportDataRow this[int index]
        {
            get { return ( (JcwReportDataRow) ( this.Rows[index] ) ); }
        }

        public void RemoveFilterCoefficientsRow ( JcwReportDataRow row )
        {
            this.Rows.Remove ( row );
        }

        #endregion

        #region IEnumerable Implementation

        public virtual IEnumerator GetEnumerator ()
        {
            return this.Rows.GetEnumerator ();
        }

        #endregion

        #region Overrides

        protected override Type GetRowType ()
        {
            return typeof ( JcwReportDataRow );
        }

        protected override void OnTableNewRow ( DataTableNewRowEventArgs e )
        {
            base.OnTableNewRow ( e );
        }

        public override DataTable Clone ()
        {
            JcwReportDataTable cln = ( (JcwReportDataTable) ( base.Clone () ) );
            return cln;
        }

        protected override DataTable CreateInstance ()
        {
            return new JcwReportDataTable ( "DefaultTableName" );
        }

        protected override DataRow NewRowFromBuilder ( DataRowBuilder builder )
        {
            return new JcwReportDataRow ( builder );
        }

        #endregion
    }
}
