using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

using DevExpress.Skins;
using DevExpress.XtraEditors.DXErrorProvider;

namespace Jcw.Common.Forms
{
    [System.ComponentModel.DesignerCategory ( "form" )]
    internal partial class JcwChangeSkinFrm : JcwBaseFixedStyleFrm
    {
        #region Properties

        public string CurrentSkin
        {
            set { this.CurrentSkinJcwTextEdit.Text = value; }
        }

        public string NewSkin
        {
            get { return this.NewSkinComboBox.Text; }
        }

        #endregion

        #region Constructors

        public JcwChangeSkinFrm ()
        {
            InitializeComponent ();
        }

        #endregion

        #region Overrides

        protected override void JcwBaseFrm_PreLoad ( object sender, EventArgs e )
        {
            base.JcwBaseFrm_PreLoad ( sender, e );

            // set the skin items for the new skin combo box
            foreach ( SkinContainer skin in SkinManager.Default.Skins )
                this.NewSkinComboBox.Items.Add ( skin.SkinName );

            // select the current skin in the new skin combo box
            this.NewSkinComboBox.Text = this.CurrentSkinJcwTextEdit.Text;
        }

        #endregion

        #region Event Handlers

        private void RenameJcwSafeButton_Click ( object sender, EventArgs e )
        {
            DialogResult = DialogResult.OK;
            Close ();
        }

        private void CancelJcwSafeButton_Click ( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close ();
        }

        #endregion
    }
}
