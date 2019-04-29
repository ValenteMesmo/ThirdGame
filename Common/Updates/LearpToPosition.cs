using Common;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    class LearpToPosition : IHandleUpdates
    {
        private readonly IHavePosition From;
        private readonly IHavePosition To;

        public LearpToPosition(IHavePosition From, IHavePosition To)
        {
            this.From = From;
            this.To = To;
        }

        public void Update()
        {
            From.Position = new Vector2(
                MathHelper.Lerp(From.Position.X, To.Position.X, 0.08f),
                MathHelper.Lerp(From.Position.Y, To.Position.Y, 0.08f)
            );
        }
    }
}
