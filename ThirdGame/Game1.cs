using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ThirdGame
{
    public interface WifiDirect
    {
        Action<string> NewAddressFound { get; set; }
    }

   
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public readonly WifiDirect WifiDirect;
        string addressList = @"Teste:
";
        SpriteFont SpriteFont;

        public Game1(WifiDirect WifiDirect)
        {
            this.WifiDirect = WifiDirect;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            WifiDirect.NewAddressFound += address => {
                addressList += $@"{address}
";
            };
            SpriteFont = Content.Load<SpriteFont>("SpriteFont");
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.DrawString(SpriteFont, addressList,new Vector2(),Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
