using Xunit;

namespace ThirdGame.Tests
{
    public class ChangePlayerFromIdleToFallingTests
    {
        [Theory, AutoMockPlayerData]
        public void Idle_to_falling_right(ChangePlayerFromIdleToFalling sut)
        {
            sut.Player.State = PlayerState.IDLE_RIGHT;

            sut.Update();
            Assert.Equal(PlayerState.FALLING_RIGHT, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Idle_to_falling_left(ChangePlayerFromIdleToFalling sut)
        {
            sut.Player.State = PlayerState.IDLE_LEFT;

            sut.Update();
            Assert.Equal(PlayerState.FALLING_LEFT, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Idle_left_to_falling_idle(ChangePlayerFromIdleToFalling sut)
        {
            sut.Player.Grounded = true;
            sut.Player.State = PlayerState.IDLE_LEFT;

            sut.Update();
            Assert.Equal(PlayerState.IDLE_LEFT, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Idle_right_to_falling_idle(ChangePlayerFromIdleToFalling sut)
        {
            sut.Player.Grounded = true;
            sut.Player.State = PlayerState.IDLE_RIGHT;

            sut.Update();
            Assert.Equal(PlayerState.IDLE_RIGHT, sut.Player.State);
        }
    }
}
