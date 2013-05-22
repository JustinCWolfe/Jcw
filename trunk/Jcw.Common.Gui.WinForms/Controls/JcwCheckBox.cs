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
    public partial class JcwCheckBox : CheckEdit
    {
        public JcwCheckBox ()
            : base ()
        {
            this.BackColor = System.Drawing.Color.Transparent;
            this.Font = Jcw.Common.JcwStyle.JcwStyleFont;
        }
    }
}
