using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;

using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking;

using Jcw.Charting.Interfaces;
using Jcw.Charting.Gui.WinForms.Controls;
using Jcw.Charting.Gui.WinForms.Interfaces;
using Jcw.Charting.Metadata;
using Jcw.Common;
using Jcw.Common.Interfaces;
using Jcw.Common.Gui.WinForms.Controls;
using Jcw.Common.Gui.WinForms.Forms;

using CD = ChartDirector;

namespace Jcw.Charting.Gui.WinForms.Forms
{
    public partial class JcwChartFrm : XtraForm, IJcwChartFrm
    {
        #region Static Fields

        public static readonly ResourceManager JcwResources = new ResourceManager ("Jcw.Resources.Properties.Resources",
            Assembly.GetAssembly (typeof (Jcw.Resources.Properties.Resources)));

        #endregion

        #region Fields

        private JcwChartMetadataCtl m_jcwChartMetadataCtl;
        private JcwChartStatisticsCtl m_jcwChartStatisticsCtl;

        private bool m_chartMetadataSaveRequired = false;
        private ChartMetadata m_chartMetadata = null;
        private ChartMetadata m_originalChartMetadata = null;
        private DisplayOptions m_displayOptions = DisplayOptions.None;

        #endregion

        #region Constructors

        public JcwChartFrm()
            : this (new ChartMetadata (), DisplayOptions.None)
        {
        }

        public JcwChartFrm(DisplayOptions displayOptions)
            : this (new ChartMetadata (), displayOptions)
        {
        }

        public JcwChartFrm(ChartMetadata metadata, DisplayOptions displayOptions)
            : this (metadata, displayOptions, null)
        {
        }

        public JcwChartFrm(ChartMetadata metadata, DisplayOptions displayOptions, IJcwCustomizeChart customizer)
            : base ()
        {
            // set the default skin to use
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.UseWindowsXPTheme = false;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.UseDefaultLookAndFeel = true;

            // enabled form skinning 
            SkinManager.EnableFormSkins ();

            m_displayOptions = displayOptions;
            m_originalChartMetadata = metadata;
            m_chartMetadata = metadata.Clone () as ChartMetadata;

            ChartStatistics = new Dictionary<StatisticTypes, Dictionary<string, double>> ();

            InitializeComponent ();

            m_jcwChartCtl = new JcwChartCtl (m_chartMetadata, this, customizer);
            m_jcwChartMetadataCtl = new JcwChartMetadataCtl ();
            m_jcwChartStatisticsCtl = new JcwChartStatisticsCtl ();
            m_jcwChartCtl.SuspendLayout ();
            m_jcwChartMetadataCtl.SuspendLayout ();
            m_jcwChartStatisticsCtl.SuspendLayout ();
            jcwDockManager.SuspendLayout ();
            SuspendLayout ();
            // 
            // jcwChartCtl
            // 
            m_jcwChartCtl.Dock = DockStyle.Fill;
            // 
            // jcwChartMetadataCtl
            // 
            m_jcwChartMetadataCtl.AutoHide = true;
            m_jcwChartMetadataCtl.ChartControl = m_jcwChartCtl;
            m_jcwChartMetadataCtl.ChartForm = this;
            m_jcwChartMetadataCtl.Metadata = m_chartMetadata;
            m_jcwChartMetadataCtl.PanelCaption = JcwResources.GetString ("MetadataBarText");
            // 
            // jcwChartStatisticsCtl 
            // 
            m_jcwChartStatisticsCtl.AutoHide = true;
            m_jcwChartStatisticsCtl.ChartControl = m_jcwChartCtl;
            m_jcwChartStatisticsCtl.Metadata = m_chartMetadata;
            m_jcwChartStatisticsCtl.PanelCaption = JcwResources.GetString ("StatisticsBarText");

            Controls.Add (m_jcwChartCtl);
            ProcessDisplayOptions ();

            m_jcwChartCtl.ResumeLayout (false);
            m_jcwChartMetadataCtl.ResumeLayout (false);
            m_jcwChartStatisticsCtl.ResumeLayout (false);
            jcwDockManager.ResumeLayout (false);
            ResumeLayout (false);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_chartMetadata = null;
                m_originalChartMetadata = null;

                if (m_jcwChartCtl != null)
                {
                    Controls.Remove (m_jcwChartCtl);
                    m_jcwChartCtl.Dispose ();
                    m_jcwChartCtl = null;
                }

                if (jcwDockManager != null)
                {
                    Controls.Remove (jcwDockManager);
                    jcwDockManager.Dispose ();
                    jcwDockManager = null;
                }
            }

            base.Dispose (disposing);
        }

        #endregion

        #region IJcwChartFrm Implementation

        #region Events

        public event SaveMetadata OnSaveMetadata;

        #endregion

        #region Properties

