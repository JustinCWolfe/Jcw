using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Jcw.Common.Controls
{
    public partial class JcwSafeTextPanel : JcwUserControl
    {
        #region Fields

        private StringFormat m_textFormat;

        #endregion

        #region Properties

        public string PanelText
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        #endregion

        #region Constructors

        public JcwSafeTextPanel ()
        {
            InitializeComponent ();

            m_textFormat = new StringFormat (StringFormat.GenericDefault);
            m_textFormat.LineAlignment = StringAlignment.Center;
            this.SetStyle (ControlStyles.UserPaint, true);
            this.SetStyle (ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.Text = "Some Text";
        }

        #endregion

        #region Overrides

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        protected override void OnPaintBackground (PaintEventArgs pevent)
        {
        }

        protected override void OnPaint (PaintEventArgs e)
        {
            base.OnPaint (e);

            Rectangle borderRectangle = new Rectangle (0, 0, base.Size.Width - 1, base.Size.Height - 1);
            Rectangle textRectangle = borderRectangle;

            textRectangle.Offset (this.Margin.Left, this.Margin.Top);
            textRectangle.Width -= this.Margin.Horizontal;
            textRectangle.Height -= this.Margin.Vertical;

            using (Pen pen = new Pen (VisualStyleInformation.TextControlBorder, 2))
            {
                e.Graphics.DrawRectangle (pen, borderRectangle);
            }

            using (SolidBrush brush = new SolidBrush (this.ForeColor))
            {
                e.Graphics.DrawString (this.Text, this.Font, brush, textRectangle, m_textFormat);
            }
        }

        #endregion
    }
}
