using Android.Net.Wifi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirdGame
{
    public enum ConnectionState {
        disconnected,
        connecting,
        connected
    }

    public class WifiAndroidWrapper 
    {
        private readonly WifiManager WifiManager;

        public WifiAndroidWrapper(WifiManager WifiManager)
        {
            this.WifiManager = WifiManager;
        }

        private DateTime LastScanRequest;
        private const int SCAN_COOLDOWN = 10;
        public IEnumerable<string> GetSsids()
        {
            if (LastScanRequest.AddSeconds(SCAN_COOLDOWN) < DateTime.Now)
            {
                if (WifiManager.IsWifiEnabled == false)
                    WifiManager.SetWifiEnabled(true);

                WifiManager.StartScan();
                LastScanRequest = DateTime.Now.AddSeconds(SCAN_COOLDOWN);
            }
            return WifiManager.ScanResults.Select(f => f.Ssid);
        }

        public const string networkSSID = "aaaaaaaaaaaaa";
        public const string networkPass = "987654321";

        private string quoted(string text)
        {

            return $@"""{text}""";
        }

        private DateTime ConnectionCooldown;
        ConnectionState State;
        public ConnectionState ConnectTo(string ssid, string key)
        {
            if (ConnectionCooldown > DateTime.Now)
                return State;
            ConnectionCooldown = DateTime.Now.AddSeconds(10);

            //TODO: testar esse if
            if (WifiManager.IsWifiEnabled == false)
                WifiManager.SetWifiEnabled(true);

            while (WifiManager.PingSupplicant() == false)
            {
            }

            WifiInfo wifiInfo = WifiManager.ConnectionInfo;
            if (wifiInfo.SupplicantState == SupplicantState.Completed)
            {
                if (wifiInfo.SSID == quoted(ssid))
                {
                    State = ConnectionState.connected;
                    return State;
                }
            }

            int highestPriorityNumber = 0;
            WifiConfiguration selectedConfig = null;
            /* Check if not connected but has connected to that wifi in the past */
            foreach (WifiConfiguration config in WifiManager.ConfiguredNetworks)
            {
                if (config.Priority > highestPriorityNumber)
                    highestPriorityNumber = config.Priority;
                if (config.Ssid == quoted(ssid))
                {
                    selectedConfig = config;
                }
            }

            State = ConnectionState.connecting;

            if (selectedConfig != null)
            {
                selectedConfig.Priority = highestPriorityNumber + 1;
                WifiManager.UpdateNetwork(selectedConfig);
                WifiManager.Disconnect(); /* disconnect from whichever wifi you're connected to */
                WifiManager.EnableNetwork(selectedConfig.NetworkId, true);
                WifiManager.Reconnect();
                
                return State;
            }

            selectedConfig = new WifiConfiguration();
            selectedConfig.Ssid = quoted(ssid);
            selectedConfig.Priority = highestPriorityNumber + 1;
            selectedConfig.StatusField = Android.Net.Wifi.WifiStatus.Enabled;

            selectedConfig.PreSharedKey = quoted(key);
            selectedConfig.AllowedKeyManagement.Set((int)Android.Net.Wifi.KeyManagementType.WpaPsk);
            selectedConfig.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Tkip);
            selectedConfig.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Ccmp);
            selectedConfig.AllowedPairwiseCiphers.Set((int)Android.Net.Wifi.PairwiseCipherType.Tkip);
            selectedConfig.AllowedPairwiseCiphers.Set((int)Android.Net.Wifi.PairwiseCipherType.Ccmp);
            selectedConfig.AllowedProtocols.Set((int)Android.Net.Wifi.ProtocolType.Rsn);
            selectedConfig.AllowedProtocols.Set((int)Android.Net.Wifi.ProtocolType.Wpa);


            int netId = WifiManager.AddNetwork(selectedConfig);
            WifiManager.Disconnect(); /* disconnect from whichever wifi you're connected to */
            WifiManager.EnableNetwork(netId, true);
            WifiManager.Reconnect(); // todo?

            return State;
            //WifiConfiguration conf = new WifiConfiguration();
            //conf.Ssid = "\"" + networkSSID + "\"";
            //conf.PreSharedKey = "\"" + networkPass + "\"";

            //WifiManager.Disconnect();
            //var id = WifiManager.AddNetwork(conf);
            //WifiManager.EnableNetwork(id, true);
            //WifiManager.Reconnect();


            //var aa = WifiManager.ConfiguredNetworks.Select(f => f.Ssid);

            //IEnumerable<WifiConfiguration> list = WifiManager.ConfiguredNetworks;
            //foreach (WifiConfiguration i in list)
            //{
            //    if (i.Ssid != null && i.Ssid.Equals("\"" + networkSSID + "\""))
            //    {
            //        WifiManager.Disconnect();
            //        WifiManager.EnableNetwork(i.NetworkId, true);
            //        WifiManager.Reconnect();

            //        break;
            //    }
            //}


            //WifiConfiguration wfc = new WifiConfiguration();

            //wfc.Ssid = ssid;
            //wfc.StatusField = Android.Net.Wifi.WifiStatus.Disabled;


            //wfc.AllowedProtocols.Set((int)Android.Net.Wifi.ProtocolType.Rsn);
            //wfc.AllowedProtocols.Set((int)Android.Net.Wifi.ProtocolType.Wpa);
            //wfc.AllowedKeyManagement.Set((int)Android.Net.Wifi.KeyManagementType.WpaPsk);
            //wfc.AllowedPairwiseCiphers.Set((int)Android.Net.Wifi.PairwiseCipherType.Ccmp);
            //wfc.AllowedPairwiseCiphers.Set((int)Android.Net.Wifi.PairwiseCipherType.Tkip);
            //wfc.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Wep40);
            //wfc.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Wep104);
            //wfc.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Ccmp);
            //wfc.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Tkip);

            //wfc.PreSharedKey = password;



            //wfc.Priority = 10000;
            //WifiManager.Disconnect();

            //int networkId = WifiManager.AddNetwork(wfc);

            //var id = WifiManager.UpdateNetwork(wfc);
            //WifiManager.SaveConfiguration();
            //WifiManager.EnableNetwork(id, false);
            //WifiManager.Reconnect();



            //var aa = WifiManager.ConfiguredNetworks.Select(f => f.Ssid);
            //if (networkId != -1)
            //{
            //    //WifiManager.Disconnect();
            //    //// success, can call wfMgr.enableNetwork(networkId, true) to connect
            //    //WifiManager.EnableNetwork(networkId, true);

            //    //WifiManager.Reconnect();

            //}

        }
    }
}

