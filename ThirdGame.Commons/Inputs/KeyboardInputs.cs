using Microsoft.Xna.Framework.Input;

namespace Common
{
    public class KeyboardInputs : Inputs
    {
        public int Direction { get; set; }
        public int Action { get; set; }

        public void Update()
        {
            var state = Keyboard.GetState();

            Direction = DpadDirection.None;

            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
                Direction = DpadDirection.Left;
            else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
                Direction = DpadDirection.Right;
            else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
                Direction = DpadDirection.Down;
            else if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
                Direction = DpadDirection.Up;

            if (state.IsKeyDown(Keys.K) || state.IsKeyDown(Keys.Space))
            {
                Action = DpadDirection.Jump;
            }
            else
                Action = DpadDirection.None;
        }
    }
}
