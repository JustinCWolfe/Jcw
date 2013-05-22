using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public partial class JcwContextMenuStrip : ContextMenuStrip
    {
        public JcwContextMenuStrip ()
            : this (new Container ())
        {
        }

        public JcwContextMenuStrip (IContainer container)
            : base (container)
        {
            // 
            // JcwContextMenuStrip
            // 
            this.Font = Jcw.Common.JcwStyle.JcwStyleFont;
            this.ImageScalingSize = new System.Drawing.Size ( 20, 20 );
            this.Renderer = new Jcw.Common.Gui.WinForms.Controls.JcwToolstripProfessionalRenderer ();
            this.ShowCheckMargin = false;
            this.ShowImageMargin = false;
        }
    }
}