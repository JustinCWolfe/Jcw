using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

using Franson.BlueTools;

using Jcw.Common.Interfaces;

namespace Jcw.Common.Net.Bluetooth
{
    public class Bluetools : IBluetooth
    {
        #region Fields

        private bool m_statusCheckRunning = false;

        private long m_bluetoothLastTimeToConnectMilliseconds;
        private Stopwatch m_bluetoothStopwatch = new Stopwatch ();
        private readonly int m_bluetoothDirectConnectTimeoutSeconds = 45;
        private readonly int m_bluetoothDirectConnectIntervalMilliseconds = 200;

        private string m_pinCode;

        private string m_deviceName;
        private string m_deviceAddress;

        private string m_serviceName;
        private string m_serviceAddress;
        private int m_serviceChannel;

        private Manager m_manager;
        private Network m_network;
        private Franson.BlueTools.License m_license;

        private string m_stackName;
        private Version m_stackVer;
        private Version m_bluetoolsVer;

        private RemoteDevice m_device;
        private ServiceStream m_stream;
        private RemoteService m_service;

        #endregion

        #region Properties

        public Manager BTManager
        {
            get { return m_manager; }
            set { m_manager = value; }
        }

        public Network BTNetwork
        {
            get { return m_network; }
            set { m_network = value; }
        }

        public RemoteDevice Device
        {
            get { return m_device; }
            set { m_device = value; }
        }

        public RemoteService Service
        {
            get { return m_service; }
            set { m_service = value; }
        }

        #endregion

        #region Constructors

        public Bluetools ()
        {
            // get bluetools m_manager            
            m_manager = Manager.GetManager ();

            // bluetools m_manager auto detects the stack id
            switch (Manager.StackID)
            {
                case StackID.STACK_WIDCOMM:
                    m_stackName = "WidComm Stack";
                    break;
                case StackID.STACK_MICROSOFT:
                    m_stackName = "Microsoft Stack";
                    break;
                default:
                    m_stackName = "Unknown Stack";
                    break;
            }

            // set our m_license key here
            m_license = new Franson.BlueTools.License ();
            m_license.LicenseKey = "XVCnxyi545D7MKO43OTWOgpKm3RdH50FXrL8";

            // Get first network (BlueTools 1.0 only supports one network == one dongle)            
            m_network = m_manager.Networks[0];
            m_bluetoolsVer = Manager.BlueToolsVersion;
            m_stackVer = m_network.StackVersion;
        }

        public void Dispose ()
        {
            try
            {
                try
                {
                    // flush and close the m_stream connected to the remote m_device
                    if (m_stream != null)
                    {
                        m_stream.Flush ();
                        m_stream.Close ();
                    }
                }
                catch { }

                try
                {
                    if (m_network != null && m_network.DiscoveryPending)
                    {
                        m_network.CancelDeviceDiscovery ();
                    }
                }
                catch { }

                m_manager.Dispose ();
            }
            catch { }
        }

        #endregion

        #region IBluetooth Implemenation

        public event DeviceLost OnDeviceLost;

        public long BluetoothLastTimeToConnect
        {
            get { return m_bluetoothLastTimeToConnectMilliseconds; }
        }

        public string PINCode
        {
            get { return m_pinCode; }
            set { m_pinCode = value; }
        }

        public string DeviceName
        {
            get { return m_deviceName; }
            set { m_deviceName = value; }
        }

        public string DeviceAddress
        {
            get { return m_deviceAddress; }
            set { m_deviceAddress = value; }
        }

        public string ServiceName
        {
            get { return m_serviceName; }
            set { m_serviceName = value; }
        }

        public string ServiceAddress
        {
            get { return m_serviceAddress; }
            set { m_serviceAddress = value; }
        }

        public int ServiceChannel
        {
            get { return m_serviceChannel; }
            set { m_serviceChannel = value; }
        }

        public Stream ServiceStream
        {
            get { return m_service.Stream; }
        }

        public string StackName
        {
            get { return m_stackName; }
            set { m_stackName = value; }
        }

        public Version StackVersion
        {
            get { return m_stackVer; }
            set { m_stackVer = value; }
        }

        public Version LibraryVersion
        {
            get { return m_bluetoolsVer; }
        }

        public void InitializeRadio ()
        {
            if (m_network.DiscoveryPending)
            {
                m_network.CancelDeviceDiscovery ();
            }

            if (m_service != null)
            {
                // close the stream object not the stream factory object
                if (m_stream != null)
                {
                    m_stream.Close ();
                }

                m_service = null;
            }

            if (m_device != null)
            {
                m_device = null;
            }

            // wait 1 second to make sure any disconnect has completed
            Thread.Sleep (1000);
        }

