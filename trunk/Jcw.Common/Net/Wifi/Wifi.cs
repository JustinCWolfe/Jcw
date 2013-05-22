using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

using Jcw.Common;
using Jcw.Common.Interfaces;
using Jcw.Common.Net.Wifi.NativeWifi;

namespace Jcw.Common.Net.Wifi
{
    public class Wifi : IWifi, IDisposable
    {
        #region Constants

        public const int ConnectionIntervals = 100;
        public const int ConnectionTimeout = 120000;

        public const string Connected = "Connected";
        public const string NotConnected = "Not Connected";

        #endregion

        #region Events

        public event ActiveConnectionLost OnActiveConnectionLost;

        #endregion

        #region Properties

        public WifiAccessPoint CurrentAccessPoint
        {
            get { return GetCurrentWifiConnection (); }
        }

        private ManualResetEvent ActiveConnectionMonitoringManualResetEvent { get; set; }
        private ManualResetEvent ConnectionStatusChangedManualResetEvent { get; set; }

        private WifiAccessPoint OriginalAccessPoint { get; set; }
        private WlanClient.WlanInterface WlanInterface { get; set; }

        #endregion

        #region Constructors

        public Wifi ()
        {
            OriginalAccessPoint = CurrentAccessPoint;
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose ()
        {
            Dispose (true);

            // This object will be cleaned up by the Dispose method. Therefore, you should call GC.SupressFinalize to take 
            // this object off the finalization queue and prevent finalization code for this object from executing a second time.
            GC.SuppressFinalize (this);
        }

        private bool disposed = false;
        private void Dispose (bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if (disposing)
                {
                    if (WlanInterface != null)
                    {
                        // If the current access point is different from the original, disconnect from the current one and reconnect
                        // to the original one.
                        if (!CurrentAccessPoint.Equals (OriginalAccessPoint))
                        {
                            // Disconnect from the wifi access point we are currently connected to.
                            DisconnectWifi ();

                            // If there was an existing wifi connection when the wifi class was constructed and that access point is different 
                            // from the current access point, rejoin the original access point.
                            if (!NotConnected.Equals (OriginalAccessPoint.SSID))
                            {
                                WifiAccessPoint currentAccessPoint = CurrentAccessPoint;
                                if (!currentAccessPoint.SSID.Equals (OriginalAccessPoint.SSID))
                                {
                                    JoinWifi (OriginalAccessPoint.SSID, OriginalAccessPoint.BssType);
                                }
                            }
                        }
                    }
                }

                // Note disposing has been done.
                disposed = true;
            }
        }

        /// <summary>
        /// Use C# destructor syntax for finalization code. This destructor will run only if the Dispose method
        /// does not get called. It gives your base class the opportunity to finalize. Do not provide destructors 
        /// in types derived from this class.
        /// </summary>
        ~Wifi ()
        {
            // Do not re-create Dispose clean-up code here. Calling Dispose(false) is optimal in terms of readability and maintainability.
            Dispose (false);
        }

        #endregion

        #region Public Methods

        public bool DisconnectWifi ()
        {
            // If we aren't currently connected to a wireless access point, return without attempting to disconnect.
            if (Wifi.NotConnected.Equals (CurrentAccessPoint.SSID))
            {
                return false;
            }
            return DisconnectWifiInternal ();
        }

        public bool JoinWifi (string ssidToJoin, Wlan.Dot11BssType bssType)
        {
            DoWorkEventArgs dwea = new DoWorkEventArgs (null);
            JoinWifi (ssidToJoin, bssType, null, dwea);

            IJcwTaskResult taskResult = dwea.Result as IJcwTaskResult;
            if (taskResult != null)
            {
                return (taskResult.Status == TaskResultStatus.Passed);
            }

            return false;
        }

