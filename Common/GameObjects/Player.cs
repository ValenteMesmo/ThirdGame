using Common;

namespace ThirdGame
{
    public class Player : GameObject
    {
        public Player(string Id, Inputs Inputs, Camera2d Camera, NetworkHandler network) : base(Id)
        {
            var playerUpdateHandler = new UpdateAggregation(
                 new ChangeSpeedUsingKeyboard(Inputs, this)
                 //, new MovesPlayerUsingMouse(Position, Camera)
                 , new BroadCastState(Camera, Position, network)
            );

            Colliders = new Collider[] {
                new Collider(this) {
                    X = 0,
                    Y=0,
                    Width=PlayerAnimator.SIZE,
                    Height=PlayerAnimator.SIZE
                }
            };

            Animation = new PlayerAnimator(Position, Inputs);
            Update = playerUpdateHandler;
            Collision = new LogCollision();
        }
    }

    public class LogCollision : CollisionHandler
    {
        public void Bot(Collider collider)
        {
            Game1.LOG += $@"
BOTCollidingWith {collider.Parent}";
        }

        public void Left(Collider collider)
        {
            Game1.LOG += $@"
LEFTCollidingWith {collider.Parent}";
        }

        public void Right(Collider collider)
        {
            Game1.LOG += $@"
RIGHTCollidingWith {collider.Parent}";
        }

        public void Top(Collider collider)
        {
            Game1.LOG += $@"
TOPCollidingWith {collider.Parent}";
        }
    }
}
