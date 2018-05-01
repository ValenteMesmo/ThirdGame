using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Android.Net.Wifi.P2p;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static Android.Net.Wifi.P2p.WifiP2pManager;

namespace ThirdGame
{
    public interface WifiDirect
    {
        Action<string> NewAddressFound { get; set; }
    }

    public class ConnectionInfoListener : IConnectionInfoListener
    {
        public IntPtr Handle { get; }

        public void Dispose()
        {
        }

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

    public class PeerListListener : IPeerListListener
    {
        public IntPtr Handle { get; }
        private readonly WifiP2pManager WifiP2pManager;
        public Channel wifip2pChannel { get; }

        public PeerListListener(WifiP2pManager WifiP2pManager
            , Channel wifip2pChannel)
        {
            this.WifiP2pManager = WifiP2pManager;
            this.wifip2pChannel = wifip2pChannel;
        }

        public void Dispose()
        {

        }

        public void OnPeersAvailable(WifiP2pDeviceList peers)
        {

            foreach (var device in peers.DeviceList)
            {
                WifiP2pConfig config = new WifiP2pConfig();
                config.DeviceAddress = device.DeviceAddress;
                config.Wps.Setup = WpsInfo.Pbc;
                config.GroupOwnerIntent = 4;
                WifiP2pManager.Connect(wifip2pChannel, config, new MyActionListener(() =>
                {
                    //TODO: call handler
                }));
            }
        }
    }

    public class AndroidWifiDirect2 : WifiDirect
        //, IConnectionInfoListener
        //, IPeerListListener
    {
        private readonly WifiP2pManager WifiP2pManager;
        public Channel wifip2pChannel { get; }
        public IntPtr Handle { get; }
        public Action<string> NewAddressFound { get; set; }

        public AndroidWifiDirect2(WifiP2pManager WifiP2pManager
            , Channel wifip2pChannel)
        {
            this.WifiP2pManager = WifiP2pManager;
            this.wifip2pChannel = wifip2pChannel;
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
                    mManager.RequestPeers(mChannel, new PeerListListener(mManager, mChannel));
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

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
