using System;
using Microsoft.Xna.Framework;

namespace Common
{
    public class Collider
    {
        private float OffsetX;
        private float OffsetY;
        public float X { get => Parent.Position.X + OffsetX; set => OffsetX = value; }
        public float Y { get => Parent.Position.Y + OffsetY; set => OffsetY = value; }
        public float Width { get; set; }
        public float Height { get; set; }
        public bool Disabled { get; set; }
        public readonly GameObject Parent;

        public Collider(GameObject Parent)
        {
            this.Parent = Parent;
        }

        public Rectangle AsRectangle() => new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
    }
}
