using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jcw.Common.Net.Wifi.NativeWifi
{
    public class WifiAccessPoint
    {
        #region Fields

        private readonly int hashCode;

        #endregion

        #region Static Fields

        public static WifiAccessPoint NotConnected = new WifiAccessPoint (Wifi.NotConnected);

        #endregion

        #region Properties

        public string SSID { get; private set; }
        public Wlan.Dot11BssType BssType { get; private set; }

        #endregion

        public WifiAccessPoint (string ssid)
            : this (ssid, Wlan.Dot11BssType.Infrastructure)
        {
        }

        public WifiAccessPoint (string ssid, Wlan.Dot11BssType bssType)
        {
            BssType = bssType;
            SSID = ssid;

            hashCode = ssid.GetHashCode () ^ bssType.ToString ().GetHashCode ();
        }

        #region Overrides

        public override bool Equals (object obj)
        {
            WifiAccessPoint otherAccessPoint = obj as WifiAccessPoint;
            if (otherAccessPoint != null)
            {
                return (SSID == otherAccessPoint.SSID && BssType == otherAccessPoint.BssType);
            }

            return false;
        }

        public override int GetHashCode ()
        {
            return hashCode;
        }

        public override string ToString ()
        {
            return string.Format ("{0} ({1})", SSID, BssType.ToString ());
        }

        #endregion
    }
}
