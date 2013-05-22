using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Jcw.Common.Net.Wifi;
using Jcw.Common.Net.Wifi.NativeWifi;

namespace WiFlyTestApp
{
    public partial class Form1 : Form
    {
        #region Constants

        private const int ReadDataPacketSize = 8192;

        #endregion

        #region Properties

        private TcpClient WiFlyClient { get; set; }
        private IPAddress WiFlyIPAddress { get; set; }
        private int WiFlyPortNumber { get; set; }
        private string WiFlyPassword { get; set; }

        private Wifi MyWifi { get; set; }
        private WlanClient.WlanInterface WlanInterface { get; set; }

        #endregion

        #region Constructors

        public Form1 ()
        {
            InitializeComponent ();

            WiFlyIPAddress = IPAddress.Parse (ConfigurationManager.AppSettings["IPAddress"]);
            WiFlyPassword = ConfigurationManager.AppSettings["Password"];
            WiFlyPortNumber = Convert.ToInt32 (ConfigurationManager.AppSettings["PortNumber"]);

            MyWifi = new Wifi ();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing)
            {
                MyWifi.OnActiveConnectionLost -= OnActiveConnectionLostHandler;
                MyWifi.Dispose ();
                if (components != null)
                {
                    components.Dispose ();
                }
            }
            base.Dispose (disposing);
        }

        #endregion

        #region Overrides

        protected override void OnLoad (EventArgs e)
        {
            JoinStatusTextBox.Text = MyWifi.CurrentAccessPoint.SSID;
            base.OnLoad (e);
        }

        #endregion

        #region Event Handlers

        private void JoinSSIDButton_Click (object sender, EventArgs e)
        {
            // GCog-833613AA7F
            string ssidToJoin = SSIDToJoinTextBox.Text;
            Wlan.Dot11BssType bssType = Wlan.Dot11BssType.Infrastructure;
            if (AdHocCheckBox.Checked)
            {
                bssType = Wlan.Dot11BssType.Independent;
            }

            if (MyWifi.JoinWifi (ssidToJoin, bssType))
            {
                MyWifi.OnActiveConnectionLost -= OnActiveConnectionLostHandler;
                MyWifi.OnActiveConnectionLost += OnActiveConnectionLostHandler;
                JoinStatusTextBox.Text = MyWifi.CurrentAccessPoint.SSID;
            }
        }

