using Xunit;
using Common;
using NSubstitute;

namespace ThirdGame.Tests
{
    public class TouchControlInputsTests
    {
        [Theory, AutoMockData]
        public void pressing_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
              .Returns(sut.BOT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.TOP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_with_left_intention(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTRAL_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_with_right_intention(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTRAL_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }
    }
}
