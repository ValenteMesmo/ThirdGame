using Common;

namespace ThirdGame
{
    public class FlagAsHit: CollisionHandler
    {
        private readonly Player Player;

        public FlagAsHit(Player Player)
        {
            this.Player = Player;
        }

        public void BeforeCollisions()
        {
            Player.Hit = false;
        }

        public void Bot(Collider Source, Collider Target)
        {
            if (Target is AttackCollider)
                Player.Hit = true;
        }

        public void Left(Collider Source, Collider Target)
        {
            if (Target is AttackCollider)
                Player.Hit = true;
        }

        public void Right(Collider Source, Collider Target)
        {
            if (Target is AttackCollider)
                Player.Hit = true;
        }

        public void Top(Collider Source, Collider Target)
        {
            if (Target is AttackCollider)
                Player.Hit = true;
        }
    }
}