        [Browsable (false)]
        private JcwChartCtl m_jcwChartCtl;
        public IJcwChartCtl JcwChartControl
        {
            get { return m_jcwChartCtl; }
        }

        [Browsable (false)]
        public JcwDockManager DockManager
        {
            get { return jcwDockManager; }
        }

        [Browsable (false)]
        public Dictionary<StatisticTypes, Dictionary<string, double>> ChartStatistics { get; private set; }

        [Browsable (false)]
        public bool DisplayChartStatisticsDockingPanel
        {
            get { return (m_displayOptions & 
                DisplayOptions.ShowStatisticsControlOnDockingPanel) == DisplayOptions.ShowStatisticsControlOnDockingPanel; }
        }

        [Browsable (false)]
        public bool DisplayChartMetadataDockingPanel
        {
            get
            {
                return
                    (m_displayOptions & DisplayOptions.ShowMetadataControlOnDockingPanel) == 
                        DisplayOptions.ShowMetadataControlOnDockingPanel ||
                    (m_displayOptions & DisplayOptions.ShowSaveableMetadataControlOnDockingPanel) == 
                        DisplayOptions.ShowSaveableMetadataControlOnDockingPanel;
            }
        }

        [Browsable (false)]
        public bool CanSaveChartMetadata
        {
            get
            {
                return (m_displayOptions & DisplayOptions.ShowSaveableMetadataControlOnDockingPanel) == 
                    DisplayOptions.ShowSaveableMetadataControlOnDockingPanel;
            }
        }

        [Browsable (false)]
        public bool DisplayHotSpots
        {
            get { return (m_displayOptions & DisplayOptions.ShowHotSpots) == DisplayOptions.ShowHotSpots; }
            set
            {
                if (value)
                {
                    m_displayOptions |= DisplayOptions.ShowHotSpots;
                }
                else
                {
                    m_displayOptions &= ~DisplayOptions.ShowHotSpots;
                }
            }
        }

        #endregion

        #region Methods

        public void AddDockingPanelControl(IJcwChartDockPanelControl control)
        {
            JcwUserControl jcwControl = control as JcwUserControl;
            if (control.AutoHide)
            {
                jcwDockManager.AddAutoHidePanel (control.PanelCaption, jcwControl);
            }
            else
            {
                jcwDockManager.AddPanel (control.PanelCaption, jcwControl);
            }
        }

        public IJcwChartDockPanelControl FindDockingPanelControl(string dockingControlName)
        {
            Control.ControlCollection controls = jcwDockManager.FindDockPanelControlsByPanelText (dockingControlName);

            if (controls != null)
            {
                foreach (Control ctl in controls)
                {
                    IJcwChartDockPanelControl jcwDockCtl = ctl as IJcwChartDockPanelControl;
                    if (jcwDockCtl != null)
                    {
                        return jcwDockCtl;
                    }
                }
            }

            return null;
        }

        public void SetVisibilityOfDockingPanel(string dockingControlName, DockVisibility visibility)
        {
            jcwDockManager.SetDockPanelVisibilityByPanelText (dockingControlName, visibility);
        }

        public void RefreshChartMetadataControl(ChartMetadata metadata)
        {
            if (DisplayChartMetadataDockingPanel)
            {
                m_jcwChartMetadataCtl.BeginInit ();
                m_jcwChartMetadataCtl.Metadata = metadata;
                m_jcwChartMetadataCtl.EndInit ();
            }
        }

