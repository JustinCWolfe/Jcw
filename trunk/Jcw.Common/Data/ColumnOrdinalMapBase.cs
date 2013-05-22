using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Jcw.Common.Data
{
    public abstract class ColumnOrdinalMapBase
    {
        public abstract void SetColumnOrdinals ( params DataTable[] tables );
    }
}
