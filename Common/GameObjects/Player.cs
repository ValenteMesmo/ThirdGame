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
                 , new BroadCastState(Camera, this, network)
            );

            Colliders = new Collider[] {
                new Collider(this) {
                    X = 0,
                    Y=0,
                    Width=PlayerAnimator.SIZE,
                    Height=PlayerAnimator.SIZE,
                    Collision =new CollisionHandlerAggregation( new LogCollision(), new BlockCollisionHandler())
                }
            };

            Animation = new PlayerAnimator(this, Inputs);
            Update = playerUpdateHandler;
            
        }
    }
}
