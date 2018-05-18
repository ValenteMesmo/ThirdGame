using Android.App;
using Android.Content;
using Android.Content.PM;
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
                netConfig.Ssid = "test";
                netConfig.PreSharedKey = "987654321";
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
            catch (Exception e)
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

        public void ConnectTo(string ssid, string password)
        {
            //if (WifiManager.IsWifiEnabled == false)
                WifiManager.SetWifiEnabled(true);

            while (WifiManager.PingSupplicant() == false)
            {
            }

            //usar o codigo antigo, agora que corrigi o ssid

            ssid = "test";
            password = "987654321";

            //WifiConfiguration wifiConfig = new WifiConfiguration();
            //wifiConfig.Ssid = string.Format("\"{0}\"", ssid);
            //wifiConfig.PreSharedKey = string.Format("\"{0}\"", password);
            //wifiConfig.HiddenSSID = false;
            //WifiManager wifiManager = (WifiManager)Application.Context.GetSystemService(Context.WifiService);

            //// Use ID
            //int netId = wifiManager.AddNetwork(wifiConfig);
            //wifiManager.Disconnect();
            //wifiManager.EnableNetwork(netId, true);
            //wifiManager.Reconnect();

   


            WifiConfiguration wfc = new WifiConfiguration();

            wfc.Ssid = ssid;
            wfc.StatusField = Android.Net.Wifi.WifiStatus.Disabled;
            

            wfc.AllowedProtocols.Set((int)Android.Net.Wifi.ProtocolType.Rsn);
            wfc.AllowedProtocols.Set((int)Android.Net.Wifi.ProtocolType.Wpa);
            wfc.AllowedKeyManagement.Set((int)Android.Net.Wifi.KeyManagementType.WpaPsk);
            wfc.AllowedPairwiseCiphers.Set((int)Android.Net.Wifi.PairwiseCipherType.Ccmp);
            wfc.AllowedPairwiseCiphers.Set((int)Android.Net.Wifi.PairwiseCipherType.Tkip);
            wfc.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Wep40);
            wfc.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Wep104);
            wfc.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Ccmp);
            wfc.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Tkip);

            wfc.PreSharedKey = password;



            wfc.Priority = 10000;
            WifiManager.Disconnect();
            
            int networkId = WifiManager.AddNetwork(wfc);

            var id = WifiManager.UpdateNetwork(wfc);
            WifiManager.SaveConfiguration();
            WifiManager.EnableNetwork(id, false);
            WifiManager.Reconnect();

            
            
            var aa = WifiManager.ConfiguredNetworks.Select(f => f.Ssid);
            if (networkId != -1)
            {
                //WifiManager.Disconnect();
                //// success, can call wfMgr.enableNetwork(networkId, true) to connect
                //WifiManager.EnableNetwork(networkId, true);

                //WifiManager.Reconnect();

            }

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

