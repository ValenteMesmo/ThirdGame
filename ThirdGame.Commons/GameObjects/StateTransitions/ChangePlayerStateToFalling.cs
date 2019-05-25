using Common;

namespace ThirdGame
{
    public class ChangePlayerStateToFalling : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerStateToFalling(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (!Player.Grounded && Player.Velocity.Y > 0)
            {
                Player.State = PlayerState.FALLING;
            }
        }
    }
}
