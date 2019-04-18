using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    public interface AnimationHandler
    {
        IEnumerable<AnimationFrame> GetFrame();
        void Update();
        bool RenderOnUiLayer { get; }
    }
}
