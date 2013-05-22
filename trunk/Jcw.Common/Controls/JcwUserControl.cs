using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;

namespace Jcw.Common.Controls
{
    public partial class JcwUserControl : XtraUserControl, IJcwDockable
    {
        #region Static Fields

        private static readonly bool isJcwDesignMode;

        #endregion

        #region Static Properties

        protected static bool IsJcwDesignMode
        {
            // the Visual Studio 2005 debugger does not stop in a method marked with this attribute but does allow a breakpoint to be set in the method.
            [DebuggerStepThrough]
            get { return isJcwDesignMode; }
        }

        #endregion

        #region Static Constructor

        static JcwUserControl ()
        {
            isJcwDesignMode = ( Process.GetCurrentProcess ().ProcessName.IndexOf ( "devenv" ) != -1 );
        }

        #endregion

        #region Constructors

        public JcwUserControl ()
        {
            InitializeComponent ();

            // set the default skin to use
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.UseWindowsXPTheme = false;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.UseDefaultLookAndFeel = true;
        }

        #endregion

        #region Event Handlers

        private void Control_Load ( object sender, EventArgs e )
        {
            JcwUserControl_PreLoad ( sender, e );
            JcwUserControl_Load ( sender, e );
            JcwUserControl_PostLoad ( sender, e );
        }

        #endregion

        #region Virtual Methods

        protected virtual void JcwUserControl_PreLoad ( object sender, EventArgs e )
        {
        }

        protected virtual void JcwUserControl_Load ( object sender, EventArgs e )
        {
        }

        protected virtual void JcwUserControl_PostLoad ( object sender, EventArgs e )
        {
        }

        #endregion
    }
}
