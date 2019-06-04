using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ThirdGame;

namespace Common
{
    public class Collider
    {
        public float OffsetX;
        public float OffsetY;
        public float Width { get; set; }
        public float Height { get; set; }
        public bool Disabled { get; set; }
        public readonly GameObject Parent;
        public CollisionHandler Collision = NoCollision.Instance;

        public Collider(GameObject Parent)
        {
            this.Parent = Parent;
        }

        public Rectangle AsRectangle() => new Rectangle((int)this.RelativeX(), (int)this.RelativeY(), (int)Width, (int)Height);

        public override string ToString() => $"{nameof(Collider)} ({Parent})";
    }
}
