using Common;

namespace ThirdGame
{
    public class ChangePlayerStateToLookingUp : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerStateToLookingUp(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.Grounded && Player.Inputs.Direction == DpadDirection.Up)
            {
                Player.State = PlayerState.LOOKING_UP;
            }
        }
    }
}
