using ThirdGame;

namespace Common
{
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
            if (Inputs.Direction == Direction.Left 
                || Inputs.Direction == Direction.DownLeft
                || Inputs.Direction == Direction.UpLeft)
                Speed.X -= 10;
            else if (Inputs.Direction == Direction.Right
                || Inputs.Direction == Direction.DownRight
                || Inputs.Direction == Direction.UpRight)
                Speed.X += 10;
            else if (Speed.X > 0)
                Speed.X -= 10;
            else if (Speed.X < 0)
                Speed.X += 10;

            if (Speed.X > 100)
                Speed.X = 100;

            if (Speed.X < -100)
                Speed.X = -100;

            //if (Inputs.IsPressingJump)
            //    Speed.Y = -200;
            //else
            //    Speed.Y += 20;

            //if (Speed.Y > 200)
            //    Speed.Y = 200;
            //if (Speed.Y < -200)
            //    Speed.Y = -200;
        }
    }
}
