using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net.Wifi.P2p;
using Android.OS;
using Android.Runtime;
using Android.Views;
using System;

namespace ThirdGame
{
    public class MyActionListener : WifiP2pManager.IActionListener
    {
        private readonly Action OnSuccessHandler;

        public IntPtr Handle { get; set; }

        public MyActionListener(Action OnSuccessHandler)
        {
            this.OnSuccessHandler = OnSuccessHandler;
        }

        public void Dispose()
        {
        }

        public void OnFailure([GeneratedEnum] WifiP2pFailureReason reason)
        {

        }

        public void OnSuccess()
        {
            OnSuccessHandler();
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
            var mChannel = mManager.Initialize(this, MainLooper, null);//mainloop mesmo!?

            mManager.DiscoverPeers(mChannel, new MyActionListener(() =>
            {

            }));

            AndroidWifiDirect2 aa = new AndroidWifiDirect2(mManager, mChannel);
            mReceiver = new WiFiDirectBroadcastReceiver(mManager, mChannel, aa, this);

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

