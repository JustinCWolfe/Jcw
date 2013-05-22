using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public partial class JcwToolStripStatusLabel : ToolStripStatusLabel
    {
        public JcwToolStripStatusLabel()
            : base ()
        {
            Init ();
        }

        public JcwToolStripStatusLabel(Image image)
            : base (image)
        {
            Init ();
        }

        public JcwToolStripStatusLabel(string text)
            : base (text)
        {
            Init ();
        }

        private void Init()
        {
            this.Name = "JcwToolStripStatusLabel";
        }
    }
}
