using CD = ChartDirector;

namespace Jcw.Charting.Gui.WinForms.Controls
{
    partial class JcwChartCtl
    {
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.chartMenuStrip = new Jcw.Common.Gui.WinForms.Controls.JcwMenuStrip ();
            this.chartDirectorViewer = new CD.WinChartViewer ();
            this.chartContextMenu = new Jcw.Common.Gui.WinForms.Controls.JcwContextMenuStrip ();
            this.chartDirectorViewer.SuspendLayout ();
            this.chartContextMenu.SuspendLayout ();
            this.chartMenuStrip.SuspendLayout ();
            this.SuspendLayout ();
            // 
            // chartDirectorViewer
            // 
            this.chartDirectorViewer.AutoPopDelay = 30000;
            this.chartDirectorViewer.ChartSizeMode = CD.WinChartSizeMode.AutoSize;
            this.chartDirectorViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDirectorViewer.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartDirectorViewer.InitialDelay = 50;
            this.chartDirectorViewer.Location = new System.Drawing.Point (0, 0);
            this.chartDirectorViewer.MouseUsage = CD.WinChartMouseUsage.ScrollOnDrag;
            this.chartDirectorViewer.Name = "chartDirectorViewer";
            this.chartDirectorViewer.ReshowDelay = 25;
            this.chartDirectorViewer.ScrollDirection = CD.WinChartDirection.HorizontalVertical;
            this.chartDirectorViewer.Size = new System.Drawing.Size (20, 20);
            this.chartDirectorViewer.TabIndex = 4;
            this.chartDirectorViewer.TabStop = true;
            this.chartDirectorViewer.ZoomDirection = CD.WinChartDirection.HorizontalVertical;
            //
            // chartContextMenu
            // 
            this.chartContextMenu.Font = new System.Drawing.Font ("Arial", 8F);
            this.chartContextMenu.ImageScalingSize = new System.Drawing.Size (20, 20);
            this.chartContextMenu.Name = "chartContextMenu";
            this.chartContextMenu.ShowImageMargin = false;
            this.chartContextMenu.Size = new System.Drawing.Size (36, 4);
            // 
            // chartMenuStrip
            // 
            this.chartMenuStrip.Location = new System.Drawing.Point (0, 0);
            this.chartMenuStrip.Name = "chartMenuStrip";
            this.Dock = System.Windows.Forms.DockStyle.Top;
            //this.chartMenuStrip.Size = new System.Drawing.Size (292, 24);
            this.chartMenuStrip.TabIndex = 0;
            this.chartMenuStrip.Text = "Chart Menu Strip";
            // 
            // JcwChartDirectorChartCtl2
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add (this.chartDirectorViewer);
            this.Controls.Add (this.chartMenuStrip);
            this.Name = "JcwChartCtl";
            this.Load += new System.EventHandler (ChartCtl_Load);
            this.chartDirectorViewer.ResumeLayout (false);
            this.chartContextMenu.ResumeLayout (false);
            this.chartMenuStrip.ResumeLayout (false);
            this.ResumeLayout (false);
        }

        #endregion

        private CD.WinChartViewer chartDirectorViewer;
        private Jcw.Common.Gui.WinForms.Controls.JcwContextMenuStrip chartContextMenu;
        private Jcw.Common.Gui.WinForms.Controls.JcwMenuStrip chartMenuStrip;
    }
}
