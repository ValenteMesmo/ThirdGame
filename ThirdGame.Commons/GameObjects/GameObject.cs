using Microsoft.Xna.Framework;

namespace Common
{
    public class GameObject : PositionComponent
    {
        public string Id { get; }        
        public IHandleUpdates Update { get; set; } = NoUpdate.Instance;
        public AnimationHandler Animation { get; set; } = NoAnimation.Instance;
        public Collider[] Colliders { get; set; } = NoCollision.Empty;

        public GameObject(string Id) => this.Id = Id;

        internal bool Destroyed;
        public void Destroy() => Destroyed = true;

        public override string ToString() => $"{nameof(GameObject)} - {Id}";
    }
}
