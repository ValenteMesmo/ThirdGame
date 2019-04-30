using Common;

namespace ThirdGame
{
    public class FlagAsGrounded : CollisionHandler
    {
        private readonly Player Player;

        public FlagAsGrounded(Player Player)
        {
            this.Player = Player;
        }

        public void BeforeCollisions()
        {
            Player.Grounded = false;
        }

        public void Bot(Collider Source, Collider Target)
        {
            if (Target.Parent is Block)
                Player.Grounded = true;
        }

        public void Left(Collider Source, Collider Target)
        {
        }

        public void Right(Collider Source, Collider Target)
        {
        }

        public void Top(Collider Source, Collider Target)
        {
        }
    }

    public class Player : GameObject
    {
        public bool Grounded { get; set; }

        public Player(string Id, Inputs Inputs, Camera2d Camera, NetworkHandler network) : base(Id)
        {
            var playerUpdateHandler = new UpdateAggregation(
                 new ChangeSpeedUsingKeyboard(Inputs, this)
                 , new AffectedByGravity(this)
                 , new Jump(this, Inputs)
                 , new BroadCastState(Camera, this, network)
            );

            Colliders = new Collider[] {
                new Collider(this) {
                    X = 0,
                    Y = 0,
                    Width = PlayerAnimator.SIZE,
                    Height = PlayerAnimator.SIZE,
                    Collision =new CollisionHandlerAggregation(
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
            Update = playerUpdateHandler;
        }
    }
}
