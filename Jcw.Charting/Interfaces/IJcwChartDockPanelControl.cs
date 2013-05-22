using System;
using System.Collections.Generic;
using System.Text;

using Jcw.Charting.Metadata;

namespace Jcw.Charting.Interfaces
{
    public interface IJcwChartDockPanelControl
    {
        bool AutoHide { get; set; }
        string PanelCaption { get; set; }
        IJcwChartCtl ChartControl { get; set; }
        ChartMetadata Metadata { get; set; }
    }

    public interface IJcwChartUpdateSelectionControl
    {
        double X { get; set; }
        double Y { get; set; }
        string SeriesName { get; set; }
    }
}
