using System.Collections.Generic;
using System.Linq;
using ThirdGame;

namespace Common
{
    internal class NoAnimation : AnimationHandler
    {
        public static NoAnimation Instance { get; } = new NoAnimation();
        private static AnimationFrame[] Empty = new AnimationFrame[0];
        private NoAnimation() { }

        public AnimationFrame[] GetFrame() => Empty;

        public void Update() { }

        public bool ActAsUI() => false;
    }
}
