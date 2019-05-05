using ThirdGame;

namespace Common
{
    public class BroadCastState : IHandleUpdates
    {
        private readonly Camera2d Camera;
        private readonly PositionComponent Something;
        private readonly NetworkHandler NetworkHandler;

        public BroadCastState(
            Camera2d Camera
            , PositionComponent Something
            , NetworkHandler NetworkHandler
        )
        {
            this.Camera = Camera;
            this.Something = Something;
            this.NetworkHandler = NetworkHandler;
        }

        public void Update()
        {
            NetworkHandler.Send(Something.Position.ToPoint());
            Game1.LOG = $@"X = {Something.Position.X}
Y = {Something.Position.Y}";
        }
    }
}
