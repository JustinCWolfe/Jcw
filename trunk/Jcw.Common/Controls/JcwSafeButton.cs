using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;

namespace Jcw.Common.Controls
{
    public partial class JcwSafeButton : SimpleButton
    {
        #region Fields

        private Color m_initialBorderColor;
        private Color m_initialBackColor;
        private Color m_initialForeColor;

        private Color m_clickRequiredBackColor = Color.LightCoral;
        private Color m_clickRequiredBorderColor = Color.Red;
        private Color m_clickRequiredForeColor = Color.Red;

        #endregion

        #region Constructors

        public JcwSafeButton ()
            : base ()
        {
            InitializeComponent ();

            m_initialBackColor = this.Appearance.BackColor;
            m_initialBorderColor = this.Appearance.BorderColor;
            m_initialForeColor = this.Appearance.ForeColor;

            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseBorderColor = true;
            this.Appearance.Options.UseForeColor = true;

            // use the default button style to have full look and feel support
            this.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
        }

        #endregion

        #region Event Handlers

        protected void JcwSafeButton_Click (object sender, EventArgs e)
        {
            SetButtonInitialState ();
        }

        #endregion

        #region Public Methods

        public void SetButtonInitialState ()
        {
            this.Appearance.BackColor = m_initialBackColor;
            this.Appearance.BorderColor = m_initialBorderColor;
            this.Appearance.ForeColor = m_initialForeColor;
        }

        public void SetButtonClickRequired ()
        {
            this.Appearance.BackColor = m_clickRequiredBackColor;
            this.Appearance.BorderColor = m_clickRequiredBorderColor;
            this.Appearance.ForeColor = m_clickRequiredForeColor;
        }

        #endregion
    }
}