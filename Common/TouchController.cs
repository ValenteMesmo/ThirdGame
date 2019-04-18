using System.Collections.Generic;
using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    public class AnimationGroup : AnimationHandler
    {
        private readonly AnimationHandler[] animations;

        public bool RenderOnUiLayer { get; set; }

        public AnimationGroup(params AnimationHandler[] animations)
        {
            this.animations = animations;
        }

        public IEnumerable<AnimationFrame> GetFrame()
        {
            foreach (var animation in animations)
                foreach (var frame in animation.GetFrame())
                    yield return frame;
        }

        public void Update()
        {
            foreach (var animation in animations)
                animation.Update();
        }
    }

    public class TouchControllerRenderer : GameObject
    {
        public TouchControllerRenderer(Camera2d camera, Inputs inputs) : base("Touch Controller")
        {
            Animation = new AnimationGroup(
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(-580, -20),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "dpad_up"
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(-580, 180),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "dpad_down"
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(-680, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "dpad_left"
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(-480, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "dpad_right"
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(380, -20),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_up"
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(380, 180),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_down"
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(280, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_left"
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(480, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_right"
                })
            )
            { RenderOnUiLayer = true };
        }
    }
}