        private void OnActiveConnectionLostHandler (object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke (new Action<object, EventArgs> (OnActiveConnectionLostHandler), sender, e);
                return;
            }
            JoinStatusTextBox.Text = MyWifi.CurrentAccessPoint.SSID;
        }

        private void DisconnectWifiButton_Click (object sender, EventArgs e)
        {
            MyWifi.OnActiveConnectionLost -= OnActiveConnectionLostHandler;
            if (MyWifi.DisconnectWifi ())
            {
                JoinStatusTextBox.Text = MyWifi.CurrentAccessPoint.SSID;
            }
        }

        private void ConnectToDeviceButton_Click (object sender, EventArgs e)
        {
            ClearResults ();

            if (WiFlyClient == null || WiFlyClient.Client == null || !WiFlyClient.Connected)
            {
                WiFlyClient = new TcpClient ();
                try
                {
                    WiFlyClient.Connect (WiFlyIPAddress, WiFlyPortNumber);
                    NetworkStream stream = WiFlyClient.GetStream ();

                    // After connecting to the WiFly it will write *PASS?* into the stream.
                    Thread.Sleep (500);
                    ReadFromWiFly (stream);

                    // Enter command mode.
                    string password = WiFlyPassword;
                    password += Environment.NewLine;
                    AppendToResults (string.Format ("Sending WiFly command: {0}", password));
                    byte[] passwordWriteBuffer = ASCIIEncoding.ASCII.GetBytes (password);
                    stream.Write (passwordWriteBuffer, 0, passwordWriteBuffer.Length);

                    // After accepting the password it will write AOK into the stream.
                    Thread.Sleep (500);
                    ReadFromWiFly (stream);

                    ConnectStatusTextBox.Text = Wifi.Connected;
                }
                catch (SocketException se)
                {
                    AppendToResults (se.ToString ());
                }
            }
        }

        private void DisconnectDeviceButton_Click (object sender, EventArgs e)
        {
            if (WiFlyClient != null && WiFlyClient.Connected)
            {
                WiFlyClient.Close ();
                ClearResults ();
                ConnectStatusTextBox.Text = Wifi.NotConnected;
            }
        }

        private void RunCommandButton_Click (object sender, EventArgs e)
        {
            string commandToRun = CommandToRunTextBox.Text;
            if (WiFlyClient != null && WiFlyClient.Client != null && WiFlyClient.Connected && !string.IsNullOrEmpty (commandToRun))
            {
                try
                {
                    NetworkStream stream = WiFlyClient.GetStream ();
                    ClearResults ();

                    // Enter command mode.
                    string enterCommandMode = "$$$";
                    AppendToResults (string.Format ("Sending WiFly command: {0}", enterCommandMode));
                    byte[] enterCommandModeWriteBuffer = ASCIIEncoding.ASCII.GetBytes (enterCommandMode);
                    stream.Write (enterCommandModeWriteBuffer, 0, enterCommandModeWriteBuffer.Length);
                    Thread.Sleep (500);
                    ReadFromWiFly (stream);

                    AppendToResults (string.Format ("Sending WiFly command: {0}", commandToRun));
                    commandToRun += Environment.NewLine;
                    byte[] commandWriteBuffer = ASCIIEncoding.ASCII.GetBytes (commandToRun);
                    stream.Write (commandWriteBuffer, 0, commandWriteBuffer.Length);
                    Thread.Sleep (500);
                    ReadFromWiFly (stream);

                    // Exit command mode.
                    string exitCommandMode = "exit" + Environment.NewLine;
                    AppendToResults (string.Format ("Sending WiFly command: {0}", exitCommandMode));
                    byte[] exitCommandModeWriteBuffer = ASCIIEncoding.ASCII.GetBytes (exitCommandMode);
                    stream.Write (exitCommandModeWriteBuffer, 0, exitCommandModeWriteBuffer.Length);
                    Thread.Sleep (500);
                    ReadFromWiFly (stream);
                }
                catch (IOException ex)
                {
                    AppendToResults (ex.ToString ());
                    ConnectStatusTextBox.Text = Wifi.NotConnected;
                    return;
                }
            }
        }

        private void SendDataButton_Click (object sender, EventArgs e)
        {
            string dataToSend = DataToSendTextBox.Text;
            if (WiFlyClient != null && WiFlyClient.Client != null && WiFlyClient.Connected && !string.IsNullOrEmpty (dataToSend))
            {
                try
                {
                    NetworkStream stream = WiFlyClient.GetStream ();
                    ClearResults ();

                    AppendToResults (string.Format ("Sending WiFly command: {0}", dataToSend));
                    byte[] dataToSendWriteBuffer = ASCIIEncoding.ASCII.GetBytes (dataToSend);
                    stream.Write (dataToSendWriteBuffer, 0, dataToSendWriteBuffer.Length);
                    Thread.Sleep (100);
                    ReadFromWiFly (stream);
                }
                catch (IOException ex)
                {
                    AppendToResults (ex.ToString ());
                    ConnectStatusTextBox.Text = Wifi.NotConnected;
                    return;
                }
            }
        }

        #endregion

        #region Private Methods

        private void ClearResults ()
        {
            if (InvokeRequired)
            {
                Invoke (new Action (ClearResults));
                return;
            }

            ResultsTextBox.Text = string.Empty;
        }

        private void AppendToResults (string results)
        {
            if (InvokeRequired)
            {
                Invoke (new Action<string> (AppendToResults), results);
                return;
            }

            string formattedResults = string.Format ("[{0}] {1}{2}", DateTime.Now, results, Environment.NewLine);
            Debug.Write (formattedResults);
            ResultsTextBox.Text += formattedResults;
        }

        private void ReadFromWiFly (NetworkStream stream)
        {
            ReadFromWiFly (stream, true);
        }

        private void ReadFromWiFly (NetworkStream stream, bool writeResults)
        {
            int totalReadByteCount = 0;
            List<byte> totalReadBuffer = new List<byte> ();

            int loopReadByteCount = 0;
            byte[] loopReadBuffer = new byte[ReadDataPacketSize];

            try
            {
                int readAttempts = 0;
                do
                {
                    if (stream.DataAvailable)
                    {
                        // If we found data to read, set the read attempts counter so that we won't 
                        // keep looping after completing the read.
                        readAttempts = 10;

                        while (stream.DataAvailable)
                        {
                            loopReadByteCount = stream.Read (loopReadBuffer, 0, ReadDataPacketSize);
                            if (writeResults)
                            {
                                AppendToResults (string.Format ("Read {0} bytes", loopReadByteCount));
                            }
                            totalReadByteCount += loopReadByteCount;

                            byte[] loopReadBufferWithoutPadding = new byte[loopReadByteCount];
                            Array.Copy (loopReadBuffer, loopReadBufferWithoutPadding, loopReadByteCount);
                            totalReadBuffer.AddRange (loopReadBufferWithoutPadding);

                            Thread.Sleep (50);
                        }
                    }
                    Thread.Sleep (100);
                    readAttempts++;
                }
                while (readAttempts < 10); 

                if (writeResults)
                {
                    AppendToResults (string.Format ("Total {0} bytes", totalReadByteCount));
                }
            }
            catch (SocketException ex)
            {
                AppendToResults (ex.ToString ());
                return;
            }

            if (writeResults && totalReadBuffer.Count > 0)
            {
                string readData = ASCIIEncoding.ASCII.GetString (totalReadBuffer.ToArray ());
                AppendToResults (string.Format ("Data read: {0}", readData));
            }
        }

        #endregion
    }
}
