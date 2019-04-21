namespace Common
{
    public class Collider
    {
        public Area Area { get; set; }
        public bool Disabled { get; set; }
        public readonly object Parent;

        internal IHandleCollision[] TopCollisionHandlers = new IHandleCollision[0];
        internal IHandleCollision[] BotCollisionHandlers = new IHandleCollision[0];
        internal IHandleCollision[] LeftCollisionHandlers = new IHandleCollision[0];
        internal IHandleCollision[] RightCollisionHandlers = new IHandleCollision[0];

        public Collider(object Parent)
        {
            this.Parent = Parent;
        }
    }
}
