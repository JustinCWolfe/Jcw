using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Jcw.Common.Applications;

namespace Jcw.Common.Forms
{
    public partial class JcwSplashScreenFrm : Form
    {
        #region Constants

        private const int TIMER_INTERVAL = 50;

        #endregion

        #region Static Fields

        private static Thread SplashScreenThread = null;
        private static JcwSplashScreenFrm ApplicationSplashScreen = null;

        #endregion

        #region Static Properties

        public static JcwSplashScreenFrm SplashForm
        {
            get { return ApplicationSplashScreen; }
        }

        #endregion

        #region Private Static Methods

        private static void ShowForm (object threadStartData)
        {
            JcwSplashScreenParameters jcwThreadStartData = threadStartData as JcwSplashScreenParameters;
            if (jcwThreadStartData != null)
            {
                ApplicationSplashScreen = new JcwSplashScreenFrm (jcwThreadStartData);
                Application.Run (ApplicationSplashScreen);
            }
        }

        #endregion

        #region Public Static Methods

        public static void CloseForm ()
        {
            if (ApplicationSplashScreen != null && ApplicationSplashScreen.IsDisposed == false)
                // start splash screen fade away process
                ApplicationSplashScreen.m_opacityIncrement = -ApplicationSplashScreen.m_opacityDecrement;

            SplashScreenThread = null;
            ApplicationSplashScreen = null;
        }

        public static void ShowSplashScreen (Bitmap splashScreenImage)
        {
            // Make sure splash screen is only launched once
            if (ApplicationSplashScreen == null)
            {
                SplashScreenThread = new Thread (new ParameterizedThreadStart (JcwSplashScreenFrm.ShowForm));
                SplashScreenThread.Name = "Splash Screen Thread";
                SplashScreenThread.IsBackground = true;
                SplashScreenThread.SetApartmentState (ApartmentState.STA);
                SplashScreenThread.Start (new JcwSplashScreenParameters (splashScreenImage));
            }
        }

        public static void SetStatus (string newStatus)
        {
            if (ApplicationSplashScreen != null)
                ApplicationSplashScreen.m_status = newStatus;
        }

        #endregion

        #region Fields

        private string m_status = null;
        private double m_opacityIncrement = .06;
        private double m_opacityDecrement = .06;

        #endregion

        #region Constructors

        private JcwSplashScreenFrm (JcwSplashScreenParameters splashParameters)
        {
            InitializeComponent ();

            this.BackgroundImage = splashParameters.SplashScreenImage;
            this.ClientSize = this.BackgroundImage.Size;

            // start timer that will provide the fade-in/fade-out effect of the splash screen by changing the opacity
            this.Opacity = .00;
            this.splashScreenOpacityTimer.Interval = TIMER_INTERVAL;
            this.splashScreenOpacityTimer.Start ();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose ();

            base.Dispose (disposing);
        }

        #endregion

        #region Event Handlers

        private void splashScreenOpacityTimer_Tick (object sender, EventArgs e)
        {
            if (m_opacityIncrement > 0)
            {
                if (this.Opacity < 1)
                    this.Opacity += m_opacityIncrement;
            }
            else
            {
                if (this.Opacity > 0)
                    this.Opacity += m_opacityIncrement;
                else
                    this.Close ();
            }

            // if some status was set, make the status label visible and size it appropriately 
            if (string.IsNullOrEmpty (m_status) == false)
            {
                // decide whether or not we are displaying status on our splash screen
                this.statusLabel.Visible = true;

                SizeF statusStringSize;
                using (Graphics g = Graphics.FromHwnd (this.Handle))
                    statusStringSize = g.MeasureString (m_status, this.statusLabel.Font);

                // position status display on splash screen based on the status text 
                this.statusLabel.Location = new Point (this.ClientSize.Width / 2 - this.statusLabel.Size.Width / 2, this.ClientSize.Height / 2 - this.statusLabel.Height / 2);
                this.statusLabel.Size = statusStringSize.ToSize ();
                this.statusLabel.Text = m_status;
            }
        }

        private void JcwSplashScreenFrm_DoubleClick (object sender, EventArgs e)
        {
            CloseForm ();
        }

        #endregion
    }

    public class JcwSplashScreenParameters
    {
        private Bitmap m_splashScreenImage;

        public Bitmap SplashScreenImage
        {
            get { return m_splashScreenImage; }
        }

        public JcwSplashScreenParameters (Bitmap splashScreenImage)
        {
            m_splashScreenImage = splashScreenImage;
        }
    }
}