using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    public class TouchController : GameObject
    {
        public TouchController(Camera2d camera, Inputs inputs) : base("Touch Controller")
        {

            Animation = new Animation(
                new AnimationFrame
                {
                    Offset = new Vector2(-580, -20),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "dpad_up"
                },
                new AnimationFrame
                {
                    Offset = new Vector2(-580, 180),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "dpad_down"
                },
                new AnimationFrame
                {
                    Offset = new Vector2(-680, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "dpad_left"
                },
                new AnimationFrame
                {
                    Offset = new Vector2(-480, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "dpad_right"
                },


                new AnimationFrame
                {
                    Offset = new Vector2(380, -20),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_up"
                },
                new AnimationFrame
                {
                    Offset = new Vector2(380, 180),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_down"
                },
                new AnimationFrame
                {
                    Offset = new Vector2(280, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_left"
                },
                new AnimationFrame
                {
                    Offset = new Vector2(480, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_right"
                }
                );
            //Update = new TouchControlsUpdate(camera, inputs);
        }
    }
}
