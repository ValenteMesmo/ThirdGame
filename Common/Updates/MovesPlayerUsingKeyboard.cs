using Microsoft.Xna.Framework;
using ThirdGame;

namespace Common
{
    public class MovesWithSpeed : IHandleUpdates
    {
        private readonly PositionComponent PlayerPosition;
        private readonly Speedometer Speed;

        public MovesWithSpeed(PositionComponent PlayerPosition, Speedometer Speed)
        {
            this.PlayerPosition = PlayerPosition;
            this.Speed = Speed;
        }

        public void Update()
        {
            var x = PlayerPosition.Current.X;
            var y = PlayerPosition.Current.Y;

            x += Speed.X;
            y += Speed.Y;

            if (y > 800)
            {
                Speed.Y = 0;
                y = 800;
            }

            PlayerPosition.Current = new Vector2(x, y);
        }
    }

    public class ChangeSpeedUsingKeyboard : IHandleUpdates
    {
        private readonly Inputs Inputs;
        private readonly Speedometer Speed;

        public ChangeSpeedUsingKeyboard(Inputs Inputs, Speedometer Speed)
        {
            this.Inputs = Inputs;
            this.Speed = Speed;
        }

        public void Update()
        {
            if (Inputs.IsPressingLeft)
                Speed.X -= 10;
            else if (Inputs.IsPressingRight)
                Speed.X += 10;
            else if (Speed.X > 0)
                Speed.X -= 10;
            else if (Speed.X < 0)
                Speed.X += 10;

            if (Inputs.IsPressingJump)
                Speed.Y = -200;
            else
                Speed.Y += 20;

            if (Speed.X > 100)
                Speed.X = 100;

            if (Speed.X < -100)
                Speed.X = -100;
            
            if (Speed.Y > 200)
                Speed.Y = 200;
            if (Speed.Y < -200)
                Speed.Y = -200;
        }
    }
}
