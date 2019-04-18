using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ThirdGame
{
    public class Game1 : Game
    {
        public static string LOG;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteBatch spriteBatchUi;
        public readonly bool RuningOnAndroid;
        private readonly UdpService UdpWrapper;
        private GameLoop GameLoop;
        private Camera2d Camera;
        private Camera2d CameraUI;
        private Dictionary<string, Texture2D> Sprites = new Dictionary<string, Texture2D>();
        private SpriteFont SpriteFont;
        FramerateCounter smartFPS = new FramerateCounter();

        public Game1(UdpService UdpWrapper, bool RuningOnAndroid = false)
        {
            this.RuningOnAndroid = RuningOnAndroid;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Camera = new Camera2d();
            CameraUI = new Camera2d();
            Camera.Zoom = 0.05f;
            CameraUI.Zoom = 1f;
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
                //graphics.SynchronizeWithVerticalRetrace = false;
            }
            else
            {
                //TODO: alt enter toggle fullscreen
                graphics.IsFullScreen = false;
                graphics.PreferredBackBufferWidth =
                    1176;
                //1366;
                graphics.PreferredBackBufferHeight =
                    664;
                //768;
                graphics.SynchronizeWithVerticalRetrace = true;
            }

            IsMouseVisible = true;

            IsFixedTimeStep = true;
            //IsFixedTimeStep = false;
            //graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchUi = new SpriteBatch(GraphicsDevice);
            Sprites.Add("char", Content.Load<Texture2D>("char"));
            Sprites.Add("char_walk", Content.Load<Texture2D>("char_walk"));
            Sprites.Add("btn_up", Content.Load<Texture2D>("btn_up"));
            Sprites.Add("btn_down", Content.Load<Texture2D>("btn_down"));
            Sprites.Add("btn_left", Content.Load<Texture2D>("btn_left"));
            Sprites.Add("btn_right", Content.Load<Texture2D>("btn_right"));
            Sprites.Add("dpad_up", Content.Load<Texture2D>("dpad_up"));
            Sprites.Add("dpad_down", Content.Load<Texture2D>("dpad_down"));
            Sprites.Add("dpad_left", Content.Load<Texture2D>("dpad_left"));
            Sprites.Add("dpad_right", Content.Load<Texture2D>("dpad_right"));
            SpriteFont = Content.Load<SpriteFont>("SpriteFont");

            GameLoop = new GameLoop(UdpWrapper, Camera, CameraUI);
        }

        //double timer;
        protected override void Update(GameTime gameTime)
        {
            //timer += gameTime.ElapsedGameTime.TotalSeconds;
            //float updateTime = 1f / 60;

            //while (timer >= updateTime)
            //{
            try
            {

                Camera.Update();
                GameLoop.Update();
            }
            catch (System.Exception ex)
            {

            }

            // timer -= updateTime;
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            try
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
                spriteBatchUi.Begin(
                    SpriteSortMode.BackToFront,
                    BlendState.AlphaBlend,
                    null,
                    null,
                    null,
                    null,
                    CameraUI.GetTransformation(GraphicsDevice)
                );

                spriteBatch.DrawString(
                    SpriteFont
                    , $@"FPS: {smartFPS.AverageFramesPerSecond}
LOG: {LOG}"
                    , new Vector2(500, 2000)
                    , Color.Black
                    , 0
                    , Vector2.Zero
                    , 25
                    , SpriteEffects.None
                    , 0);

                foreach(var obj in GameLoop.GameObjects)
                {
                    foreach (var frame in obj.Animation.GetFrame())
                    {
                        (obj.Animation.RenderOnUiLayer ? spriteBatchUi : spriteBatch).Draw(
                        Sprites[frame.Texture]
                        , new Rectangle(
                            (frame.Anchor.Current + frame.Offset).ToPoint()
                            , new Point(frame.Width, frame.Height)
                        )
                        , null
                        , Color.White
                        , 0
                        , Vector2.Zero//draws[j].CenterOfRotation.Current
                        , SpriteEffects.None
                        , 0
                    );
                    }

                }
                spriteBatch.End();
                spriteBatchUi.End();
            }
            catch (System.Exception ex)
            {

            }
            base.Draw(gameTime);
        }
    }
}
