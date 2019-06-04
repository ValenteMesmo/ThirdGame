using Common;

namespace ThirdGame
{
    public class ChangePlayerStateToAfterAttack : IHandleUpdates
    {
        public readonly Player Player;
        public readonly CooldownTracker Cooldown;

        public ChangePlayerStateToAfterAttack(Player Player, CooldownTracker Cooldown)
        {
            this.Player = Player;
            this.Cooldown = Cooldown;
        }

        public void Update()
        {
            if (Cooldown.IsFree())
                Player.State = PlayerState.AFTER_ATTACK;
            else
                Cooldown.Update();
        }
    }
}
