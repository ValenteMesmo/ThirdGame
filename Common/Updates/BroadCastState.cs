using ThirdGame;

namespace Common
{
    public class BroadCastState : IHandleUpdates
    {
        private readonly Camera2d Camera;
        private readonly UdpService UdpWrapper;
        private readonly MyMessageEncoder MyMessageEncoder;
        private readonly PositionComponent Position;

        public BroadCastState(
            Camera2d Camera
            , PositionComponent Position
            , UdpService UdpWrapper
            , MyMessageEncoder MyMessageEncoder
        )
        {
            this.Camera = Camera;
            this.UdpWrapper = UdpWrapper;
            this.MyMessageEncoder = MyMessageEncoder;
            this.Position = Position;
        }

        public void Update()
        {
            UdpWrapper.Send(
                //TODO: order
                MyMessageEncoder.Encode(
                    new Message(
                        0
                        , Position.Current.X
                        , Position.Current.Y
                    )
                )
            );
        }
    }
}
