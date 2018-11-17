using Microsoft.Xna.Framework;

namespace Common
{
    public class PositionComponent
    {
        public Point Current { get; set; }
        public Point Previous { get; internal set; }
    }
}
