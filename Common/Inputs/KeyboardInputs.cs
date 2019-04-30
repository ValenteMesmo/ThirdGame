using Microsoft.Xna.Framework.Input;

namespace Common
{
    public class KeyboardInputs : Inputs
    {
        public DpadDirection Direction { get; set; }
        public bool Jump { get; set; }

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

            Jump = (state.IsKeyDown(Keys.K) || state.IsKeyDown(Keys.Space));
        }
    }
}
