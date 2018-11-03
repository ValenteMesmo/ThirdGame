using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThirdGame
{
    public struct DrawingModel
    {
        public Texture2D Texture { get; set; }
        public Rectangle DestinationRectangle { get; set; }
        public Vector2 CenterOfRotation { get; set; }
    }
}
