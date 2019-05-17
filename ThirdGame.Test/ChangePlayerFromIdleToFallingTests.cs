using Xunit;

namespace ThirdGame.Tests
{
    public class ChangePlayerFromIdleToFallingTests
    {
        [Theory, AutoMockPlayerData]
        public void Idle_to_falling(ChangePlayerStateToFalling sut)
        {
            sut.Player.State = PlayerState.IDLE;
            sut.Player.Grounded = false;
            sut.Player.Velocity.Y = 10;
            sut.Update();
            Assert.Equal(PlayerState.FALLING, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Idle_left_to_falling_idle(ChangePlayerStateToFalling sut)
        {
            sut.Player.Grounded = true;
            sut.Player.FacingRight = false;
            sut.Player.State = PlayerState.IDLE;

            sut.Update();
            Assert.Equal(PlayerState.IDLE, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Idle_right_to_falling_idle(ChangePlayerStateToFalling sut)
        {
            sut.Player.Grounded = true;
            sut.Player.FacingRight = true;
            sut.Player.State = PlayerState.IDLE;

            sut.Update();
            Assert.Equal(PlayerState.IDLE, sut.Player.State);
        }
    }
}
