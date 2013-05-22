using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace Jcw.Common.Data
{
    public class JcwReportDataColumn : DataColumn
    {
        #region Constructors

        public JcwReportDataColumn ( string columnName, Type dataType )
            : base ( columnName, dataType )
        {
        }

        #endregion
    }
}
