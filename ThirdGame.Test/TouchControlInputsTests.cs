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
                .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right(IFixture fixture, TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());
            var aa = new TouchControlInputs(fixture.Create<TouchInputs>());
            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
              .Returns(sut.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_up(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns(sut.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_down_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_down_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_up_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_up_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns(sut.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTER_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }


        [Theory, AutoMockData]
        public void pressing_up_from_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_up(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_up(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_down_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_down_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_down_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_down_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_dowm_from_down_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_dowm_from_down_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_dowm_from_up_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_dowm_from_up_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_up_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_up_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_up_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_up_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_up_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.LEFT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_up_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, -TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_left_from_down_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_down_right(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns((sut.RIGHT_BUTTON.Center.ToVector2() + new Microsoft.Xna.Framework.Vector2(0, TouchControllerRenderer.BUTTON_HEIGHT)).Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.RIGHT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.DOWN_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.UP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }
    }
}
