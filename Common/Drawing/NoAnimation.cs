using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    internal class NoAnimation : AnimationHandler
    {
        public static NoAnimation Instance { get; } = new NoAnimation();
        private static AnimationFrame[] Empty = new AnimationFrame[0];
        private NoAnimation() { }

        public IEnumerable<AnimationFrame> GetFrame() => Empty;

        public void Update() { }

        public bool RenderOnUiLayer => false;
    }
}
