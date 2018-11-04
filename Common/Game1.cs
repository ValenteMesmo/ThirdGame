using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ThirdGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public readonly bool RuningOnAndroid;

        private readonly GameLoop GameLoop;
        private Camera2d Camera;
        private Texture2D Btn_texture;
        private SpriteFont SpriteFont;
        SmartFramerate smartFPS = new SmartFramerate(5);

        public Game1(UdpService UdpWrapper, bool RuningOnAndroid = false)
        {
            this.RuningOnAndroid = RuningOnAndroid;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Camera = new Camera2d();
            Camera.Zoom = 0.15f;

            GameLoop = new GameLoop(UdpWrapper, Camera);
        }

        protected override void Initialize()
        {
            base.Initialize();

            if (RuningOnAndroid)
            {
                graphics.IsFullScreen = true;
                graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            }
            else
            {
                graphics.IsFullScreen = false;
                graphics.PreferredBackBufferWidth = 1366;
                graphics.PreferredBackBufferHeight = 768;
                graphics.SynchronizeWithVerticalRetrace = true;
            }

            IsFixedTimeStep = true;

            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Btn_texture = Content.Load<Texture2D>("btn");
            SpriteFont = Content.Load<SpriteFont>("SpriteFont");
        }

        protected override void Update(GameTime gameTime)
        {
            smartFPS.Update(gameTime.ElapsedGameTime.TotalSeconds);
            Camera.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            GameLoop.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                Camera.GetTransformation(GraphicsDevice)
            );

            spriteBatch.DrawString(
                SpriteFont
                , smartFPS.framerate.ToString("0000")
                , new Vector2(500, 2000)
                , Color.Black
                , 0
                , Vector2.Zero
                , 25
                , SpriteEffects.None
                , 0);

            for (int i = 0; i < GameLoop.GameObjects.Count; i++)
            {
                var obj = GameLoop.GameObjects[i];
                var rect = new Rectangle(
                        obj.Position.Current.ToPoint()
                        , new Point(800, 800)
                    );

                var origin = new Vector2(50, 50);

                spriteBatch.Draw(
                    Btn_texture
                    , rect
                    , null
                    , Color.White
                    , 0
                    , origin
                    , SpriteEffects.None
                    , 0
                );
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
