using Common;

namespace ThirdGame
{
    public class ChangePlayerStateToHurt : IHandleUpdates
    {
        public readonly Player Player;
        private readonly CooldownTracker HurtCooldwon;

        public ChangePlayerStateToHurt(Player Player, CooldownTracker HurtCooldwon)
        {
            this.Player = Player;
            this.HurtCooldwon = HurtCooldwon;
        }

        public void Update()
        {
            if (Player.Hit)
            {
                Player.State = PlayerState.HURT;
                HurtCooldwon.Start();
            }
        }
    }
}
