using Common;

namespace ThirdGame
{
    public class ChangePlayerStateToAttack : IHandleUpdates
    {
        public readonly Player Player;
        private readonly CooldownTracker Cooldown;

        public ChangePlayerStateToAttack(Player Player, CooldownTracker Cooldown)
        {
            this.Player = Player;
            this.Cooldown = Cooldown;
        }

        public void Update()
        {
            if (Player.Grounded
                && Player.Inputs.Action == DpadAction.Attack
                && Cooldown.IsFree())
            {
                Cooldown.Start();
                Player.State = PlayerState.ATTACK;
            }
        }
    }
}
