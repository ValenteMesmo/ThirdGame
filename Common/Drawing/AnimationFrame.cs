using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThirdGame
{
    public struct AnimationFrame
    {
        public string Texture { get; set; }
        //public Rectangle DestinationRectangle { get; set; }
        public PositionComponent Anchor { get; set; }
        public Vector2 Offset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DurationInUpdateCount { get; set; }
        public Color Color { get; set; }
    }
}
