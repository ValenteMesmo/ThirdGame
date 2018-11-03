using Microsoft.Xna.Framework;

namespace Common
{
    public class PositionComponent
    {
        public Vector2 Current { get; set; }
        public Vector2 Previous { get; internal set; }

        internal static PositionComponent NoPosition { get; } = new PositionComponent();
    }
}
