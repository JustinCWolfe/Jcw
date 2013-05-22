using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jcw.Common.Controls
{
    public partial class JcwToolStripButton : ToolStripButton
    {
        public JcwToolStripButton ()
            : base ()
        {
            InitializeComponent ();
        }

        public JcwToolStripButton (string text)
            : this ()
        {
            Text = text;
        }

        public JcwToolStripButton (Image image)
            : this ()
        {
            Image = image;
        }
    }
}
