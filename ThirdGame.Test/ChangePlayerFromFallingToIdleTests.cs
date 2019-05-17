using Xunit;

namespace ThirdGame.Tests
{
    public class ChangePlayerFromFallingToIdleTests
    {
        [Theory, AutoMockPlayerData]
        public void Falling_to_idle_right(ChangePlayerStateToIdle sut)
        {
            sut.Player.State = PlayerState.FALLING;
            sut.Player.FacingRight = true;
            sut.Player.Grounded = true;

            sut.Update();
            Assert.Equal(PlayerState.IDLE, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Falling_to_idle_left(ChangePlayerStateToIdle sut)
        {
            sut.Player.State = PlayerState.FALLING;
            sut.Player.FacingRight = false;
            sut.Player.Grounded = true;

            sut.Update();
            Assert.Equal(PlayerState.IDLE, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Falling_to_falling_left(ChangePlayerStateToIdle sut)
        {
            sut.Player.State = PlayerState.FALLING;
            sut.Player.FacingRight = false;

            sut.Update();
            Assert.Equal(PlayerState.FALLING, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Falling_to_falling_right(ChangePlayerStateToIdle sut)
        {
            sut.Player.State = PlayerState.FALLING;
            sut.Player.FacingRight = true;

            sut.Update();
            Assert.Equal(PlayerState.FALLING, sut.Player.State);
        }
    }
}
