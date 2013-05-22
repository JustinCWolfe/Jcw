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
    public partial class JcwListBox : ListBoxControl
    {
        #region Constructors

        public JcwListBox ()
            : base ()
        {
            this.SuspendLayout ();
            // 
            // JcwListBox
            // 
            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.DoubleBuffered = true;
            this.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.HotTrackItems = true;
            this.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnClick;
            this.Font = Jcw.Common.JcwStyle.JcwStyleFont;
            this.ResumeLayout ( false );
        }

        #endregion
    }
}