using Microsoft.Xna.Framework;

namespace Common
{
    public class MovesPlayerUsingKeyboard : IHandleUpdates
    {
        private readonly KeyboardInputs Inputs;
        private readonly PositionComponent PlayerPosition;

        public MovesPlayerUsingKeyboard(PositionComponent PlayerPosition, KeyboardInputs Inputs)
        {
            this.Inputs = Inputs;
            this.PlayerPosition = PlayerPosition;
        }

        public void Update()
        {
            if (Inputs.IsPressingLeft)
                PlayerPosition.Current -= new Vector2(100, 0);

            if (Inputs.IsPressingRight)
                PlayerPosition.Current += new Vector2(100, 0);

            if (Inputs.IsPressingJump)
                PlayerPosition.Current -= new Vector2(0, 100);
            else if (PlayerPosition.Current.Y < 800)
                PlayerPosition.Current += new Vector2(0, 100);
        }
    }
}
