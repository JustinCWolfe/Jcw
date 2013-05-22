using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Jcw.Common;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public partial class JcwDataGridViewButtonColumn : DataGridViewButtonColumn
    {
        #region Fields

        private Color m_defaultBackColor = JcwStyle.JcwStyleControlBackColor;
        private Color m_defaultCheckedBackColor = Color.LightGray;

        #endregion

        #region Constructors

        public JcwDataGridViewButtonColumn ()
            : base ()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle ();
            // 
            // JcwDataGridViewButtonColumn
            // 
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            dataGridViewCellStyle9.BackColor = m_defaultBackColor;
            dataGridViewCellStyle9.Font = Jcw.Common.JcwStyle.JcwStyleFont;
            dataGridViewCellStyle9.SelectionBackColor = m_defaultCheckedBackColor;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DefaultCellStyle = dataGridViewCellStyle9;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UseColumnTextForButtonValue = true;
        }

        #endregion
    }
}
