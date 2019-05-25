using Common;

namespace ThirdGame
{
    public class ChangePlayerStateToIdle : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerStateToIdle(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.Grounded && Player.Inputs.Direction == DpadDirection.None)
                Player.State = PlayerState.IDLE;
        }
    }
}
