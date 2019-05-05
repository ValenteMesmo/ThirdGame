namespace Common
{
    public interface CollisionHandler
    {
        void Top(Collider Source, Collider Target);
        void Left(Collider Source, Collider Target);
        void Bot(Collider Source, Collider Target);
        void Right(Collider Source, Collider Target);
        void BeforeCollisions();
    }
}
