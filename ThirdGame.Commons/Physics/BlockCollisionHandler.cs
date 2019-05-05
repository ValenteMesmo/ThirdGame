using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    public class AffectedByGravity : IHandleUpdates
    {
        private readonly GameObject GameObject;

        public AffectedByGravity(GameObject GameObject)
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

    public class Jump : IHandleUpdates
    {
        private readonly Player GameObject;
        private readonly Inputs Inputs;

        public Jump(Player GameObject, Inputs Inputs)
        {
            this.GameObject = GameObject;
            this.Inputs = Inputs;
        }

        public void Update()
        {
            if (Inputs.Jump && GameObject.Grounded)
                GameObject.Velocity.Y = -200;
        }
    }

    public class BlockCollisionHandler : CollisionHandler
    {
        public void BeforeCollisions() { }

        public void Bot(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                Source.Parent.Position.Y = target.Y - Source.Height - 1;
                Source.Parent.Velocity.Y = 0;
            }
        }

        public void Left(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                Source.Parent.Position.X = target.X + target.Width + 1;
                Source.Parent.Velocity.X = 0;
            }
        }

        public void Right(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                Source.Parent.Position.X = target.X - Source.Width - 1;
                Source.Parent.Velocity.X = 0;
            }
        }

        public void Top(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                Source.Parent.Position.Y = target.Y + target.Height + 1;
                Source.Parent.Velocity.Y = 0;
            }
        }
    }
}
