using Common;

namespace ThirdGame
{
    public class FlagAsGrounded : CollisionHandler
    {
        private readonly Player Player;

        public FlagAsGrounded(Player Player)
        {
            this.Player = Player;
        }

        public void BeforeCollisions()
        {
            Player.Grounded = false;
        }

        public void Bot(Collider Source, Collider Target)
        {
            if (Target.Parent is Block)
                Player.Grounded = true;
        }

        public void Left(Collider Source, Collider Target)
        {
        }

        public void Right(Collider Source, Collider Target)
        {
        }

        public void Top(Collider Source, Collider Target)
        {
        }
    }
}
