using Microsoft.Xna.Framework.Input;

namespace Common
{
    public class KeyboardInputs : Inputs
    {
        public bool IsPressingLeft { get; set; }
        public bool WasPressingLeft { get; private set; }

        public bool IsPressingRight { get; set; }
        public bool WasPressingRight { get; private set; }

        public bool IsPressingJump { get; set; }
        public bool WasPressingJump { get; private set; }

        public bool IsPressingDown { get; set; }
        public bool WasPressingDown { get; private set; }

        public bool IsPressingUp { get; set; }
        public bool WasPressingUp { get; private set; }

        public void Update()
        {
            var state = Keyboard.GetState();
            IsPressingLeft = state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left);
            IsPressingRight = state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right);
            IsPressingJump = state.IsKeyDown(Keys.K) || state.IsKeyDown(Keys.Space);
            IsPressingDown = state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down);
            IsPressingUp = state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up);
        }

        //public void AfterUpdate()
        //{
        //    WasPressingLeft = IsPressingLeft;
        //    WasPressingRight = IsPressingRight;
        //    WasPressingJump = IsPressingJump;
        //}
    }
}
