using Microsoft.Xna.Framework;

namespace Common
{

    public class GameObject : IHavePosition
    {
        public string Id { get; }
        public Vector2 Position { get; set; }
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
