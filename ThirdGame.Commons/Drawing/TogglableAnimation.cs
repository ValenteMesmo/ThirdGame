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
}
