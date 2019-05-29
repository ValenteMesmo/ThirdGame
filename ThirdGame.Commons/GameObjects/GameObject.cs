using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Common
{
    public interface IHaveColliders
    {
        IEnumerable<Collider> GetColliders();
    }

    public class GameObject : PositionComponent
    {
        public string Id { get; }        
        public IHandleUpdates Update { get; set; } = NoUpdate.Instance;
        public AnimationHandler Animation { get; set; } = NoAnimation.Instance;
        public IHaveColliders Colliders { get; set; } = NoCollision.Instance;

        public GameObject(string Id) => this.Id = Id;

        internal bool Destroyed;
        public void Destroy() => Destroyed = true;

        public override string ToString() => $"{nameof(GameObject)} - {Id}";
    }
}
