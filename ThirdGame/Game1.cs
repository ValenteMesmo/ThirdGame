using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ThirdGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        static List<string> addressList = new List<string>();
        Rectangle btn_Rect = new Rectangle(200, 200, 200, 200);
        Rectangle btn_Rect2 = new Rectangle(300, 900, 200, 200);
        private WifiAndroidWrapper wifiConnector;
        private HotSpotStarter hotSpotStarter;

        internal static void LOG(string name)
        {
            addressList[0] = name;
        }

        SpriteFont SpriteFont;

        public Game1(WifiAndroidWrapper wifiConnector, HotSpotStarter hotSpotStarter)
        {
            this.wifiConnector = wifiConnector;
            this.hotSpotStarter = hotSpotStarter;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        private static object locker = new object();
        private Texture2D Btn_texture;

        protected override void Initialize()
        {
            base.Initialize();
            //WifiManagerWrapper.ToggleWifi(false);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteFont = Content.Load<SpriteFont>("SpriteFont");
            Btn_texture = Content.Load<Texture2D>("btn");
        }

        protected override void UnloadContent()
        {
        }

        string senha = "umasenhaqualquer";
        bool? hosting;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            var touchCollection = TouchPanel.GetState();
            foreach (var touch in touchCollection)
            {
                if (btn_Rect.Contains(touch.Position))
                {
                    hosting = false;
                    break;
                }
                if (btn_Rect2.Contains(touch.Position))
                {
                    hosting = true;
                }
            }

            if (hosting.HasValue)
                if (hosting.Value)
                {
                    hotSpotStarter.Start();
                }
                else
                {
                    addressList.Clear();
                    foreach (var ssid in wifiConnector.GetSsids())
                    {
                        if (ssid.Contains("c0de") || ssid.Contains("aaaaaaa"))
                        {
                            addressList.Add(ssid);
                            //todo: remove
                            wifiConnector.ConnectTo(ssid, "987654321");
                        }
                    }
                }

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(Btn_texture, btn_Rect, Color.White);
            spriteBatch.Draw(Btn_texture, btn_Rect2, Color.Blue);


            lock (locker)
                for (int i = 0; i < addressList.Count; i++)
                {
                    spriteBatch.DrawString(SpriteFont
                        , RemoveSpecialCharacters(addressList[i])
                        , new Vector2(0, 80 * i)
                        , Color.Black
                        , 0, Vector2.Zero
                        , 3
                        , SpriteEffects.None
                        , 0);

                }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
    }
}
