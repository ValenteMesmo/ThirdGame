using Common;

namespace ThirdGame
{
    public class LogCollision : CollisionHandler
    {
        public void BeforeCollisions() { }

        public void Bot(Collider Source, Collider Target) =>
            Game1.LOG.Add($"BOT   CollidingWith {Target.Parent}");

        public void Left(Collider Source, Collider Target) =>
            Game1.LOG.Add($"LEFT  CollidingWith {Target.Parent}");

        public void Right(Collider Source, Collider Target) =>
            Game1.LOG.Add($"RIGHT CollidingWith {Target.Parent}");

        public void Top(Collider Source, Collider Target) =>
            Game1.LOG.Add($"TOP   CollidingWith {Target.Parent}");
    }
}
