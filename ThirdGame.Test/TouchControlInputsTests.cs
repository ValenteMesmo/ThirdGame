﻿using AutoFixture;
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
        public void pressing_center_from_right(TouchControlInputs sut)
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
        public void pressing_center_from_left(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTRAL_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_up(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns(sut.TOP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTRAL_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_from_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
             .Returns(sut.BOT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.CENTRAL_BUTTON.Center.ToVector2().Yield());

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
                .Returns(sut.TOP_BUTTON.Center.ToVector2().Yield());

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
                .Returns(sut.TOP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right_from_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.BOT_BUTTON.Center.ToVector2().Yield());

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
                .Returns(sut.BOT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.LEFT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up_from_down(TouchControlInputs sut)
        {
            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.BOT_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            sut.TouchInputs.GetTouchCollection()
                .Returns(sut.TOP_BUTTON.Center.ToVector2().Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }
    }
}
