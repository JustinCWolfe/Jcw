using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraBars;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public partial class JcwMenuStrip : MenuStrip
    {
        public JcwMenuStrip ()
            : base ()
        {
            // 
            // JcwMenuStrip
            // 
            this.Renderer = new Jcw.Common.Gui.WinForms.Controls.JcwToolstripProfessionalRenderer ();
            this.Font = Jcw.Common.JcwStyle.JcwStyleFont;
        }
    }
}
