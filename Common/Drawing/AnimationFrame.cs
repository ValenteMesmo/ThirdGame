using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    public class AnimationFrame
    {
        public AnimationFrame(IHavePosition Anchor, string TextureName, int Width, int Height, Color? TextureColor = null)
        {
            if (TextureColor.HasValue)
                Color = TextureColor.Value;
            else
                Color = Color.White;

            this.Width = Width;
            this.Height = Height;
            this.Anchor = Anchor;
            Texture = TextureName;
        }

        public string Texture { get; }        
        public Vector2 RotationAnchor { get; set; }
        public IHavePosition Anchor { get;  }
        public Vector2 Offset { get; set; }
        public int Width { get; }
        public int Height { get; }
        public int DurationInUpdateCount { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; internal set; }
    }
}
