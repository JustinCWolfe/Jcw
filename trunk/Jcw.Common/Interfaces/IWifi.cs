using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Jcw.Common.Net.Wifi;
using Jcw.Common.Net.Wifi.NativeWifi;

namespace Jcw.Common.Interfaces
{
    #region Delegates

    public delegate void ActiveConnectionLost (object sender, EventArgs ea);

    #endregion

    public interface IWifi
    {
        #region Events

        event ActiveConnectionLost OnActiveConnectionLost;

        #endregion

        #region Properties

        WifiAccessPoint CurrentAccessPoint { get; }

        #endregion

        #region Methods

        bool DisconnectWifi ();
        bool JoinWifi (string ssidToJoin, Wlan.Dot11BssType bssType);
        void JoinWifi (string ssidToJoin, Wlan.Dot11BssType bssType, BackgroundWorker worker, DoWorkEventArgs e);
        void Dispose ();

        #endregion
    }
}
