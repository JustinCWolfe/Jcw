using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;

using Jcw.Common.Gui.Interfaces;

namespace Jcw.Common.Gui.WinForms.Forms
{
    public abstract partial class JcwBaseNavigationFrm : XtraForm, INavigationBehavior
    {
        #region Constructors

        public JcwBaseNavigationFrm ()
        {
            InitializeComponent ();
        }

        #endregion

        #region INavigationBehavior Interface

        public abstract bool CanNavigateBack { get; }

        #endregion
    }
}
