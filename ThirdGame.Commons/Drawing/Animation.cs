using System;
using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    public class Animation : AnimationHandler
    {
        private readonly AnimationFrame[] Frames;
        public bool Loop { get; set; } = true;

        public Animation(params AnimationFrame[] Frames)
        {
            this.Frames = Frames;

            currentFrameDuration = Frames[0].DurationInUpdateCount;
        }

        public bool RenderOnUiLayer { get; set; }

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
                if (currentFrame + 1 > Frames.Length - 1)
                {
                    if (Loop)
                        currentFrame = 0;
                }
                else
                    currentFrame++;

                currentFrameDuration = Frames[currentFrame].DurationInUpdateCount;
            }
        }

        internal void Reset()
        {
            currentFrame = 0;
            currentFrameDuration = Frames[currentFrame].DurationInUpdateCount;
        }
    }
}
