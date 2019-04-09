using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Common
{
    public interface Inputs
    {
        bool IsPressingLeft { get; set; }
        bool WasPressingLeft { get; }

        bool IsPressingRight { get; set; }
        bool WasPressingRight { get; }

        bool IsPressingJump { get; set; }
        bool WasPressingJump { get; }

        void Update();
    }

    public class NetworkInputs : Inputs
    {
        public bool IsPressingLeft { get; set; }
        public bool WasPressingLeft { get; private set; }

        public bool IsPressingRight { get; set; }
        public bool WasPressingRight { get; private set; }

        public bool IsPressingJump { get; set; }
        public bool WasPressingJump { get; private set; }

        public void AfterUpdate()
        {
            WasPressingLeft = IsPressingLeft;
            WasPressingRight = IsPressingRight;
            WasPressingJump = IsPressingJump;
        }

        public void Update()
        {
        }
    }

    public class KeyboardInputs : Inputs
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

        //public void AfterUpdate()
        //{
        //    WasPressingLeft = IsPressingLeft;
        //    WasPressingRight = IsPressingRight;
        //    WasPressingJump = IsPressingJump;
        //}
    }

    public class MultipleInputSource : Inputs
    {
        private readonly Inputs[] inputs;

        public bool IsPressingLeft { get; set; }
        public bool WasPressingLeft { get; private set; }

        public bool IsPressingRight { get; set; }
        public bool WasPressingRight { get; private set; }

        public bool IsPressingJump { get; set; }
        public bool WasPressingJump { get; private set; }

        public MultipleInputSource(params Inputs[] inputs)
        {
            this.inputs = inputs;
        }

        public void Update()
        {
            IsPressingLeft = false;
            IsPressingRight = false;
            IsPressingJump = false;

            foreach (var item in inputs)
            {
                item.Update();

                if (item.IsPressingLeft)
                    IsPressingLeft = true;
                if (item.IsPressingRight)
                    IsPressingRight = true;
                if (item.IsPressingJump)
                    IsPressingJump = true;
            }
        }
    }

    public class TouchControlInputs : Inputs
    {
        private readonly Camera2d camera;

        public TouchControlInputs(Camera2d camera)
        {
            this.camera = camera;
        }

        public bool IsPressingLeft { get; set; }
        public bool WasPressingLeft { get; private set; }

        public bool IsPressingRight { get; set; }
        public bool WasPressingRight { get; private set; }

        public bool IsPressingJump { get; set; }
        public bool WasPressingJump { get; private set; }

        public void Update()
        {
            var mouse = Mouse.GetState();
            var touchCollection = TouchPanel.GetState();

            var clicked = mouse.LeftButton == ButtonState.Pressed;
            var touched = touchCollection.Count > 0;

            if (clicked || touched)
            {
                Vector2 position;

                if (touched)
                    position = camera.ToWorldLocation(touchCollection[0].Position);
                else 
                    position = camera.ToWorldLocation(mouse.Position.ToVector2());

                if (position.X > -680 && position.X < -480)
                {
                    IsPressingLeft = true;
                }
                else
                    IsPressingLeft = false;

                if (position.X > -480 && position.X < -280)
                {
                    IsPressingRight = true;
                }
                else
                    IsPressingRight = false;
            }
            else
                IsPressingRight = IsPressingLeft = false;

        }
    }


}
