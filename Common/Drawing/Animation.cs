using System;
using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    public class TogglableAnimation : AnimationHandler
    {
        private readonly Func<bool> Condition;
        private readonly AnimationHandler A;
        private readonly AnimationHandler B;
        private bool toggle;

        public bool RenderOnUiLayer { get; set; }

        public TogglableAnimation(Func<bool> Condition, AnimationHandler A, AnimationHandler B)
        {
            this.Condition = Condition;
            this.A = A;
            this.B = B;
        }

        public IEnumerable<AnimationFrame> GetFrame()
        {
            if (toggle)
                return B.GetFrame();
            else
                return A.GetFrame();
        }

        public void Update()
        {
            toggle = Condition();
        }
    }

    public class Animation : AnimationHandler
    {
        private readonly AnimationFrame[] Frames;

        public Animation(params AnimationFrame[] Frames)
        {
            this.Frames = Frames;

            currentFrameDuration = Frames[0].DurationInUpdateCount;
        }

        public bool RenderOnUiLayer => true;

        public IEnumerable<AnimationFrame> GetFrame()
        {
            yield return Frames[currentFrame];
        }

        private int currentFrame = 0;
        private int currentFrameDuration = 0;
               
        public void Update()
        {
            if (currentFrameDuration > 0)
                currentFrameDuration--;
            else
            {
                currentFrame++;
                if (currentFrame > Frames.Length - 1)
                    currentFrame = 0;

                currentFrameDuration = Frames[currentFrame].DurationInUpdateCount;
            }
        }
    }
}
