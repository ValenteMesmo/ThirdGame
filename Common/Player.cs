using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ThirdGame;

namespace Common
{
    public interface IUpdateGameObject
    {
        void Update(GameObject GameObject);
    }

    public class BroadCastState : IUpdateGameObject
    {
        private readonly Camera2d Camera;
        private readonly UdpService UdpWrapper;
        private readonly MyMessageEncoder MyMessageEncoder;

        public BroadCastState(Camera2d Camera, UdpService UdpWrapper, MyMessageEncoder MyMessageEncoder)
        {
            this.Camera = Camera;
            this.UdpWrapper = UdpWrapper;
            this.MyMessageEncoder = MyMessageEncoder;
        }

        public void Update(GameObject GameObject)
        {
            UdpWrapper.Send(
                MyMessageEncoder.Encode(
                   GameObject.Position
                    , UdpWrapper.myIp
                )
            );
        }
    }

    public class MovesPlayerUsingKeyboard : IUpdateGameObject
    {
        private readonly KeyboardInputs Inputs;

        public MovesPlayerUsingKeyboard(KeyboardInputs Inputs)
        {
            this.Inputs = Inputs;
        }

        public void Update(GameObject Player)
        {
            if (Inputs.IsPressingLeft)
                Player.Position -= new Vector2(100, 0);

            if (Inputs.IsPressingRight)
                Player.Position += new Vector2(100, 0);
        }
    }

    public class GameObject
    {
        public Vector2 Position { get; set; }
        public Vector2 PreviousPosition { get; private set; }

        private readonly IUpdateGameObject[] UpdateHandlers;

        public GameObject(params IUpdateGameObject[] UpdateHandlers)
        {
            this.UpdateHandlers = UpdateHandlers;
        }

        public void Update()
        {
            foreach (var handler in UpdateHandlers)
                handler.Update(this);
        }

        public void AfterUpdate()
        {
            PreviousPosition = Position;
        }
    }

    public class KeyboardInputs
    {
        public bool IsPressingLeft { get; set; }
        public bool WasPressingLeft { get; private set; }

        public bool IsPressingRight { get; set; }
        public bool WasPressingRight { get; private set; }

        public bool IsPressingJump { get; set; }
        public bool WasPressingJump { get; private set; }

        public void Update()
        {
            var state = Keyboard.GetState();
            IsPressingLeft = state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left);
            IsPressingRight = state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right);
            IsPressingJump = state.IsKeyDown(Keys.K) || state.IsKeyDown(Keys.Space);
        }

        public void AfterUpdate()
        {
            WasPressingLeft = IsPressingLeft;
            WasPressingRight = IsPressingRight;
            WasPressingJump = IsPressingJump;
        }
    }
}
