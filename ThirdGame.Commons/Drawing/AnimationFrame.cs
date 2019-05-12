using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    public class AnimationFrame
    {
        public AnimationFrame(
            PositionComponent Anchor
            , string TextureName
            , int Width
            , int Height
            , Color? TextureColor = null
            , Rectangle? SourceRectangle = null)
        {
            if (TextureColor.HasValue)
                Color = TextureColor.Value;
            else
                Color = Color.White;

            this.Width = Width;
            this.Height = Height;
            this.Anchor = Anchor;
            this.SourceRectangle = SourceRectangle;
            Texture = TextureName;
        }

        public string Texture { get; }
        public Vector2 RotationAnchor;
        public PositionComponent Anchor { get;  }
        public Vector2 Offset;
        public int Width { get; }
        public int Height { get; }
        public int DurationInUpdateCount { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public bool Flipped { get; set; }
        public Rectangle? SourceRectangle { get;  set; }
    }
}
