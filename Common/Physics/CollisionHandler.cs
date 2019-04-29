namespace Common
{
    public interface CollisionHandler
    {
        void Top(Collider collider);
        void Left(Collider collider);
        void Bot(Collider collider);
        void Right(Collider collider);
    }
}
