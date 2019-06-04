using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    public class GravityChangesVerticalSpeed : IHandleUpdates
    {
        private readonly GameObject GameObject;

        public GravityChangesVerticalSpeed(GameObject GameObject)
        {
            this.GameObject = GameObject;
        }

        public void Update()
        {
            GameObject.Velocity.Y += 10;
            if (GameObject.Velocity.Y > 150)
                GameObject.Velocity.Y = 150;
        }
    }

    public class BlockCollisionHandler : CollisionHandler
    {
        public void BeforeCollisions() { }

        public void Bot(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                //TODO: - offsetY
                Source.Parent.Position.Y = target.Top() - Source.Height - 1;
                Source.Parent.Velocity.Y = 0;
            }
        }

        public void Left(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                Source.Parent.Position.X = target.Right()  + 1 - Source.OffsetX;
                Source.Parent.Velocity.X = 0;
            }
        }

        public void Right(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                Source.Parent.Position.X = target.Left() - Source.OffsetX - Source.Width - 1;
                Source.Parent.Velocity.X = 0;
            }
        }

        public void Top(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                //TODO: - offsetY
                Source.Parent.Position.Y = target.Bottom() + target.Height + 1;
                Source.Parent.Velocity.Y = 0;
            }
        }
    }
}
