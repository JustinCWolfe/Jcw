using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Jcw.Charting.Gui.WinForms.Forms;
using Jcw.Charting.Gui.WinForms.Interfaces;
using Jcw.Charting.Interfaces;
using Jcw.Charting.Metadata;
using Jcw.Common;
using Jcw.Common.Gui.WinForms.Controls;
using Jcw.Common.Gui.WinForms.Forms;
using Jcw.Common.Interfaces;
using Jcw.Resources.Properties;

using CD = ChartDirector;

namespace Jcw.Charting.Gui.WinForms.Controls
{
    public partial class JcwChartCtl : JcwUserControl, IJcwChartCtl, IJcwChartMetadataSave, IMessageFilter
    {
        #region Constants

        private const int MaxPointsWithoutAggregate = 7500;

        #endregion

        #region Static Members

        public static readonly NumberFormatInfo SerializedDataNumberFormat = new CultureInfo ("en-US").NumberFormat;
        private static readonly List<Color> SeriesColorSequence = new List<Color> ();
        private static readonly List<int> SeriesShapeSequence = new List<int> ();

        #endregion

        #region Menu Properties

        // TODO: figure out how to have borders for menu strip buttons.
        private JcwToolStripButton LineChartButton { get; set; }
        private JcwToolStripButton ScatterChartButton { get; set; }
        private JcwToolStripSeparator MenuStripSeparator { get; set; }
        private JcwToolStripButton ZoomInButton { get; set; }
        private JcwToolStripButton ZoomOutButton { get; set; }
        private JcwToolStripButton ZoomResetButton { get; set; }
        private JcwToolStripSeparator CustomButtonMenuStripSeparator { get; set; }

        #endregion

        #region Layout Fields

        private CD.XYChart m_chart = null;
        private CD.PlotArea m_plotArea = null;
        private Size m_plotAreaSize;
        private Point m_plotAreaLocation;
        private int m_plotBackColor = ColorTranslator.ToOle (Color.White);
        private int m_plotGridColor = ColorTranslator.ToOle (Color.LightGray);
        private int m_chartBackColor = ColorTranslator.ToOle (Color.WhiteSmoke);

        private Bitmap m_addNoteImage;
        private Bitmap m_addVerticalLineImage;
        private Bitmap m_addHorizontalLineImage;

        private int m_chartPadding = 10;

        private int m_axisWidth = 1;
        private List<string> m_xAxisTitles = null;
        private List<string> m_yAxisTitles = null;
        // These are y axis titles after spaces in title text have been replaced with new line characters.
        // This collection is used for title display on the chart and related sizing calculations.
        private List<string> m_WrappedYAxisTitles = null;
        private int m_axisMinimumMajorTickSpacing = 50;
        private Dictionary<AxisTypes, Size> m_axisSizes = null;
        private Dictionary<IVector<double>, CD.Axis> m_yAxes = null;
        private Dictionary<CD.Axis, AxisTypes> m_cdAxisToAxisTypeMap = null;

        private Size m_legendSize;
        private Font m_legendFont = new Font (JcwStyle.JcwStyleFont.FontFamily, 8);

        private Font m_axisFont = new Font (JcwStyle.JcwStyleFont.FontFamily, 8, FontStyle.Bold);
        private Font m_noteFont = new Font (JcwStyle.JcwStyleFont.FontFamily, 8, FontStyle.Regular);

        #endregion

        #region Data Fields

        // Store the minimum and maximum x and y values for the complete chart dataset.
        // These are used as multipliers when computing the new size of the view port after zooming or scrolling.
        private double m_xMinValue;
        private double m_xMaxValue;
        private double m_yMinValue;
        private double m_yMaxValue;

        /// <summary>
        /// Dictionary of min and max values for each vertical axis in the chart. These are used to display min 
        /// and max information for the view port in the chart statistics panel.
        /// </summary>
        private Dictionary<AxisTypes, Dictionary<string, double>> m_axisValues = null;

        /// <summary>
        /// Dictionary of original label (x axis) data keys and clipped and/or aggregated label data values.
        /// </summary>
        private Dictionary<IJcwChartVectorGroup<double>, IJcwChartVectorGroup<double>> m_labelData = null;

        /// <summary>
        /// Dictionary of original series (y axis) data keys and clipped and/or aggregated series data values.
        /// </summary>
        private Dictionary<IJcwChartVectorGroup<double>, IJcwChartVectorGroup<double>> m_seriesData = null;

        /// <summary>
        /// List of all chart data groups to display where each chart data group encapsulates label (x axis) 
        /// data and the associated series data.
        /// </summary>
        private List<IJcwChartDataGroup<double>> m_chartDataGroups = null;

        #endregion

        #region Metadata Fields

        private IMark m_metadataNoteToAdd;
        private IMark m_metadataVerticalLineToAdd;
        private IMark m_metadataHorizontalLineToAdd;

        private ChartMetadata ChartMetadata { get; set; }

        JcwCheckBox m_notesVisibleCheck = null;
        JcwCheckBox m_verticalLinesVisibleCheck = null;
        JcwCheckBox m_horizontalLinesVisibleCheck = null;
        JcwCheckBox m_circleMarkersVisibleCheck = null;

        #endregion

        #region Option Fields

        private bool LineChart { get; set; }
        private string m_axisLabelFormat = "{value|2,.}";

        #endregion

        #region Private Properties

        private IJcwCustomizeChart Customizer { get; set; }
        private List<JcwChartCustomButton> CustomButtons { get; set; }
        private List<JcwChartCustomContextMenu> CustomContextMenus { get; set; }
        private List<JcwChartCustomToolstripButton> CustomToolstripButtons { get; set; }
        private Cursor DefaultHotSpotCursor { get; set; }

        private double GetAxisLowerLimit (AxisTypes axisType)
        {
            if (!m_axisValues.ContainsKey (axisType) || !m_axisValues[axisType].ContainsKey ("max"))
            {
                return 0;
            }
            return m_axisValues[axisType]["max"] - (YRange * (chartDirectorViewer.ViewPortTop + chartDirectorViewer.ViewPortHeight));
        }

        private double GetAxisUpperLimit (AxisTypes axisType)
        {
            if (!m_axisValues.ContainsKey (axisType) || !m_axisValues[axisType].ContainsKey ("max"))
            {
                return 1;
            }
            return m_axisValues[axisType]["max"] - (YRange * chartDirectorViewer.ViewPortTop);
        }

        private double XRange
        {
            get { return m_xMaxValue - m_xMinValue; }
        }

        private double YRange
        {
            get { return m_yMaxValue - m_yMinValue; }
        }

        #endregion

        #region Constructors

        static JcwChartCtl ()
        {
            // Set the cache size for the static Regex usages. Without doing this the static Regex methods
            // cache the last searched string which can be quite large for charts.
            Regex.CacheSize = 0;
            ChartDirector.Chart.setLicenseCode ("RDST-24TT-BFKF-PTAF-71E6-7D77");

            SeriesColorSequence.AddRange (new Color[] {
                Color.Red, Color.Green, Color.Blue, Color.Black, Color.Fuchsia, 
                Color.Maroon, Color.Brown, Color.Coral, Color.DarkGoldenrod, Color.DarkGray, 
                Color.ForestGreen, Color.Gold, Color.Purple, Color.SeaGreen, Color.Silver, 
                Color.SpringGreen, Color.SteelBlue, Color.Tomato, Color.Turquoise, Color.Violet, 
                Color.YellowGreen, Color.SlateGray
            });

            SeriesShapeSequence.AddRange (new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
        }

        public JcwChartCtl (ChartMetadata metadata, IJcwChartFrm chartForm)
            : this (metadata, chartForm, null)
        {
        }

        public JcwChartCtl (ChartMetadata metadata, IJcwChartFrm chartForm, IJcwCustomizeChart customizer)
        {
            // By default draw line charts.
            LineChart = true;

            ChartMetadata = metadata;
            ChartForm = chartForm;

            // Note that we use our own 'chartContextMenu' instead of the ContextMenuStrip from 
            // our base class.  This is so that the right click event will be handled by the chart director
            // ClickHotSpot method instead of being intercepted by the ContextMenuStrip object.

            Customizer = customizer;
            CustomButtons = new List<JcwChartCustomButton> ();
            CustomContextMenus = new List<JcwChartCustomContextMenu> ();
            CustomToolstripButtons = new List<JcwChartCustomToolstripButton> ();

            InitializeComponent ();

            SuspendLayout ();
            InitializeToolstrip ();
            CreateContextMenus ();
            ResumeLayout (false);

            DefaultHotSpotCursor = chartDirectorViewer.HotSpotCursor;

            Application.AddMessageFilter (this);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing)
            {
                Application.RemoveMessageFilter (this);

                chartMenuStrip.Items.Remove (ZoomInButton);
                ZoomInButton.Click -= ZoomInButton_Click;
                ZoomInButton.Dispose ();

                chartMenuStrip.Items.Remove (ZoomOutButton);
                ZoomOutButton.Click -= ZoomOutButton_Click;
                ZoomOutButton.Dispose ();

                chartMenuStrip.Items.Remove (ZoomResetButton);
                ZoomResetButton.Click -= ZoomResetButton_Click;
                ZoomResetButton.Dispose ();

                chartMenuStrip.Items.Remove (LineChartButton);
                LineChartButton.Click -= ChartTypeButton_Click;
                LineChartButton.Dispose ();

                chartMenuStrip.Items.Remove (ScatterChartButton);
                ScatterChartButton.Click -= ChartTypeButton_Click;
                ScatterChartButton.Dispose ();

                chartMenuStrip.Items.Remove (MenuStripSeparator);
                MenuStripSeparator.Dispose ();

                if (CustomButtonMenuStripSeparator != null)
                {
                    chartMenuStrip.Items.Remove (CustomButtonMenuStripSeparator);
                    CustomButtonMenuStripSeparator.Dispose ();
                }

                chartMenuStrip.Dispose ();

                // Context menu items were added dynamically so need to dispose of them to avoid a memory leak.
                chartContextMenu.Items.Clear ();

                // Remove references to contents of Customizer and Customizer itself.
                foreach (JcwChartCustomButton customButton in CustomButtons)
                {
                    if (customButton.Button != null)
                    {
                        customButton.Button.Click -= CustomButton_Clicked;
                        Controls.Remove (customButton.Button);
                        customButton.Button.Dispose ();
                    }
                }
                CustomButtons.Clear ();

                foreach (JcwChartCustomContextMenu customContextMenu in CustomContextMenus)
                {
                }
                CustomContextMenus.Clear ();

                foreach (JcwChartCustomToolstripButton customToolstripButton in CustomToolstripButtons)
                {
                    if (customToolstripButton.Button != null)
                    {
                        customToolstripButton.Button.Click -= CustomToolstripButton_Clicked;
                        chartMenuStrip.Items.Remove (customToolstripButton.Button);
                        customToolstripButton.Button.Dispose ();
                    }
                }
                CustomToolstripButtons.Clear ();

                Customizer = null;

                if (m_addNoteImage != null)
                {
                    m_addNoteImage.Dispose ();
                }
                if (m_addVerticalLineImage != null)
                {
                    m_addVerticalLineImage.Dispose ();
                }
                if (m_addHorizontalLineImage != null)
                {
                    m_addHorizontalLineImage.Dispose ();
                }

                m_circleMarkersVisibleCheck.CheckedChanged -= CircleMarkersVisible_CheckedChanged;
                m_horizontalLinesVisibleCheck.CheckedChanged -= HorizontalMarkersVisible_CheckedChanged;
                m_notesVisibleCheck.CheckedChanged -= NotesVisible_CheckedChanged;
                m_verticalLinesVisibleCheck.CheckedChanged -= VerticalMarkersVisible_CheckedChanged;

                // unsubscribe to user control resize event
                SizeChanged -= ChartCtl_SizeChanged;

                if (chartDirectorViewer != null)
                {
                    // unsubscribe to chart director events.
                    chartDirectorViewer.ViewPortChanged -= chartDirectorViewer_ViewPortChanged;
                    chartDirectorViewer.ClickHotSpot -= chartDirectorViewer_ClickHotSpot;

                    chartDirectorViewer.ImageMap = null;
                    chartDirectorViewer.Chart = null;
                    chartDirectorViewer.Dispose ();
                }

                ChartMetadata = null;
                ChartForm = null;

                m_axisValues = null;
                m_chart = null;

                m_chartDataGroups = null;
                m_labelData = null;
                m_plotArea = null;
                m_seriesData = null;
                m_yAxisTitles = null;
                m_xAxisTitles = null;
            }

            base.Dispose (disposing);
        }

