using Xunit;

namespace ThirdGame.Tests
{
    public class ChangePlayerFromFallingToIdleTests
    {
        [Theory, AutoMockPlayerData]
        public void Falling_to_idle_right(ChangePlayerFromFallingToIdle sut)
        {
            sut.Player.State = PlayerState.FALLING_RIGHT;
            sut.Player.Grounded = true;

            sut.Update();
            Assert.Equal(PlayerState.IDLE_RIGHT, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Falling_to_idle_left(ChangePlayerFromFallingToIdle sut)
        {
            sut.Player.State = PlayerState.FALLING_LEFT;
            sut.Player.Grounded = true;

            sut.Update();
            Assert.Equal(PlayerState.IDLE_LEFT, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Falling_to_falling_left(ChangePlayerFromFallingToIdle sut)
        {
            sut.Player.State = PlayerState.FALLING_LEFT;

            sut.Update();
            Assert.Equal(PlayerState.FALLING_LEFT, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Falling_to_falling_right(ChangePlayerFromFallingToIdle sut)
        {
            sut.Player.State = PlayerState.FALLING_RIGHT;

            sut.Update();
            Assert.Equal(PlayerState.FALLING_RIGHT, sut.Player.State);
        }
    }
}
