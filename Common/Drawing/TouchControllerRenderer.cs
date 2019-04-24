using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    public class TouchControllerRenderer : GameObject
    {
        private const int BONUS_SIZE = 25;
        private const int BONUS_X = 50;
        private const int BONUS_Y = -100;
        public const int BUTTON_TOP_X = -580 - BONUS_SIZE + BONUS_X;
        public const int BUTTON_TOP_Y = -20 + BONUS_SIZE + BONUS_SIZE + BONUS_SIZE + BONUS_SIZE + BONUS_Y;
        public const int BUTTON_BOT_X = -580 - BONUS_SIZE + BONUS_X;
        public const int BUTTON_BOT_Y = 180 - BONUS_SIZE + BONUS_SIZE + BONUS_SIZE + BONUS_SIZE + BONUS_Y;
        public const int BUTTON_LEFT_X = -680 + BONUS_X;
        public const int BUTTON_LEFT_Y = 80 + BONUS_SIZE + BONUS_SIZE + BONUS_SIZE + BONUS_Y;
        public const int BUTTON_RIGHT_X = -480 - BONUS_SIZE - BONUS_SIZE + BONUS_X;
        public const int BUTTON_RIGHT_Y = 80 + +BONUS_SIZE + BONUS_SIZE + BONUS_SIZE + BONUS_Y;
        public const int BUTTON_WIDTH = 200 - BONUS_SIZE - BONUS_SIZE;
        public const int BUTTON_HEIGHT = 200 - BONUS_SIZE - BONUS_SIZE;

        public TouchControllerRenderer(Camera2d camera, Inputs inputs) : base("Touch Controller Renderer")
        {
            Animation = new AnimationGroup(
                new TogglableAnimation(() => inputs.Direction == Direction.Up,
                    new Animation(
                        new AnimationFrame
                        {
                            Offset = new Vector2(BUTTON_TOP_X, BUTTON_TOP_Y),
                            Anchor = camera.Pos,
                            Height = BUTTON_HEIGHT,
                            Width = BUTTON_WIDTH,
                            Texture = "dpad_up",
                            Color = Color.White
                        })
                    , new Animation(
                        new AnimationFrame
                        {
                            Offset = new Vector2(BUTTON_TOP_X, BUTTON_TOP_Y),
                            Anchor = camera.Pos,
                            Height = BUTTON_HEIGHT,
                            Width = BUTTON_WIDTH,
                            Texture = "dpad_up",
                            Color = Color.Red
                        })
                ),
                new TogglableAnimation(() => inputs.Direction == Direction.Down
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON_BOT_X, BUTTON_BOT_Y),
                        Anchor = camera.Pos,
                        Height = BUTTON_WIDTH,
                        Width = BUTTON_HEIGHT,
                        Texture = "dpad_down",
                        Color = Color.White

                    })
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON_BOT_X, BUTTON_BOT_Y),
                        Anchor = camera.Pos,
                        Height = BUTTON_WIDTH,
                        Width = BUTTON_HEIGHT,
                        Texture = "dpad_down",
                        Color = Color.Red
                    })),
                new TogglableAnimation(() => inputs.Direction == Direction.Left
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON_LEFT_X, BUTTON_LEFT_Y),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad_left",
                        Color = Color.White
                    })
                    , new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON_LEFT_X, BUTTON_LEFT_Y),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad_left",
                        Color = Color.Red
                    })),
                new TogglableAnimation(() => inputs.Direction == Direction.Right
                    ,new Animation(new AnimationFrame
                    {
                        Offset = new Vector2(BUTTON_RIGHT_X, BUTTON_RIGHT_Y),
                        Anchor = camera.Pos,
                        Height = BUTTON_HEIGHT,
                        Width = BUTTON_WIDTH,
                        Texture = "dpad_right",
                        Color = Color.White
                    })
                     , new Animation(new AnimationFrame
                     {
                         Offset = new Vector2(BUTTON_RIGHT_X, BUTTON_RIGHT_Y),
                         Anchor = camera.Pos,
                         Height = BUTTON_HEIGHT,
                         Width = BUTTON_WIDTH,
                         Texture = "dpad_right",
                         Color = Color.Red
                     })),



                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(380, -20),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_up",
                    Color = Color.White
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(380, 180),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_down",
                    Color = Color.White
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(280, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_left",
                    Color = Color.White
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(480, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_right",
                    Color = Color.White
                })
            )
            { RenderOnUiLayer = true };
        }
    }
}
