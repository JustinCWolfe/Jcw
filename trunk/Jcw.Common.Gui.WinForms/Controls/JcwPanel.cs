using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public partial class JcwPanel : PanelControl
    {
        public JcwPanel ()
            : base ()
        {
            // 
            // JcwPanel
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.ForeColor = System.Drawing.Color.FromArgb (((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseForeColor = true;
            this.Font = Jcw.Common.JcwStyle.JcwStyleFont;
        }
    }
}
