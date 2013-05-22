using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Jcw.Charting.Gui.WinForms.Interfaces;
using Jcw.Charting.Interfaces;
using Jcw.Charting.Metadata;
using Jcw.Common;
using Jcw.Common.Gui.WinForms.Controls;

namespace Jcw.Charting.Gui.WinForms.Controls
{
    public partial class JcwChartStatisticsCtl : JcwUserControl, IJcwChartDockPanelControl
    {
        #region Fields

        // Must use an object that implements IList as DataGridView DataSource.
        private List<IJcwChartStatistic> m_minValues = new List<IJcwChartStatistic> ();
        private List<IJcwChartStatistic> m_maxValues = new List<IJcwChartStatistic> ();
        private List<IJcwChartStatistic> m_aveValues = new List<IJcwChartStatistic> ();

        #endregion

        #region Properties

        /// <summary>
        /// This property stores the actual chart series information while the min, max and ave list fields store the series information
        /// and also the aggregate information.
        /// </summary>
        private Dictionary<StatisticTypes, List<IJcwChartStatistic>> Series { get; set; }

        #endregion

        #region Constructors

        public JcwChartStatisticsCtl ()
        {
            AutoHide = true;
            Series = new Dictionary<StatisticTypes, List<IJcwChartStatistic>> ();

            InitializeComponent ();

            MinDataGridView.OnRecalculateAggregate += GridView_OnRecalculateAggregate;
            MaxDataGridView.OnRecalculateAggregate += GridView_OnRecalculateAggregate;
            AveDataGridView.OnRecalculateAggregate += GridView_OnRecalculateAggregate;

            // don't auto generate data grid view columns when binding to datasources below
            MinDataGridView.AutoGenerateColumns = false;
            MaxDataGridView.AutoGenerateColumns = false;
            AveDataGridView.AutoGenerateColumns = false;

            MinSeriesNameColumn.DataPropertyName = "Name";
            MinSeriesValueColumn.DataPropertyName = "Value";

            MaxSeriesNameColumn.DataPropertyName = "Name";
            MaxSeriesValueColumn.DataPropertyName = "Value";

            AveSeriesNameColumn.DataPropertyName = "Name";
            AveSeriesValueColumn.DataPropertyName = "Value";

            MinDataGridView.AfterInitialize ();
            MaxDataGridView.AfterInitialize ();
            AveDataGridView.AfterInitialize ();
        }

        #endregion

        #region IJcwChartDockPanelControl Implementation

        #region Properties

        public bool AutoHide { get; set; }
        public string PanelCaption { get; set; }
        public IJcwChartCtl ChartControl { get; set; }
        public ChartMetadata Metadata { get; set; }

        #endregion

        #endregion

        #region Event Handlers

        private void JcwChartStatisticsCtl_Load (object sender, EventArgs e)
        {
            MinDataGridView.DataSource = m_minValues;
            MaxDataGridView.DataSource = m_maxValues;
            AveDataGridView.DataSource = m_aveValues;

            // Force recalculation of aggregates based on the min and max values set above.
            GridView_OnRecalculateAggregate (this, EventArgs.Empty);
        }

        private void GridView_OnRecalculateAggregate (object sender, EventArgs e)
        {
            // Clear the grid view binding list data sources and rebuild them.
            BeginLoadData ();
            EndLoadData ();
        }

        #endregion

        #region Public Methods

        public void BeginInit ()
        {
            Series.Clear ();
        }

        public void EndInit ()
        {
            // Set value to itself so that analyzer will not complain wanting this to be a static method.
            // If actual logic is added in this method, this can be removed.
            bool currentAutoHide = AutoHide;
            AutoHide = currentAutoHide;
        }

        public void BeginLoadData ()
        {
            m_minValues.Clear ();
            m_maxValues.Clear ();
            m_aveValues.Clear ();
        }

