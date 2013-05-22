using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraTab;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public partial class JcwTabPage : XtraTabPage
    {
        public JcwTabPage ()
            : base ()
        {
            this.BackColor = Jcw.Common.JcwStyle.JcwStyleControlBackColor;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Font = Jcw.Common.JcwStyle.JcwStyleFont;
            this.Padding = new System.Windows.Forms.Padding (3);
        }
    }
}
