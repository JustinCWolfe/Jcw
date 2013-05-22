using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Jcw.Common.Data
{
    public class JcwReportDataRow : DataRow
    {
        #region Constructors

        public JcwReportDataRow (DataRowBuilder builder)
            : base (builder)
        {
        }

        #endregion
    }
}
