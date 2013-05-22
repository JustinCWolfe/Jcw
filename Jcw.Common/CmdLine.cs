using System;
using System.Data;
using System.Security;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

namespace Jcw.Common
{
    public abstract class CmdLine : IDisposable
    {
        #region Fields

        private Process m_process;
        private int m_returnValue;
        private string m_commandError;
        private string m_commandOutput;

        private string m_username;
        private int m_timeout = 60;
        private string m_arguments;
        private string m_executable;
        private bool m_hidden = true;
        private SecureString m_password;

        #endregion

        #region Properties

        protected Process MyProcess
        {
            get { return m_process; }
            set { m_process = value; }
        }

        protected int Timeout
        {
            get { return m_timeout; }
            set { m_timeout = value; }
        }

        protected int ReturnValue
        {
            get { return m_returnValue; }
            set { m_returnValue = value; }
        }

        protected bool Hidden
        {
            get { return m_hidden; }
            set { m_hidden = value; }
        }

        protected string Username
        {
            get { return m_username; }
            set { m_username = value; }
        }

        protected SecureString Password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        protected string Arguments
        {
            get { return m_arguments; }
            set { m_arguments = value; }
        }

        protected string Executable
        {
            get { return m_executable; }
            set { m_executable = value; }
        }

        protected string CommandOutput
        {
            get { return m_commandOutput; }
            set { m_commandOutput = value; }
        }

        protected string CommandError
        {
            get { return m_commandError; }
            set { m_commandError = value; }
        }

        #endregion

        #region Public Methods 

        public string RunCommand ()
        {
            m_returnValue = 0;
            m_process = new Process ();
            m_commandError = string.Empty;
            m_commandOutput = string.Empty;

            try
            {
                if ( m_hidden )
                {
                    m_process.StartInfo.CreateNoWindow = true;
                }

                if ( m_username != null )
                {
                    m_process.StartInfo.UserName = Username;
                }

                if ( m_password != null )
                {
                    m_process.StartInfo.Password = Password;
                }

                m_process.StartInfo.FileName = m_executable;
                m_process.StartInfo.Arguments = m_arguments;

                // redirect m_process output           
                m_process.StartInfo.UseShellExecute = false;
                m_process.StartInfo.RedirectStandardError = true;
                m_process.StartInfo.RedirectStandardOutput = true;

                // set event handler to asynchronously read data from stdout
                m_process.OutputDataReceived += new DataReceivedEventHandler ( OutputHandler );

                if ( m_process.Start () == false )
                {
                    Error.LastError = "Could not start Process";
                    return null;
                }

                // use asynchronous read on standard output stream to avoid deadlock
                m_process.BeginOutputReadLine ();
                m_commandError = m_process.StandardError.ReadToEnd ();

                m_process.WaitForExit ();
            }
            catch ( Exception e )
            {
                Error.LastError = e.Message + "\n" + e.StackTrace;
                return null;
            }
            finally
            {
                // check to see if the m_process is still running
                if ( !m_process.HasExited )
                {
                    // check to see if the m_process is hung
                    if ( !m_process.Responding )
                    {
                        m_process.Close ();
                    }
                    else
                    {
                        m_process.Kill ();
                    }
                }

                m_returnValue = m_process.ExitCode;
            }

            return string.Empty;
        }

        public string RunCommandWithTimeout ()
        {
            m_returnValue = 0;
            m_process = new Process ();
            m_commandError = string.Empty;
            m_commandOutput = string.Empty;

            try
            {
                if ( m_hidden )
                {
                    m_process.StartInfo.CreateNoWindow = true;
                }

                if ( m_username != null )
                {
                    m_process.StartInfo.UserName = m_username;
                }

                if ( m_password != null )
                {
                    m_process.StartInfo.Password = m_password;
                }

                m_process.StartInfo.FileName = m_executable;
                m_process.StartInfo.Arguments = m_arguments;

                // redirect m_process output           
                m_process.StartInfo.UseShellExecute = false;
                m_process.StartInfo.RedirectStandardError = true;
                m_process.StartInfo.RedirectStandardOutput = true;

                // set event handler to asynchronously read data from stdout
                m_process.OutputDataReceived += new DataReceivedEventHandler ( OutputHandler );

                if ( m_process.Start () == false )
                {
                    Error.LastError = "Could not start Process";
                    return null;
                }

                // use asynchronous read on standard output stream to avoid deadlock
                m_process.BeginOutputReadLine ();
                m_commandError = m_process.StandardError.ReadToEnd ();

                if ( !m_process.WaitForExit ( m_timeout * 1000 ) )
                {
                    Error.LastError = "Command exceeded m_timeout";
                    return null;
                }
            }
            catch ( Exception e )
            {
                Error.LastError = e.Message + "\n" + e.StackTrace;
                return null;
            }
            finally
            {
                // check to see if the m_process is still running
                if ( !m_process.HasExited )
                {
                    // check to see if the m_process is hung
                    if ( !m_process.Responding )
                    {
                        m_process.Close ();
                    }
                    else
                    {
                        m_process.Kill ();
                    }
                }

                this.m_returnValue = m_process.ExitCode;
            }

            return string.Empty;
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose ()
        {
            if (m_process != null)
            {
                m_process.Dispose ();
            }

            GC.SuppressFinalize (this);
        }

        #endregion

        #region Event Handlers

        private void OutputHandler ( object sender, DataReceivedEventArgs e )
        {
            if ( !string.IsNullOrEmpty ( e.Data ) )
            {
                m_commandOutput += ( e.Data + "\n" );
            }
        }

        #endregion
    }
}
