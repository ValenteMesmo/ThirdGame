using Common;
using Common.Interfaces;
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
        private Texture2D Btn_texture;
        private Vector2 otherPlayer;
        private readonly UdpService UdpWrapper;
        MyMessageEncoder MyMessageEncoder = new MyMessageEncoder();
        private Camera2d Camera;
        public readonly bool RuningOnAndroid;

        internal static void LOG(string name)
        {
            addressList[0] = name;
        }

        SpriteFont SpriteFont;
        string message2 = "esperando...";
        public Game1(UdpService UdpWrapper, bool RuningOnAndroid = false)
        {
            this.RuningOnAndroid = RuningOnAndroid;
            this.UdpWrapper = UdpWrapper;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

           
            Camera = new Camera2d();
            Camera.Zoom = 0.15f;


            this.UdpWrapper.Listen(message =>
            {
                var infos = MyMessageEncoder.Decode(message);
                foreach (var info in infos)
                {
                    otherPlayer = info.Value;
                }
            });
        }

        protected override void Initialize()
        {
            base.Initialize();

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            graphics.SynchronizeWithVerticalRetrace = true;
            if (RuningOnAndroid)
            {
                graphics.IsFullScreen = true;
                graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            }
            IsFixedTimeStep = true;

            graphics.ApplyChanges();
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

        protected override void Update(GameTime gameTime)
        {
            Camera.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            var touchCollection = TouchPanel.GetState();

            if (touchCollection.Any() && UdpWrapper != null)
                UdpWrapper.Send(
                    MyMessageEncoder.Encode(
                        Camera.ToWorldLocation(touchCollection[0].Position)
                        , UdpWrapper.myIp
                    )
                );

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                 BlendState.AlphaBlend,
                 null,
                 null,
                 null,
                 null,
                 Camera.GetTransformation(GraphicsDevice));

            spriteBatch.Draw(Btn_texture, new Rectangle(otherPlayer.ToPoint(), new Point(800, 800)), Color.White);


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
