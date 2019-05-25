using Common;

namespace ThirdGame
{
    public class ChangePlayerStateToJumping : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerStateToJumping(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.Grounded && Player.Inputs.Action == DpadAction.Jump)
            {
                Player.Velocity.Y = -200;
                Player.State = PlayerState.JUMP;
            }
        }
    }
}
