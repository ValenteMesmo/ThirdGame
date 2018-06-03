using Android.Net.Wifi;
using Java.Lang.Reflect;
using System;

namespace ThirdGame
{
    interface HotSpotService
    {
        void Start(string ssid, string password);
        void Stop();
    }

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

        public ConnectionState Start()
        {
            if (isWifiApEnabled())
                return ConnectionState.connected;

            WifiManager.SetWifiEnabled(false);

            try
            {
                WifiConfiguration netConfig = new WifiConfiguration();
                netConfig.Ssid = WifiAndroidWrapper.networkSSID;
                netConfig.PreSharedKey = WifiAndroidWrapper.networkPass;
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

            return ConnectionState.connecting;
        }
    }
}