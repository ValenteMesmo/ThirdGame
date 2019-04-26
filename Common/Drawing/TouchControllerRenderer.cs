using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    public class TouchControllerRenderer : GameObject
    {
        private const int BONUS_SIZE = 0;
        private const int BONUS_X = -450;
        private const int BONUS_Y = 0;
        private const int BONUS2_X = 400;
        private const int BONUS2_Y = 0;

        public const int BUTTON_TOP_X = 0 + BONUS_X;
        public const int BUTTON_TOP_Y = 0 + BONUS_Y;
        public const int BUTTON_BOT_X = 0 + BONUS_X;
        public const int BUTTON_BOT_Y = (BUTTON_HEIGHT * 2) + BONUS_Y;
        public const int BUTTON_LEFT_X = -BUTTON_WIDTH + BONUS_X;
        public const int BUTTON_LEFT_Y = BUTTON_HEIGHT + BONUS_Y;
        public const int BUTTON_RIGHT_X = BUTTON_WIDTH + BONUS_X;
        public const int BUTTON_RIGHT_Y = BUTTON_HEIGHT + BONUS_Y;

        public const int BUTTON2_TOP_X = 0 + BONUS2_X;
        public const int BUTTON2_TOP_Y = 0 + BONUS2_Y;
        public const int BUTTON2_BOT_X = 0 + BONUS2_X;
        public const int BUTTON2_BOT_Y = (BUTTON_HEIGHT * 2) + BONUS2_Y;
        public const int BUTTON2_LEFT_X = -BUTTON_WIDTH + BONUS2_X;
        public const int BUTTON2_LEFT_Y = BUTTON_HEIGHT + BONUS2_Y;
        public const int BUTTON2_RIGHT_X = BUTTON_WIDTH + BONUS2_X;
        public const int BUTTON2_RIGHT_Y = BUTTON_HEIGHT + BONUS2_Y;

        public const int BUTTON_WIDTH = 100 + BONUS_SIZE;
        public const int BUTTON_HEIGHT = 100 + BONUS_SIZE;

        public const float DEGREE_90 = 1.57f;
        public const float DEGREE_180 = 3.141f;
        public const float DEGREE_270 = 4.713f;

        public TouchControllerRenderer(Camera2d camera, Inputs inputs) : base("Touch Controller Renderer")
        {
            Animation = new AnimationGroup(
                new TogglableAnimation(() => inputs.Direction == Direction.Up,
                    new Animation(
                        new AnimationFrame
                        {
                            Offset = new Vector2(BUTTON_TOP_X + (BUTTON_WIDTH / 2), BUTTON_TOP_Y + (BUTTON_HEIGHT / 2)),
                            Anchor = camera.Pos,
                            Height = BUTTON_HEIGHT,
                            Width = BUTTON_WIDTH,
                            Texture = "dpad",
                            Color = Color.White,
                            RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2)
                        })
                    , new Animation(
                        new AnimationFrame
                        {
                            Offset = new Vector2(BUTTON_TOP_X + (BUTTON_WIDTH / 2), BUTTON_TOP_Y + (BUTTON_HEIGHT / 2)),
                            Anchor = camera.Pos,
                            Height = BUTTON_HEIGHT,
                            Width = BUTTON_WIDTH,
                            Texture = "dpad",
                            Color = Color.Red,
                            RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2)
                        })
                ),
                new TogglableAnimation(() => inputs.Direction == Direction.Down
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON_BOT_X + (BUTTON_WIDTH / 2), BUTTON_BOT_Y + (BUTTON_HEIGHT / 2)),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad",
                        Color = Color.White,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_180

                    })
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON_BOT_X + (BUTTON_WIDTH / 2), BUTTON_BOT_Y + (BUTTON_HEIGHT / 2)),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad",
                        Color = Color.Red,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_180
                    })),
                new TogglableAnimation(() => inputs.Direction == Direction.Left
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON_LEFT_X + (BUTTON_WIDTH / 2), BUTTON_LEFT_Y + (BUTTON_HEIGHT / 2)),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad",
                        Color = Color.White,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_270
                    })
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON_LEFT_X + (BUTTON_WIDTH / 2), BUTTON_LEFT_Y + (BUTTON_HEIGHT / 2)),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad",
                        Color = Color.Red,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_270
                    })),
                new TogglableAnimation(() => inputs.Direction == Direction.Right
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON_RIGHT_X + (BUTTON_WIDTH / 2), BUTTON_RIGHT_Y + (BUTTON_HEIGHT / 2)),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad",
                        Color = Color.White,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_90
                    })
                     , new Animation(new AnimationFrame
                     {
                         Offset = new Vector2(BUTTON_RIGHT_X + (BUTTON_WIDTH / 2), BUTTON_RIGHT_Y + (BUTTON_HEIGHT / 2)),
                         Anchor = camera.Pos,
                         Height = BUTTON_HEIGHT,
                         Width = BUTTON_WIDTH,
                         Texture = "dpad",
                         Color = Color.Red,
                         RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                         Rotation = DEGREE_90
                     })),



                new TogglableAnimation(() => inputs.Direction == Direction.Up,
                    new Animation(
                        new AnimationFrame
                        {
                            Offset = new Vector2(BUTTON2_TOP_X + (BUTTON_WIDTH / 2), BUTTON2_TOP_Y + (BUTTON_HEIGHT / 2)),
                            Anchor = camera.Pos,
                            Height = BUTTON_HEIGHT,
                            Width = BUTTON_WIDTH,
                            Texture = "dpad",
                            Color = Color.White,
                            RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2)
                        })
                    , new Animation(
                        new AnimationFrame
                        {
                            Offset = new Vector2(BUTTON2_TOP_X + (BUTTON_WIDTH / 2), BUTTON2_TOP_Y + (BUTTON_HEIGHT / 2)),
                            Anchor = camera.Pos,
                            Height = BUTTON_HEIGHT,
                            Width = BUTTON_WIDTH,
                            Texture = "dpad",
                            Color = Color.Red,
                            RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2)
                        })
                ),
                new TogglableAnimation(() => inputs.Direction == Direction.Down
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON2_BOT_X + (BUTTON_WIDTH / 2), BUTTON2_BOT_Y + (BUTTON_HEIGHT / 2)),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad",
                        Color = Color.White,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_180

                    })
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON2_BOT_X + (BUTTON_WIDTH / 2), BUTTON2_BOT_Y + (BUTTON_HEIGHT / 2)),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad",
                        Color = Color.Red,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_180
                    })),
                new TogglableAnimation(() => inputs.Direction == Direction.Left
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON2_LEFT_X + (BUTTON_WIDTH / 2), BUTTON2_LEFT_Y + (BUTTON_HEIGHT / 2)),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad",
                        Color = Color.White,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_270
                    })
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON2_LEFT_X + (BUTTON_WIDTH / 2), BUTTON2_LEFT_Y + (BUTTON_HEIGHT / 2)),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad",
                        Color = Color.Red,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_270
                    })),
                new TogglableAnimation(() => inputs.Direction == Direction.Right
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON2_RIGHT_X + (BUTTON_WIDTH / 2), BUTTON2_RIGHT_Y + (BUTTON_HEIGHT / 2)),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad",
                        Color = Color.White,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_90
                    })
                     , new Animation(new AnimationFrame
                     {
                         Offset = new Vector2(BUTTON2_RIGHT_X + (BUTTON_WIDTH / 2), BUTTON2_RIGHT_Y + (BUTTON_HEIGHT / 2)),
                         Anchor = camera.Pos,
                         Height = BUTTON_HEIGHT,
                         Width = BUTTON_WIDTH,
                         Texture = "dpad",
                         Color = Color.Red,
                         RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                         Rotation = DEGREE_90
                     }))
            )
            { RenderOnUiLayer = true };
        }
    }
}
