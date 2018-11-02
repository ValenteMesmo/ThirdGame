using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using ThirdGame;

namespace Common
{
    //em fvez de destroy no gameobject... que tal passar ele para um trashbin

    public class PlayerAnimation : IGetDrawingModels
    {
        private readonly Texture2D texture;
        private readonly DrawingModel a1;

        public PlayerAnimation(Texture2D texture)
        {
            this.texture = texture;
            a1 = new DrawingModel();
            a1.Texture = texture;
            a1.CenterOfRotation = new Vector2(50, 50);
            a1.DestinationRectangle = new Rectangle(
                Point.Zero
                , new Point(800, 800)
            );
        }

        public IEnumerable<DrawingModel> GetDrawingModels()
        {
            yield return a1;
        }
    }

    internal class NoAnimation : IGetDrawingModels
    {
        public static NoAnimation Instance { get; } = new NoAnimation();

        private NoAnimation() { }

        public IEnumerable<DrawingModel> GetDrawingModels() => Enumerable.Empty<DrawingModel>();
    }

    public interface IHandleUpdates
    {
        void Update();
    }

    public interface IGetDrawingModels
    {
        IEnumerable<DrawingModel> GetDrawingModels();
    }

    public class ActualGameObject : IHandleUpdates
    {
        private readonly IHandleUpdates updateHandler;
        private readonly IGetDrawingModels drawingModelsGetter;
        internal bool destroyed;

        public ActualGameObject(IHandleUpdates updateHandler) : this(updateHandler, NoAnimation.Instance) { }

        public ActualGameObject(
            IHandleUpdates updateHandler
            , IGetDrawingModels drawingModelsGetter
        )
        {
            this.updateHandler = updateHandler;
            this.drawingModelsGetter = drawingModelsGetter;
        }

        public void Update() => updateHandler.Update();
        public void Destroy() => destroyed = true;

        public IEnumerable<DrawingModel> Draw() => drawingModelsGetter.GetDrawingModels();
    }

    public class BroadCastState : IHandleUpdates
    {
        private readonly Camera2d Camera;
        private readonly UdpService UdpWrapper;
        private readonly MyMessageEncoder MyMessageEncoder;

        public BroadCastState(
            Camera2d Camera
            , UdpService UdpWrapper
            , MyMessageEncoder MyMessageEncoder
        )
        {
            this.Camera = Camera;
            this.UdpWrapper = UdpWrapper;
            this.MyMessageEncoder = MyMessageEncoder;
        }

        public void Update()
        {
            UdpWrapper.Send(
                MyMessageEncoder.Encode(
                   GameObject.Position
                    , UdpWrapper.myIp
                )
            );
        }
    }

    public class MovesPlayerUsingKeyboard : IHandleUpdates
    {
        private readonly KeyboardInputs Inputs;

        public MovesPlayerUsingKeyboard(KeyboardInputs Inputs)
        {
            this.Inputs = Inputs;
        }

        public void Update()
        {
            if (Inputs.IsPressingLeft)
                Player.Position -= new Vector2(100, 0);

            if (Inputs.IsPressingRight)
                Player.Position += new Vector2(100, 0);

            if (Inputs.IsPressingJump)
                Player.Position -= new Vector2(0, 100);
            else if (Player.Position.Y < 800)
                Player.Position += new Vector2(0, 100);
        }
    }

    public class GameObject
    {
        public Vector2 Position { get; set; }
        public Vector2 PreviousPosition { get; private set; }

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
