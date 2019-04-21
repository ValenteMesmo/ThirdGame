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
}
