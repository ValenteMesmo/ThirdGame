using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    class LearpToPosition : IHandleUpdates
    {
        private readonly PositionComponent From;
        private readonly PositionComponent To;

        public LearpToPosition(PositionComponent From, PositionComponent To)
        {
            this.From = From;
            this.To = To;
        }

        public void Update()
        {
            From.Current = new Vector2(
                MathHelper.Lerp(From.Current.X, To.Current.X, 0.08f),
                MathHelper.Lerp(From.Current.Y, To.Current.Y, 0.08f)
            );
        }
    }
}
