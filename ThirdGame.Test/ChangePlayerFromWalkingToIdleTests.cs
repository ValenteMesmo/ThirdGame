using Common;
using Xunit;
using NSubstitute;

namespace ThirdGame.Tests
{
    public class ChangePlayerFromWalkingToIdleTests
    {
        [Theory, AutoMockPlayerData]
        public void Falling_to_idle(ChangePlayerStateToIdle sut)
        {
            sut.Player.State = PlayerState.WALKING;
            sut.Player.FacingRight = true;
            sut.Player.Grounded = true;

            sut.Update();
            Assert.Equal(PlayerState.IDLE, sut.Player.State);
            Assert.True(sut.Player.FacingRight);
        }
    }

    public class IncreaseHorizontalVelocityTests
    {
        [Theory, AutoMockData]
        public void Positive_speed_test(IncreaseHorizontalVelocity sut)
        {
            var initialSpeed = sut.Target.Velocity.X;
            sut.Update();

            Assert.Equal(initialSpeed, sut.Target.Velocity.X - sut.Speed);
        }
    }

    public class LimitHorizontalVelocityTests
    {
        [Theory, AutoMockData]
        public void Limit_Positive(PositionComponent position)
        {
            var sut = new LimitHorizontalVelocity(position, 100);
            sut.Target.Velocity.X = 200;

            sut.Update();

            Assert.Equal(100, sut.Target.Velocity.X);
        }

        [Theory, AutoMockData]
        public void Limit_negative(PositionComponent position)
        {
            var sut = new LimitHorizontalVelocity(position, 100);
            sut.Target.Velocity.X = -200;

            sut.Update();

            Assert.Equal(-100, sut.Target.Velocity.X);
        }
    }

    public class DecreaseHorizontalVelocityTests
    {
        [Theory, AutoMockData]
        public void Decrease_Positive(PositionComponent position)
        {
            var sut = new DecreaseHorizontalVelocity(position, 10);
            sut.Target.Velocity.X = 100;

            sut.Update();

            Assert.Equal(90, sut.Target.Velocity.X);
        }

        [Theory, AutoMockData]
        public void Decrease_Negative(PositionComponent position)
        {
            var sut = new DecreaseHorizontalVelocity(position, 10);
            sut.Target.Velocity.X = -100;

            sut.Update();

            Assert.Equal(-90, sut.Target.Velocity.X);
        }
    }
    
}
