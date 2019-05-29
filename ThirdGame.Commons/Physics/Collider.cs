using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Common
{
    public class CollisionComponent : IHaveColliders
    {
        private readonly Collider[] Colliders;

        public CollisionComponent(params Collider[] Colliders) =>
            this.Colliders = Colliders;

        public IEnumerable<Collider> GetColliders() => Colliders;
    }

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
        public CollisionHandler Collision = NoCollision.Instance;

        public Collider(GameObject Parent)
        {
            this.Parent = Parent;
        }

        public Rectangle AsRectangle() => new Rectangle((int)X, (int)Y, (int)Width, (int)Height);

        public override string ToString() => $"{nameof(Collider)} ({Parent})";
    }
}
