using System.Collections.Generic;
using Common;

namespace ThirdGame
{
    public class Block : GameObject
    {
        private static int count;
        private readonly IEnumerable<Collider> Colliders;

        public Block() : base($"{nameof(Block)}{count++}")
        {
            Colliders = new Collider(this)
            {
                Width = 1000,
                Height = 1000
            }.Yield();

            Animation = new Animation(new AnimationFrame(this, "block", 1000, 1000));
        }

        public override IEnumerable<Collider> GetColliders()
        {
            return Colliders;
        }
    }
}
