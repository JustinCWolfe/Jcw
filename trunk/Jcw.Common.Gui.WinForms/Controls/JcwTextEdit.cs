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
    public partial class JcwTextEdit : TextEdit
    {
        public JcwTextEdit ()
            : base ()
        {
            ((System.ComponentModel.ISupportInitialize)(this.Properties)).BeginInit ();
            this.SuspendLayout ();
            // 
            // JcwTextEdit
            // 
            this.Name = "JcwTextEdit";
            this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            ((System.ComponentModel.ISupportInitialize)(this.Properties)).EndInit ();
            this.ResumeLayout (false);
        }
    }
}
