namespace Common
{
    internal class NoCollision : CollisionHandler
    {
        public static NoCollision Instance { get; } = new NoCollision();
        internal static Collider[] Empty = new Collider[0];

        private NoCollision() { }

        public void Bot(Collider collider) { }
        public void Left(Collider collider) { }
        public void Right(Collider collider) { }
        public void Top(Collider collider) { }
    }
}
