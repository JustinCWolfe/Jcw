using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Jcw.Common;
using Jcw.Common.Interfaces;
using Jcw.Common.Gui.WinForms.Controls;

namespace Jcw.Common.Gui.WinForms.Forms
{
    public partial class JcwRunTaskFrm<T> : JcwBaseFixedStyleFrm, IJcwRunTask<T>
    {
        #region Fields

        private int m_minimumWidth = 300;
        private int m_maximumWidth = 1000;
        private int m_minimumHeight = 100;

        private TaskToRun m_task;
        private T m_argument;
        private IJcwTaskResult m_taskResult = new JcwTaskResult ();

        private string m_caption = "Running {0}.  Please wait...";
        private string m_description = string.Empty;
        private int? m_taskDuration = null;

        private JcwProgressBar m_progressBar = null;
        private BackgroundWorker m_backgroundWorkerThread = null;

        private Icon m_formIcon = null;

        #endregion

        #region Constructors

        public JcwRunTaskFrm ()
            : this (null)
        {
        }

        public JcwRunTaskFrm (Icon formIcon)
        {
            m_formIcon = formIcon;

            InitializeComponent ();

            // default task result status
            m_taskResult.Status = TaskResultStatus.Cancelled;

            m_backgroundWorkerThread = new BackgroundWorker ();
            m_backgroundWorkerThread.WorkerReportsProgress = true;
            m_backgroundWorkerThread.WorkerSupportsCancellation = true;
            m_backgroundWorkerThread.DoWork += BackgroundWorkerThread_DoWork;
            m_backgroundWorkerThread.ProgressChanged += BackgroundWorkerThread_ProgressChanged;
            m_backgroundWorkerThread.RunWorkerCompleted += BackgroundWorkerThread_RunWorkerCompleted;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing)
            {
                if (m_progressBar != null)
                {
                    if (this.tableLayoutPanel1 != null && this.tableLayoutPanel1.Controls != null)
                    {
                        this.tableLayoutPanel1.Controls.Remove (m_progressBar);
                    }
                    m_progressBar.Dispose ();
                }

                m_backgroundWorkerThread.DoWork -= BackgroundWorkerThread_DoWork;
                m_backgroundWorkerThread.ProgressChanged -= BackgroundWorkerThread_ProgressChanged;
                m_backgroundWorkerThread.RunWorkerCompleted -= BackgroundWorkerThread_RunWorkerCompleted;

                if (m_backgroundWorkerThread.IsBusy && !m_backgroundWorkerThread.CancellationPending)
                {
                    m_backgroundWorkerThread.CancelAsync ();
                }

                m_backgroundWorkerThread.Dispose ();

                if (m_taskResult != null)
                {
                    m_taskResult.Dispose ();
                    m_taskResult = null;
                }

                if (components != null)
                {
                    components.Dispose ();
                }
            }

            base.Dispose (disposing);
        }

        #endregion

        #region IJcwRunTask Implementation

        public string TaskName
        {
            set { m_caption = string.Format (m_caption, value); }
        }

        public string TaskDescription
        {
            set { m_description = value; }
        }

        public T TaskArgument
        {
            set { m_argument = value; }
        }

        public IJcwTaskResult TaskResult
        {
            get { return m_taskResult; }
        }

        public TaskToRun TaskDelegate
        {
            set { m_task = value; }
        }

        public int? TaskDuration
        {
            set { m_taskDuration = value; }
        }

