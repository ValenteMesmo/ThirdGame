using Common;
using System;
using System.Collections.Generic;

namespace ThirdGame
{
    public struct StateTransition
    {
        public StateTransition(int From, int To)
        {
            this.From = From;
            this.To = To;
        }

        public int From { get; }
        public int To { get; }
    }

    public class State
    {
        private Dictionary<int, List<StateTransition>> Transitions = new Dictionary<int, List<StateTransition>>();

        public int Value { get; private set; }

        public void AddTransition(int From, int To, int When)
        {
            var transition = new StateTransition(From, To);

            if (!Transitions.ContainsKey(From))
                Transitions.Add(When, new List<StateTransition>());

            if (Transitions[When].Contains(transition))
                throw new Exception("Transition already added!");

            Transitions[When].Add(transition);
        }

        public void Update(int command)
        {
            foreach (var transition in Transitions[command])
                if (transition.From == Value)
                {
                    Value = transition.To;
                    break;
                }
        }
    }


    public interface IHaveState
    {
        int State { get; set; }
    }

    public class ChangePlayerFromIdleToFalling : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerFromIdleToFalling(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (!Player.Grounded)
            {
                if (Player.State == PlayerState.IDLE_LEFT)
                    Player.State = PlayerState.FALLING_LEFT;
                else if (Player.State == PlayerState.IDLE_RIGHT)
                    Player.State = PlayerState.FALLING_RIGHT;
            }
        }
    }

    public class ChangePlayerFromIdleToWalking : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerFromIdleToWalking(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.Grounded)
            {
                if (Player.Inputs.Direction == DpadDirection.Left)
                    Player.State = PlayerState.WALKING_LEFT;
                else if (Player.Inputs.Direction == DpadDirection.Right)
                    Player.State = PlayerState.WALKING_RIGHT;
            }
        }
    }

    public class ChangePlayerFromWalkingToIdle : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerFromWalkingToIdle(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.Grounded)
            {
                if (Player.Inputs.Direction == DpadDirection.None)
                {
                    if (Player.State == PlayerState.WALKING_LEFT)
                        Player.State = PlayerState.IDLE_LEFT;
                    else if (Player.State == PlayerState.WALKING_RIGHT)
                        Player.State = PlayerState.IDLE_RIGHT;
                }
            }
        }
    }

    public class ChangePlayerFromFallingToIdle : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerFromFallingToIdle(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (Player.Grounded)
            {
                if (Player.State == PlayerState.FALLING_LEFT)
                    Player.State = PlayerState.IDLE_LEFT;
                else if (Player.State == PlayerState.FALLING_RIGHT)
                    Player.State = PlayerState.IDLE_RIGHT;
            }
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
            Game1.LOG += $@"
STATE: {gameOjbect.State}";
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
        public const int IDLE_RIGHT = 0;
        public const int IDLE_LEFT = 1;
        public const int WALKING_RIGHT = 2;
        public const int WALKING_LEFT = 3;
        public const int FALLING_LEFT = 4;
        public const int FALLING_RIGHT = 5;
        public const int CROUCH_LEFT = 6;
        public const int CROUCH_RIGHT = 7;
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

            Animation = new PlayerAnimator(this, Inputs);

            if (remote)
                Update = CreateUpdatesByState(Inputs);
            else
                Update = new UpdateAggregation(
                    CreateUpdatesByState(Inputs),
                    new BroadCastState(Camera, this, network)
                );
        }

        private UpdateByState CreateUpdatesByState(Inputs Inputs)
        {
            var jumpCommandChangesVerticalSpeed = new JumpCommandChangesVerticalSpeed(this, Inputs);
            var changesSpeedRight = new IncreaseHorizontalVelocity(this, 10);
            var changesSpeedLeft = new IncreaseHorizontalVelocity(this, -10);
            var decreaseVelocity = new DecreaseHorizontalVelocity(this, 10);
            var limitHorizontalVelocity = new LimitHorizontalVelocity(this, 100);
            var gravityChangesVerticalSpeed = new GravityChangesVerticalSpeed(this);
            var changePlayerFromIdleToFalling = new ChangePlayerFromIdleToFalling(this);
            var changePlayerFromFallingToIdle = new ChangePlayerFromFallingToIdle(this);
            var changePlayerFromIdleToWalking = new ChangePlayerFromIdleToWalking(this);
            var changePlayerFromWalkingToIdle = new ChangePlayerFromWalkingToIdle(this);

            var updateByState = new UpdateByState(this);

            updateByState.Add(PlayerState.IDLE_LEFT, new UpdateAggregation(
                changePlayerFromIdleToFalling
                , changePlayerFromIdleToWalking
                , gravityChangesVerticalSpeed
            ));

            updateByState.Add(PlayerState.IDLE_RIGHT, new UpdateAggregation(
                changePlayerFromIdleToFalling
                 , changePlayerFromIdleToWalking
                 , gravityChangesVerticalSpeed
            ));

            updateByState.Add(PlayerState.FALLING_LEFT, new UpdateAggregation(
                changePlayerFromFallingToIdle
                , gravityChangesVerticalSpeed
            ));

            updateByState.Add(PlayerState.FALLING_RIGHT, new UpdateAggregation(
                changePlayerFromFallingToIdle
                , gravityChangesVerticalSpeed
            ));

            updateByState.Add(PlayerState.WALKING_LEFT, new UpdateAggregation(
                changePlayerFromWalkingToIdle
                , gravityChangesVerticalSpeed
                , changesSpeedLeft
                , decreaseVelocity
                , limitHorizontalVelocity
            ));

            updateByState.Add(PlayerState.WALKING_RIGHT, new UpdateAggregation(
                changePlayerFromWalkingToIdle
                , gravityChangesVerticalSpeed
                , changesSpeedRight
                , decreaseVelocity
                , limitHorizontalVelocity
            ));

            return updateByState;
        }
    }
}
