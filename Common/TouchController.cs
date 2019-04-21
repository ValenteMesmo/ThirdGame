﻿using System.Collections.Generic;
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
        public const int BUTTON_TOP_X = -580;
        public const int BUTTON_TOP_Y = -20;
        public const int BUTTON_BOT_X = -580;
        public const int BUTTON_BOT_Y = 180;
        public const int BUTTON_LEFT_X = -680;
        public const int BUTTON_LEFT_Y = 80;
        public const int BUTTON_RIGHT_X = -480;
        public const int BUTTON_RIGHT_Y = 80;
        public const int BUTTON_WIDTH = 200;
        public const int BUTTON_HEIGHT = 200;

        public TouchControllerRenderer(Camera2d camera, Inputs inputs) : base("Touch Controller Renderer")
        {
            Animation = new AnimationGroup(
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(BUTTON_TOP_X, BUTTON_TOP_Y),
                    Anchor = camera.Pos,
                    Height = BUTTON_HEIGHT,
                    Width = BUTTON_WIDTH,
                    Texture = "dpad_up"
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(BUTTON_BOT_X, BUTTON_BOT_Y),
                    Anchor = camera.Pos,
                    Height = BUTTON_WIDTH,
                    Width = BUTTON_HEIGHT,
                    Texture = "dpad_down"
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(BUTTON_LEFT_X, BUTTON_LEFT_Y),
                    Anchor = camera.Pos,
                    Height = BUTTON_HEIGHT,
                    Width = BUTTON_WIDTH,
                    Texture = "dpad_left"
                }),
                new Animation(new AnimationFrame
                {
                    Offset = new Vector2(BUTTON_RIGHT_X, BUTTON_RIGHT_Y),
                    Anchor = camera.Pos,
                    Height = BUTTON_HEIGHT,
                    Width = BUTTON_WIDTH,
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
