using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jcw.Common.Controls
{
    public partial class JcwToolStripMenuItem : ToolStripMenuItem
    {
        #region Constructors

        public JcwToolStripMenuItem (string text)
            : base (text)
        {
            InitializeComponent ();
        }

        public JcwToolStripMenuItem (string text, EventHandler handler)
            : base (text, null, handler)
        {
            InitializeComponent ();
        }

        public JcwToolStripMenuItem (string text, EventHandler handler, Keys shortcutKeys)
            : base (text, null, handler, shortcutKeys)
        {
            InitializeComponent ();
        }

        public JcwToolStripMenuItem (string text, Image image, EventHandler handler, Keys shortcutKeys)
            : base (text, image, handler, shortcutKeys)
        {
            InitializeComponent ();
        }

        #endregion
    }
}
