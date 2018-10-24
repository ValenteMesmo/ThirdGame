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
            Btn_texture = Content.Load<Texture2D>("btn");
        }

        protected override void Update(GameTime gameTime)
        {
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

            GameLoop.ForeachOtherPlayer((otherIp, otherPosition) =>
            {
                var rect = new Rectangle(
                   otherPosition.ToPoint()
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
            });

            {
                var rect = new Rectangle(
                        GameLoop.playerPosition.ToPoint()
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
