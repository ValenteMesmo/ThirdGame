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
        private readonly Action<WifiConfiguration> setWifiApEnabled;
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
                    setWifiApEnabled = WifiConfiguration => method.Invoke(WifiManager, WifiConfiguration, true);
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

            WifiManager.SetWifiEnabled(false);
        }

        public void Start()
        {
            if (isWifiApEnabled())
                return;

            WifiManager.SetWifiEnabled(false);

            try
            {
                WifiConfiguration WifiConfiguration = new WifiConfiguration();
                WifiConfiguration.Ssid = @"""test""";
                WifiConfiguration.PreSharedKey = @"""pass""";

                WifiConfiguration.AllowedAuthAlgorithms.Set((int)Android.Net.Wifi.AuthAlgorithmType.Shared);
                setWifiApEnabled(WifiConfiguration);

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
            if (WifiManager.IsWifiEnabled == false)
                WifiManager.SetWifiEnabled(true);

            WifiInfo info = WifiManager.ConnectionInfo;
            String ssidAtual = info.SSID;


            WifiManager.SetWifiEnabled(true);

            String networkSSID = @"""test""";
            String networkPass = @"""pass""";

            WifiConfiguration conf = new WifiConfiguration();
            conf.Ssid = networkSSID;
            conf.AllowedKeyManagement.Set((int)Android.Net.Wifi.KeyManagementType.None);

            //conf.WepKeys[0] = "\"" + networkPass + "\"";
            //conf.WepTxKeyIndex = 0;
            //conf.AllowedKeyManagement.Set((int)Android.Net.Wifi.KeyManagementType.None);
            //conf.AllowedGroupCiphers.Set((int)Android.Net.Wifi.GroupCipherType.Wep40);

            conf.PreSharedKey = networkPass;
            var id = WifiManager.AddNetwork(conf);
            //WifiManager.ConfiguredNetworks.Add(conf);

            var ssids = WifiManager.ConfiguredNetworks.Select(f => f.Ssid);
            //foreach (WifiConfiguration i in WifiManager.ConfiguredNetworks)
            //{
            //    if (i.Ssid != null && i.Ssid == networkSSID)
            //    {
            WifiManager.Disconnect();
            WifiManager.EnableNetwork(id, true);
            WifiManager.Reconnect();

            //        break;
            //    }
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

