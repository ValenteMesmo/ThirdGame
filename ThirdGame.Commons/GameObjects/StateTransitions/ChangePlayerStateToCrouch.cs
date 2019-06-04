using System;
using Common;

namespace ThirdGame
{
    public class ChangePlayerStateToCrouch : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerStateToCrouch(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.Grounded && Player.Inputs.Direction == DpadDirection.Down)
                Player.State = PlayerState.CROUCH;
        }
    }
}
