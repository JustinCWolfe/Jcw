using System;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Jcw.Common;
using Jcw.Common.Gui.WinForms.Forms;

namespace Jcw.Common.Gui.WinForms.Applications
{
    public static class JcwSingleton
    {
        #region Fields

        static Mutex m_mutex;
        static Logger m_logger;

        #endregion

        #region Properties

        public static Logger ApplicationLogger
        {
            get { return m_logger; }
        }

        #endregion

        #region Static Constructor

        static JcwSingleton()
        {
            m_logger = new Logger ();
            m_logger.InitLog ();

            InitializeApplication ();
        }

        #endregion

        #region Event Handlers

        // handles the exception event for all non ui threads
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            m_logger.Log ("CurrentDomain_UnhandledException: " + e.ExceptionObject.ToString ());
            JcwMessageBox.Show ("CurrentDomain_UnhandledException: " + e.ExceptionObject.ToString (), "Error");
        }

        // handles the exception event for the ui thread
        private static void OnThreadException(object sender, System.Threading.ThreadExceptionEventArgs t)
        {
            StringBuilder e = new StringBuilder ();
            e.AppendLine (t.Exception.Message);
            e.AppendLine (t.Exception.StackTrace);

            if (t.Exception.InnerException != null)
            {
                e.AppendLine ("\n\nInnerException" + t.Exception.InnerException.Message);
                e.AppendLine (t.Exception.InnerException.StackTrace);
            }

            m_logger.Log ("OnThreadException: " + e.ToString ());
            JcwMessageBox.Show ("OnThreadException: " + e.ToString (), "Error");
        }

        private static void OnExit(object sender, EventArgs e)
        {
            m_mutex.ReleaseMutex ();
            m_mutex.Close ();
        }

        #endregion

        #region Private Methods

        private static void InitializeApplication()
        {
            // These must be called before the Application.Run method is called.
            Application.EnableVisualStyles ();
            Application.SetCompatibleTextRenderingDefault (false);
        }

        static bool IsFirstInstance()
        {
            Assembly assembly = Assembly.GetEntryAssembly ();
            string name = assembly.GetName ().Name;

            // create the named mutex if possible (if it isn't already in existence)
            m_mutex = new Mutex (false, name);

            bool owned = false;
            owned = m_mutex.WaitOne (TimeSpan.Zero, false);
            return owned;
        }

        #endregion

        #region Public Methods

        public static void Run(Type mainFormType)
        {
            Run (mainFormType, null);
        }

        public static void Run(Type mainFormType, EventHandler applicationStartedCallback)
        {
            Run (mainFormType, applicationStartedCallback, null);
        }

        public static void Run(Type mainFormType, EventHandler applicationStartedCallback, params object[] parameters)
        {
            Form mainForm = (Form)Factory.Instance.Create (mainFormType, null, parameters);
            Run (mainForm, applicationStartedCallback, parameters);
        }

        public static void Run(Form mainForm, EventHandler applicationStartedCallback, params object[] parameters)
        {
            bool first = IsFirstInstance ();

            if (first)
            {
                Thread.CurrentThread.Name = "Main UI Thread";

                // add event handler for all threads in the AppDomain except for the Main UI Thread
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler (CurrentDomain_UnhandledException);

                // add event handler to catch any exceptions that happen in the Main UI Thread
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler (OnThreadException);

                // add event handler to release and close named mutex on application exit
                Application.ApplicationExit += new EventHandler (OnExit);

                // if the application was started and the callback is not null, call the callback method
                if (applicationStartedCallback != null)
                {
                    applicationStartedCallback (mainForm, EventArgs.Empty);
                }

                Application.Run (mainForm);
            }

            if (mainForm != null && mainForm.IsDisposed == false)
            {
                mainForm.Close ();
            }
        }

        #endregion
    }
}
