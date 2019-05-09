using AutoFixture;
using Common;
using NSubstitute;
using Xunit;

namespace ThirdGame.Tests
{
    public class TouchControlInputsTests
    {
        [Theory, AutoMockData]
        public void pressing_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right(IFixture fixture, TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());
            var aa = new TouchControlInputs(fixture.Create<TouchInputs>());
            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
              .Returns(sut.Dpad.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_center_center(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_rightdown_rightdown(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_rightdown_leftup_leftup(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_rightdown_rightup_rightup(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_leftdown_leftup_leftup(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_leftup_leftdown_leftdown(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_rightup_rightdown_rightdown(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns(sut.Dpad.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_up(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns(sut.Dpad.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_down_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_down_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_up_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_up_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns(sut.Dpad.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }


        [Theory, AutoMockData]
        public void pressing_up_from_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_up(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_up(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_down_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_down_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_down_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_down_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_dowm_from_down_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_dowm_from_down_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_dowm_downRight_downRight(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns(sut.Dpad.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();
            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_dowm_downLeft_downLeft(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns(sut.Dpad.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();
            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_dowm_from_up_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_dowm_from_up_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_up_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_up_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_up_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_up_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_up_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_up_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_down_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_down_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.Dpad.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.Dpad.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }
    }
}
