using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public partial class JcwToolStripMenuItem : ToolStripMenuItem
    {
        #region Constructors

        public JcwToolStripMenuItem ()
            : this (null)
        {
        }

        public JcwToolStripMenuItem (EventHandler handler)
            : base (null, null, handler)
        {
            // 
            // JcwToolStripMenuItem
            // 
            this.Font = Jcw.Common.JcwStyle.JcwStyleFont;
            this.Height = 10;
            this.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.SizeToFit;
        }

        #endregion
    }
}
