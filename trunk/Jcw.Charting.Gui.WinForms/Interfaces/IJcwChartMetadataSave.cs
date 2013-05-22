using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Jcw.Charting.Metadata;
using Jcw.Common;

namespace Jcw.Charting.Gui.WinForms.Interfaces
{
    public interface IJcwChartMetadataSave
    {
        #region Properties

        IJcwChartFrm ChartForm { get; }

        #endregion

        #region Methods

        void SaveRequired (bool saveRequired, ChartMetadata metadata);

        #endregion
    }
}