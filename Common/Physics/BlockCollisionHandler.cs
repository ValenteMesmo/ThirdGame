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
        private readonly GameObject GameObject;
        private readonly Inputs Inputs;

        public Jump(GameObject GameObject, Inputs Inputs)
        {
            this.GameObject = GameObject;
            this.Inputs = Inputs;
        }

        public void Update()
        {
            if (Inputs.Jump)
                GameObject.Velocity.Y = -200;
        }
    }

    public class BlockCollisionHandler : CollisionHandler
    {
        public void Bot(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                Source.Parent.Position = new Vector2(Source.Parent.Position.X, target.Y - Source.Height - 1);
                Source.Parent.Velocity = new Vector2(Source.Parent.Velocity.X, 0);
            }
        }

        public void Left(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                Source.Parent.Position = new Vector2(target.X + target.Width + 1, Source.Parent.Position.Y);
                Source.Parent.Velocity = new Vector2(0, Source.Parent.Velocity.Y);
            }
        }

        public void Right(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                Source.Parent.Position = new Vector2(target.X - Source.Width - 1, Source.Parent.Position.Y);
                Source.Parent.Velocity = new Vector2(0, Source.Parent.Velocity.Y);
            }
        }

        public void Top(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                Source.Parent.Position = new Vector2(Source.Parent.Position.X, target.Y + target.Height + 1);
                Source.Parent.Velocity = new Vector2(Source.Parent.Velocity.X, 0);
            }
        }
    }
}
