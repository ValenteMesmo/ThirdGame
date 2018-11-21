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
        private readonly UdpService UdpWrapper;
        private GameLoop GameLoop;
        private Camera2d Camera;
        private Texture2D Btn_texture;
        private SpriteFont SpriteFont;
        FramerateCounter smartFPS = new FramerateCounter();

        public Game1(UdpService UdpWrapper, bool RuningOnAndroid = false)
        {
            this.RuningOnAndroid = RuningOnAndroid;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Camera = new Camera2d();
            Camera.Zoom = 0.05f;
            this.UdpWrapper = UdpWrapper;
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
                //TODO: alt enter toggle fullscreen
                graphics.IsFullScreen = false;
                graphics.PreferredBackBufferWidth = 1366;
                graphics.PreferredBackBufferHeight = 768;
                graphics.SynchronizeWithVerticalRetrace = true;
            }

            IsFixedTimeStep = true;
            //TargetElapsedTime = TimeSpan.FromTicks(333333);

            //graphics.PreparingDeviceSettings += (s, e) => e.GraphicsDeviceInformation.PresentationParameters.PresentationInterval = PresentInterval.Two;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Btn_texture = Content.Load<Texture2D>("char");
            SpriteFont = Content.Load<SpriteFont>("SpriteFont");


            GameLoop = new GameLoop(UdpWrapper, Camera, Btn_texture);
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
            smartFPS.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
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
                , string.Format("FPS: {0}", smartFPS.AverageFramesPerSecond)
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
               

                var draws = obj.Animation.GetFrame();
                for (int j = 0; j < draws.Length; j++)
                {
                    spriteBatch.Draw(
                    draws[j].Texture
                    , draws[j].DestinationRectangle
                    , null
                    , Color.White
                    , 0
                    , draws[j].CenterOfRotation
                    , SpriteEffects.None
                    , 0
                );
                }

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
