using ThirdGame;

namespace Common
{
    public class BroadCastState : IHandleUpdates
    {
        private readonly Camera2d Camera;
        private readonly PositionComponent Position;
        private readonly NetworkHandler NetworkHandler;

        public BroadCastState(
            Camera2d Camera
            , PositionComponent Position
            , NetworkHandler NetworkHandler
        )
        {
            this.Camera = Camera;
            this.Position = Position;
            this.NetworkHandler = NetworkHandler;
        }

        public void Update()
        {
            NetworkHandler.Send(Position.Current.ToPoint());
        }
    }
}
