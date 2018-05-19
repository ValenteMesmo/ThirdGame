using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.Net.Wifi;
using Android.OS;
using Android.Views;
using Java.Lang.Reflect;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirdGame
{
    public class HotSpotStarter
    {
        private readonly WifiManager WifiManager;
        private readonly Action<WifiConfiguration, bool> setWifiApEnabled;
        private readonly Func<bool> isWifiApEnabled;
        private readonly Func<int> getWifiApState;

        public HotSpotStarter(WifiManager WifiManager)
        {
            this.WifiManager = WifiManager;
            Method[] methods = WifiManager.Class.GetDeclaredMethods();
            foreach (Method method in methods)
            {
                if (method.Name == "setWifiApEnabled")
                {
                    setWifiApEnabled = (WifiConfiguration, enabled) => method.Invoke(WifiManager, WifiConfiguration, enabled);
                }

                if (method.Name == "isWifiApEnabled")
                {
                    isWifiApEnabled = () => (bool)method.Invoke(WifiManager);
                }

                if (method.Name == "getWifiApState")
                {
                    getWifiApState = () => (int)method.Invoke(WifiManager);
                }
            }

            setWifiApEnabled(new WifiConfiguration(), false);
        }

        public void Start()
        {
            if (isWifiApEnabled())
                return;

            WifiManager.SetWifiEnabled(false);

            try
            {
                WifiConfiguration netConfig = new WifiConfiguration();
                netConfig.Ssid = WifiConnector.networkSSID;
                netConfig.PreSharedKey = WifiConnector.networkPass;
                netConfig.HiddenSSID = false;
                netConfig.StatusField = Android.Net.Wifi.WifiStatus.Enabled;
                netConfig.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Tkip);
                netConfig.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Ccmp);
                netConfig.AllowedKeyManagement.Set((int)Android.Net.Wifi.KeyManagementType.WpaPsk);
                netConfig.AllowedPairwiseCiphers.Set((int)Android.Net.Wifi.PairwiseCipherType.Tkip);
                netConfig.AllowedPairwiseCiphers.Set((int)Android.Net.Wifi.PairwiseCipherType.Ccmp);
                netConfig.AllowedProtocols.Set((int)Android.Net.Wifi.ProtocolType.Rsn);
                netConfig.AllowedProtocols.Set((int)Android.Net.Wifi.ProtocolType.Wpa);

                setWifiApEnabled(netConfig, true);

                while (!(Boolean)isWifiApEnabled())
                {
                };

                var status = getWifiApState();
            }
            catch (Exception)
            {
            }
        }
    }

    public class WifiConnector
    {
        private readonly WifiManager WifiManager;

        public WifiConnector(WifiManager WifiManager)
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
        public void ConnectTo(string ssid, string key)
        {
            if (ConnectionCooldown > DateTime.Now)
                return;
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
                    return;
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

            if (selectedConfig != null)
            {
                selectedConfig.Priority = highestPriorityNumber + 1;
                WifiManager.UpdateNetwork(selectedConfig);
                WifiManager.Disconnect(); /* disconnect from whichever wifi you're connected to */
                WifiManager.EnableNetwork(selectedConfig.NetworkId, true);
                WifiManager.Reconnect();

                return;
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

    [Activity(Label = "ThirdGame"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.FullUser
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var WifiManager = (WifiManager)GetSystemService(WifiService);

            var g = new Game1(
                new WifiConnector(WifiManager)
                , new HotSpotStarter(WifiManager)
            );

            SetContentView((View)g.Services.GetService(typeof(View)));

            g.Run();
        }

        protected override void OnResume()
        {
            base.OnResume();

        }

        protected override void OnPause()
        {
            base.OnPause();
        }
    }
}

