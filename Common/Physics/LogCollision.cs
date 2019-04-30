using Common;

namespace ThirdGame
{
    public class LogCollision : CollisionHandler
    {
        public void BeforeCollisions() { }

        public void Bot(Collider Source, Collider Target)
        {
            Game1.LOG += $@"
BOT   CollidingWith {Target.Parent}";
        }

        public void Left(Collider Source, Collider Target)
        {
            Game1.LOG += $@"
LEFT  CollidingWith {Target.Parent}";
        }

        public void Right(Collider Source, Collider Target)
        {
            Game1.LOG += $@"
RIGHT CollidingWith {Target.Parent}";
        }

        public void Top(Collider Source, Collider Target)
        {
            Game1.LOG += $@"
TOP   CollidingWith {Target.Parent}";
        }
    }
}
