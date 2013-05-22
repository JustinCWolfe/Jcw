using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Jcw.Common.Net.Wifi.NativeWifi;

namespace Jcw.Common.Net.Wifi
{
    public static class WifiHelper
    {
        public static string GetAdhocProfileXmlForSSID (string ssid)
        {
            return string.Format ("<?xml version=\"1.0\"?> <WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\"> <name>{0}</name> <SSIDConfig> <SSID> <name>{0}</name> </SSID> </SSIDConfig> <connectionType>IBSS</connectionType> <connectionMode>manual</connectionMode> <MSM> <security> <authEncryption> <authentication>open</authentication> <encryption>none</encryption> <useOneX>false</useOneX> </authEncryption> </security> </MSM> </WLANProfile>", ssid);
        }

        /// <summary>
        /// Converts a 802.11 SSID to a string.
        /// </summary>
        public static string GetStringForSSID (Wlan.Dot11Ssid ssid)
        {
            return Encoding.ASCII.GetString (ssid.SSID, 0, (int)ssid.SSIDLength);
        }

        /// <summary>
        /// Converts a string SSID to an 802.11 structure.
        /// </summary>
        public static Wlan.Dot11Ssid GetSSIDForString (string ssid)
        {
            Wlan.Dot11Ssid ssidStructure;
            byte[] ssidBytes = new byte[32];
            byte[] encodedSsidName = Encoding.ASCII.GetBytes (ssid);
            encodedSsidName.CopyTo (ssidBytes, 0);
            ssidStructure.SSID = ssidBytes;
            ssidStructure.SSIDLength = (uint)encodedSsidName.Length;
            return ssidStructure;
        }

        /// <summary>
        /// Retrieves XML configurations of existing profiles.
        /// This can assist you in constructing your own XML configuration (that is, it will give you an example to follow).
        /// </summary>
        /// <param name="wlanIface"></param>
        /// <returns></returns>
        public static string GetWlanProfileXml (WlanClient.WlanInterface wlanIface)
        {
            StringBuilder profileXml = new StringBuilder ();

            Wlan.WlanProfileInfo[] profiles = wlanIface.GetProfiles ();
            foreach (Wlan.WlanProfileInfo profileInfo in profiles)
            {
                // This is typically the network's SSID.
                string name = profileInfo.profileName;
                string xml = wlanIface.GetProfileXml (name);
                Debug.WriteLine (string.Format ("Found Wifi profile for SSID {0}.", name));
                Debug.WriteLine (string.Format ("Wifi profile xml {0}.", xml));
                profileXml.AppendLine (xml);
            }

            return profileXml.ToString ();
        }
    }
}
