using System.Collections.Generic;
using ThirdGame;

namespace Common
{    
    public class Animation : AnimationHandler
    {
        private readonly AnimationFrame[] Frames;

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
                currentFrame++;
                if (currentFrame > Frames.Length - 1)
                    currentFrame = 0;

                currentFrameDuration = Frames[currentFrame].DurationInUpdateCount;
            }
        }
    }
}
