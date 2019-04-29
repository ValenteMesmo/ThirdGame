using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    public struct AnimationFrame
    {
        public string Texture { get; set; }        
        public Vector2 RotationAnchor { get; set; }
        public IHavePosition Anchor { get; set; }
        public Vector2 Offset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DurationInUpdateCount { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; internal set; }
    }
}
