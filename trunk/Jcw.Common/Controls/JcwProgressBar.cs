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
    public partial class JcwProgressBar : JcwUserControl
    {
        #region Enumerations

        public enum ProgressBarType
        {
            OnePass,
            Cycling
        }

        #endregion

        #region Fields

        private ProgressBarBaseControl m_progressBar;

        #endregion

        #region Properties

        public ProgressBarControl ProgressBar
        {
            get { return m_progressBar as ProgressBarControl; }
        }

        public int MaxValue
        {
            get
            {
                return ( m_progressBar is ProgressBarControl ) ?
                    ( (ProgressBarControl) m_progressBar ).Properties.Maximum :
                    default ( int );
            }
            set
            {
                if ( m_progressBar is ProgressBarControl )
                    ( (ProgressBarControl) m_progressBar ).Properties.Maximum = value;
            }
        }

        public bool ShowTitle
        {
            set
            {
                if ( m_progressBar is ProgressBarControl )
                    ( (ProgressBarControl) m_progressBar ).Properties.ShowTitle = value;
            }
        }

        public bool PercentView
        {
            get
            {
                return ( m_progressBar is ProgressBarControl ) ?
                    ( (ProgressBarControl) m_progressBar ).Properties.PercentView :
                    default ( bool );
            }
            set
            {
                if ( m_progressBar is ProgressBarControl )
                    ( (ProgressBarControl) m_progressBar ).Properties.PercentView = value;
            }
        }

        #endregion

        #region Constructors

        public JcwProgressBar ()
            : this ( ProgressBarType.OnePass )
        {
        }

        public JcwProgressBar ( ProgressBarType barType )
        {
            InitializeComponent ();

            if ( barType == ProgressBarType.Cycling )
            {
                MarqueeProgressBarControl bar = new MarqueeProgressBarControl ();
                bar.Properties.BeginInit ();
                bar.Properties.MarqueeAnimationSpeed = 100;
                bar.Properties.ProgressKind = DevExpress.XtraEditors.Controls.ProgressKind.Horizontal;
                bar.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                bar.Properties.EndInit ();
                m_progressBar = bar;
            }
            else
            {
                ProgressBarControl bar = new ProgressBarControl ();
                bar.Properties.BeginInit ();
                bar.Properties.Minimum = 0;
                bar.Properties.PercentView = false;
                bar.Properties.ProgressKind = DevExpress.XtraEditors.Controls.ProgressKind.Horizontal;
                bar.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                bar.Properties.ShowTitle = true;
                bar.Properties.Step = 10;
                bar.Properties.EndInit ();
                m_progressBar = bar;
            }

            m_progressBar.Dock = DockStyle.Fill;
            this.Controls.Add ( m_progressBar );
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                if ( m_progressBar != null )
                {
                    this.Controls.Remove ( m_progressBar );
                    m_progressBar.Dispose ();
                }

                if ( components != null )
                    components.Dispose ();
            }
            base.Dispose ( disposing );
        }

        #endregion

        #region Event Handlers

        private void JcwProgressBar_Load ( object sender, EventArgs ea )
        {
            if ( IsJcwDesignMode )
                return;
        }

        #endregion

        #region Public Methods

        public void SetBarPosition ( int position )
        {
            if ( m_progressBar is ProgressBarControl )
                ( (ProgressBarControl) m_progressBar ).Position = position;
        }

        public void PerformStep ()
        {
            if ( m_progressBar is ProgressBarControl )
                ( (ProgressBarControl) m_progressBar ).PerformStep ();
        }

        #endregion
    }
}