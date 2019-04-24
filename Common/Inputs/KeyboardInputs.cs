using Microsoft.Xna.Framework.Input;

namespace Common
{
    public class KeyboardInputs : Inputs
    {
        public Direction Direction { get; set; }

        public void Update()
        {
            var state = Keyboard.GetState();

            Direction = Direction.None;

            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) Direction = Direction.Left;
            else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) Direction = Direction.Right;
            else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down)) Direction = Direction.Down;
            else if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up)) Direction = Direction.Up;
        }

        //public void AfterUpdate()
        //{
        //    WasPressingLeft = IsPressingLeft;
        //    WasPressingRight = IsPressingRight;
        //    WasPressingJump = IsPressingJump;
        //}
    }
}
