using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    public class BlockCollisionHandler : CollisionHandler
    {
        public void Bot(Collider Source, Collider target)
        {   
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
        }
    }
}
