using ThirdGame;

namespace Common
{
    public class ChangeSpeedUsingKeyboard : IHandleUpdates
    {
        private readonly Inputs Inputs;
        private readonly GameObject GameObject;

        public ChangeSpeedUsingKeyboard(Inputs Inputs, GameObject GameObject)
        {
            this.Inputs = Inputs;
            this.GameObject = GameObject;
        }

        public void Update()
        {
            if (Inputs.Direction == DpadDirection.Left 
                || Inputs.Direction == DpadDirection.DownLeft
                || Inputs.Direction == DpadDirection.UpLeft)
                GameObject.Velocity.X -= 10;
            else if (Inputs.Direction == DpadDirection.Right
                || Inputs.Direction == DpadDirection.DownRight
                || Inputs.Direction == DpadDirection.UpRight)
                GameObject.Velocity.X += 10;
            else if (GameObject.Velocity.X > 0)
                GameObject.Velocity.X -= 10;
            else if (GameObject.Velocity.X < 0)
                GameObject.Velocity.X += 10;

            if (GameObject.Velocity.X > 100)
                GameObject.Velocity.X = 100;

            if (GameObject.Velocity.X < -100)
                GameObject.Velocity.X = -100;
        }
    }
}
