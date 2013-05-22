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
    public partial class JcwMemoEdit : MemoEdit
    {
        public JcwMemoEdit ()
            : base ()
        {
            this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
        }
    }
}
