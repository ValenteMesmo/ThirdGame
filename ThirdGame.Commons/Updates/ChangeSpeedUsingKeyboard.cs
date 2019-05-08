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
            if (Inputs.Direction == DpadDirection.Left )
                GameObject.Velocity.X -= 10;
            else if (Inputs.Direction == DpadDirection.Right)
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
