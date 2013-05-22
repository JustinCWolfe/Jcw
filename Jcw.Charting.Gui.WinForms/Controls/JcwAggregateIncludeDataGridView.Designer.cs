namespace Jcw.Charting.Gui.WinForms.Controls
{
    partial class JcwAggregateIncludeDataGridView
    {
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle ();
            this.IncludeColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn ();
            ((System.ComponentModel.ISupportInitialize)this).BeginInit ();
            this.SuspendLayout ();
            // 
            // IncludeColumn
            // 
            this.IncludeColumn.HeaderText = "Include";
            this.IncludeColumn.Name = "IncludeColumn";
            this.IncludeColumn.ToolTipText = "Include in Aggregate Computation";
            this.IncludeColumn.Width = 50;
            // 
            // JcwAggregateIncludeDataGridView
            // 
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb (((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Location = new System.Drawing.Point (15, 33);
            this.MultiSelect = false;
            this.Name = "JcwAggregateIncludeDataGridView";
            this.RowHeadersVisible = false;
            this.Size = new System.Drawing.Size (390, 141);
            this.TabIndex = 4;
            this.CurrentCellDirtyStateChanged += new System.EventHandler (this.GridView_CurrentCellDirtyStateChanged);
            this.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler (this.GridView_CellContentClick);
            this.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler (this.GridView_CellContentDoubleClick);
            ((System.ComponentModel.ISupportInitialize)this).EndInit ();
            this.ResumeLayout (false);
        }

        #endregion

        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeColumn;
    }
}
