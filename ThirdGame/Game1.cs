using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ThirdGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        static List<string> addressList = new List<string>();
        private WifiAndroidWrapper wifiConnector;
        private HotSpotStarter hotSpotStarter;
        private Texture2D Btn_texture;
        private Vector2 otherPlayer;
        private UdpAndroidWrapper UdpWrapper;

        internal static void LOG(string name)
        {
            addressList[0] = name;
        }

        SpriteFont SpriteFont;
        string message2 = "esperando...";
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
            createUdpSocket();
        }

        protected override void Initialize()
        {
            base.Initialize();
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

        MyMessageEncoder MyMessageEncoder = new MyMessageEncoder();
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            var touchCollection = TouchPanel.GetState();

            if (touchCollection.Any() && UdpWrapper != null)
                UdpWrapper.Send(
                    MyMessageEncoder.Encode(
                        new Vector2(
                            touchCollection[0].Position.X
                            , touchCollection[0].Position.Y
                        )
                        , UdpWrapper.myIp
                    )
                );

            base.Update(gameTime);
        }

        private void createUdpSocket()
        {
            if (UdpWrapper == null)
                this.UdpWrapper = new UdpAndroidWrapper(message =>
                {
                    var infos = MyMessageEncoder.Decode(message);
                    foreach (var info in infos)
                    {
                        otherPlayer = info.Value;
                    }
                });
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(Btn_texture, new Rectangle(otherPlayer.ToPoint(), new Point(200, 200)), Color.White);


            spriteBatch.DrawString(SpriteFont
                        , message2
                        , new Vector2(0, 80)
                        , Color.Black
                        , 0, Vector2.Zero
                        , 3
                        , SpriteEffects.None
                        , 0);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
    }
}
