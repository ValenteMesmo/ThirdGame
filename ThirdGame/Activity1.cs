using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.Net.Wifi;
using Android.Net.Wifi.P2p;
using Android.OS;
using Android.Runtime;
using Android.Views;
using System;
using static Android.Net.Wifi.P2p.WifiP2pManager;

namespace ThirdGame
{
    public class MyActionListener : Java.Lang.Object, WifiP2pManager.IActionListener
    {
        private readonly Action OnSuccessHandler;

        public MyActionListener(Action OnSuccessHandler)
        {
            this.OnSuccessHandler = OnSuccessHandler;
        }
        
        public void OnFailure(WifiP2pFailureReason reason)
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
            var aa = new AndroidWifiDirect2();
            mReceiver = new WiFiDirectBroadcastReceiver(mManager, mChannel, aa, this);

            mManager.DiscoverPeers(mChannel, new MyActionListener(()=> {

            }));


            mIntentFilter = new IntentFilter();
            mIntentFilter.AddAction(WifiP2pManager.WifiP2pStateChangedAction);
            mIntentFilter.AddAction(WifiP2pManager.WifiP2pPeersChangedAction);
            mIntentFilter.AddAction(WifiP2pManager.WifiP2pConnectionChangedAction);
            mIntentFilter.AddAction(WifiP2pManager.WifiP2pThisDeviceChangedAction);
            RegisterReceiver(mReceiver, mIntentFilter);



            var g = new Game1(aa);
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
    public class ConnectionInfoListener : Java.Lang.Object, IConnectionInfoListener
    {

        public void OnConnectionInfoAvailable(WifiP2pInfo info)
        {
            // InetAddress from WifiP2pInfo struct.
            var groupOwnerAddress = info.GroupOwnerAddress.HostAddress;

            // After the group negotiation, we can determine the group owner
            // (server).
            if (info.GroupFormed && info.IsGroupOwner)
            {
                // Do whatever tasks are specific to the group owner.
                // One common case is creating a group owner thread and accepting
                // incoming connections.
            }
            else if (info.GroupFormed)
            {
                // The other device acts as the peer (client). In this case,
                // you'll want to create a peer thread that connects
                // to the group owner.
            }
        }
    }

    public class PeerListListener : Java.Lang.Object,  IPeerListListener
    {
        private readonly WifiP2pManager WifiP2pManager;
        private readonly Func<Action<string>> GetHandler;

        public Channel wifip2pChannel { get; }

        public PeerListListener(
            WifiP2pManager WifiP2pManager
            , Channel wifip2pChannel
            , Func<Action<string>> GetHandler)
        {
            this.WifiP2pManager = WifiP2pManager;
            this.wifip2pChannel = wifip2pChannel;
            this.GetHandler = GetHandler;
        }

        public void OnPeersAvailable(WifiP2pDeviceList peers)
        {

            foreach (var device in peers.DeviceList)
            {
                WifiP2pConfig config = new WifiP2pConfig();
                config.DeviceAddress = device.DeviceAddress;
                config.Wps.Setup = WpsInfo.Pbc;
                config.GroupOwnerIntent = 4;
                WifiP2pManager.Connect(wifip2pChannel, config, new MyActionListener(
                    () =>
                {
                    GetHandler()(config.DeviceAddress);
                }
                )
                );
            }
        }
    }

    public class AndroidWifiDirect2 : WifiDirect
    {
        public Action<string> NewAddressFound { get; set; } = _ => { };

        public AndroidWifiDirect2()
        {

        }
    }

    public class WiFiDirectBroadcastReceiver : BroadcastReceiver
    {
        private WifiP2pManager mManager;
        private Channel mChannel;
        private Activity mActivity;

        public AndroidWifiDirect2 myWrapper { get; }

        public WiFiDirectBroadcastReceiver(
            WifiP2pManager manager
            , Channel channel
            , AndroidWifiDirect2 myWrapper
            , Activity activity) : base()
        {
            this.mManager = manager;
            this.mChannel = channel;
            this.mActivity = activity;
            this.myWrapper = myWrapper;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var action = intent.Action;

            if (WifiP2pStateChangedAction == action)
            {
                // Check to see if Wi-Fi is enabled and notify appropriate activity
                int state = intent.GetIntExtra(WifiP2pManager.ExtraWifiState, -1);
                if (state == (int)Android.Net.Wifi.P2p.WifiP2pState.Enabled)
                {
                    // Wifi P2P is enabled
                }
                else
                {
                    // Wi-Fi P2P is not enabled
                }
            }
            else if (WifiP2pPeersChangedAction == action)
            {
                // request available peers from the wifi p2p manager. This is an
                // asynchronous call and the calling activity is notified with a
                // callback on PeerListListener.onPeersAvailable() of passed activity
                // the activity implements the listener interface
                if (mManager != null)
                {
                    mManager.RequestPeers(
                        mChannel
                        , new PeerListListener(
                            mManager
                            , mChannel
                            , () => myWrapper.NewAddressFound));
                }
            }
            else if (WifiP2pConnectionChangedAction == action)
            {
                // Respond to new connection or disconnections
                NetworkInfo networkInfo = (NetworkInfo)intent
                    .GetParcelableExtra(WifiP2pManager.ExtraNetworkInfo);

                if (networkInfo.IsConnected)
                {

                    // We are connected with the other device, request connection
                    // info to find group owner IP

                    mManager.RequestConnectionInfo(mChannel, new ConnectionInfoListener());
                }
            }
            else if (WifiP2pThisDeviceChangedAction == action)
            {
                // Respond to this device's wifi state changing
            }
        }
    }

}

