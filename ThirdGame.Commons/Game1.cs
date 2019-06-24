using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ThirdGame
{
    public class Game1 : Game
    {
        public static Queue<Rectangle> RectanglesToRender = new Queue<Rectangle>();
        public static Queue<Rectangle> RectanglesToRenderUI = new Queue<Rectangle>();
        public static List<string> LOG = new List<string>();
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteBatch spriteBatchUi;
        public readonly bool RuningOnAndroid;
        private readonly NetworkService UdpWrapper;
        private GameLoop GameLoop;
        private Camera2d Camera;
        private Camera2d CameraUI;
        private Dictionary<string, Texture2D> Sprites = new Dictionary<string, Texture2D>();

        public static Action<int> AndroidVibrate { get; set; } = f => { };

        private SpriteFont SpriteFont;
        private FramerateCounter smartFPS = new FramerateCounter();

        public Game1(NetworkService UdpWrapper, bool RuningOnAndroid = false)
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
                //320;
                graphics.PreferredBackBufferHeight =
                664;
                //768;
                //240;
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
            Sprites.Add("freeze_0", Content.Load<Texture2D>("freeze_0"));
            Sprites.Add("char", Content.Load<Texture2D>("char"));
            Sprites.Add("char_walk", Content.Load<Texture2D>("char_walk"));
            Sprites.Add("char_crouch", Content.Load<Texture2D>("char_crouch"));
            Sprites.Add("char_up", Content.Load<Texture2D>("char_up"));
            Sprites.Add("dpad", Content.Load<Texture2D>("dpad"));
            Sprites.Add("block", Content.Load<Texture2D>("block"));

            var pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
            Sprites.Add("pixel", pixel);

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
                LOG.Clear();
                Camera.Update();
                GameLoop.Update(gameTime.ElapsedGameTime.Milliseconds * 0.05f);
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
LOG: {string.Join("\n", LOG)}"
                    , new Vector2(500, 2000)
                    , Color.Black
                    , 0
                    , Vector2.Zero
                    , 25
                    , SpriteEffects.None
                    , 0);

                for (var i=0;i<GameLoop.GameObjects.Count;i++)
                {
                    var obj = GameLoop.GameObjects[i];
                    foreach (var frame in obj.Animation.GetFrame())
                    {
                        (obj.Animation.RenderOnUiLayer ? spriteBatchUi : spriteBatch).Draw(
                            texture: Sprites[frame.Texture]
                            , destinationRectangle: new Rectangle(
                                (frame.Anchor.Position + frame.Offset).ToPoint()
                                , new Point(frame.Width, frame.Height)
                            )
                            , sourceRectangle: frame.SourceRectangle
                            , color: frame.Color
                            , rotation: frame.Rotation
                            , origin: frame.RotationAnchor
                            , effects: frame.Flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None
                            , layerDepth: 0f
                        );
                    }

                }

                while (RectanglesToRenderUI.Count > 0)
                {
                    var rectangle = RectanglesToRenderUI.Dequeue();
                    DrawBorder(rectangle, 2, Color.Red, spriteBatchUi);
                }

                while (RectanglesToRender.Count > 0)
                {
                    var rectangle = RectanglesToRender.Dequeue();
                    DrawBorder(rectangle, 50, Color.Green, spriteBatch);
                }

                spriteBatch.End();
                spriteBatchUi.End();
            }
            catch //(System.Exception ex)
            {

            }

            base.Draw(gameTime);
        }

        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, SpriteBatch spriteBatch)
        {
            var pixel = Sprites["pixel"];
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder), rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder, rectangleToDraw.Width, thicknessOfBorder), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
