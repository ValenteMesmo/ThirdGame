using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Common
{
    public interface TouchInputs
    {
        void Update();
        IEnumerable<Vector2> GetTouchCollection();
    }
}