        public void RefreshChartStatisticsControl(IEnumerable<IJcwChartVectorGroup<double>> seriesVectorGroups,
            IEnumerable<IJcwChartVectorGroup<double>> viewPortSeriesVectorGroups)
        {
            ChartStatistics.Clear ();

            if (DisplayChartStatisticsDockingPanel)
            {
                m_jcwChartStatisticsCtl.BeginInit ();
                m_jcwChartStatisticsCtl.BeginLoadData ();

                foreach (IJcwChartVectorGroup<double> viewPortSeriesVectorGroup in viewPortSeriesVectorGroups)
                {
                    // Add data points for the view port for each vector.
                    foreach (IVector<double> viewPortVector in viewPortSeriesVectorGroup.Vectors)
                    {
                        // Add the series min to the chart statistics control.
                        double min = Utilities.GetMinimum (viewPortVector.Data);
                        m_jcwChartStatisticsCtl.AddSeriesDataPoint (StatisticTypes.Minimum, viewPortVector.Name, min, false);
                        if (!ChartStatistics.ContainsKey (StatisticTypes.Minimum))
                        {
                            ChartStatistics.Add (StatisticTypes.Minimum, new Dictionary<string, double> ());
                        }
                        // Add the series min to the ChartStatistics collection.
                        ChartStatistics[StatisticTypes.Minimum].Add (viewPortVector.Name, min);

                        // Add the series max to the chart statistics control.
                        double max = Utilities.GetMaximum (viewPortVector.Data);
                        m_jcwChartStatisticsCtl.AddSeriesDataPoint (StatisticTypes.Maximum, viewPortVector.Name, max, false);
                        if (!ChartStatistics.ContainsKey (StatisticTypes.Maximum))
                        {
                            ChartStatistics.Add (StatisticTypes.Maximum, new Dictionary<string, double> ());
                        }
                        // Add the series max to the ChartStatistics collection.
                        ChartStatistics[StatisticTypes.Maximum].Add (viewPortVector.Name, max);

                        // Add the series ave to the chart statistics control.
                        double ave = Utilities.GetAverage (viewPortVector.Data);
                        m_jcwChartStatisticsCtl.AddSeriesDataPoint (StatisticTypes.Average, viewPortVector.Name, ave, false);
                        if (!ChartStatistics.ContainsKey (StatisticTypes.Average))
                        {
                            ChartStatistics.Add (StatisticTypes.Average, new Dictionary<string, double> ());
                        }
                        // Add the series ave to the ChartStatistics collection.
                        ChartStatistics[StatisticTypes.Average].Add (viewPortVector.Name, ave);
                    }
                }

                // Add data points for each event in each series vector group vector.
                foreach (IJcwChartVectorGroup<double> seriesVectorGroup in seriesVectorGroups)
                {
                    foreach (IVector<double> vector in seriesVectorGroup.Vectors)
                    {
                        foreach (IVectorAggregate<double> vectorAggregate in vector.Aggregates)
                        {
                            string caption = string.Format ("{0} - {1}", vector.Name, vectorAggregate.Name);
                            m_jcwChartStatisticsCtl.AddSeriesDataPoint (StatisticTypes.Minimum, caption, vectorAggregate.MinValue, true);
                            m_jcwChartStatisticsCtl.AddSeriesDataPoint (StatisticTypes.Maximum, caption, vectorAggregate.MaxValue, true);
                            m_jcwChartStatisticsCtl.AddSeriesDataPoint (StatisticTypes.Average, caption, vectorAggregate.AveValue, true);
                        }
                    }
                }

                m_jcwChartStatisticsCtl.EndLoadData ();
                m_jcwChartStatisticsCtl.EndInit ();
            }
        }

        public void ChartMetadataSaveRequired(ChartMetadata metadata)
        {
            m_chartMetadata = metadata;
            m_chartMetadataSaveRequired = true;

            m_jcwChartCtl.SaveRequired (m_chartMetadataSaveRequired, m_chartMetadata);
            m_jcwChartMetadataCtl.SaveRequired (m_chartMetadataSaveRequired, m_chartMetadata);
        }

        public void RevertChartMetadataSaveRequired()
        {
            if (m_chartMetadataSaveRequired)
            {
                m_chartMetadataSaveRequired = false;
                m_chartMetadata = m_originalChartMetadata.Clone () as ChartMetadata;

                m_jcwChartCtl.SaveRequired (m_chartMetadataSaveRequired, m_chartMetadata);
                m_jcwChartMetadataCtl.SaveRequired (m_chartMetadataSaveRequired, m_chartMetadata);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private void ProcessDisplayOptions()
        {
            bool dockingOn = false;

            if (DisplayChartStatisticsDockingPanel)
            {
                // create auto hide panel to display the chart statistics control
                AddDockingPanelControl (m_jcwChartStatisticsCtl);
                dockingOn = true;
            }

            if (DisplayChartMetadataDockingPanel)
            {
                // create auto hide panel to display the chart metadata control
                AddDockingPanelControl (m_jcwChartMetadataCtl);
                dockingOn = true;
            }

            if (dockingOn)
            {
                Controls.Add (jcwDockManager);
            }
        }

        #endregion

        #region Public Methods

        public void SaveChartMetadata()
        {
            if (CanSaveChartMetadata && m_chartMetadataSaveRequired)
            {
                // make sure that the zooming parameters are not saved in the chart metadata
                m_chartMetadata.IsChartZoomedIn = false;
                m_chartMetadata.IsChartZoomInSelected = false;
                m_chartMetadata.IsChartZoomOutSelected = false;
                m_chartMetadata.ChartHorizontalMinimum = m_originalChartMetadata.ChartHorizontalMinimum;
                m_chartMetadata.ChartHorizontalMaximum = m_originalChartMetadata.ChartHorizontalMaximum;

                m_chartMetadataSaveRequired = false;
                m_originalChartMetadata = m_chartMetadata;

                // raise save metadata required event to any subscribers
                if (OnSaveMetadata != null)
                {
                    OnSaveMetadata (this, new JcwEventArgs<ChartMetadata> (m_chartMetadata));
                }

                m_jcwChartCtl.SaveRequired (m_chartMetadataSaveRequired, m_chartMetadata);
                m_jcwChartMetadataCtl.SaveRequired (m_chartMetadataSaveRequired, m_chartMetadata);
            }
        }

        #endregion
    }
}