        public void JoinWifi (string ssidToJoin, Wlan.Dot11BssType bssType, BackgroundWorker worker, DoWorkEventArgs e)
        {
            IJcwTaskResult result = new JcwTaskResult ();
            result.Status = TaskResultStatus.Failed;
            Error.LastError = null;

            // If the ssid to join is null, return without attempting to join.
            if (string.IsNullOrEmpty (ssidToJoin))
            {
                return;
            }

            // The SSID profile name for adhoc access points includes the -adhoc suffix.
            string ssidProfileName = ssidToJoin;
            if (bssType == Wlan.Dot11BssType.Independent)
            {
                ssidProfileName += "-adhoc";
            }

            // If we are already connected to that ssid, return without attempting to join. Note that the SSID in the
            // CurrentAccessPoint is actually the SSID profile name.
            if (ssidProfileName.Equals (CurrentAccessPoint.SSID))
            {
                result.Status = TaskResultStatus.Passed;
                result.Text = string.Format ("Already connected to wifi access point: {0}", ssidProfileName);
                e.Result = result;
                return;
            }

            try
            {
                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // If we are currently connected to a wireless access point other than the one we want to join, disconnect from it.
                if (!Wifi.NotConnected.Equals (CurrentAccessPoint.SSID))
                {
                    DisconnectWifi ();
                }

                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // Check to see if the profile exists on this system before trying to connect.
                try
                {
                    string profileXml = WlanInterface.GetProfileXml (ssidProfileName);
                }
                catch (Win32Exception ex)
                {
                    // If the exception is 'Element not found' this profile has not been registered.  If it is an adhoc access point we 
                    // should register the profile for it.
                    if ("Element not found".Equals (ex.Message) && bssType == Wlan.Dot11BssType.Independent)
                    {
                        string profileXml = WifiHelper.GetAdhocProfileXmlForSSID (ssidToJoin);
                        Debug.WriteLine (string.Format ("Setting wifi profile xml {0}.", profileXml));
                        WlanInterface.SetProfile (Wlan.WlanProfileFlags.AllUser, profileXml, true);
                    }
                }

                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                bool connectComplete = false;
                Stopwatch sw = new Stopwatch ();
                sw.Start ();
                try
                {
                    // Create unsignaled manual reset event that will be signaled in the NetworkAddressChanged handler.
                    ConnectionStatusChangedManualResetEvent = new ManualResetEvent (false);

                    // Windows XP with SP3 and Wireless LAN API for Windows XP with SP2:  Only the wlan_connection_mode_profile value is supported.
                    WlanInterface.Connect (Wlan.WlanConnectionMode.Profile, bssType, ssidProfileName);

                    // Subscribe to the NetworkAddressChanged event and wait for either the manual reset event to be signaled or the connection timeout.
                    NetworkChange.NetworkAddressChanged += NetworkAddressChanged_ConnectStatus;

                    int totalWaitTime = 0;
                    int loopWaitTime = ConnectionTimeout / ConnectionIntervals;
                    do
                    {
                        connectComplete = ConnectionStatusChangedManualResetEvent.WaitOne (loopWaitTime);
                        totalWaitTime += loopWaitTime;

                        if (worker != null && worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                    while (!connectComplete && totalWaitTime < ConnectionTimeout);

                    if (connectComplete)
                    {
                        // Create unsignaled manual reset event that will be signaled in the NetworkAddressChanged handler.
                        ActiveConnectionMonitoringManualResetEvent = new ManualResetEvent (false);

                        // If the connection was established successfully, queue a method to the thread pool to monitor this active connection.
                        ThreadPool.QueueUserWorkItem (MonitorActiveWifiConnection);

                        result.Status = TaskResultStatus.Passed;
                        result.Text = "Joined wifi access point";
                    }
                    else
                    {
                        result.Text = "Timeout attempting to join wifi access point";
                    }
                }
                finally
                {
                    NetworkChange.NetworkAddressChanged -= NetworkAddressChanged_ConnectStatus;
                    sw.Stop ();
                    Debug.WriteLine (string.Format ("Time attempting to connect: {0}ms", sw.ElapsedMilliseconds));

                    // If the user canceled the connect disconnect before exiting. Use the internal disconnect because 
                    // the regular disconnect checks to see if we are connected to the current access point and only
                    // then would it attempt to disconnect.
                    if (e.Cancel)
                    {
                        DisconnectWifiInternal ();
                    }
                }
            }
            catch (Win32Exception ex)
            {
                Error.LastError = ex.ToString ();
            }
            finally
            {
                e.Result = result;
            }
        }

        #endregion

        #region Private Methods

        private bool DisconnectWifiInternal ()
        {
            Stopwatch sw = new Stopwatch ();
            sw.Start ();
            try
            {
                // Create unsignaled manual reset event that will be signaled in the NetworkAddressChanged handler.
                ConnectionStatusChangedManualResetEvent = new ManualResetEvent (false);

                WlanInterface.Disconnect ();

                // Subscribe to the NetworkAddressChanged event and wait for either the manual reset event to be signaled or the connection timeout.
                NetworkChange.NetworkAddressChanged += NetworkAddressChanged_ConnectStatus;
                return ConnectionStatusChangedManualResetEvent.WaitOne (ConnectionTimeout);
            }
            catch (Win32Exception ex)
            {
                Error.LastError = ex.ToString ();
                return false;
            }
            finally
            {
                NetworkChange.NetworkAddressChanged -= NetworkAddressChanged_ConnectStatus;
                sw.Stop ();
                Debug.WriteLine (string.Format ("Time to disconnect: {0}ms", sw.ElapsedMilliseconds));
            }
        }

        private WifiAccessPoint GetCurrentWifiConnection ()
        {
            WlanClient client = new WlanClient ();
            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                // Subscribe to events on this interface.
                if (WlanInterface == null)
                {
                    WlanInterface = wlanIface;
                }

                if (wlanIface.InterfaceState == Wlan.WlanInterfaceState.Connected)
                {
                    try
                    {
                        return new WifiAccessPoint (WlanInterface.CurrentConnection.profileName, WlanInterface.CurrentConnection.wlanAssociationAttributes.dot11BssType);
                    }
                    catch (Win32Exception ex)
                    {
                        Error.LastError = ex.ToString ();
                    }
                }

                break;
            }

            return WifiAccessPoint.NotConnected;
        }

        private void NetworkAddressChanged_ConnectStatus (object sender, EventArgs ea)
        {
            if (ConnectionStatusChangedManualResetEvent != null)
            {
                ConnectionStatusChangedManualResetEvent.Set ();
            }
        }

        private void NetworkAddressChanged_ActiveConnectionMonitor (object sender, EventArgs ea)
        {
            if (ActiveConnectionMonitoringManualResetEvent != null)
            {
                ActiveConnectionMonitoringManualResetEvent.Set ();
            }
        }

        private void MonitorActiveWifiConnection (object state)
        {
            Stopwatch sw = new Stopwatch ();
            sw.Start ();
            try
            {
                // Create unsignaled manual reset event that will be signaled in the NetworkAddressChanged handler.
                ActiveConnectionMonitoringManualResetEvent = new ManualResetEvent (false);

                // Subscribe to the NetworkAddressChanged event and wait for either the manual reset event to be signaled.
                NetworkChange.NetworkAddressChanged += NetworkAddressChanged_ActiveConnectionMonitor;

                // Wait indefinitely on the active connection monitoring wait handle for a connection status changed indication.
                ActiveConnectionMonitoringManualResetEvent.WaitOne (Timeout.Infinite);
            }
            finally
            {
                if (OnActiveConnectionLost != null)
                {
                    OnActiveConnectionLost (this, EventArgs.Empty);
                }

                NetworkChange.NetworkAddressChanged -= NetworkAddressChanged_ActiveConnectionMonitor;
                Debug.WriteLine (string.Format ("Active connection monitoring complete: {0}ms", sw.ElapsedMilliseconds));
            }
        }

        #endregion
    }
}