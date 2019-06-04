using Common;

namespace ThirdGame
{
    public class ChangePlayerStateToAfterHurt : IHandleUpdates
    {
        public readonly Player Player;
        public readonly CooldownTracker Cooldown;

        public ChangePlayerStateToAfterHurt(Player Player, CooldownTracker Cooldown)
        {
            this.Player = Player;
            this.Cooldown = Cooldown;
        }

        public void Update()
        {
            if (Cooldown.IsFree())
                Player.State = PlayerState.AFTER_HURT;
            else
                Cooldown.Update();
        }
    }
}
