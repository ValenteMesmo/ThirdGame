using Common;

namespace ThirdGame
{
    public class ChangePlayerStateToWalking : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerStateToWalking(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.Grounded)
            {
                if (Player.Inputs.Direction == DpadDirection.Left)
                {
                    Player.State = PlayerState.WALKING;
                    Player.FacingRight = false;
                }
                else if (Player.Inputs.Direction == DpadDirection.Right)
                {
                    Player.State = PlayerState.WALKING;
                    Player.FacingRight = true;
                }
            }
        }
    }
}
