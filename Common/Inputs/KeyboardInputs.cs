using Microsoft.Xna.Framework.Input;

namespace Common
{
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