        public void FindAndConnect (BackgroundWorker worker, DoWorkEventArgs e)
        {
            m_bluetoothStopwatch.Start ();

            // Force device discovery instead of direct connect
            //m_deviceAddress = null;

            // if we have an address try to direct connect
            if (string.IsNullOrEmpty (m_deviceAddress))
            {
                RunDiscover (worker, e);
            }
            else
            {
                RunDirectConnect (worker, e);
            }

            m_bluetoothStopwatch.Stop ();
            m_bluetoothLastTimeToConnectMilliseconds = m_bluetoothStopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Cancelled status is the default which means that a status check is already running so we 
        /// won't check status this go around.  All other cases will return status of passed except if we
        /// hit an exception in which case we will return a status of Failed.
        /// </summary>
        /// <returns></returns>
        public IJcwTaskResult TestConnectionStatus ()
        {
            IJcwTaskResult result = new JcwTaskResult ();
            result.Status = TaskResultStatus.Cancelled;

            if (!m_statusCheckRunning)
            {
                m_statusCheckRunning = true;
                try
                {
                    if (!m_network.IsHardwareAvailable)
                    {
                        if (OnDeviceLost != null)
                        {
                            OnDeviceLost (this, new JcwEventArgs<string> (m_deviceName));
                            result.Text = "Hardware not available. Raising device lost";
                            result.Status = TaskResultStatus.Passed;
                            return result;
                        }
                    }
                    else if (!m_network.DiscoveryPending)
                    {
                        Device[] devices = m_network.DiscoverDevices ();

                        List<string> deviceNames = new List<string> ();
                        foreach (Device device in devices)
                        {
                            deviceNames.Add (device.Name);
                        }
                        StringBuilder sb = new StringBuilder ();
                        foreach (string deviceName in deviceNames)
                        {
                            sb.Append (deviceName + ",");
                        }

                        Debug.WriteLine ("Devices: " + sb.ToString ());

                        foreach (Device device in devices)
                        {
                            if (device.Name == m_deviceName)
                            {
                                // If there was a direct connect, the services for the device won't populated without discovery of them. 
                                // In that case, we will assume that you are connected so return.
                                if (device.Services.Length == 0)
                                {
                                    result.Text = string.Format ("Found device: {0} via direct connect.", m_deviceName);
                                    result.Status = TaskResultStatus.Passed;
                                    return result;
                                }

                                foreach (Service service in device.Services)
                                {
                                    if (service.Name == m_serviceName)
                                    {
                                        result.Text = string.Format ("Found device: {0}, service: {1}", m_deviceName, m_serviceName);
                                        result.Status = TaskResultStatus.Passed;
                                        return result;
                                    }
                                }
                            }
                        }

                        if (OnDeviceLost != null)
                        {
                            OnDeviceLost (this, new JcwEventArgs<string> (m_deviceName));
                            result.Text = string.Format ("Device not available. Raising device lost for: {0}", m_deviceName);
                            result.Status = TaskResultStatus.Passed;
                            return result;
                        }
                    }
                }
                catch (BlueToolsException bte)
                {
                    Error.LastError = result.Text = bte.ToString ();
                    result.Status = TaskResultStatus.Failed;
                }
                catch (Exception e)
                {
                    Error.LastError = result.Text = e.ToString ();
                    result.Status = TaskResultStatus.Failed;
                }
                finally
                {
                    m_statusCheckRunning = false;
                    result.Status = TaskResultStatus.Passed;
                }
            }

            return result;
        }

        public void DisconnectRadio ()
        {
            InitializeRadio ();
        }

        #endregion

        #region Private Methods

        private void RunDiscover (BackgroundWorker worker, DoWorkEventArgs e)
        {
            IJcwTaskResult result = new JcwTaskResult ();
            result.Status = TaskResultStatus.Failed;
            Error.LastError = null;

            try
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Device[] devices = m_network.DiscoverDevices ();
                if (devices.Length == 0)
                {
                    throw new Exception ("No devices found. Verify that " + m_deviceName + " is on and in range");
                }

                bool exactMatch = true;
                if (m_deviceName.Contains ("%"))
                {
                    m_deviceName = m_deviceName.Replace ("%", "");
                    exactMatch = false;
                }

                // go through the devices we found, looking for our gcog
                bool foundOurDevice = false;
                foreach (Device d in devices)
                {
                    RemoteDevice rd = d as RemoteDevice;

                    // verify that the device name we found matches the one we were looking for
                    if ((exactMatch && rd.Name == m_deviceName) || rd.Name.Contains (m_deviceName))
                    {
                        m_device = rd;
                        m_deviceAddress = rd.Address.ToString ();
                        foundOurDevice = true;
                        break;
                    }
                }
                if (!foundOurDevice)
                {
                    throw new Exception ("Could not find device " + m_deviceName);
                }

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // before trying to discover services, enter the pin code                                
                if (!m_device.Bonded)
                {
                    m_device.Bond (m_pinCode);
                }

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // discover all services offered by the device                                          
                Service[] services = m_device.DiscoverServices (ServiceType.SerialPort);

                if (services.Length == 0)
                {
                    // try to discover services a second time
                    services = m_device.DiscoverServices (ServiceType.SerialPort);
                    if (services.Length == 0)
                    {
                        throw new Exception ("No services found");
                    }
                }

                exactMatch = true;
                if (m_serviceName.Contains ("%"))
                {
                    m_serviceName = m_serviceName.Replace ("%", "");
                    exactMatch = false;
                }

                bool foundOurService = false;
                foreach (Service s in services)
                {
                    RemoteService rs = s as RemoteService;

                    // verify that the service name we found matches the one we were looking for
                    if ((exactMatch && rs.Name == m_serviceName) || rs.Name.Contains (m_serviceName))
                    {
                        m_service = rs;
                        m_stream = rs.Stream;
                        m_serviceAddress = rs.Address.ToString ();
                        m_serviceChannel = rs.Address.ServiceChannelNumber;
                        foundOurService = true;
                        break;
                    }
                }

                if (!foundOurService)
                {
                    throw new Exception ("Could not find service " + m_serviceName);
                }

                result.Status = TaskResultStatus.Passed;
                result.Text = "Found bluetooth device";
            }
            catch (ThreadAbortException) { }
            catch (BlueToolsException ex)
            {
                Error.LastError = ex.Message + ex.StackTrace;
                result.Text = Error.LastError;
            }
            catch (Exception ex)
            {
                Error.LastError = ex.Message + ex.StackTrace;
                result.Text = Error.LastError;
            }
            finally
            {
                e.Result = result;
            }
        }

