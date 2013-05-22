using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Jcw.Common.Interfaces;

namespace Jcw.Common
{
    #region Enumerations

    public enum TaskResultStatus
    {
        None,
        Passed,
        Failed,
        Cancelled
    }

    public enum TaskProgressType
    {
        PercentageChange,
        MessageToDisplay,
        MessageToLog,
    }

    #endregion

    #region Delegates

    public delegate void TaskToRun (BackgroundWorker worker, DoWorkEventArgs e);

    #endregion

    #region IJcwTaskProgress Default Implementation

    public class JcwTaskProgress : IJcwTaskProgress
    {
        public int Progress { get; set; }
        public string ProgressMessage { get; set; }
        public string ProgressMessageCaption { get; set; }
        public TaskProgressType ProgressType { get; set; }

        #region IDisposable Implementation

        public void Dispose ()
        {
            Dispose (true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize (this);
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing"></param>
        private bool disposed = false;
        private void Dispose (bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (disposed == false)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if (disposing)
                {
                    ProgressMessage = null;
                    ProgressMessageCaption = null;
                }

                // Note disposing has been done.
                disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~JcwTaskProgress ()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose (false);
        }

        #endregion
    }

    #endregion

    #region IJcwTaskResult Default Implementation

    public class JcwTaskResult : IJcwTaskResult
    {
        private TaskResultStatus m_status = TaskResultStatus.None;
        public TaskResultStatus Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        public string Text { get; set; }

        #region IDisposable Implementation

        public void Dispose ()
        {
            Dispose (true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize (this);
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing"></param>
        private bool disposed = false;
        private void Dispose (bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (disposed == false)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if (disposing)
                    Text = null;

                // Note disposing has been done.
                disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~JcwTaskResult ()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose (false);
        }

        #endregion
    }

    #endregion
}
