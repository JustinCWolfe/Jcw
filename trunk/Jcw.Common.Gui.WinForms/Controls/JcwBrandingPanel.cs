using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public partial class JcwBrandingPanel : UserControl
    {
        #region Properties

        public Bitmap BrandingLogo { get; set; }

        #endregion

        #region Constructors

        public JcwBrandingPanel ()
        {
            InitializeComponent ();
        }

        #endregion

        #region Event Handlers

        private void JcwBrandingPanel_Load (object sender, EventArgs ea)
        {
            if (DesignMode)
            {
                return;
            }
            BrandingLogo.MakeTransparent (BrandingLogo.GetPixel (1, 1));
            this.BackgroundImage = BrandingLogo;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        #endregion
    }
}