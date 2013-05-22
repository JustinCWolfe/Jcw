using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using DevExpress.XtraBars.Docking;

using Jcw.Charting.Interfaces;
using Jcw.Charting.Metadata;
using Jcw.Common;
using Jcw.Common.Gui.WinForms.Controls;

namespace Jcw.Charting.Gui.WinForms.Interfaces
{
    #region Delegates

    public delegate void SaveMetadata(object sender, JcwEventArgs<ChartMetadata> chartMetadata);

    #endregion

    public interface IJcwChartFrm
    {
        #region Events

        event SaveMetadata OnSaveMetadata;

        #endregion

        #region Properties

        IJcwChartCtl JcwChartControl { get; }
        JcwDockManager DockManager { get; }

        /// <summary>
        /// Get the statistics for each chart series based on the view port.
        /// </summary>
        Dictionary<StatisticTypes, Dictionary<string, double>> ChartStatistics { get; }

        bool DisplayChartStatisticsDockingPanel { get; }
        bool DisplayChartMetadataDockingPanel { get; }
        bool CanSaveChartMetadata { get; }
        bool DisplayHotSpots { get; }

        #endregion

        #region Methods

        void AddDockingPanelControl(IJcwChartDockPanelControl control);
        IJcwChartDockPanelControl FindDockingPanelControl(string dockingControlName);
        void SetVisibilityOfDockingPanel(string dockingControlName, DockVisibility visibility);

        void RefreshChartMetadataControl(ChartMetadata metadata);
        void RefreshChartStatisticsControl(IEnumerable<IJcwChartVectorGroup<double>> seriesVectorGroups,
            IEnumerable<IJcwChartVectorGroup<double>> viewPortSeriesVectorGroups);

        void ChartMetadataSaveRequired(ChartMetadata metadata);
        void RevertChartMetadataSaveRequired();
        void SaveChartMetadata();

        #endregion
    }
}
