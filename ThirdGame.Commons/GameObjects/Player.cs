using Common;
using System;
using System.Collections.Generic;

namespace ThirdGame
{
    public interface IHaveState
    {
        int State { get; set; }
    }

    public class ChangePlayerStateToJumping : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerStateToJumping(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.Grounded && Player.Inputs.Action == DpadDirection.Down)
            {
                if (Player.State == PlayerState.IDLE || Player.State == PlayerState.WALKING)
                {
                    Player.Velocity.Y = -200;
                    Player.State = PlayerState.JUMP;
                }
            }
        }
    }

    public class ChangePlayerStateToFalling : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerStateToFalling(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (!Player.Grounded && Player.Velocity.Y > 0)
            {
                Player.State = PlayerState.FALLING;
            }
        }
    }

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

    public class UpdateByState : IHandleUpdates
    {
        private readonly Dictionary<int, IHandleUpdates> Options = new Dictionary<int, IHandleUpdates>();
        private readonly IHaveState gameOjbect;

        public UpdateByState(IHaveState gameOjbect) =>
            this.gameOjbect = gameOjbect;

        public void Update()
        {
            Options[gameOjbect.State].Update();
        }

        public void Add(int state, IHandleUpdates updateHandler)
        {
            if (Options.ContainsKey(state))
                throw new Exception($"{nameof(UpdateByState)} already have an update handler for state {state}");

            Options[state] = updateHandler;
        }
    }

    public static class PlayerState
    {
        public const int IDLE = 0;
        public const int WALKING = 1;
        public const int FALLING = 2;
        public const int CROUCH = 3;
        public const int JUMP = 4;
    }

    public class Player : GameObject, IHaveState
    {
        public bool Grounded { get; set; }
        public int State { get; set; }

        public readonly Inputs Inputs;

        public Player(string Id, Inputs Inputs, Camera2d Camera, NetworkHandler network, bool remote) : base(Id)
        {
            this.Inputs = Inputs;

            Colliders = new Collider[] {
                new Collider(this) {
                    X = 0,
                    Y = 0,
                    Width = PlayerAnimator.SIZE,
                    Height = PlayerAnimator.SIZE,
                    Collision = new CollisionHandlerAggregation(
                        new LogCollision()
                        , new BlockCollisionHandler()
                    )
                }
                , new Collider(this){
                    X = 0,
                    Y = 0,
                    Width = PlayerAnimator.SIZE,
                    Height = PlayerAnimator.SIZE + 1,
                    Collision = new FlagAsGrounded(this)
                }
            };

            Animation = new PlayerAnimator(this);

            if (remote)
                Update = CreateUpdatesByState(Inputs);
            else
                Update = new UpdateAggregation(
                    CreateUpdatesByState(Inputs)
                , new BroadCastState(Camera, this, network)
                );
        }

        private UpdateByState CreateUpdatesByState(Inputs Inputs)
        {
            //var jumpCommandChangesVerticalSpeed = new JumpCommandChangesVerticalSpeed(this, Inputs);
            var changesSpeed = new IncreaseHorizontalVelocity(this, 10);
            var decreaseVelocity = new DecreaseHorizontalVelocity(this, 5);
            var limitHorizontalVelocity = new LimitHorizontalVelocity(this, 100);
            var gravityChangesVerticalSpeed = new GravityChangesVerticalSpeed(this);

            var changePlayerToFalling = new ChangePlayerStateToFalling(this);
            var changePlayerToIdle = new ChangePlayerStateToIdle(this);
            var changePlayerToWalking = new ChangePlayerStateToWalking(this);
            var ChangePlayerToJumpingState = new ChangePlayerStateToJumping(this);
            var changePlayerStateToCrouch = new ChangePlayerStateToCrouch(this);

            var updateByState = new UpdateByState(this);

            updateByState.Add(PlayerState.IDLE, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , changePlayerToFalling
                , changePlayerToWalking
                , ChangePlayerToJumpingState
                , changePlayerStateToCrouch
            ));

            updateByState.Add(PlayerState.FALLING, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , changePlayerToWalking
                , changePlayerToIdle
            ));

            updateByState.Add(PlayerState.WALKING, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , limitHorizontalVelocity
                , changesSpeed
                , changePlayerToWalking
                , ChangePlayerToJumpingState
                , changePlayerToFalling
                , changePlayerToIdle
                , changePlayerStateToCrouch
            ));

            updateByState.Add(PlayerState.JUMP, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , changePlayerToFalling
            ));

            updateByState.Add(PlayerState.CROUCH, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , changePlayerToWalking
                , changePlayerToIdle
                , changePlayerToFalling
            ));

            return updateByState;
        }
    }
}
