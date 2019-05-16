using ThirdGame;

namespace Common
{
    public class IncreaseHorizontalVelocity : IHandleUpdates
    {
        public readonly int Speed;
        public readonly PositionComponent Target;

        public IncreaseHorizontalVelocity(PositionComponent Target, int Speed)
        {
            this.Speed = Speed;
            this.Target = Target;
        }

        public void Update()
        {
            Target.Velocity.X += Speed;
        }
    }

    public class LimitHorizontalVelocity : IHandleUpdates
    {
        public readonly int Limit;
        public readonly PositionComponent Target;

        public LimitHorizontalVelocity(PositionComponent Target, int Limit)
        {
            if (Limit <= 0)
                throw new System.Exception("Limit must be positive!");

            this.Limit = Limit;
            this.Target = Target;
        }

        public void Update()
        {
            if (Target.Velocity.X > Limit)
                Target.Velocity.X = Limit;
            else if (Target.Velocity.X < -Limit)
                Target.Velocity.X = -Limit;
        }
    }

    public class DecreaseHorizontalVelocity : IHandleUpdates
    {
        public readonly int Speed;
        public readonly PositionComponent Target;

        public DecreaseHorizontalVelocity(PositionComponent Target, int Speed)
        {
            if (Speed <= 0)
                throw new System.Exception("Speed must be positive!");

            this.Speed = Speed;
            this.Target = Target;
        }

        public void Update()
        {
            if (Target.Velocity.X > 0)
                Target.Velocity.X -= Speed;
            else if (Target.Velocity.X < 0)
                Target.Velocity.X += Speed;
        }
    }
}
