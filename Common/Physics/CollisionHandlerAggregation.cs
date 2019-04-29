using Common;

namespace ThirdGame
{
    public class CollisionHandlerAggregation : CollisionHandler
    {
        private readonly CollisionHandler[] collisionHandlers;

        public CollisionHandlerAggregation(params CollisionHandler[] collisionHandlers)
        {
            this.collisionHandlers = collisionHandlers;
        }

        public void Bot(Collider Source, Collider target)
        {
            for (int i = 0; i < collisionHandlers.Length; i++)
                collisionHandlers[i].Bot(Source, target);
        }

        public void Left(Collider Source, Collider target)
        {
            for (int i = 0; i < collisionHandlers.Length; i++)
                collisionHandlers[i].Left(Source, target);
        }

        public void Right(Collider Source, Collider target)
        {
            for (int i = 0; i < collisionHandlers.Length; i++)
                collisionHandlers[i].Right(Source, target);
        }

        public void Top(Collider Source, Collider target)
        {
            for (int i = 0; i < collisionHandlers.Length; i++)
                collisionHandlers[i].Top(Source, target);
        }
    }
}
