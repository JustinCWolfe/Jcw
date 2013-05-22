using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace Jcw.Charting.Interfaces
{
    #region Enumerations

    public enum AxisTypes
    {
        VerticalLeft = 0,
        VerticalRight = 1,
        VerticalLeft2 = 2,
        VerticalRight2 = 3,
        HorizontalTop = 4,
        HorizontalBottom = 5,
    }

    [Flags]
    public enum DisplayOptions : int
    {
        None = 0,
        ShowHotSpots = 1,
        ShowStatisticsControlOnDockingPanel = 2,
        ShowMetadataControlOnDockingPanel = 4,
        ShowSaveableMetadataControlOnDockingPanel = 8,
        All = ShowHotSpots | ShowStatisticsControlOnDockingPanel | ShowSaveableMetadataControlOnDockingPanel
    }

    #endregion

    public interface IJcwChartCtl
    {
        #region Events

        event EventHandler OnChartComplete;

        #endregion

        #region Properties

        bool IsXYChartInitialized { get; }
        Image ChartImage { get; }
        string AxisLabelFormat { get; set; }
        string XAxisTitle { get; }
        string YAxisTitle { get; }
        List<string> YAxisTitles { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to set chart data for charts with common label (x axis) data.
        /// </summary>
        /// <param name="chartDataGroup"></param>
        void SetChartData (IJcwChartDataGroup<double> chartDataGroup);

        /// <summary>
        /// Method to set chart data for charts series using different label (x axis) data.
        /// </summary>
        /// <param name="chartDataGroups"></param>
        void SetChartData (List<IJcwChartDataGroup<double>> chartDataGroups);

        #endregion
    }
}