        private void RunDirectConnect (BackgroundWorker worker, DoWorkEventArgs e)
        {
            IJcwTaskResult result = new JcwTaskResult ();
            result.Status = TaskResultStatus.Failed;
            Error.LastError = null;

            try
            {
                do
                {
                    m_device = m_network.ConnectDevice (new Address (m_deviceAddress), m_deviceName);

                    if (m_device == null)
                    {
                        Thread.Sleep (m_bluetoothDirectConnectIntervalMilliseconds);
                    }
                    else
                    {
                        // before trying to discover services, enter the pin code                                
                        if (!m_device.Bonded)
                        {
                            m_device.Bond (m_pinCode);
                        }

                        // this will connect without regard for whether the service exists
                        m_service = m_device.ConnectService (m_serviceChannel, m_serviceName);

                        if (m_service == null)
                        {
                            Thread.Sleep (m_bluetoothDirectConnectIntervalMilliseconds);
                        }
                        else
                        {
                            try
                            {
                                m_stream = m_service.Stream;
                                break;
                            }
                            catch (Exception)
                            {
                                Thread.Sleep (m_bluetoothDirectConnectIntervalMilliseconds);
                            }
                        }
                    }
                }
                while (m_bluetoothStopwatch.Elapsed.Seconds < m_bluetoothDirectConnectTimeoutSeconds);

                if (m_device == null)
                {
                    throw new Exception ("Could not connect to device: " + m_deviceName);
                }
                if (m_service == null)
                {
                    throw new Exception ("Could not connect to service " + m_serviceName);
                }

                result.Status = TaskResultStatus.Passed;
                result.Text = "Direct connect completed successfully";
            }
            catch (ThreadAbortException) { }
            catch (BlueToolsException ex)
            {
                Error.LastError = ex.Message + ex.StackTrace;
                result.Text = Error.LastError;
            }
            catch (Exception ex)
            {
                Error.LastError = ex.Message + ex.StackTrace;
                result.Text = Error.LastError;
            }
            finally
            {
                e.Result = result;
            }
        }

        #endregion
    }
}