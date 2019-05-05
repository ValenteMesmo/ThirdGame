using System.Collections.Generic;
using Common;

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
}
