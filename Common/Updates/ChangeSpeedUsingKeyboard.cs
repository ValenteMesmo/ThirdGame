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
            if (Inputs.IsPressingLeft)
                Speed.X -= 10;
            else if (Inputs.IsPressingRight)
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
