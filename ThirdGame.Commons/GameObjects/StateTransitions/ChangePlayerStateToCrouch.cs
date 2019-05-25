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

    public class CooldownTracker : IHandleUpdates
    {
        private int Count = 0;
        private readonly int Duration;

        public CooldownTracker(int Duration)
        {
            this.Duration = Duration;
        }

        public void Start()
        {
            Count = Duration;
        }

        public bool IsFree()
        {
            return Count == 0;
        }

        public void Update()
        {
            if (Count > 0)
                Count--;
        }
    }
}