        public void Execute ()
        {
            if (m_task == null)
            {
                m_taskResult.Status = TaskResultStatus.Failed;
                m_taskResult.Text = "No task to run";
            }

            // if no task duration is set use a cycling progress bar, otherwise use a one-pass progress bar with the specified duration
            m_progressBar = (m_taskDuration == null) ?
                new JcwProgressBar (JcwProgressBar.ProgressBarType.Cycling) :
                new JcwProgressBar (JcwProgressBar.ProgressBarType.OnePass);
            m_progressBar.Dock = DockStyle.Fill;

            // this will post the Load event for the progress bar control
            this.tableLayoutPanel1.Controls.Add (m_progressBar, 1, 1);

            if (m_formIcon != null)
            {
                this.Icon = m_formIcon;
            }

            // if no task duration is set use a cycling progress bar, otherwise use a one-pass progress bar with the specified duration
            if (m_taskDuration != null)
            {
                m_progressBar.MaxValue = m_taskDuration.GetValueOrDefault ();
            }

            // set the size of the wait dialog box depending on the size of the text it needs to display and the size of the caption
            SizeF stringSize;
            using (Graphics g = Graphics.FromHwnd (this.Handle))
            {
                SizeF layoutArea = new SizeF (m_maximumWidth, float.PositiveInfinity);
                stringSize = g.MeasureString (m_description, this.MessageTextBox.Font, layoutArea);

                // measure the width of the dialog caption and if it is wider than the string, use the caption width to size the dialog
                SizeF captionSize = g.MeasureString (m_caption, this.Font, layoutArea);
                if (captionSize.Width > stringSize.Width)
                {
                    stringSize.Width = captionSize.Width;
                }
            }

            this.MessageTextBox.Text = m_description;

            // sets the client width and height to some minimum values
            int clientWidth = Convert.ToInt32 (stringSize.Width + 20);
            if (clientWidth < m_minimumWidth)
            {
                clientWidth = m_minimumWidth;
            }

            int clientHeight = Convert.ToInt32 (stringSize.Height * 3 + 20 + this.Button.Size.Height + 20);
            if (clientHeight < m_minimumHeight)
            {
                clientHeight = m_minimumHeight;
            }

            this.ClientSize = new Size (clientWidth, clientHeight);
            this.Text = m_caption;
            this.Button.Select ();

            // start the worker thread running the Task To Run delegate method
            m_backgroundWorkerThread.RunWorkerAsync (m_argument);

            // show modal dialog that will be closed when background thread task completes
            this.ShowDialog ();
        }

        #endregion

        #region Event Handlers

        protected virtual void Button_Click (object sender, EventArgs e)
        {
            Button.Enabled = false;

            if (m_backgroundWorkerThread.IsBusy && !m_backgroundWorkerThread.CancellationPending)
            {
                m_backgroundWorkerThread.CancelAsync ();
                this.Close ();
            }
        }

        private void BackgroundWorkerThread_DoWork (object sender, DoWorkEventArgs e)
        {
            // get the background worker that raised this event
            BackgroundWorker worker = sender as BackgroundWorker;
            m_task.Invoke (worker, e);
        }

        private void BackgroundWorkerThread_ProgressChanged (object sender, ProgressChangedEventArgs e)
        {
            IJcwTaskProgress progress = e.UserState as IJcwTaskProgress;
            if (progress != null)
            {
                if (progress.ProgressType == TaskProgressType.PercentageChange && m_progressBar != null)
                {
                    m_progressBar.SetBarPosition (progress.Progress);
                }
                else if (progress.ProgressType == TaskProgressType.MessageToDisplay)
                {
                    JcwMessageBox.Show (progress.ProgressMessage, progress.ProgressMessageCaption);
                }
            }
        }

        private void BackgroundWorkerThread_RunWorkerCompleted (object sender, EventArgs e)
        {
            // marshal form close call onto the thread where the control was created
            if (this.InvokeRequired)
            {
                this.Invoke (new EventHandler (BackgroundWorkerThread_RunWorkerCompleted), new object[] { sender, e });
                return;
            }

            RunWorkerCompletedEventArgs ea = e as RunWorkerCompletedEventArgs;

            if (ea.Cancelled)
            {
                m_taskResult.Status = TaskResultStatus.Cancelled;
            }
            else if (ea.Error != null)
            {
                m_taskResult = ea.Result as IJcwTaskResult;
                m_taskResult.Text = ea.Error.Message + ea.Error.StackTrace;
                if (ea.Error.InnerException != null)
                {
                    m_taskResult.Text += ea.Error.InnerException.Message + ea.Error.InnerException.StackTrace;
                }
                m_taskResult.Status = TaskResultStatus.Failed;
            }
            else
            {
                m_taskResult = ea.Result as IJcwTaskResult;
            }

            // The progress bar was dynamically added to the table layout panel's control 
            // collection so need to call dispose on it or else we will have a memory leak.
            if (m_progressBar != null)
            {
                if (this.tableLayoutPanel1 != null && this.tableLayoutPanel1.Controls != null)
                {
                    this.tableLayoutPanel1.Controls.Remove (m_progressBar);
                }
                m_progressBar.Dispose ();
            }

            m_task = null;

            this.Close ();
        }

        #endregion
    }
}
