using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jcw.Common
{
    public class Logger
    {
        #region Fields

        private string m_name;
        private List<string> m_data = new List<string> ();

        #endregion

        #region Properties

        public string LogName
        {
            get { return m_name; }
        }

        public string[] LogData
        {
            get { return m_data.ToArray (); }
        }

        public string LogDataAsString
        {
            get { return String.Join ("\n", LogData); }
        }

        public string LogDataAsHtmlOrderedList
        {
            get { return "<ol><li>" + String.Join ("</li><li>", LogData) + "</li></ol>"; }
        }

        public string LogDataAsHtmlUnorderedList
        {
            get { return "<ul><li>" + String.Join ("</li><li>", LogData) + "</li></ul>"; }
        }

        #endregion

        #region Constructors

        public Logger ()
        {
            this.m_name = AppDomain.CurrentDomain.FriendlyName + ".log";
        }

        public Logger (string name)
        {
            this.m_name = name;
        }

        #endregion

        #region Static Methods

        private static string GetCurrentTime ()
        {
            string timestamp = Utilities.GetCurrentDateTime ();
            return "[ " + timestamp + " ] ";
        }

        #endregion

        #region Methods

        public string InitLog ()
        {
            try
            {
                lock (m_data)
                {
                    m_data.Clear ();

                    if (File.Exists (m_name))
                    {
                        File.Delete (m_name);
                        File.CreateText (m_name).Close ();
                    }
                }

                return "";
            }
            catch (Exception e)
            {
                Error.LastError = e.Message;
                return null;
            }
        }

        public string Log (string data)
        {
            try
            {
                lock (m_data)
                {
                    StreamWriter fs = new StreamWriter (File.Open (m_name, FileMode.Append));
                    fs.WriteLine (data);
                    fs.Close ();
                    m_data.Add (data);
                }

                return "";
            }
            catch (Exception e)
            {
                Error.LastError = e.Message;
                return null;
            }
        }

        public string Log (string[] data)
        {
            try
            {
                foreach (string line in data)
                {
                    Log (line);
                }

                return "";
            }
            catch (Exception e)
            {
                Error.LastError = e.Message;
                return null;
            }
        }

        public string LogWithTime (string data)
        {
            try
            {
                Log (GetCurrentTime () + data);
                return "";
            }
            catch (Exception e)
            {
                Error.LastError = e.Message;
                return null;
            }
        }

        public string LogWithTime (string[] data)
        {
            try
            {
                foreach (string line in data)
                {
                    Log (GetCurrentTime () + line);
                }

                return "";
            }
            catch (Exception e)
            {
                Error.LastError = e.Message;
                return null;
            }
        }

        #endregion
    }
}
