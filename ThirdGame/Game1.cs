using System;
using Android.Content;
using Android.Net.Wifi.P2p;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Android.Net.Wifi.P2p.WifiP2pManager;
using System.Collections.Generic;
using Android.Net.Wifi;

namespace ThirdGame
{
    public class PeerListListener : IPeerListListener
    {
        public IntPtr Handle { get; set; }

        public void Dispose()
        {
        }

        public void OnPeersAvailable(WifiP2pDeviceList peers)
        {
            List<WifiP2pDevice> devices = (new List<WifiP2pDevice>());
            devices.AddRange(peers.DeviceList);
            //olha a lista ai!



        }
    }

    public class WiFiDirectBroadcastReceiver : BroadcastReceiver
    {
        private WifiP2pManager mManager;
        private Channel mChannel;
        private Activity1 mActivity;

        public WiFiDirectBroadcastReceiver(WifiP2pManager manager, Channel channel,
                Activity1 activity) : base()
        {
            this.mManager = manager;
            this.mChannel = channel;
            this.mActivity = activity;
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
                    mManager.RequestPeers(mChannel, new PeerListListener());


                   
            }
            }
            else if (WifiP2pConnectionChangedAction == action)
            {
                // Respond to new connection or disconnections
            }
            else if (WifiP2pThisDeviceChangedAction == action)
            {
                // Respond to this device's wifi state changing
            }
        }
    }


    /// <summary>
    /// This is the main type for your game.
    /// </summary>
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
