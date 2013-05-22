namespace Jcw.Charting.Gui.WinForms.Controls
{
    partial class JcwChartStatisticsCtl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle ();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle ();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle ();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel ();
            this.MinLabel = new Jcw.Common.Gui.WinForms.Controls.JcwLabel ();
            this.MinDataGridView = new Jcw.Charting.Gui.WinForms.Controls.JcwAggregateIncludeDataGridView ();
            this.MinSeriesNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn ();
            this.MinSeriesValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn ();
            this.MaxLabel = new Jcw.Common.Gui.WinForms.Controls.JcwLabel ();
            this.MaxDataGridView = new Jcw.Charting.Gui.WinForms.Controls.JcwAggregateIncludeDataGridView ();
            this.MaxSeriesNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn ();
            this.MaxSeriesValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn ();
            this.AveLabel = new Jcw.Common.Gui.WinForms.Controls.JcwLabel ();
            this.AveDataGridView = new Jcw.Charting.Gui.WinForms.Controls.JcwAggregateIncludeDataGridView ();
            this.AveSeriesNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn ();
            this.AveSeriesValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn ();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit ();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit ();
            this.tableLayoutPanel1.SuspendLayout ();
            ((System.ComponentModel.ISupportInitialize)(this.MinDataGridView)).BeginInit ();
            ((System.ComponentModel.ISupportInitialize)(this.MaxDataGridView)).BeginInit ();
            ((System.ComponentModel.ISupportInitialize)(this.AveDataGridView)).BeginInit ();
            this.SuspendLayout ();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add (new System.Windows.Forms.ColumnStyle (System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add (new System.Windows.Forms.ColumnStyle (System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add (new System.Windows.Forms.ColumnStyle (System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.Controls.Add (this.MinLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add (this.MinDataGridView, 1, 2);
            this.tableLayoutPanel1.Controls.Add (this.MaxLabel, 1, 4);
            this.tableLayoutPanel1.Controls.Add (this.MaxDataGridView, 1, 5);
            this.tableLayoutPanel1.Controls.Add (this.AveLabel, 1, 7);
            this.tableLayoutPanel1.Controls.Add (this.AveDataGridView, 1, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point (0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size (420, 545);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // MinLabel
            // 
            this.MinLabel.AutoSize = true;
            this.MinLabel.BackColor = System.Drawing.Color.Transparent;
            this.MinLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MinLabel.Location = new System.Drawing.Point (15, 12);
            this.MinLabel.Name = "MinLabel";
            this.MinLabel.Size = new System.Drawing.Size (390, 18);
            this.MinLabel.TabIndex = 1;
            this.MinLabel.Text = "Minimum(s)";
            // 
            // MinDataGridView
            // 
            this.MinDataGridView.Columns.AddRange (new System.Windows.Forms.DataGridViewColumn[]  { 
            this.MinSeriesNameColumn, 
            this.MinSeriesValueColumn});
            this.MinDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MinDataGridView.Location = new System.Drawing.Point (15, 33);
            this.MinDataGridView.Name = "MinDataGridView";
            this.MinDataGridView.Size = new System.Drawing.Size (390, 141);
            this.MinDataGridView.TabIndex = 4;
            // 
            // MinSeriesNameColumn
            // 
            this.MinSeriesNameColumn.HeaderText = "Series Name";
            this.MinSeriesNameColumn.Name = "MinSeriesNameColumn";
            this.MinSeriesNameColumn.ReadOnly = true;
            this.MinSeriesNameColumn.Width = 190;
            // 
            // MinSeriesValueColumn
            // 
            dataGridViewCellStyle2.Format = "N4";
            dataGridViewCellStyle2.NullValue = null;
            this.MinSeriesValueColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.MinSeriesValueColumn.HeaderText = "Value";
            this.MinSeriesValueColumn.Name = "MinSeriesValueColumn";
            this.MinSeriesValueColumn.ReadOnly = true;
            // 
            // MaxLabel
            // 
            this.MaxLabel.AutoSize = true;
            this.MaxLabel.BackColor = System.Drawing.Color.Transparent;
            this.MaxLabel.Location = new System.Drawing.Point (15, 189);
            this.MaxLabel.Name = "MaxLabel";
            this.MaxLabel.Size = new System.Drawing.Size (64, 13);
            this.MaxLabel.TabIndex = 2;
            this.MaxLabel.Text = "Maximum(s)";
            // 
            // MaxDataGridView
            // 
            this.MaxDataGridView.Columns.AddRange (new System.Windows.Forms.DataGridViewColumn[]  { 
            this.MaxSeriesNameColumn, 
            this.MaxSeriesValueColumn});
            this.MaxDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MaxDataGridView.Location = new System.Drawing.Point (15, 210);
            this.MaxDataGridView.Name = "MaxDataGridView";
            this.MaxDataGridView.Size = new System.Drawing.Size (390, 141);
            this.MaxDataGridView.TabIndex = 5;
            // 
            // MaxSeriesNameColumn
            // 
            this.MaxSeriesNameColumn.HeaderText = "Series Name";
            this.MaxSeriesNameColumn.Name = "MaxSeriesNameColumn";
            this.MaxSeriesNameColumn.ReadOnly = true;
            this.MaxSeriesNameColumn.Width = 190;
            // 
            // MaxSeriesValueColumn
            // 
            dataGridViewCellStyle4.Format = "N4";
            dataGridViewCellStyle4.NullValue = null;
            this.MaxSeriesValueColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.MaxSeriesValueColumn.HeaderText = "Value";
            this.MaxSeriesValueColumn.Name = "MaxSeriesValueColumn";
            this.MaxSeriesValueColumn.ReadOnly = true;
            // 
            // AveLabel
            // 
            this.AveLabel.AutoSize = true;
            this.AveLabel.BackColor = System.Drawing.Color.Transparent;
            this.AveLabel.Location = new System.Drawing.Point (15, 366);
            this.AveLabel.Name = "AveLabel";
            this.AveLabel.Size = new System.Drawing.Size (61, 13);
            this.AveLabel.TabIndex = 3;
            this.AveLabel.Text = "Average(s)";
            // 
            // AveDataGridView
            // 
            this.AveDataGridView.Columns.AddRange (new System.Windows.Forms.DataGridViewColumn[]  { 
            this.AveSeriesNameColumn, 
            this.AveSeriesValueColumn});
            this.AveDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AveDataGridView.Location = new System.Drawing.Point (15, 387);
            this.AveDataGridView.Name = "AveDataGridView";
            this.AveDataGridView.Size = new System.Drawing.Size (390, 141);
            this.AveDataGridView.TabIndex = 6;
            // 
            // AveSeriesNameColumn
            // 
            this.AveSeriesNameColumn.HeaderText = "Series Name";
            this.AveSeriesNameColumn.Name = "AveSeriesNameColumn";
            this.AveSeriesNameColumn.ReadOnly = true;
            this.AveSeriesNameColumn.Width = 190;
            // 
            // AveSeriesValueColumn
            // 
            dataGridViewCellStyle6.Format = "N4";
            dataGridViewCellStyle6.NullValue = null;
            this.AveSeriesValueColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.AveSeriesValueColumn.HeaderText = "Value";
            this.AveSeriesValueColumn.Name = "AveSeriesValueColumn";
            this.AveSeriesValueColumn.ReadOnly = true;
            // 
            // JcwChartStatisticsCtl
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add (this.tableLayoutPanel1);
            this.Name = "JcwChartStatisticsCtl";
            this.Size = new System.Drawing.Size (420, 545);
            this.Load += new System.EventHandler (this.JcwChartStatisticsCtl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit ();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit ();
            this.tableLayoutPanel1.ResumeLayout (false);
            this.tableLayoutPanel1.PerformLayout ();
            ((System.ComponentModel.ISupportInitialize)(this.MinDataGridView)).EndInit ();
            ((System.ComponentModel.ISupportInitialize)(this.MaxDataGridView)).EndInit ();
            ((System.ComponentModel.ISupportInitialize)(this.AveDataGridView)).EndInit ();
            this.ResumeLayout (false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Jcw.Common.Gui.WinForms.Controls.JcwLabel MinLabel;
        private Jcw.Common.Gui.WinForms.Controls.JcwLabel MaxLabel;
        private Jcw.Common.Gui.WinForms.Controls.JcwLabel AveLabel;
        private Jcw.Charting.Gui.WinForms.Controls.JcwAggregateIncludeDataGridView MinDataGridView;
        private Jcw.Charting.Gui.WinForms.Controls.JcwAggregateIncludeDataGridView MaxDataGridView;
        private Jcw.Charting.Gui.WinForms.Controls.JcwAggregateIncludeDataGridView AveDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinSeriesNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinSeriesValueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxSeriesNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxSeriesValueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AveSeriesNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AveSeriesValueColumn;
    }
}
