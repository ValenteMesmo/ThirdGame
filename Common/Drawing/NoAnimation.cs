using System.Collections.Generic;
using System.Linq;
using ThirdGame;

namespace Common
{
    internal class NoAnimation : IGetDrawingModels
    {
        public static NoAnimation Instance { get; } = new NoAnimation();

        private NoAnimation() { }

        public IEnumerable<DrawingModel> GetDrawingModels() => Enumerable.Empty<DrawingModel>();
    }
}
