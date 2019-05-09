using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    public class TouchControllerRenderer : GameObject
    {
        private const int BONUS_SIZE = 0;
        private const int BONUS_X = -560;
        private const int BONUS2_X = 560;
        private const int BONUS_Y = -340;
        private const int BONUS2_Y = -340;

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

        public const int BUTTON_WIDTH = 80 + BONUS_SIZE;
        public const int BUTTON_HEIGHT = 80 + BONUS_SIZE;

        public const int ANIMATION_BONUS = BUTTON_WIDTH/10;

        public const float DEGREE_90 = 0;//1.57f;
        public const float DEGREE_180 = 0;//3.141f;
        public const float DEGREE_270 = 0;//4.713f;

        public TouchControllerRenderer(Camera2d camera, Inputs inputs) : base("Touch Controller Renderer")
        {
            Animation = new AnimationGroup(
                new TogglableAnimation(() => inputs.Direction == DpadDirection.Up ,
                    new Animation(
                        new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                        {
                            Offset = new Vector2(BUTTON_TOP_X + (BUTTON_WIDTH / 2), BUTTON_TOP_Y + (BUTTON_HEIGHT / 2)),
                            RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2)
                        })
                    , new Animation(
                        new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                        {
                            Offset = new Vector2(BUTTON_TOP_X + (BUTTON_WIDTH / 2)+ ANIMATION_BONUS, BUTTON_TOP_Y + (BUTTON_HEIGHT / 2)+ ANIMATION_BONUS),
                            Color = Color.DarkGray,
                            RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2)
                        })
                ),
                new TogglableAnimation(() => inputs.Direction == DpadDirection.Down 
                    , new Animation(new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                    {
                        Offset = new Vector2(BUTTON_BOT_X + (BUTTON_WIDTH / 2), BUTTON_BOT_Y + (BUTTON_HEIGHT / 2)),
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_180

                    })
                    , new Animation(new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                    {
                        Offset = new Vector2(BUTTON_BOT_X + (BUTTON_WIDTH / 2)+ ANIMATION_BONUS, BUTTON_BOT_Y + (BUTTON_HEIGHT / 2) + ANIMATION_BONUS),
                        Color = Color.DarkGray,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_180
                    })),
                new TogglableAnimation(() => inputs.Direction == DpadDirection.Left 
                    , new Animation(new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                    {
                        Offset = new Vector2(BUTTON_LEFT_X + (BUTTON_WIDTH / 2), BUTTON_LEFT_Y + (BUTTON_HEIGHT / 2)),
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_270
                    })
                    , new Animation(new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                    {
                        Offset = new Vector2(BUTTON_LEFT_X + (BUTTON_WIDTH / 2) + ANIMATION_BONUS, BUTTON_LEFT_Y + (BUTTON_HEIGHT / 2) + ANIMATION_BONUS),
                        Color = Color.DarkGray,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_270
                    })),
                new TogglableAnimation(() => inputs.Direction == DpadDirection.Right 
                    , new Animation(new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                    {
                        Offset = new Vector2(BUTTON_RIGHT_X + (BUTTON_WIDTH / 2), BUTTON_RIGHT_Y + (BUTTON_HEIGHT / 2)),
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_90
                    })
                     , new Animation(new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                     {
                         Offset = new Vector2(BUTTON_RIGHT_X + (BUTTON_WIDTH / 2) + ANIMATION_BONUS, BUTTON_RIGHT_Y + (BUTTON_HEIGHT / 2) + ANIMATION_BONUS),
                         Color = Color.DarkGray,
                         RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                         Rotation = DEGREE_90
                     })),



                //new TogglableAnimation(() => inputs.Direction == DpadDirection.Up,
                //    new Animation(
                //        new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                //        {
                //            Offset = new Vector2(BUTTON2_TOP_X + (BUTTON_WIDTH / 2), BUTTON2_TOP_Y + (BUTTON_HEIGHT / 2)),
                //            RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2)
                //        })
                //    , new Animation(
                //        new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                //        {
                //            Offset = new Vector2(BUTTON2_TOP_X + (BUTTON_WIDTH / 2), BUTTON2_TOP_Y + (BUTTON_HEIGHT / 2)),
                //            Color = Color.DarkGray,
                //            RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2)
                //        })
                //),
                new TogglableAnimation(() => inputs.Jump
                    , new Animation(new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                    {
                        Offset = new Vector2(BUTTON2_BOT_X + (BUTTON_WIDTH / 2), BUTTON2_BOT_Y + (BUTTON_HEIGHT / 2)),
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_180

                    })
                    , new Animation(new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                    {
                        Offset = new Vector2(BUTTON2_BOT_X + (BUTTON_WIDTH / 2) + ANIMATION_BONUS, BUTTON2_BOT_Y + (BUTTON_HEIGHT / 2) + ANIMATION_BONUS),
                        Color = Color.DarkGray,
                        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                        Rotation = DEGREE_180
                    }))
                    //,
                //new TogglableAnimation(() => inputs.Direction == DpadDirection.Left
                //    , new Animation(new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                //    {
                //        Offset = new Vector2(BUTTON2_LEFT_X + (BUTTON_WIDTH / 2), BUTTON2_LEFT_Y + (BUTTON_HEIGHT / 2)),
                //        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                //        Rotation = DEGREE_270
                //    })
                //    , new Animation(new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                //    {
                //        Offset = new Vector2(BUTTON2_LEFT_X + (BUTTON_WIDTH / 2), BUTTON2_LEFT_Y + (BUTTON_HEIGHT / 2)),
                //        Color = Color.DarkGray,
                //        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                //        Rotation = DEGREE_270
                //    })),
                //new TogglableAnimation(() => inputs.Direction == DpadDirection.Right
                //    , new Animation(new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                //    {
                //        Offset = new Vector2(BUTTON2_RIGHT_X + (BUTTON_WIDTH / 2), BUTTON2_RIGHT_Y + (BUTTON_HEIGHT / 2)),
                //        RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                //        Rotation = DEGREE_90
                //    })
                //     , new Animation(new AnimationFrame(camera, "dpad", BUTTON_WIDTH, BUTTON_HEIGHT)
                //     {
                //         Offset = new Vector2(BUTTON2_RIGHT_X + (BUTTON_WIDTH / 2), BUTTON2_RIGHT_Y + (BUTTON_HEIGHT / 2)),
                //         Color = Color.DarkGray,
                //         RotationAnchor = new Vector2(BUTTON_WIDTH / 2, BUTTON_HEIGHT / 2),
                //         Rotation = DEGREE_90
                //     }))
            )
            { RenderOnUiLayer = true };
        }
    }
}
