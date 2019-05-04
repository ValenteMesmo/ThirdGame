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
            //TODO: 
            From.Position.X = MathHelper.Lerp(From.Position.X, To.Position.X, 0.08f);
            From.Position.Y = MathHelper.Lerp(From.Position.Y, To.Position.Y, 0.5f);
        }
    }
}
