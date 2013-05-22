using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Jcw.Common.Interfaces
{
    #region Delegates 

    public delegate void DeviceLost ( object sender, JcwEventArgs<string> jea );

    #endregion

    public interface IBluetooth
    {
        #region Events

        event DeviceLost OnDeviceLost;

        #endregion

        #region Properties

        long BluetoothLastTimeToConnect { get; }

        string PINCode { get; set; }

        string DeviceName { get; set; }
        string DeviceAddress { get; set; }

        string ServiceName { get; set; }
        string ServiceAddress { get; set; }
        int ServiceChannel { get; set; }

        Stream ServiceStream { get; }

        Version LibraryVersion { get; }
        string StackName { get; }
        Version StackVersion { get; }

        #endregion

        #region Methods

        void InitializeRadio ();
        void FindAndConnect ( BackgroundWorker worker, DoWorkEventArgs e );
        void DisconnectRadio ();
        void Dispose ();
        IJcwTaskResult TestConnectionStatus ();

        #endregion
    }
}