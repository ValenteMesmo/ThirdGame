using Microsoft.Xna.Framework;
using System;

namespace Common
{
    public interface CollisionHandler
    {
        void Top(Collider collider);
        void Left(Collider collider);
        void Bot(Collider collider);
        void Right(Collider collider);
    }

    internal class NoCollision : CollisionHandler
    {
        public static NoCollision Instance { get; } = new NoCollision();
        internal static Collider[] Empty = new Collider[0];

        private NoCollision() { }

        public void Bot(Collider collider) { }
        public void Left(Collider collider) { }
        public void Right(Collider collider) { }
        public void Top(Collider collider) { }
    }

    //https://gamedev.stackexchange.com/questions/104954/2d-collision-detection-xna-c
    public enum CollisionDirection
    {
        Horizontal,
        Vertical
    }

    public class GameObject
    {
        public string Id { get; }
        [Obsolete("Não faz mais sentindo... passar o gameobject logo, em vez de passar a positioncomponent")]
        public PositionComponent Position { get; } = new PositionComponent();
        public IHandleUpdates Update = NoUpdate.Instance;
        public AnimationHandler Animation = NoAnimation.Instance;
        public CollisionHandler Collision = NoCollision.Instance;
        public Collider[] Colliders = NoCollision.Empty;
        public Vector2 Velocity;

        internal bool Destroyed;

        public GameObject(string Id) => this.Id = Id;

        public void Destroy() => Destroyed = true;

        public override string ToString() => $"{nameof(GameObject)} - {Id}";
    }
}
