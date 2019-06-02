using System.Collections.Generic;
using Common;

namespace ThirdGame
{
    public class Player : GameObject, IHaveState
    {
        public bool Grounded { get; set; }
        public int State { get; set; }

        public readonly Inputs Inputs;
        private readonly PlayerAnimator animator;

        public Player(string Id, Inputs Inputs, Camera2d Camera, NetworkHandler network, bool remote) : base(Id)
        {
            this.Inputs = Inputs;

            animator = new PlayerAnimator(this);
            animator.mainCollider.Collision = new CollisionHandlerAggregation(
                new LogCollision()
                , new BlockCollisionHandler()
            );
            animator.groundDetection.Collision = new FlagAsGrounded(this);

            Animation = animator;

            if (remote)
                Update = CreateUpdatesByState(Inputs);
            else
                Update = new UpdateAggregation(
                    CreateUpdatesByState(Inputs)
                    , new BroadCastState(Camera, this, network)
                );
        }

        public override IEnumerable<Collider> GetColliders()
        {
            return animator.GetColliders();
        }

        private UpdateByState CreateUpdatesByState(Inputs Inputs)
        {
            var attackCooldwon = new CooldownTracker(20);

            var changesSpeed = new IncreaseHorizontalVelocity(this, 10);
            var decreaseVelocity = new DecreaseHorizontalVelocity(this, 5);
            var limitHorizontalVelocity = new LimitHorizontalVelocity(this, 100);
            var gravityChangesVerticalSpeed = new GravityChangesVerticalSpeed(this);

            var ChangePlayerStateToFalling = new ChangePlayerStateToFalling(this);
            var changePlayerToIdle = new ChangePlayerStateToIdle(this);
            var changePlayerToWalking = new ChangePlayerStateToWalking(this);
            var ChangePlayerToJumpingState = new ChangePlayerStateToJumping(this);
            var changePlayerStateToCrouch = new ChangePlayerStateToCrouch(this);
            var changePlayerStateToLookingUp = new ChangePlayerStateToLookingUp(this);
            var ChangePlayerStateToAttack = new ChangePlayerStateToAttack(this, attackCooldwon);
            var ChangePlayerStateToAfterAttack = new ChangePlayerStateToAfterAttack(this, attackCooldwon);

            var updateByState = new UpdateByState(this);

            updateByState.Add(PlayerState.IDLE, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , ChangePlayerStateToFalling
                , changePlayerToWalking
                , ChangePlayerToJumpingState
                , changePlayerStateToCrouch
                , changePlayerStateToLookingUp
                , ChangePlayerStateToAttack
            ));

            updateByState.Add(PlayerState.FALLING, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , changePlayerToWalking
                , changePlayerToIdle
                , changePlayerStateToCrouch
                , changePlayerStateToLookingUp
            ));

            updateByState.Add(PlayerState.WALKING, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , limitHorizontalVelocity
                , changesSpeed
                , changePlayerToWalking
                , ChangePlayerToJumpingState
                , ChangePlayerStateToFalling
                , changePlayerToIdle
                , changePlayerStateToCrouch
                , changePlayerStateToLookingUp
                , ChangePlayerStateToAttack
            ));

            updateByState.Add(PlayerState.JUMP, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , ChangePlayerStateToFalling
            ));

            updateByState.Add(PlayerState.CROUCH, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , changePlayerToWalking
                , changePlayerToIdle
                , ChangePlayerStateToFalling
                , changePlayerStateToLookingUp
            ));

            updateByState.Add(PlayerState.LOOKING_UP, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , changePlayerToWalking
                , changePlayerToIdle
                , ChangePlayerStateToFalling
                , changePlayerStateToCrouch
            ));

            updateByState.Add(PlayerState.ATTACK, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , ChangePlayerStateToAfterAttack
            ));

            updateByState.Add(PlayerState.AFTER_ATTACK, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , changePlayerToIdle
                , changePlayerToWalking
                , ChangePlayerStateToFalling
                , changePlayerStateToCrouch
            ));

            return updateByState;
        }
    }
}
