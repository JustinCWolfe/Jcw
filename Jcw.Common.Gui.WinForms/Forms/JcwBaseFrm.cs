using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;

using Jcw.Common.Report;

namespace Jcw.Common.Gui.WinForms.Forms
{
    public partial class JcwBaseFrm : JcwBaseNavigationFrm
    {
        #region Static Fields

        private static readonly bool isJcwDesignMode;
        public static readonly ResourceManager JcwResources = null;

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

        static JcwBaseFrm ()
        {
            isJcwDesignMode = (Process.GetCurrentProcess ().ProcessName.IndexOf ("devenv") != -1);
            if (isJcwDesignMode)
            {
                return;
            }

            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Office 2007 Black";

            JcwResources = new ResourceManager ("Jcw.Resources.Properties.Resources",
                Assembly.GetAssembly (typeof (Jcw.Resources.Properties.Resources)));
        }

        #endregion

        #region Properties

        protected JcwTabularReport TabularReport
        {
            get { return this.printDocument; }
        }

        #endregion

        #region Constructors

        public JcwBaseFrm ()
        {
            InitializeComponent ();

            // register the bonus skins and office skins
            BonusSkins.Register ();
            OfficeSkins.Register ();

            // set the default skin to use
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.UseWindowsXPTheme = false;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.UseDefaultLookAndFeel = true;

            // enabled form skinning 
            SkinManager.EnableFormSkins ();

            // force form's title bar to be repainted
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged ();
        }

        #endregion

        #region Overrides

        public override bool CanNavigateBack
        {
            get { return false; }
        }

        #endregion

        #region Event Handlers

        private void Form_Load (object sender, EventArgs e)
        {
            JcwBaseFrm_PreLoad (sender, e);
            JcwBaseFrm_Load (sender, e);
            JcwBaseFrm_PostLoad (sender, e);
        }

        private void JcwBaseFrm_KeyDown (object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control && e.Alt)
            {
                ShowChangeSkinDialog ();
                e.Handled = true;
            }
        }

        #endregion

        #region Virtual Methods

        protected virtual void JcwBaseFrm_PreLoad (object sender, EventArgs e)
        {
        }

        protected virtual void JcwBaseFrm_Load (object sender, EventArgs e)
        {
        }

        protected virtual void JcwBaseFrm_PostLoad (object sender, EventArgs e)
        {
        }

        protected virtual void JcwBaseFrm_ChangeSkin (string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle (skinName);
        }

        #endregion

        #region Printing Implementation

        protected virtual void CreateTabularReport (string company, string title, string name, string date, DataView report)
        {
            this.printDocument.Title = title;
            this.printDocument.Date = date;
            this.printDocument.Name = name;
            this.printDocument.Company = company;

            this.printDocument.DataView = report;

            CustomizeTabularReport ();
        }

        protected virtual void CustomizeTabularReport ()
        {
        }

        protected void PrintDocument ()
        {
            this.printPreviewDialog.ShowDialog ();
        }

        #endregion

        #region Public Methods

        public void ShowChangeSkinDialog ()
        {
            JcwChangeSkinFrm skinChangeFrm = new JcwChangeSkinFrm ();
            skinChangeFrm.CurrentSkin = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;

            if (skinChangeFrm.ShowDialog (this) == DialogResult.OK)
                JcwBaseFrm_ChangeSkin (skinChangeFrm.NewSkin);
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