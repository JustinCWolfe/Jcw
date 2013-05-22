using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public partial class JcwDataGridView : DataGridView
    {
        public JcwDataGridView ()
            : base ()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle ();
            // 
            // JcwDataGridView
            // 
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb (((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Name = "JcwDataGridView";
            this.RowHeadersVisible = false;
        }
    }
}
