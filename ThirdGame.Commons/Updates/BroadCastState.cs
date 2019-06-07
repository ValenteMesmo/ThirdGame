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
            Game1.LOG.Add($"X = {Something.Position.X}");
            Game1.LOG.Add($"Y = {Something.Position.Y}");
            NetworkHandler.Send(Something.Position.ToPoint());
        }
    }
}
