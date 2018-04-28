using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net.Wifi.P2p;
using Android.OS;
using Android.Runtime;
using Android.Views;
using System.Collections.Generic;

namespace ThirdGame
{
    public interface WifiDirect
    {
        Func<IEnumerable<WifiP2pDevice>> OnPeersChanged { get; set; }
        void Connect(WifiP2pDevice device);
    }

    public class AndroidWifiDirect2 : WifiDirect
    {
        public Func<IEnumerable<WifiP2pDevice>> OnPeersChanged { get ; set; }

        public void Connect(WifiP2pDevice device)
        {
            
        }
    }

    public class MyActionListener : WifiP2pManager.IActionListener
    {
        public IntPtr Handle { get; set; }

        public void Dispose()
        {
        }

        public void OnFailure([GeneratedEnum] WifiP2pFailureReason reason)
        {
        }

        public void OnSuccess()
        {
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
        private WiFiDirectBroadcastReceiver mReceiver;
        private IntentFilter mIntentFilter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            var mManager = (WifiP2pManager)GetSystemService(WifiP2pService);
            var mChannel = mManager.Initialize(this, MainLooper, null);

            mManager.DiscoverPeers(mChannel, new MyActionListener());

            mReceiver = new WiFiDirectBroadcastReceiver(mManager, mChannel, this);

            mIntentFilter = new IntentFilter();
            mIntentFilter.AddAction(WifiP2pManager.WifiP2pStateChangedAction);
            mIntentFilter.AddAction(WifiP2pManager.WifiP2pPeersChangedAction);
            mIntentFilter.AddAction(WifiP2pManager.WifiP2pConnectionChangedAction);
            mIntentFilter.AddAction(WifiP2pManager.WifiP2pThisDeviceChangedAction);
 RegisterReceiver(mReceiver, mIntentFilter);

            var g = new Game1();
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

