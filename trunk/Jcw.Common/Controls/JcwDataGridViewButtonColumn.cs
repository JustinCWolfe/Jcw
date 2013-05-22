using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jcw.Common.Controls
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
            InitializeComponent ();
        }

        #endregion
    }
}