        #endregion

        #region Static Methods

        private static string GetChartDirectorFontName (Font font)
        {
            StringBuilder fontStyle = new StringBuilder (font.SizeInPoints.ToString ());

            switch (font.Style)
            {
                case FontStyle.Bold:
                    fontStyle.Append (" Bold");
                    break;
                case FontStyle.Italic:
                    fontStyle.Append (" Italic");
                    break;
            }

            return fontStyle.ToString ();
        }

        #endregion

        #region Context Menu Handlers

        private void SaveChartAsImage_Click (object sender, EventArgs ea)
        {
            // Save the current directory because the save file dialog opened below can change
            // it if the user chooses to save a file into a location other than the current directory.
            string originalCurrentDirectory = System.Environment.CurrentDirectory;

            Stream chartFileStream = null;
            try
            {
                SaveFileDialog saveChartAsImageDlg = new SaveFileDialog ();
                saveChartAsImageDlg.Filter = JcwResources.GetString ("SaveChartAsImageDialogFilter");
                saveChartAsImageDlg.Title = JcwResources.GetString ("SaveChartAsImageDialogTitle");
                saveChartAsImageDlg.FileName = JcwResources.GetString ("DefaultSaveChartAsImageName");

                if (saveChartAsImageDlg.ShowDialog () == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty (saveChartAsImageDlg.FileName))
                    {
                        // open the specified filename as a filestream which will be written to below
                        chartFileStream = saveChartAsImageDlg.OpenFile () as FileStream;
                        Debug.Assert (chartFileStream != null, "Could not get filestream for selected file");

                        // Saves the Image in the appropriate ImageFormat based upon the file type selected in the dialog box.
                        // NOTE that the FilterIndex property is one-based.
                        switch (saveChartAsImageDlg.FilterIndex)
                        {
                            case 1:
                                ChartImage.Save (chartFileStream, ImageFormat.Jpeg);
                                break;
                            case 2:
                                ChartImage.Save (chartFileStream, ImageFormat.Bmp);
                                break;
                            case 3:
                                ChartImage.Save (chartFileStream, ImageFormat.Gif);
                                break;
                            case 4:
                                ChartImage.Save (chartFileStream, ImageFormat.Png);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error.LastError = ex.Message;
                JcwMessageBox.Show (ex.ToString (), "Error");
            }
            finally
            {
                // If the current directory was changed by the save file dialog above, set it back to its original value.
                if (!System.Environment.CurrentDirectory.Equals (originalCurrentDirectory))
                {
                    System.Environment.CurrentDirectory = originalCurrentDirectory;
                }

                if (chartFileStream != null)
                {
                    chartFileStream.Close ();
                }
            }
        }

        private void AddChartNote_Click (object sender, EventArgs ea)
        {
            // If we can't save chart metdata, don't give the user the illusion that notes can be added.
            if (ChartForm.CanSaveChartMetadata)
            {
                // add text to node
                Note note = m_metadataNoteToAdd as Note;

                // popup dialog to get text for chart note
                ChartNoteDlg noteCtl = new ChartNoteDlg ();
                noteCtl.ChartNote = note;
                noteCtl.StartPosition = FormStartPosition.CenterParent;

                if (DialogResult.OK == noteCtl.ShowDialog (Parent))
                {
                    ChartMetadata.AddChartNote (note);
                    ChartForm.ChartMetadataSaveRequired (ChartMetadata);
                }
            }
        }

        private void NotesVisible_CheckedChanged (object sender, EventArgs ea)
        {
            ChartMetadata.AreNotesVisible = !ChartMetadata.AreNotesVisible;
            chartContextMenu.Close (ToolStripDropDownCloseReason.ItemClicked);
            ChartForm.ChartMetadataSaveRequired (ChartMetadata);
        }

        private void AddVerticalMarker_Click (object sender, EventArgs ea)
        {
            if (ChartForm.CanSaveChartMetadata)
            {
                ChartMetadata.AddChartVerticalLine (m_metadataVerticalLineToAdd as VerticalLine);
                ChartForm.ChartMetadataSaveRequired (ChartMetadata);
            }
        }

        private void VerticalMarkersVisible_CheckedChanged (object sender, EventArgs ea)
        {
            ChartMetadata.AreVerticalLinesVisible = !ChartMetadata.AreVerticalLinesVisible;
            chartContextMenu.Close (ToolStripDropDownCloseReason.ItemClicked);
            ChartForm.ChartMetadataSaveRequired (ChartMetadata);
        }

        private void AddHorizontalMarker_Click (object sender, EventArgs ea)
        {
            if (ChartForm.CanSaveChartMetadata)
            {
                ChartMetadata.AddChartHorizontalLine (m_metadataHorizontalLineToAdd as HorizontalLine);
                ChartForm.ChartMetadataSaveRequired (ChartMetadata);
            }
        }

        private void HorizontalMarkersVisible_CheckedChanged (object sender, EventArgs ea)
        {
            ChartMetadata.AreHorizontalLinesVisible = !ChartMetadata.AreHorizontalLinesVisible;
            chartContextMenu.Close (ToolStripDropDownCloseReason.ItemClicked);
            ChartForm.ChartMetadataSaveRequired (ChartMetadata);
        }

        private void CircleMarkersVisible_CheckedChanged (object sender, EventArgs ea)
        {
            if (ChartForm.CanSaveChartMetadata)
            {
                ChartMetadata.AreCircleMarkersVisible = !ChartMetadata.AreCircleMarkersVisible;
                chartContextMenu.Close (ToolStripDropDownCloseReason.ItemClicked);
                ChartForm.ChartMetadataSaveRequired (ChartMetadata);
            }
        }

        #endregion

        #region Event Handlers

        private void ChartCtl_SizeChanged (object sender, EventArgs e)
        {
            SetChartData ();
        }

        private void ChartTypeButton_Click (object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            if (btn != null && !btn.Checked)
            {
                LineChart = LineChartButton.Equals (sender);

                LineChartButton.Checked = LineChart;
                ScatterChartButton.Checked = !LineChart;

                SetChartData ();
            }
        }

        private void ZoomInButton_Click (object sender, EventArgs e)
        {
            if (chartDirectorViewer.MouseUsage != CD.WinChartMouseUsage.ZoomIn)
            {
                ZoomInButton.Checked = ChartMetadata.IsChartZoomInSelected = true;
                ZoomOutButton.Checked = false;
                chartDirectorViewer.MouseUsage = CD.WinChartMouseUsage.ZoomIn;
            }
            else
            {
                ZoomInButton.Checked = ChartMetadata.IsChartZoomInSelected = false;
                chartDirectorViewer.MouseUsage = CD.WinChartMouseUsage.ScrollOnDrag;
            }
        }

        private void ZoomOutButton_Click (object sender, EventArgs e)
        {
            if (chartDirectorViewer.MouseUsage != CD.WinChartMouseUsage.ZoomOut)
            {
                ZoomInButton.Checked = false;
                ZoomOutButton.Checked = ChartMetadata.IsChartZoomOutSelected = true;
                chartDirectorViewer.MouseUsage = CD.WinChartMouseUsage.ZoomOut;
            }
            else
            {
                ZoomOutButton.Checked = ChartMetadata.IsChartZoomOutSelected = false;
                chartDirectorViewer.MouseUsage = CD.WinChartMouseUsage.ScrollOnDrag;
            }
        }

        private void ZoomResetButton_Click (object sender, EventArgs e)
        {
            InitializeChart ();
            chartDirectorViewer.updateViewPort (true, true);
        }

        private void CustomButton_Clicked (object sender, EventArgs ea)
        {
        }

        private void CustomToolstripButton_Clicked (object sender, EventArgs ea)
        {
            ToolStripButton clickedButton = sender as ToolStripButton;
            clickedButton.Checked = !clickedButton.Checked;

            // Find the JcwChartCustomButton that was clicked by comparing the sender to the custom button Button property.
            JcwChartCustomToolstripButton clickedToolstripButton = null;
            foreach (JcwChartCustomToolstripButton toolstripButton in CustomToolstripButtons)
            {
                if (clickedButton.Equals (toolstripButton.Button))
                {
                    clickedToolstripButton = toolstripButton;
                }
            }

            if (clickedToolstripButton != null)
            {
                // Set the hot spot cursor to either the default cursor or the cursor specified for this toolstrip button.
                chartDirectorViewer.HotSpotCursor = (chartDirectorViewer.HotSpotCursor != clickedToolstripButton.HotSpotCursor) ?
                    clickedToolstripButton.HotSpotCursor :
                    chartDirectorViewer.HotSpotCursor = DefaultHotSpotCursor;

                // Fire the button clicked event for this toolstrip button.
                if (clickedToolstripButton.ButtonProperties.ClickEventHandler != null)
                {
                    clickedToolstripButton.ButtonProperties.ClickEventHandler (sender, ea);
                }
            }
        }

        private void chartDirectorViewer_ClickHotSpot (object sender, CD.WinHotSpotEventArgs whsea)
        {
            double x = Convert.ToDouble (whsea.AttrValues["x"], SerializedDataNumberFormat);
            double y = Convert.ToDouble (whsea.AttrValues["value"], SerializedDataNumberFormat);
            string seriesName = whsea.AttrValues["dataSetName"] as string;

            // Set the selection details in the chart customizer.
            if (Customizer != null)
            {
                Customizer.SetSelectionProperties (x, y, seriesName);
            }

            if (ChartForm.CanSaveChartMetadata)
            {
                if (whsea.Button == MouseButtons.Right)
                {
                    if (m_xAxisTitles.Count != 0 && m_yAxisTitles.Count != 0)
                    {
                        m_metadataVerticalLineToAdd = new VerticalLine (x, m_yAxisTitles[0]);
                        m_metadataHorizontalLineToAdd = new HorizontalLine (y, m_yAxisTitles[0]);
                        m_metadataNoteToAdd = new Note (x, m_xAxisTitles[0], y, m_yAxisTitles[0]);
                    }

                    // get the location of the clicked hot spot and move it 20 pixels to the right to find the 
                    // location of the upper left corner of the context menu
                    Point contextMenuStartPoint = PointToScreen (whsea.Location);
                    contextMenuStartPoint.Offset (20, 0);

                    ToolStripDropDownDirection direction = Jcw.Common.Gui.WinForms.Utilities.GetToolStripDropDownDirection (chartContextMenu, contextMenuStartPoint);

                    // show the context menu relative to the chart control
                    chartContextMenu.Show (contextMenuStartPoint, direction);
                }
            }
        }

        #endregion

        #region IJcwChartCtl Implementation

        #region Events

        public event EventHandler OnChartComplete;

        #endregion

        #region Properties

        [Browsable (false)]
        public bool IsXYChartInitialized
        {
            get { return (m_chart != null); }
        }

        [Browsable (false)]
        public Image ChartImage
        {
            get { return m_chart.makeImage (); }
        }

        [Browsable (true)]
        public string AxisLabelFormat
        {
            get { return m_axisLabelFormat; }
            set { m_axisLabelFormat = value; }
        }

        public string XAxisTitle
        {
            get
            {
                return (m_xAxisTitles != null && m_xAxisTitles.Count > 0) ?
                    m_xAxisTitles[0] :
                    null;
            }
        }

        public List<string> XAxisTitles
        {
            get { return m_xAxisTitles; }
        }

        public string YAxisTitle
        {
            get
            {
                return (m_yAxisTitles != null && m_yAxisTitles.Count > 0) ?
                    m_yAxisTitles[0] :
                    null;
            }
        }

        public List<string> YAxisTitles
        {
            get { return m_yAxisTitles; }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// This method is called before the control becomes visible for the first time.  
        /// It could be called multiple times if it is part of an MDI child form or if the user control handle is recreated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartCtl_Load (object sender, EventArgs e)
        {
            // Unsubscribe and subscribe to user control resize event.
            SizeChanged -= ChartCtl_SizeChanged;
            SizeChanged += ChartCtl_SizeChanged;

            // Unsubscribe and subscribe to chart director events.
            chartDirectorViewer.ViewPortChanged -= chartDirectorViewer_ViewPortChanged;
            chartDirectorViewer.ViewPortChanged += chartDirectorViewer_ViewPortChanged;
            chartDirectorViewer.ClickHotSpot -= chartDirectorViewer_ClickHotSpot;
            chartDirectorViewer.ClickHotSpot += chartDirectorViewer_ClickHotSpot;

            InitializeChart ();
        }

        #endregion

        #region Methods

        public void SetChartData (IJcwChartDataGroup<double> chartDataGroup)
        {
            List<IJcwChartDataGroup<double>> chartDataGroups = new List<IJcwChartDataGroup<double>> ();
            chartDataGroups.Add (chartDataGroup);
            SetChartData (chartDataGroups);
        }

        public void SetChartData (List<IJcwChartDataGroup<double>> chartDataGroups)
        {
            InitializeToolstripState ();

            m_chartDataGroups = chartDataGroups;

            // Populate the x and y axis title collections based on the contents of the chart data group.
            // In the case where there are multiple data groups, use the axis title from the first data 
            // group as the x axis title.
            m_xAxisTitles = new List<string> { m_chartDataGroups[0].LabelData.AxisTitle };

            // Get the unique set of y axis series names and add them to the y axis titles member variable list.
            HashSet<string> setOfYAxisTitles = new HashSet<string> ();
            foreach (IJcwChartDataGroup<double> chartDataGroup in m_chartDataGroups)
            {
                foreach (IJcwChartVectorGroup<double> seriesVectorGroup in chartDataGroup.SeriesData)
                {
                    setOfYAxisTitles.Add (seriesVectorGroup.AxisTitle);
                }
            }
            m_yAxisTitles = new List<string> (setOfYAxisTitles);

            m_axisValues = new Dictionary<AxisTypes, Dictionary<string, double>> ();

            // find the x & y mins and maxs for the complete data set
            double? xMin = null, xMax = null, yMin = null, yMax = null;

            // check the y values for each series in this row against the y min and max values
            foreach (IJcwChartDataGroup<double> chartDataGroup in m_chartDataGroups)
            {
                int yAxisCount = 0;

                // Find the horizontal range for the chart by comparing the min and max values of each label data vector in the chart data groups.
                foreach (IVector<double> vector in chartDataGroup.LabelData.Vectors)
                {
                    // Get the min and max values for this vector.
                    double vectorMaximum = Utilities.GetMaximum (vector.Data);
                    double vectorMinimum = Utilities.GetMinimum (vector.Data);

                    // Currently I am only supporting horizontal bottom charts.
                    AxisTypes axisType = AxisTypes.HorizontalBottom;

                    if (!m_axisValues.ContainsKey (axisType))
                    {
                        m_axisValues.Add (axisType, new Dictionary<string, double> 
                            {
                                { "max", vectorMaximum }, 
                                { "min", vectorMinimum },
                            });
                    }
                    else
                    {
                        // Check if the max value for this vector is greater than the current max value for the axis 
                        if (vectorMaximum > m_axisValues[axisType]["max"])
                        {
                            m_axisValues[axisType]["max"] = vectorMaximum;
                        }

                        // Check if the min value for this vector is less than the current min value for the axis 
                        if (vectorMinimum < m_axisValues[axisType]["min"])
                        {
                            m_axisValues[axisType]["min"] = vectorMinimum;
                        }
                    }

                    // Find the maximum x value for all series in the chart.
                    if (xMax == null || vectorMaximum > xMax)
                    {
                        xMax = vectorMaximum;
                    }

                    // Find the minimum x value for all series in the chart.
                    if (xMin == null || vectorMinimum < xMin)
                    {
                        xMin = vectorMinimum;
                    }
                }

                // Find the vertical range for the chart by comparing the min and max values of each series data vector in the chart data groups.
                // Also find the min and max values for each vertical axis in the case where the chart has multiple y axes.
                foreach (IJcwChartVectorGroup<double> seriesVectorGroup in chartDataGroup.SeriesData)
                {
                    // The vertical axis types start from 0 in the enumeration which allows this cast
                    // to correctly map to the axis type enumeration value.
                    AxisTypes axisType = (AxisTypes)yAxisCount++;

                    foreach (IVector<double> vector in seriesVectorGroup.Vectors)
                    {
                        // Get the min and max values for this vector.
                        double vectorMaximum = Utilities.GetMaximum (vector.Data);
                        double vectorMinimum = Utilities.GetMinimum (vector.Data);

                        if (!m_axisValues.ContainsKey (axisType))
                        {
                            m_axisValues.Add (axisType, new Dictionary<string, double> 
                            {
                                { "max", vectorMaximum }, 
                                { "min", vectorMinimum },
                            });
                        }
                        else
                        {
                            // Check if the max value for this vector is greater than the current max value for the axis 
                            if (vectorMaximum > m_axisValues[axisType]["max"])
                            {
                                m_axisValues[axisType]["max"] = vectorMaximum;
                            }

                            // Check if the min value for this vector is less than the current min value for the axis 
                            if (vectorMinimum < m_axisValues[axisType]["min"])
                            {
                                m_axisValues[axisType]["min"] = vectorMinimum;
                            }
                        }

                        // Find the maximum y value for all series in the chart.
                        if (yMax == null || vectorMaximum > yMax)
                        {
                            yMax = vectorMaximum;
                        }

                        // Find the minimum y value for all series in the chart.
                        if (yMin == null || vectorMinimum < yMin)
                        {
                            yMin = vectorMinimum;
                        }
                    }
                }
            }

            // Set the min and max values for the x axis.
            m_xMinValue = xMin.GetValueOrDefault ();
            m_xMaxValue = xMax.GetValueOrDefault ();

            // Set the min and max values for the y axis.
            m_yMinValue = yMin.GetValueOrDefault ();
            m_yMaxValue = yMax.GetValueOrDefault ();

            // This resets the view port and calls updateViewPort which posts the view port changed event.
            InitializeViewPort ();
            chartDirectorViewer.updateViewPort (true, true);
        }

        #endregion

        #endregion

        #region Zooming and Scrolling Handler

        /// <summary>
        /// This event handler gets called when the chart is zoomed or scrolled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartDirectorViewer_ViewPortChanged (object sender, CD.WinViewPortEventArgs e)
        {
            // Set the horizontal axis minimum and maximum in our chart metadata object.
            double xMin = m_xMinValue + (chartDirectorViewer.ViewPortLeft * XRange);
            double xMax = xMin + (chartDirectorViewer.ViewPortWidth * XRange);

            // Only reset x axis range if the chart horizontal range has actually changed.
            if (xMin != ChartMetadata.ChartHorizontalMinimum || xMax != ChartMetadata.ChartHorizontalMaximum)
            {
                ChartMetadata.ChartHorizontalMinimum = xMin;
                ChartMetadata.ChartHorizontalMaximum = xMax;
            }

            SetChartData ();
        }

        #endregion

        #region Private Layout Methods

        private void InitializeToolstripState ()
        {
            LineChartButton.Checked = LineChart;
            ScatterChartButton.Checked = !LineChart;

            ZoomInButton.Checked = false;
            ZoomOutButton.Checked = false;
            ZoomResetButton.Checked = false;
        }

        private void InitializeToolstrip ()
        {
            ScatterChartButton = new JcwToolStripButton ();
            ScatterChartButton.Alignment = ToolStripItemAlignment.Right;
            ScatterChartButton.Click += ChartTypeButton_Click;
            ScatterChartButton.Text = JcwResources.GetString ("ScatterChartButtonText");
            chartMenuStrip.Items.Add (ScatterChartButton);

            LineChartButton = new JcwToolStripButton ();
            LineChartButton.Alignment = ToolStripItemAlignment.Right;
            LineChartButton.Click += ChartTypeButton_Click;
            LineChartButton.Text = JcwResources.GetString ("LineChartButtonText");
            chartMenuStrip.Items.Add (LineChartButton);

            MenuStripSeparator = new JcwToolStripSeparator ();
            MenuStripSeparator.Height = 100;
            MenuStripSeparator.Alignment = ToolStripItemAlignment.Right;
            chartMenuStrip.Items.Add (MenuStripSeparator);

            ZoomResetButton = new JcwToolStripButton ();
            ZoomResetButton.Alignment = ToolStripItemAlignment.Right;
            ZoomResetButton.Click += ZoomResetButton_Click;
            ZoomResetButton.Text = JcwResources.GetString ("ZoomResetButtonText");
            chartMenuStrip.Items.Add (ZoomResetButton);

            ZoomOutButton = new JcwToolStripButton ();
            ZoomOutButton.Alignment = ToolStripItemAlignment.Right;
            ZoomOutButton.Click += ZoomOutButton_Click;
            ZoomOutButton.Text = JcwResources.GetString ("ZoomOutButtonText");
            chartMenuStrip.Items.Add (ZoomOutButton);

            ZoomInButton = new JcwToolStripButton ();
            ZoomInButton.Alignment = ToolStripItemAlignment.Right;
            ZoomInButton.Click += ZoomInButton_Click;
            ZoomInButton.Text = JcwResources.GetString ("ZoomInButtonText");
            chartMenuStrip.Items.Add (ZoomInButton);

            if (Customizer != null)
            {
                List<IJcwChartCustomToolstripButtonProperties> customToolstripButtonPropertiesList = Customizer.GetCustomToolstripButtonProperties ();
                if (customToolstripButtonPropertiesList != null && customToolstripButtonPropertiesList.Count > 0)
                {
                    // Since we display toolstrip buttons from the right edge of the chart, we should add them to the toolstrip in reverse order.
                    List<IJcwChartCustomToolstripButtonProperties> reverseOrderCustomToolstripButtonProperties =
                        new List<IJcwChartCustomToolstripButtonProperties> (customToolstripButtonPropertiesList);
                    reverseOrderCustomToolstripButtonProperties.Reverse ();

                    // We have custom toolstrip buttons to add to this chart so add a toolstrip separator between custom buttons and the standard buttons.
                    CustomButtonMenuStripSeparator = new JcwToolStripSeparator ();
                    CustomButtonMenuStripSeparator.Height = 100;
                    CustomButtonMenuStripSeparator.Alignment = ToolStripItemAlignment.Right;
                    chartMenuStrip.Items.Add (CustomButtonMenuStripSeparator);

                    // Add custom toolstrip buttons to the chart menu strip.
                    foreach (JcwChartCustomToolstripButtonProperties customToolstripButtonProperties in reverseOrderCustomToolstripButtonProperties)
                    {
                        ToolStripButton button = new ToolStripButton ();
                        button.Alignment = ToolStripItemAlignment.Right;
                        button.Click += CustomToolstripButton_Clicked;
                        button.Text = customToolstripButtonProperties.Text;
                        button.ToolTipText = customToolstripButtonProperties.TooltipText;

                        JcwChartCustomToolstripButton customToolstripButton = null;
                        if (customToolstripButtonProperties.CursorImage != null)
                        {
                            Cursor hotSpotCursor = GetCursorThumbnailImage (customToolstripButtonProperties.CursorImage);
                            customToolstripButton = new JcwChartCustomToolstripButton (button, customToolstripButtonProperties, hotSpotCursor);
                        }
                        else
                        {
                            customToolstripButton = new JcwChartCustomToolstripButton (button, customToolstripButtonProperties, null);
                        }

                        CustomToolstripButtons.Add (customToolstripButton);
                        chartMenuStrip.Items.Add (button);
                    }
                }
            }
        }

        private bool Thumbnail_Callback () { return false; }

        private Cursor GetCursorThumbnailImage (Bitmap image)
        {
            Bitmap imageThumbnail = image.GetThumbnailImage (30, 30, Thumbnail_Callback, IntPtr.Zero) as Bitmap;
            return Jcw.Common.Gui.WinForms.Utilities.CreateCursor (imageThumbnail, 3, 3);
        }

        private void SetChartLegendTop ()
        {
            float legendHeight = 0;
            float longestSeriesName = 0;
            using (Graphics g = Graphics.FromHwnd (Handle))
            {
                // Find the longest series name and the total height for all the series names.
                foreach (IJcwChartDataGroup<double> chartDataGroup in m_chartDataGroups)
                {
                    // Add height for all series (y axis) data to the legend height.
                    foreach (IJcwChartVectorGroup<double> chartVectorGroup in chartDataGroup.SeriesData)
                    {
                        foreach (IVector<double> vector in chartVectorGroup.Vectors)
                        {
                            SizeF seriesSize = g.MeasureString (vector.Name, m_legendFont);
                            legendHeight += seriesSize.Height;

                            if (seriesSize.Width > longestSeriesName)
                            {
                                longestSeriesName = seriesSize.Width;
                            }
                        }
                    }
                }
            }

            // Add padding space on the top of the chart legend.
            legendHeight += (m_chartPadding * 2);

            // Legend width is the length of the longest series name plus right and left padding for margin from text to legend box.
            float legendWidth = longestSeriesName + (m_chartPadding * 2);

            m_legendSize = new Size (Convert.ToInt32 (Math.Ceiling (legendWidth)), Convert.ToInt32 (Math.Ceiling (legendHeight)));
            CD.LegendBox legend = m_chart.addLegend (m_chartPadding, m_chartPadding, true);
            legend.setAlignment (CD.Chart.TopLeft);
            legend.setBackground (m_chartBackColor);
            legend.setFontSize (m_legendFont.SizeInPoints);
            legend.setFontStyle (GetChartDirectorFontName (m_legendFont));
            legend.setMargin (m_chartPadding / 2);
        }

        private void CalculateAxisWidth ()
        {
            // Calculate the width for all the vertical axes.
            using (Graphics g = Graphics.FromHwnd (Handle))
            {
                int yAxisCount = 0;
                foreach (AxisTypes axisType in m_axisValues.Keys)
                {
                    if (axisType != AxisTypes.HorizontalBottom && axisType != AxisTypes.HorizontalTop)
                    {
                        // Get the measured size in pixels for the maximum value displayed on the axis.
                        string formattedMaxValue = m_chart.formatValue (GetAxisUpperLimit (axisType), m_axisLabelFormat);
                        SizeF measuredMaxValueSize = g.MeasureString (formattedMaxValue, m_axisFont);

                        // Get the measured size in pixels for the minimum value.
                        string formattedMinValue = m_chart.formatValue (GetAxisLowerLimit (axisType), m_axisLabelFormat);
                        SizeF measuredMinValueSize = g.MeasureString (formattedMinValue, m_axisFont);

                        // The widest series value should be either the width of the measured max or min value.
                        // This accounts for the width of the axis mark text (eg. -1000 or 1000).
                        SizeF widestSeriesValueSize = (measuredMaxValueSize.Width > measuredMinValueSize.Width) ?
                            measuredMaxValueSize :
                            measuredMinValueSize;

                        // Get the measured size in pixels for the axis text.
                        SizeF measuredAxisSize = g.MeasureString (m_WrappedYAxisTitles[yAxisCount++], m_axisFont);

                        // Use the wider of: the axis title text or the text for the widest series (min or max - eg. -1000 or 1000) value.
                        float currentYAxisWidth = (measuredAxisSize.Width > widestSeriesValueSize.Width) ?
                            measuredAxisSize.Width :
                            widestSeriesValueSize.Width;

                        // Add chart padding on each size of the y axis widest value.
                        currentYAxisWidth += m_chartPadding * 2;

                        if (!m_axisSizes.ContainsKey (axisType))
                        {
                            m_axisSizes.Add (axisType, Size.Empty);
                        }

                        // Preserve any axis height that may have been calculated previously for this axis.
                        m_axisSizes[axisType] = new Size (Convert.ToInt32 (Math.Ceiling (currentYAxisWidth)), m_axisSizes[axisType].Height);
                    }
                }
            }
        }

        private void CalculateAxisHeight ()
        {
            // Calculate the height for all the horizontal axes.
            int xAxisHeight = 0;
            using (Graphics g = Graphics.FromHwnd (Handle))
            {
                AxisTypes maxYAxisType = AxisTypes.VerticalLeft;
                int xAxisCount = 0, yAxisCount = 0, maxYAxisHeight = 0;

                foreach (AxisTypes axisType in m_axisValues.Keys)
                {
                    if (AxisTypes.HorizontalBottom.Equals (axisType) || AxisTypes.HorizontalTop.Equals (axisType))
                    {
                        // Get the measured size in pixels for the maximum value displayed on the axis.
                        string formattedMaxValue = m_chart.formatValue (GetAxisUpperLimit (axisType), m_axisLabelFormat);
                        SizeF measuredMaxValueSize = g.MeasureString (formattedMaxValue, m_axisFont);

                        // Get the measured size in pixels for the minimum value.
                        string formattedMinValue = m_chart.formatValue (GetAxisLowerLimit (axisType), m_axisLabelFormat);
                        SizeF measuredMinValueSize = g.MeasureString (formattedMinValue, m_axisFont);

                        // The tallest series value should be either the height of the measured max or min value.
                        SizeF tallestSeriesValueSize = (measuredMaxValueSize.Width > measuredMinValueSize.Width) ?
                            measuredMaxValueSize :
                            measuredMinValueSize;

                        // Get the measured size in pixels for the axis text.
                        SizeF measuredAxisSize = g.MeasureString (m_xAxisTitles[xAxisCount++], m_axisFont);

                        // Calculate pixel height of the x axis title to decide how much space we need for the x axis. Height includes 
                        // the x axis title text, the height of the x axis value text (eg. -1000 or 1000) and padding on each side of the axis.
                        xAxisHeight += Convert.ToInt32 (Math.Ceiling (measuredAxisSize.Height + tallestSeriesValueSize.Height + (m_chartPadding * 2)));

                        if (!m_axisSizes.ContainsKey (axisType))
                        {
                            m_axisSizes.Add (axisType, Size.Empty);
                        }
                        m_axisSizes[axisType] = new Size (0, xAxisHeight);
                    }
                    else
                    {
                        SizeF measuredYAxisSize = g.MeasureString (m_WrappedYAxisTitles[yAxisCount++], m_axisFont);
                        if (measuredYAxisSize.Height > maxYAxisHeight)
                        {
                            maxYAxisType = axisType;
                            // The y axis title height includes the height of the title text and padding on each side of the axis title.
                            maxYAxisHeight = Convert.ToInt32 (Math.Ceiling (measuredYAxisSize.Height) + (m_chartPadding * 2));
                        }
                    }
                }

                // Since we are displaying y axis titles on the top of the chart, we need to include the height of the tallest 
                // y axis title in the total axis height get the measured size in pixels for the axis text
                xAxisHeight += maxYAxisHeight;
                if (!m_axisSizes.ContainsKey (maxYAxisType))
                {
                    m_axisSizes.Add (maxYAxisType, Size.Empty);
                }
                // Preserve any axis width that may have been calculated previously for this axis.
                m_axisSizes[maxYAxisType] = new Size (m_axisSizes[maxYAxisType].Width, maxYAxisHeight);
            }
        }

        private void SetChartLayout ()
        {
            int height = Height - chartMenuStrip.Height;
            chartDirectorViewer.Size = new Size (Width, height);

            m_chart = new CD.XYChart (Width, height, m_chartBackColor, CD.Chart.Transparent, 0);

            // if we are displaying axis titles at the top of the chart, replace all y axis title spaces with line 
            // terminators which should have the same effect as word wrapping
            m_WrappedYAxisTitles = new List<string> ();
            foreach (string axisTitle in m_yAxisTitles)
            {
                // Replace all spaces in the series name except with new line character and store translated
                // axis titles in chart display collection of 'wrapped' titles.
                m_WrappedYAxisTitles.Add (axisTitle.Replace (" ", "\n"));
            }

            m_axisSizes = new Dictionary<AxisTypes, Size> ();
            CalculateAxisHeight ();
            CalculateAxisWidth ();

            // figure out how much of the y axis width should be reserved for the left and for the right side of the chart - this
            // will only apply in the case where there are multiple y axes
            int leftMargin = 0, rightMargin = 0, topMargin = 0, bottomMargin = 0;
            foreach (AxisTypes axisType in m_axisValues.Keys)
            {
                if (axisType == AxisTypes.HorizontalBottom)
                {
                    bottomMargin += m_axisSizes[axisType].Height;
                }
                else if (axisType == AxisTypes.VerticalLeft || axisType == AxisTypes.VerticalLeft2)
                {
                    leftMargin += m_axisSizes[axisType].Width;
                }
                else if (axisType == AxisTypes.VerticalRight || axisType == AxisTypes.VerticalRight2)
                {
                    rightMargin += m_axisSizes[axisType].Width;
                }

                // the top margin is a special case because it needs to include the height of the top
                // horizontal axis as well as max height for vertical axes (the vertical axis maximum is
                // only set for the case where the axis labels are displayed at the top of the chart
                if (axisType != AxisTypes.HorizontalBottom)
                {
                    topMargin += m_axisSizes[axisType].Height;
                }
            }

            // If there are no right vertical axes, the right margin should simply be the chart padding.
            if (rightMargin == 0)
            {
                rightMargin = m_chartPadding;
            }

            SetChartLegendTop ();

            // Add the legend height to the top margin.
            topMargin += m_legendSize.Height;
            int xAxisHeight = topMargin + bottomMargin;

            m_plotAreaLocation = new Point (leftMargin, topMargin);
            m_plotAreaSize = new Size (Width - leftMargin - rightMargin, height - xAxisHeight);

            m_plotArea = m_chart.setPlotArea (m_plotAreaLocation.X, m_plotAreaLocation.Y, m_plotAreaSize.Width, m_plotAreaSize.Height, m_plotBackColor, -1, -1);
            m_plotArea.setGridColor (m_plotGridColor, m_plotGridColor);

            m_chart.setClipping ();

            // add all the chart axes
            AddChartAxes ();
        }

        #endregion

        #region Private Data Methods

        private void AddChartAxes ()
        {
            m_cdAxisToAxisTypeMap = new Dictionary<CD.Axis, AxisTypes> ();

            if (m_xAxisTitles.Count != 0)
            {
                // setup the x axis
                CD.Axis firstXAxis = m_chart.xAxis ();
                int xAxisColor = CD.Chart.CColor (Color.Black);
                m_cdAxisToAxisTypeMap.Add (firstXAxis, AxisTypes.HorizontalBottom);
                firstXAxis.setColors (xAxisColor, xAxisColor, xAxisColor);
                firstXAxis.setLabelFormat (m_axisLabelFormat);
                // show no margin on left but a bit of space on right of the plot area
                firstXAxis.setMargin (m_chartPadding, 0);
                firstXAxis.setWidth (m_axisWidth);
                firstXAxis.setTickDensity (m_axisMinimumMajorTickSpacing);

                // xy-zoom mode - compute the actual axis scale in the view port 
                double currentXAxisRange = ChartMetadata.ChartHorizontalMaximum - ChartMetadata.ChartHorizontalMinimum;
                firstXAxis.setLinearScale (ChartMetadata.ChartHorizontalMinimum, ChartMetadata.ChartHorizontalMaximum, currentXAxisRange / 10);

                // add text box to display the x axis title
                CD.TextBox firstXAxisTitle = firstXAxis.setTitle (m_xAxisTitles[0]);
                firstXAxisTitle.setFontSize (m_axisFont.SizeInPoints);
                firstXAxisTitle.setFontStyle (GetChartDirectorFontName (m_axisFont));
                firstXAxisTitle.setMargin (2);
            }

            int yAxisColor = 0;
            if (m_WrappedYAxisTitles.Count != 0)
            {
                // setup the primary (left) y axis
                yAxisColor = CD.Chart.CColor (SeriesColorSequence[0]);
                CD.Axis firstLeftYAxis = m_chart.yAxis ();
                m_cdAxisToAxisTypeMap.Add (firstLeftYAxis, AxisTypes.VerticalLeft);
                firstLeftYAxis.setColors (yAxisColor, yAxisColor, yAxisColor);
                firstLeftYAxis.setLabelFormat (m_axisLabelFormat);
                firstLeftYAxis.setMargin (m_chartPadding, m_chartPadding);
                firstLeftYAxis.setTickDensity (m_axisMinimumMajorTickSpacing);
                firstLeftYAxis.setWidth (m_axisWidth);

                // add the primary y axis title 
                CD.TextBox firstLeftYAxisTitle = firstLeftYAxis.setTitle (m_WrappedYAxisTitles[0]);
                firstLeftYAxisTitle.setAlignment (CD.Chart.TopLeft2);
                firstLeftYAxisTitle.setFontSize (m_axisFont.SizeInPoints);
                firstLeftYAxisTitle.setFontStyle (GetChartDirectorFontName (m_axisFont));
                firstLeftYAxisTitle.setMargin (2);

                m_yAxes = new Dictionary<IVector<double>, CD.Axis> ();

                // Store the series data columns that will be plotted on the primary y axis.
                foreach (IJcwChartDataGroup<double> dataGroup in m_chartDataGroups)
                {
                    IJcwChartVectorGroup<double> chartVectorGroup = dataGroup.SeriesData[0];
                    foreach (IVector<double> vector in chartVectorGroup.Vectors)
                    {
                        m_yAxes.Add (vector, firstLeftYAxis);
                    }
                }
            }

            // setup the secondary (right) y axis
            if (m_WrappedYAxisTitles.Count > 1)
            {
                yAxisColor = CD.Chart.CColor (SeriesColorSequence[1]);
                CD.Axis firstRightYAxis = m_chart.yAxis2 ();
                m_cdAxisToAxisTypeMap.Add (firstRightYAxis, AxisTypes.VerticalRight);
                firstRightYAxis.setColors (yAxisColor, yAxisColor, yAxisColor);
                firstRightYAxis.setLabelFormat (m_axisLabelFormat);
                firstRightYAxis.setMargin (m_chartPadding, m_chartPadding);
                firstRightYAxis.setTickDensity (m_axisMinimumMajorTickSpacing);
                firstRightYAxis.setWidth (m_axisWidth);

                // add the secondary y axis title 
                CD.TextBox firstRightYAxisTitle = firstRightYAxis.setTitle (m_WrappedYAxisTitles[1]);
                firstRightYAxisTitle.setAlignment (CD.Chart.TopRight2);
                firstRightYAxisTitle.setFontSize (m_axisFont.SizeInPoints);
                firstRightYAxisTitle.setFontStyle (GetChartDirectorFontName (m_axisFont));
                firstRightYAxisTitle.setMargin (2);

                // Store the series data columns that will be plotted on the secondary y axis.
                foreach (IJcwChartDataGroup<double> dataGroup in m_chartDataGroups)
                {
                    IJcwChartVectorGroup<double> chartVectorGroup = dataGroup.SeriesData[1];
                    foreach (IVector<double> vector in chartVectorGroup.Vectors)
                    {
                        m_yAxes.Add (vector, firstRightYAxis);
                    }
                }
            }

            // setup the second left y axis
            if (m_WrappedYAxisTitles.Count > 2)
            {
                // add the second left y-axis at 50 pixels to the left of the plot area
                yAxisColor = CD.Chart.CColor (SeriesColorSequence[2]);
                CD.Axis secondLeftYAxis = m_chart.addAxis (CD.Chart.Left, m_axisSizes[AxisTypes.VerticalLeft].Width);
                m_cdAxisToAxisTypeMap.Add (secondLeftYAxis, AxisTypes.VerticalLeft2);
                secondLeftYAxis.setColors (yAxisColor, yAxisColor, yAxisColor);
                secondLeftYAxis.setLabelFormat (m_axisLabelFormat);
                secondLeftYAxis.setMargin (m_chartPadding, m_chartPadding);
                secondLeftYAxis.setTickDensity (m_axisMinimumMajorTickSpacing);
                secondLeftYAxis.setWidth (m_axisWidth);

                // add the second left y axis title 
                CD.TextBox secondLeftYAxisTitle = secondLeftYAxis.setTitle (m_WrappedYAxisTitles[2]);
                secondLeftYAxisTitle.setAlignment (CD.Chart.TopLeft2);
                secondLeftYAxisTitle.setFontSize (m_axisFont.SizeInPoints);
                secondLeftYAxisTitle.setFontStyle (GetChartDirectorFontName (m_axisFont));
                secondLeftYAxisTitle.setMargin (2);

                // Store the series data columns that will be plotted on the second left y axis.
                foreach (IJcwChartDataGroup<double> dataGroup in m_chartDataGroups)
                {
                    IJcwChartVectorGroup<double> chartVectorGroup = dataGroup.SeriesData[2];
                    foreach (IVector<double> vector in chartVectorGroup.Vectors)
                    {
                        m_yAxes.Add (vector, secondLeftYAxis);
                    }
                }
            }

            // setup the second right y axis
            if (m_WrappedYAxisTitles.Count > 3)
            {
                // add the second right y-axis at 50 pixels to the left of the plot area
                yAxisColor = CD.Chart.CColor (SeriesColorSequence[3]);
                CD.Axis secondRightYAxis = m_chart.addAxis (CD.Chart.Right, m_axisSizes[AxisTypes.VerticalRight].Width);
                m_cdAxisToAxisTypeMap.Add (secondRightYAxis, AxisTypes.VerticalRight2);
                secondRightYAxis.setColors (yAxisColor, yAxisColor, yAxisColor);
                secondRightYAxis.setLabelFormat (m_axisLabelFormat);
                secondRightYAxis.setMargin (m_chartPadding, m_chartPadding);
                secondRightYAxis.setTickDensity (m_axisMinimumMajorTickSpacing);
                secondRightYAxis.setWidth (m_axisWidth);

                // add the second left y axis title 
                CD.TextBox secondRightAxisTitle = secondRightYAxis.setTitle (m_WrappedYAxisTitles[3]);
                secondRightAxisTitle.setAlignment (CD.Chart.TopRight2);
                secondRightAxisTitle.setFontSize (m_axisFont.SizeInPoints);
                secondRightAxisTitle.setFontStyle (GetChartDirectorFontName (m_axisFont));
                secondRightAxisTitle.setMargin (2);

                // Store the series data columns that will be plotted on the second left y axis.
                foreach (IJcwChartDataGroup<double> dataGroup in m_chartDataGroups)
                {
                    IJcwChartVectorGroup<double> chartVectorGroup = dataGroup.SeriesData[3];
                    foreach (IVector<double> vector in chartVectorGroup.Vectors)
                    {
                        m_yAxes.Add (vector, secondRightYAxis);
                    }
                }
            }
        }

        /// <summary>
        /// Currently we support only a single x axis and 4 y axes
        /// </summary>
        private void SetChartData ()
        {
            if (m_chartDataGroups == null || m_chartDataGroups.Count == 0)
            {
                return;
            }

            // layout chart again to create new xy chart
            SetChartLayout ();

            // Initialize the chart label and series data dictionaries.
            m_labelData = new Dictionary<IJcwChartVectorGroup<double>, IJcwChartVectorGroup<double>> ();
            m_seriesData = new Dictionary<IJcwChartVectorGroup<double>, IJcwChartVectorGroup<double>> ();

            foreach (IJcwChartDataGroup<double> chartDataGroup in m_chartDataGroups)
            {
                // Populate label and series dictionaries. Initially the key and value are the same vector group. If the 
                // chart is clipped or aggregated below, the value vector groups will be a subset of the original data.
                m_labelData[chartDataGroup.LabelData] = chartDataGroup.LabelData;
                foreach (IJcwChartVectorGroup<double> seriesVectorGroup in chartDataGroup.SeriesData)
                {
                    m_seriesData[seriesVectorGroup] = seriesVectorGroup;
                }
            }

            ClipData ();
            AggregateData ();

            ChartForm.RefreshChartStatisticsControl (m_seriesData.Keys, m_seriesData.Values);
            ChartForm.RefreshChartMetadataControl (ChartMetadata);

            int seriesColorCollectionIndex = 0;
            int seriesShapeCollectionIndex = 0;
            foreach (IJcwChartDataGroup<double> chartDataGroup in m_chartDataGroups)
            {
                foreach (IJcwChartVectorGroup<double> seriesVectorGroup in chartDataGroup.SeriesData)
                {
                    // Pull the working (clipped and/or aggregated) label and series vector groups from the label and series data dictionaries.
                    IJcwChartVectorGroup<double> workingLabelVectorGroup = m_labelData[chartDataGroup.LabelData];
                    IJcwChartVectorGroup<double> workingSeriesVectorGroup = m_seriesData[seriesVectorGroup];

                    // Only create line layers for series (y axis) data.
                    foreach (IVector<double> vector in workingSeriesVectorGroup.Vectors)
                    {
                        int seriesColor = CD.Chart.CColor (SeriesColorSequence[seriesColorCollectionIndex++]);
                        int seriesShape = SeriesShapeSequence[seriesShapeCollectionIndex++];
                        double[] labelArray = workingLabelVectorGroup.Vectors[0].Data.ToArray ();
                        double[] seriesArray = vector.Data.ToArray ();
                        CD.Layer layer = null;

                        if (LineChart)
                        {
                            // Add the line layer for the axis series data.
                            layer = m_chart.addLineLayer (seriesArray, seriesColor, vector.Name);
                            layer.setXData (labelArray);
                            layer.setLineWidth (1);
                        }
                        else
                        {
                            // Add the scatter layer for the axis series data.
                            layer = m_chart.addScatterLayer (labelArray, seriesArray, vector.Name, seriesShape, 2, seriesColor);
                        }

                        // find the chart directory axis object that corresponds to the data column series for this AxisType
                        if (m_yAxes.ContainsKey (vector))
                        {
                            layer.setUseYAxis (m_yAxes[vector]);
                        }
                    }
                }
            }

            // needs to be called before we add marks or text boxes in metadata methods below
            m_chart.layout ();

            ApplyChartMetadata ();
            AddChartMetadataNotes ();
            AddChartMetadataVerticalLines ();
            AddChartMetadataHorizontalLines ();
            AddChartMetadataVerticalZones ();
            AddChartMetadataCircleMarkers ();

            chartDirectorViewer.Chart = m_chart;

            if (ChartForm.DisplayHotSpots)
            {
                string imageMapText = m_chart.getHTMLImageMap ("clickable", "{default}", "title='" + m_xAxisTitles[0] + " = {x|2,.}, {dataSetName} = {value|2,.}'");
                imageMapText = AddMetadataToChartImageMap (imageMapText);
                chartDirectorViewer.ImageMap = imageMapText;
            }

            // TODO: figure out why when we first navigate to a chart, the size of the chart control is 1 pixel in height
            // larger than what it actually should be (this is why the border is cut off on the bottom of the chart.
            // If you maximize the chart and then resize it will be correctly sized.
            if (OnChartComplete != null)
            {
                OnChartComplete (this, null);
            }
        }

        private bool ClipData ()
        {
            bool clipped = false;

            foreach (IJcwChartDataGroup<double> chartDataGroup in m_chartDataGroups)
            {
                IVector<double> clippedLabelVector = new Vector<double> (chartDataGroup.LabelData.Vectors[0].Name);

                // Go through the label data vector and only include elements that are within the chart horizontal min and max.
                int endIndex = 0;
                int? startIndex = null;
                for (int index = 0 ; index < chartDataGroup.LabelData.Vectors[0].Data.Count ; index++)
                {
                    double labelVectorValueAtIndex = chartDataGroup.LabelData.Vectors[0].Data[index];

                    // If the horizontal value for this cell is within the chart horizontal min and max, this cell value
                    // should be included (not clipped) so add it to the clipped label data vector.
                    if (labelVectorValueAtIndex >= ChartMetadata.ChartHorizontalMinimum &&
                        labelVectorValueAtIndex <= ChartMetadata.ChartHorizontalMaximum)
                    {
                        if (startIndex == null)
                        {
                            startIndex = index;
                        }
                        endIndex = index;

                        clippedLabelVector.Data.Add (labelVectorValueAtIndex);
                    }
                }

                // If the clipped vector is shorter than the original label vector or if a prior label vector was clipped, 
                // set the clipped vector group as the label data value object.
                if (clipped || clippedLabelVector.Data.Count < chartDataGroup.LabelData.Vectors[0].Data.Count)
                {
                    clipped = true;
                    IJcwChartVectorGroup<double> clippedLabelVectorGroup = new JcwChartVectorGroup<double> (
                        chartDataGroup.LabelData.AxisTitle, chartDataGroup.LabelData.Caption);
                    clippedLabelVectorGroup.SetVectors (clippedLabelVector);
                    m_labelData[chartDataGroup.LabelData] = clippedLabelVectorGroup;

                    Debug.WriteLine (string.Format ("Label points pre {0}, post {1} clip", chartDataGroup.LabelData.Vectors[0].Data.Count,
                        m_labelData[chartDataGroup.LabelData].Vectors[0].Data.Count));
                }

                // We only need to clip the series data if at least one of the label vectors was clipped.
                if (clipped)
                {
                    foreach (IJcwChartVectorGroup<double> seriesVectorGroup in chartDataGroup.SeriesData)
                    {
                        IJcwChartVectorGroup<double> clippedSeriesVectorGroup = new JcwChartVectorGroup<double> (
                            seriesVectorGroup.AxisTitle, seriesVectorGroup.Caption);

                        foreach (IVector<double> seriesVector in seriesVectorGroup.Vectors)
                        {
                            // Create series vector copies for storing clipped series data.
                            IVector<double> clippedSeriesVector = new Vector<double> (seriesVector.Name);

                            // If you have multiple vectors, they may not be the same length.  In this case, the endIndex set 
                            // above will be the length of the longest vector.  For shorter vectors, this endIndex will be out 
                            // of range.  Modify the vector end index to be the last element in this vector instead.
                            int vectorEndIndex = endIndex;
                            if (vectorEndIndex >= seriesVector.Data.Count)
                            {
                                vectorEndIndex = seriesVector.Data.Count - 1;
                            }

                            // Clip the series vector data, create a new vector for this clipped data and store it in the clipped series collection.
                            List<double> clippedVectorData = seriesVector.Data.GetRange (startIndex.Value, (vectorEndIndex - startIndex.Value));
                            clippedSeriesVector.Data.AddRange (clippedVectorData);
                            clippedSeriesVectorGroup.Vectors.Add (clippedSeriesVector);

                            // Remove the original (not clipped) vector from the yAxes dictionary and add the new clipped vector instead.
                            CD.Axis vectorAxis = m_yAxes[seriesVector];
                            m_yAxes[clippedSeriesVector] = vectorAxis;
                            m_yAxes.Remove (seriesVector);
                        }

                        m_seriesData[seriesVectorGroup] = clippedSeriesVectorGroup;
                    }
                }
            }

            Debug.WriteLineIf (clipped, "Clipped label and series data");
            return clipped;
        }

        private bool AggregateData ()
        {
            bool aggregated = false;

            foreach (IJcwChartDataGroup<double> chartDataGroup in m_chartDataGroups)
            {
                // Pull the working (clipped) label vector group from the label data dictionary.
                IJcwChartVectorGroup<double> workingLabelVectorGroup = m_labelData[chartDataGroup.LabelData];
                if (workingLabelVectorGroup.Vectors[0].Data.Count > MaxPointsWithoutAggregate)
                {
                    aggregated = true;
                }
            }

            if (aggregated)
            {
                // Based on the algorithm below, divide the max points without aggregate constant by 3 to guarantee that we
                // aggregate when we hit this value.  Note that the maximum number of points that will be plotted will be for series
                // that have between 0 and the max number of points without aggregation (7500). For example:
                // 2000 points: floor(2000 / 2500) = 0 => no aggregate 2000 points
                // 3000 points: floor(3000 / 2500) = 1 => no aggregate 3000 points
                // 7000 points: floor(7000 / 2500) = 2 => no aggregate 7000 points
                // 7499 points: floor(7499 / 2500) = 2 => no aggregate 7499 points
                // 7500 points: floor(7500 / 2500) = 3 => aggregate to 2500 points
                // 8000 points: floor(8000 / 2500) = 3 => aggregate to ~2600 points
                // 9000 points: floor(9000 / 2500) = 3 => aggregate to 3000 points
                // 10000 points: floor(10000 / 2500) = 3 => aggregate to ~3300 points
                // 11000 points: floor(11000 / 2500) = 3 => aggregate to ~3600 points
                // 12000 points: floor(12000 / 2500) = 3 => aggregate to 4000 points
                // 13000 points: floor(13000 / 2500) = 5 => aggregate to 2600 points
                // 14999 points: floor(14999 / 2500) = 5 => aggregate to ~2900 points
                // 19999 points: floor(19999 / 2500) = 7 => aggregate to ~2800 points
                int workingMaxPointSegments = 3;
                int workingMaxPointsWithoutAggregate = MaxPointsWithoutAggregate / workingMaxPointSegments;

                foreach (IJcwChartDataGroup<double> chartDataGroup in m_chartDataGroups)
                {
                    // Pull the working (clipped) label vector group from the label data dictionary.
                    IJcwChartVectorGroup<double> workingLabelVectorGroup = m_labelData[chartDataGroup.LabelData];

                    // Get the decimal value for the ratio of the number of points in the vector to the maximum number of
                    // points allowed before we perform aggregation.
                    double pointRatioDecimalValue = Convert.ToDouble (workingLabelVectorGroup.Vectors[0].Data.Count) / workingMaxPointsWithoutAggregate;

                    // Find the largest integer less than the points ratio decimal value.  
                    // For example, if the prdv is 2.1 we will use 2, if the prdv is 2.9 we will use 2.
                    int majorTickStep = (int)Math.Floor (pointRatioDecimalValue);

                    // If the major tick step is less than or equal to 2, the operations below won't perform any aggregation so do nothing for this vector.
                    if (majorTickStep >= workingMaxPointSegments)
                    {
                        // The major tick step must be odd so that for evenly spaced x axis values the average returned by the aggregate
                        // function will select a value in the existing data set (it will average to the middle value in the group).
                        if (majorTickStep % 2 == 0)
                        {
                            majorTickStep--;
                        }

                        Debug.WriteLine ("Aggregate major tick step: " + majorTickStep);

                        double[] labelArray = workingLabelVectorGroup.Vectors[0].Data.ToArray ();

                        // Set up an aggregator to aggregate the data based on regular sized slots
                        CD.ArrayMath aM = new CD.ArrayMath (labelArray);

                        // Set the number of labels. 
                        aM.selectRegularSpacing (majorTickStep);

                        // For the x axis, take the average time on each slot.
                        double[] aggregatedLabelArray = aM.aggregate (labelArray, CD.Chart.AggregateAvg);

                        Debug.WriteLine (string.Format ("Label points before {0}, after {1} aggregate", labelArray.Length, aggregatedLabelArray.Length));

                        // Store the aggregated label data in the new label vector.
                        IVector<double> aggregatedLabelVector = new Vector<double> (workingLabelVectorGroup.Vectors[0].Name);
                        aggregatedLabelVector.Data.AddRange (aggregatedLabelArray);

                        IJcwChartVectorGroup<double> aggregatedLabelVectorGroup = new JcwChartVectorGroup<double> (
                            workingLabelVectorGroup.AxisTitle, workingLabelVectorGroup.Caption);
                        aggregatedLabelVectorGroup.SetVectors (aggregatedLabelVector);
                        m_labelData[chartDataGroup.LabelData] = aggregatedLabelVectorGroup;

                        foreach (IJcwChartVectorGroup<double> seriesVectorGroup in chartDataGroup.SeriesData)
                        {
                            // Pull the working (clipped) series vector group from the series data dictionary.
                            IJcwChartVectorGroup<double> workingSeriesVectorGroup = m_seriesData[seriesVectorGroup];

                            IJcwChartVectorGroup<double> aggregatedSeriesVectorGroup = new JcwChartVectorGroup<double> (
                                workingSeriesVectorGroup.AxisTitle, workingSeriesVectorGroup.Caption);

                            foreach (IVector<double> seriesVector in workingSeriesVectorGroup.Vectors)
                            {
                                double[] seriesArray = seriesVector.Data.ToArray ();

                                // Set up an aggregator to aggregate the data based on regular sized slots
                                CD.ArrayMath aM2 = new CD.ArrayMath (seriesArray);

                                // Set the number of labels.
                                aM2.selectRegularSpacing (majorTickStep);

                                // For the series data, take the average value on each slot.
                                double[] aggregatedSeriesArray = aM2.aggregate (seriesArray, CD.Chart.AggregateAvg);

                                // Now store the aggregated series back in our series collection.
                                IVector<double> aggregatedSeriesVector = new Vector<double> (seriesVector.Name);
                                aggregatedSeriesVector.Data.AddRange (aggregatedSeriesArray);

                                // Remove the original (not aggregated) vector from the yAxes dictionary and add the 
                                // new aggregated vector instead.
                                CD.Axis vectorAxis = m_yAxes[seriesVector];
                                m_yAxes[aggregatedSeriesVector] = vectorAxis;
                                m_yAxes.Remove (seriesVector);

                                aggregatedSeriesVectorGroup.AddVectors (aggregatedSeriesVector);
                                m_seriesData[seriesVectorGroup] = aggregatedSeriesVectorGroup;

                                Debug.WriteLine (string.Format ("Series points before {0}, after {1} aggregate", seriesArray.Length, aggregatedSeriesArray.Length));
                            }
                        }
                    }
                }
            }

            Debug.WriteLineIf (aggregated, "Aggregated label and series data");
            return aggregated;
        }

        #endregion

        #region Chart Metadata Methods

        private string AddMetadataToChartImageMap (string imageMapText)
        {
            if (ChartMetadata.AreNotesVisible)
            {
                foreach (Note n in ChartMetadata.ChartNotes)
                {
                    StringBuilder noteText = new StringBuilder ();
                    for (int i = 0 ; i < n.NoteText.Length ; i++)
                    {
                        if (i != 0 && i % 80 == 0)
                        {
                            noteText.AppendLine ();
                        }
                        noteText.Append (n.NoteText[i]);
                    }

                    // only add the note if the x and y axis titles in the note match the chart
                    if (n.XCoordinate.AxisTitle == m_xAxisTitles[0] && n.YCoordinate.AxisTitle == m_yAxisTitles[0])
                    {
                        // each data point entry in html looks like this
                        // <area shape=\"rect\" coords=\"312,218,322,228\" title='Time (seconds) = 5.22, Accel2Z = 0.85' 
                        // href=\"clickable?x=5.22&xLabel=&dataSet=1&dataSetName=Accel2Z&value=0.854519\" />

                        // replace the title='Time... with title='Note\nTime...
                        string re = @"(title=')(.*?clickable\?x=" + n.XCoordinate.Coordinate + @".*?\&value=" + n.YCoordinate.Coordinate + ")";
                        imageMapText = Regex.Replace (imageMapText, re, "$1" + noteText.ToString () + "\n\n$2");
                    }
                }

                foreach (CircleMarker m in ChartMetadata.ChartCircleMarkers)
                {
                    StringBuilder markerText = new StringBuilder ();
                    for (int i = 0 ; i < m.Text.Length ; i++)
                    {
                        if (i != 0 && i % 80 == 0)
                        {
                            markerText.AppendLine ();
                        }
                        markerText.Append (m.Text[i]);
                    }

                    // only add the circle marker if the x and y axis titles in the marker match the chart
                    if (m.XCoordinate.AxisTitle == m_xAxisTitles[0] && m.YCoordinate.AxisTitle == m_yAxisTitles[0])
                    {
                        // each data point entry in html looks like this
                        // <area shape=\"rect\" coords=\"312,218,322,228\" title='Time (seconds) = 5.22, Accel2Z = 0.85' 
                        // href=\"clickable?x=5.22&xLabel=&dataSet=1&dataSetName=Accel2Z&value=0.854519\" />

                        // replace the title='Time... with title='Note\nTime...
                        string re = @"(title=')(.*?clickable\?x=" + m.XCoordinate.Coordinate + @".*?\&value=" + m.YCoordinate.Coordinate + ")";
                        imageMapText = Regex.Replace (imageMapText, re, "$1" + markerText.ToString () + "\n\n$2");
                    }
                }
            }

            return imageMapText;
        }

        private void AddChartMetadataNotes ()
        {
            // Only add metadata if metadata is saveable - otherwise it would be displayed on the 
            // chart but the user would have no way to remove it or to make in hidden.
            if (ChartForm.CanSaveChartMetadata && ChartMetadata.AreNotesVisible)
            {
                foreach (Note n in ChartMetadata.ChartNotes)
                {
                    // convert the chart coordinate to a screen coordinate and shift coordinates slightly 
                    // because this x,y will be the top left corner of the text box added below
                    int x = m_chart.getXCoor (n.XCoordinate.Coordinate);
                    int y = m_chart.getYCoor (n.YCoordinate.Coordinate);

                    // only add the note if the x and y axis titles in the note match the chart
                    if (n.XCoordinate.AxisTitle == m_xAxisTitles[0] && n.YCoordinate.AxisTitle == m_yAxisTitles[0])
                    {
                        // figure out a general size for the note text box
                        SizeF noteSize;
                        using (Graphics g = Graphics.FromHwnd (Handle))
                        {
                            SizeF layoutArea = new SizeF (150, 200);
                            noteSize = g.MeasureString (n.NoteText, m_noteFont, layoutArea);
                        }

                        // adds an invisible text box to the chart with the contents being the chart note image
                        // add chart note image using CDML - chart director markup language
                        CD.TextBox tb = m_chart.addText (x - 5, y - 5, "<*img=ChartNote.gif,height=30,width=12*>" + n.NoteText);
                        tb.setAlignment (CD.Chart.TopLeft);
                        tb.setFontColor (CD.Chart.CColor (Color.Black));
                        tb.setFontSize (m_noteFont.SizeInPoints);
                        tb.setFontStyle (GetChartDirectorFontName (m_noteFont));
                        tb.setWidth ((int)noteSize.Width);
                        tb.setZOrder (CD.Chart.GridLinesZ);
                    }
                }
            }
        }

        private void AddChartMetadataVerticalLines ()
        {
            // Only add metadata if metadata is saveable - otherwise it would be displayed on the 
            // chart but the user would have no way to remove it or to make in hidden.
            if (ChartForm.CanSaveChartMetadata && ChartMetadata.AreVerticalLinesVisible)
            {
                foreach (VerticalLine vl in ChartMetadata.ChartVerticalLines)
                {
                    // If the vertical line has text like e# it is an event line.
                    bool isEventLine = !string.IsNullOrEmpty (vl.Text) && Regex.IsMatch (vl.Text, @"^e\d+$");

                    // If the vertical line is an event line and we are including event lines or if the vertical line axis title matches the chart y axis title, draw the line.
                    if (isEventLine || vl.Coordinate.AxisTitle == m_yAxisTitles[0])
                    {
                        // the mark location is the enumerated x value
                        CD.Mark mark = m_chart.xAxis ().addMark (vl.Coordinate.Coordinate, CD.Chart.CColor (vl.MarkColor));

                        mark.setDrawOnTop (false);
                        mark.setLineWidth (vl.LineWidth);

                        // set text if it is specified in metadata
                        if (!string.IsNullOrEmpty (vl.Text))
                        {
                            mark.setText (vl.Text);
                            mark.setAlignment (CD.Chart.BottomRight);
                            mark.setFontSize (m_axisFont.SizeInPoints);
                            mark.setFontStyle (GetChartDirectorFontName (m_axisFont));
                        }
                    }
                }
            }
        }

        private void AddChartMetadataHorizontalLines ()
        {
            // Only add metadata if metadata is saveable - otherwise it would be displayed on the 
            // chart but the user would have no way to remove it or to make in hidden.
            if (ChartForm.CanSaveChartMetadata && ChartMetadata.AreHorizontalLinesVisible)
            {
                foreach (HorizontalLine hl in ChartMetadata.ChartHorizontalLines)
                {
                    // only add the horizontal line if the y axis title in the line matches the chart
                    if (hl.Coordinate.AxisTitle == m_yAxisTitles[0])
                    {
                        // the mark location is the enumerated y value
                        CD.Mark mark = m_chart.yAxis ().addMark (hl.Coordinate.Coordinate, CD.Chart.CColor (hl.MarkColor));

                        mark.setDrawOnTop (false);
                        mark.setLineWidth (hl.LineWidth);

                        // set text if it is specified in metadata
                        if (!string.IsNullOrEmpty (hl.Text))
                        {
                            mark.setText (hl.Text);
                            mark.setAlignment (CD.Chart.BottomRight);
                            mark.setFontSize (m_axisFont.SizeInPoints);
                            mark.setFontStyle (GetChartDirectorFontName (m_axisFont));
                        }
                    }
                }
            }
        }

        private void AddChartMetadataVerticalZones ()
        {
            // Only add metadata if metadata is saveable - otherwise it would be displayed on the 
            // chart but the user would have no way to remove it or to make in hidden.
            if (ChartForm.CanSaveChartMetadata && ChartMetadata.AreVerticalZonesVisible)
            {
                foreach (VerticalZone vz in ChartMetadata.ChartVerticalZones)
                {
                    // Only add the vertical zone if the x axis title in the zone matches the chart
                    if (vz.StartCoordinate.AxisTitle == m_xAxisTitles[0] && vz.EndCoordinate.AxisTitle == m_xAxisTitles[0])
                    {
                        // If an opposite axis title is defined in the zone, only add the zone if the opposite axis title 
                        // in the zone matches the chart y axis title.
                        if (string.IsNullOrEmpty (vz.OppositeAxisTitle) || vz.OppositeAxisTitle == m_yAxisTitles[0])
                        {
                            m_chart.xAxis ().addZone (vz.StartCoordinate.Coordinate, vz.EndCoordinate.Coordinate, CD.Chart.CColor (vz.ZoneColor));
                        }
                    }
                }
            }
        }

        private void AddChartMetadataCircleMarkers ()
        {
            // Only add metadata if metadata is saveable - otherwise it would be displayed on the 
            // chart but the user would have no way to remove it or to make in hidden.
            if (ChartForm.CanSaveChartMetadata && ChartMetadata.AreCircleMarkersVisible)
            {
                foreach (CircleMarker m in ChartMetadata.ChartCircleMarkers)
                {
                    // convert the chart coordinate to a screen coordinate and shift coordinates slightly 
                    // because this x,y will be the top left corner of the text box added below
                    int x = m_chart.getXCoor (m.XCoordinate.Coordinate);
                    int y = m_chart.getYCoor (m.YCoordinate.Coordinate);

                    // only add the circle marker if the x and y axis titles in the note match the chart
                    if (m.XCoordinate.AxisTitle == m_xAxisTitles[0] && m.YCoordinate.AxisTitle == m_yAxisTitles[0])
                    {
                        // Include a space at the front of the mark text so that the text will not be
                        // directly up against the circle marker image.
                        string fullMarkText = " " + m.Text;

                        // figure out a general size for the circle marker text box
                        SizeF circleMarkerSize;
                        using (Graphics g = Graphics.FromHwnd (Handle))
                        {
                            SizeF layoutArea = new SizeF (250, 100);
                            circleMarkerSize = g.MeasureString (fullMarkText, m_noteFont, layoutArea);
                        }

                        // Add a bit of padding to make sure the text box size will be adequate
                        // for the text we are displaying.
                        circleMarkerSize.Width *= 1.1f;
                        circleMarkerSize.Height *= 1.1f;

                        // adds an invisible text box to the chart with the contents being the chart note image
                        // add chart note image using CDML - chart director markup language
                        int width = 10, height = 10;
                        string markerText = string.Format ("<*img=CircleMarker.gif,height={0},width={1}*>{2}", height, width, fullMarkText);
                        CD.TextBox tb = m_chart.addText (x - 7, y - 7, markerText);
                        tb.setAlignment (CD.Chart.TopLeft);
                        tb.setFontColor (CD.Chart.CColor (Color.Black));
                        tb.setFontSize (m_noteFont.SizeInPoints);
                        tb.setFontStyle (GetChartDirectorFontName (m_noteFont));
                        tb.setWidth ((int)circleMarkerSize.Width);
                        tb.setZOrder (CD.Chart.GridLinesZ);
                    }
                }
            }
        }

        #endregion

        #region IJcwChartMetadataSave Implementation

        #region Properties

        public IJcwChartFrm ChartForm { get; private set; }

        #endregion

        #region Methods

        public void SaveRequired (bool saveRequired, ChartMetadata metadata)
        {
            ChartMetadata = metadata;

            // Update the view port so that the chart is redrawn to include added or removed user data items.
            if (saveRequired)
            {
                chartDirectorViewer.updateViewPort (true, true);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private void InitializeChart ()
        {
            ZoomInButton.Checked = ChartMetadata.IsChartZoomInSelected = false;
            ZoomOutButton.Checked = ChartMetadata.IsChartZoomOutSelected = false;

            chartDirectorViewer.MouseUsage = CD.WinChartMouseUsage.ScrollOnDrag;

            InitializeViewPort ();
            ChartForm.RevertChartMetadataSaveRequired ();
        }

        /// <summary>
        /// Method to apply metadata values to chart context menu state.
        /// </summary>
        private void ApplyChartMetadata ()
        {
            if (ChartMetadata != null)
            {
                // Unsubscribe to checked changed events.
                m_circleMarkersVisibleCheck.CheckedChanged -= CircleMarkersVisible_CheckedChanged;
                m_horizontalLinesVisibleCheck.CheckedChanged -= HorizontalMarkersVisible_CheckedChanged;
                m_notesVisibleCheck.CheckedChanged -= NotesVisible_CheckedChanged;
                m_verticalLinesVisibleCheck.CheckedChanged -= VerticalMarkersVisible_CheckedChanged;

                // Toggle checkboxes based on metadata.
                m_circleMarkersVisibleCheck.Checked = ChartMetadata.AreCircleMarkersVisible;
                m_horizontalLinesVisibleCheck.Checked = ChartMetadata.AreHorizontalLinesVisible;
                m_notesVisibleCheck.Checked = ChartMetadata.AreNotesVisible;
                m_verticalLinesVisibleCheck.Checked = ChartMetadata.AreVerticalLinesVisible;

                // Resubscribe to checked changed events.
                m_circleMarkersVisibleCheck.CheckedChanged += CircleMarkersVisible_CheckedChanged;
                m_horizontalLinesVisibleCheck.CheckedChanged += HorizontalMarkersVisible_CheckedChanged;
                m_notesVisibleCheck.CheckedChanged += NotesVisible_CheckedChanged;
                m_verticalLinesVisibleCheck.CheckedChanged += VerticalMarkersVisible_CheckedChanged;
            }
        }

        private void CreateContextMenus ()
        {
            m_addNoteImage = JcwResources.GetObject ("AddNote") as Bitmap;
            JcwToolStripMenuItem addNoteMenu = new JcwToolStripMenuItem (AddChartNote_Click)
            {
                Text = JcwResources.GetString ("AddNoteToChartMenuText"),
                Image = m_addNoteImage,
                ShortcutKeys = Keys.Control | Keys.N
            };

            m_addVerticalLineImage = JcwResources.GetObject ("AddVerticalLine") as Bitmap;
            JcwToolStripMenuItem addVerticalLineMenu = new JcwToolStripMenuItem (AddVerticalMarker_Click)
            {
                Text = JcwResources.GetString ("AddVerticalMarkerToChartMenuText"),
                Image = m_addVerticalLineImage,
                ShortcutKeys = Keys.Control | Keys.V
            };

            m_addHorizontalLineImage = JcwResources.GetObject ("AddHorizontalLine") as Bitmap;
            JcwToolStripMenuItem addHorizontalLineMenu = new JcwToolStripMenuItem (AddHorizontalMarker_Click)
            {
                Text = JcwResources.GetString ("AddHorizontalMarkerToChartMenuText"),
                Image = m_addHorizontalLineImage,
                ShortcutKeys = Keys.Control | Keys.H
            };

            m_notesVisibleCheck = new JcwCheckBox ();
            m_notesVisibleCheck.Checked = ChartMetadata.AreNotesVisible;
            m_notesVisibleCheck.Text = JcwResources.GetString ("NotesVisibleMenuText");
            m_notesVisibleCheck.Width = 200;
            ToolStripControlHost notesVisibleCheckMenu = new ToolStripControlHost (m_notesVisibleCheck);

            m_verticalLinesVisibleCheck = new JcwCheckBox ();
            m_verticalLinesVisibleCheck.Checked = ChartMetadata.AreVerticalLinesVisible;
            m_verticalLinesVisibleCheck.Text = JcwResources.GetString ("VerticalMarkersVisbleMenuText");
            m_verticalLinesVisibleCheck.Width = 200;
            ToolStripControlHost verticalLinesVisibleCheckMenu = new ToolStripControlHost (m_verticalLinesVisibleCheck);

            m_horizontalLinesVisibleCheck = new JcwCheckBox ();
            m_horizontalLinesVisibleCheck.Checked = ChartMetadata.AreHorizontalLinesVisible;
            m_horizontalLinesVisibleCheck.Text = JcwResources.GetString ("HorizontalMarkersVisibleMenuText");
            m_horizontalLinesVisibleCheck.Width = 200;
            ToolStripControlHost horizontalLinesVisibleCheckMenu = new ToolStripControlHost (m_horizontalLinesVisibleCheck);

            m_circleMarkersVisibleCheck = new JcwCheckBox ();
            m_circleMarkersVisibleCheck.Checked = ChartMetadata.AreCircleMarkersVisible;
            m_circleMarkersVisibleCheck.Text = JcwResources.GetString ("CircleMarkersVisibleMenuText");
            m_circleMarkersVisibleCheck.Width = 200;
            ToolStripControlHost circleMarkersVisibleCheckMenu = new ToolStripControlHost (m_circleMarkersVisibleCheck);

            JcwToolStripMenuItem saveChartAsImage = new JcwToolStripMenuItem (SaveChartAsImage_Click)
            {
                Text = JcwResources.GetString ("SaveChartAsImageMenuText"),
                ShortcutKeys = Keys.Control | Keys.S
            };

            chartContextMenu.Items.Add (addNoteMenu);
            chartContextMenu.Items.Add (addVerticalLineMenu);
            chartContextMenu.Items.Add (addHorizontalLineMenu);
            chartContextMenu.Items.Add ("-");
            chartContextMenu.Items.Add (notesVisibleCheckMenu);
            chartContextMenu.Items.Add (verticalLinesVisibleCheckMenu);
            chartContextMenu.Items.Add (horizontalLinesVisibleCheckMenu);
            chartContextMenu.Items.Add (circleMarkersVisibleCheckMenu);
            chartContextMenu.Items.Add ("-");
            chartContextMenu.Items.Add (saveChartAsImage);

            if (Customizer != null)
            {
                List<IJcwChartCustomContextMenuProperties> customContextMenuProperties = Customizer.GetCustomContextMenuProperties ();
                if (customContextMenuProperties != null)
                {
                    foreach (IJcwChartCustomContextMenuProperties menuProperties in customContextMenuProperties)
                    {
                        chartContextMenu.Items.Add ("-");
                        // Add code here for custom context menus
                    }
                }
            }
        }

        private void InitializeViewPort ()
        {
            chartDirectorViewer.ZoomInWidthLimit = .01;

            chartDirectorViewer.ViewPortLeft = 0;
            chartDirectorViewer.ViewPortWidth = 1;

            chartDirectorViewer.ViewPortTop = 0;
            chartDirectorViewer.ViewPortHeight = 1;

            chartDirectorViewer.MouseUsage = CD.WinChartMouseUsage.ScrollOnDrag;
        }

        #endregion

        #region IMessageFilter Implementation

        /// <summary>
        /// This method is needed because the WinChartViewer control derives from the windows picture box control.
        /// The windows picture box control does not support setting focus to the control so without this method 
        /// the dock panels that are open and have been interacted with will not shut when you click on the chart
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage (ref Message m)
        {
            // if the left or right mouse buttons were clicked within the bounds of the chart control
            // make call to docking control to hide all of its unpinned panels
            if (!IsDisposed && Bounds.Contains (PointToClient (Control.MousePosition)))
            {
                if (m.Msg == (int)NativeMethods.WindowMessages.WM_LBUTTONDOWN || m.Msg == (int)NativeMethods.WindowMessages.WM_RBUTTONDOWN)
                {
                    if (ChartForm.DockManager != null && !ChartForm.DockManager.IsDisposed)
                    {
                        ChartForm.DockManager.HideAllUnpinnedPanels ();
                    }
                }
            }

            return false;
        }

        #endregion
    }
}