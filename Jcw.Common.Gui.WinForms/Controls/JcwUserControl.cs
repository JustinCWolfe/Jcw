using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;

using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;

using Jcw.Common;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public partial class JcwUserControl : XtraUserControl, IJcwDockable
    {
        #region Static Fields

        private static readonly bool isJcwDesignMode;

        public readonly static ResourceManager JcwResources = new ResourceManager ("Jcw.Resources.Properties.Resources",
            Assembly.GetAssembly (typeof (Jcw.Resources.Properties.Resources)));

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
            isJcwDesignMode = (Process.GetCurrentProcess ().ProcessName.IndexOf ("devenv") != -1);
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

        private void Control_Load (object sender, EventArgs e)
        {
            JcwUserControl_PreLoad (sender, e);
            JcwUserControl_Load (sender, e);
            JcwUserControl_PostLoad (sender, e);
        }

        #endregion

        #region Virtual Members

        protected virtual void JcwUserControl_PreLoad (object sender, EventArgs e)
        {
        }

        protected virtual void JcwUserControl_Load (object sender, EventArgs e)
        {
        }

        protected virtual void JcwUserControl_PostLoad (object sender, EventArgs e)
        {
        }

        #endregion

        #region Suspend/Resume Drawing

        private int suspendDrawingCounter = 0;
        public bool IsDrawingSuspended { get { return (suspendDrawingCounter > 0); } }

        public void SuspendDrawing ()
        {
            suspendDrawingCounter++;
            NativeMethods.SendMessage (Handle, (int)NativeMethods.WindowMessages.WM_SETREDRAW, false, 0);
        }

        public void ResumeDrawing ()
        {
            if (suspendDrawingCounter == 0)
            {
                return;
            }

            suspendDrawingCounter--;

            if (suspendDrawingCounter == 0)
            {
                NativeMethods.SendMessage (Handle, (int)NativeMethods.WindowMessages.WM_SETREDRAW, true, 0);
                Refresh ();
            }
        }

        #endregion
    }
}
