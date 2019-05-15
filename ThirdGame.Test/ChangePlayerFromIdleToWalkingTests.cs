using Xunit;
using NSubstitute;
using Common;

namespace ThirdGame.Tests
{
    public class ChangePlayerFromIdleToWalkingTests
    {
        [Theory, AutoMockPlayerData]
        public void Falling_to_idle_right(ChangePlayerFromIdleToWalking sut)
        {
            sut.Player.State = PlayerState.IDLE_RIGHT;
            sut.Player.Grounded = true;
            sut.Player.Inputs.Direction.Returns(DpadDirection.Right);

            sut.Update();
            Assert.Equal(PlayerState.WALKING_RIGHT, sut.Player.State);
        }

        [Theory, AutoMockPlayerData]
        public void Falling_to_idle_left(ChangePlayerFromIdleToWalking sut)
        {
            sut.Player.State = PlayerState.IDLE_LEFT;
            sut.Player.Grounded = true;
            sut.Player.Inputs.Direction.Returns(DpadDirection.Left);

            sut.Update();
            Assert.Equal(PlayerState.WALKING_LEFT, sut.Player.State);
        }
    }
}
