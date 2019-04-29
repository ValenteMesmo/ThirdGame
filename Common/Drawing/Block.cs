using Common;

namespace ThirdGame
{
    public class Block : GameObject
    {
        public Block() : base($"{nameof(Block)}")
        {
            Colliders = new Collider[] { new Collider(this) {                
                Width = 1000,
                Height = 1000
            } };

            Animation = new Animation(new AnimationFrame(this, "block", 1000, 1000));
        }
    }
}