        public void AddSeriesDataPoint (StatisticTypes statisticType, string seriesName, double value, bool includeInAggregate)
        {
            // add dictionary to store statistic's specific series value
            if (!Series.ContainsKey (statisticType))
            {
                Series[statisticType] = new List<IJcwChartStatistic> ();
            }

            // if the series name contains a line terminator, replace it with a dash
            seriesName = seriesName.Replace ("\n", " - ");

            // Store the series value.
            Series[statisticType].Add (new ChartStatistic ()
            {
                Name = seriesName,
                Value = value,
                IncludeInAggregate = includeInAggregate,
                CanIncludeInAggregate = includeInAggregate,
            });
        }

        public void EndLoadData ()
        {
            // Populate min/max/ave lists that grid view will use as datasources.  The data will come from the series collection
            // and will include calculated aggregates of the series information as well.
            if (Series.Count > 0)
            {
                if (Series.ContainsKey (StatisticTypes.Minimum))
                {
                    List<double> vector = new List<double> ();
                    foreach (IJcwChartStatistic statistic in Series[StatisticTypes.Minimum])
                    {
                        m_minValues.Add (statistic);
                        if (statistic.IncludeInAggregate)
                        {
                            vector.Add (statistic.Value);
                        }
                    }

                    double average = Utilities.GetAverage (vector);
                    IJcwChartStatistic averageStatistic = new ChartStatistic ()
                    {
                        Name = "Average",
                        Value = average,
                        IncludeInAggregate = false,
                        CanIncludeInAggregate = false
                    };
                    m_minValues.Add (averageStatistic);

                    double stdDev = Utilities.GetStandardDeviation (vector);
                    IJcwChartStatistic stdDevStatistic = new ChartStatistic ()
                    {
                        Name = "Standard Deviation",
                        Value = stdDev,
                        IncludeInAggregate = false,
                        CanIncludeInAggregate = false
                    };
                    m_minValues.Add (stdDevStatistic);
                }

                if (Series.ContainsKey (StatisticTypes.Maximum))
                {
                    List<double> vector = new List<double> ();
                    foreach (IJcwChartStatistic statistic in Series[StatisticTypes.Maximum])
                    {
                        m_maxValues.Add (statistic);
                        if (statistic.IncludeInAggregate)
                        {
                            vector.Add (statistic.Value);
                        }
                    }

                    double average = Utilities.GetAverage (vector);
                    IJcwChartStatistic averageStatistic = new ChartStatistic ()
                    {
                        Name = "Average",
                        Value = average,
                        IncludeInAggregate = false,
                        CanIncludeInAggregate = false
                    };
                    m_maxValues.Add (averageStatistic);

                    double stdDev = Utilities.GetStandardDeviation (vector);
                    IJcwChartStatistic stdDevStatistic = new ChartStatistic ()
                    {
                        Name = "Standard Deviation",
                        Value = stdDev,
                        IncludeInAggregate = false,
                        CanIncludeInAggregate = false
                    };
                    m_maxValues.Add (stdDevStatistic);
                }

                if (Series.ContainsKey (StatisticTypes.Average))
                {
                    List<double> vector = new List<double> ();
                    foreach (IJcwChartStatistic statistic in Series[StatisticTypes.Average])
                    {
                        m_aveValues.Add (statistic);
                        if (statistic.IncludeInAggregate)
                        {
                            vector.Add (statistic.Value);
                        }
                    }

                    double average = Utilities.GetAverage (vector);
                    IJcwChartStatistic averageStatistic = new ChartStatistic ()
                    {
                        Name = "Average",
                        Value = average,
                        IncludeInAggregate = false,
                        CanIncludeInAggregate = false
                    };
                    m_aveValues.Add (averageStatistic);

                    double stdDev = Utilities.GetStandardDeviation (vector);
                    IJcwChartStatistic stdDevStatistic = new ChartStatistic ()
                    {
                        Name = "Standard Deviation",
                        Value = stdDev,
                        IncludeInAggregate = false,
                        CanIncludeInAggregate = false
                    };
                    m_aveValues.Add (stdDevStatistic);
                }
            }

            MinDataGridView.Refresh ();
            MaxDataGridView.Refresh ();
            AveDataGridView.Refresh ();
        }

        #endregion
    }
}