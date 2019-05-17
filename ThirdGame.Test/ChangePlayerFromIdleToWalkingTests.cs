using Xunit;
using NSubstitute;
using Common;

namespace ThirdGame.Tests
{
    public class ChangePlayerFromIdleToWalkingTests
    {
        [Theory, AutoMockPlayerData]
        public void Falling_to_idle_right(ChangePlayerStateToWalking sut)
        {
            sut.Player.State = PlayerState.IDLE;
            sut.Player.Grounded = true;
            sut.Player.Inputs.Direction.Returns(DpadDirection.Right);

            sut.Update();
            Assert.Equal(PlayerState.WALKING, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Falling_to_idle_left(ChangePlayerStateToWalking sut)
        {
            sut.Player.State = PlayerState.IDLE;
            sut.Player.Grounded = true;
            sut.Player.Inputs.Direction.Returns(DpadDirection.Left);

            sut.Update();
            Assert.Equal(PlayerState.WALKING, sut.Player.State);
        }
    }
}
