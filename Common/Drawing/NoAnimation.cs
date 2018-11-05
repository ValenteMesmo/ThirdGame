using System.Collections.Generic;
using System.Linq;
using ThirdGame;

namespace Common
{
    internal class NoAnimation : IGetDrawingModels
    {
        public static NoAnimation Instance { get; } = new NoAnimation();
        private static DrawingModel[] Empty = new DrawingModel[0];
        private NoAnimation() { }

        public DrawingModel[] GetDrawingModels() => Empty;

        public void Update() { }
    }
}
