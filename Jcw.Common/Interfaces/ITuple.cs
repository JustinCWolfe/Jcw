using System;
using System.Collections.Generic;
using System.Text;

namespace Jcw.Common.Interfaces
{
    public interface ITuple
    {
        #region Properties

        int Count 
        {
            get;
        }

        object this[int index]
        {
            get;
        }

        #endregion
    }
}