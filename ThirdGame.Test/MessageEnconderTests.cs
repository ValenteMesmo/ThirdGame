using Xunit;
using System.Linq;
using Common;
using Microsoft.Xna.Framework;
using NSubstitute;

namespace ThirdGame.Tests
{
    public class TouchControlInputsTests
    {
        [Theory, AutoMockData]
        public void pressing_left(TouchControlInputs sut)
        {
            var positon = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_LEFT_X,
                Y = TouchControllerRenderer.BUTTON_LEFT_Y + 30
            };
            sut.TouchInputs.GetTouchCollection().Returns(positon.Yield());

            sut.Update();

            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_right(TouchControlInputs sut)
        {


            var positon = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_RIGHT_X + 60,
                Y = TouchControllerRenderer.BUTTON_RIGHT_Y + 60
            };

            sut.TouchInputs.GetTouchCollection().Returns(positon.Yield());


            sut.Update();

            Assert.Equal(DpadDirection.Right, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_down(TouchControlInputs sut)
        {


            var positon = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_BOT_X + TouchControllerRenderer.BUTTON_WIDTH / 2,
                Y = TouchControllerRenderer.BUTTON_BOT_Y + TouchControllerRenderer.BUTTON_HEIGHT / 2
            };

            sut.TouchInputs.GetTouchCollection().Returns(positon.Yield());


            sut.Update();

            Assert.Equal(DpadDirection.Down, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_up(TouchControlInputs sut)
        {


            var positon = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_TOP_X + 20,
                Y = TouchControllerRenderer.BUTTON_TOP_Y
            };

            sut.TouchInputs.GetTouchCollection().Returns(positon.Yield());


            sut.Update();

            Assert.Equal(DpadDirection.Up, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_with_left_intention(TouchControlInputs sut)
        {


            var positon_right = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_RIGHT_X + TouchControllerRenderer.BUTTON_WIDTH / 2,
                Y = TouchControllerRenderer.BUTTON_RIGHT_Y + TouchControllerRenderer.BUTTON_HEIGHT / 2
            };

            var positon_center = new Vector2()
            {
                X = -443.7177f,
                Y = 137.63858f
            };

            sut.TouchInputs.GetTouchCollection().Returns(positon_right.Yield());


            sut.Update();
            sut.TouchInputs.GetTouchCollection().Returns(positon_center.Yield());
            sut.Update();


            Assert.Equal(DpadDirection.Left, sut.Direction);
        }

        [Theory, AutoMockData]
        public void pressing_center_with_right_intention(TouchControlInputs sut)
        {


            var positon_left = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_LEFT_X + TouchControllerRenderer.BUTTON_WIDTH / 2,
                Y = TouchControllerRenderer.BUTTON_LEFT_Y + TouchControllerRenderer.BUTTON_HEIGHT / 2
            };

            var positon_center = new Vector2()
            {
                X = -443.7177f,
                Y = 137.63858f
            };

            sut.TouchInputs.GetTouchCollection().Returns(positon_left.Yield());


            sut.Update();
            sut.TouchInputs.GetTouchCollection().Returns(positon_center.Yield());
            sut.Update();


            Assert.Equal(DpadDirection.Right, sut.Direction);
        }
    }


    public class MessageEnconderTests
    {
        [Theory, AutoMockData]
        public void MyTestMethod(MyMessageEncoder sut)
        {
            var expected = new Message(1, 2, 3, true, false, true, false, true, false, true, false);
            var encoded = sut.Encode(expected);
            var actual = sut.Decode(encoded).First();

            Assert.Equal(expected.Time, actual.Time);
            Assert.Equal(expected.X, actual.X);
            Assert.Equal(expected.Y, actual.Y);
            Assert.Equal(expected.Up, actual.Up);
            Assert.Equal(expected.Down, actual.Down);
            Assert.Equal(expected.Left, actual.Left);
            Assert.Equal(expected.Right, actual.Right);
            Assert.Equal(expected.A, actual.A);
            Assert.Equal(expected.B, actual.B);
            Assert.Equal(expected.C, actual.C);
            Assert.Equal(expected.D, actual.D);
        }

        [Theory, AutoMockData]
        public void MyTestMethod2(MyMessageEncoder sut)
        {
            var expected = new Message(-100, -200, 3, true, false, true, false, true, false, true, false);
            var encoded = sut.Encode(expected);
            foreach (var actual in sut.Decode(encoded))
            {
                Assert.Equal(expected.Time, actual.Time);
                Assert.Equal(expected.X, actual.X);
                Assert.Equal(expected.Y, actual.Y);
                Assert.Equal(expected.Up, actual.Up);
                Assert.Equal(expected.Down, actual.Down);
                Assert.Equal(expected.Left, actual.Left);
                Assert.Equal(expected.Right, actual.Right);
                Assert.Equal(expected.A, actual.A);
                Assert.Equal(expected.B, actual.B);
                Assert.Equal(expected.C, actual.C);
                Assert.Equal(expected.D, actual.D);
            }
        }

        [Theory, AutoMockData]
        public void MyTestMethod3(MyMessageEncoder sut)
        {
            var expected = new Message(10000, 20000, 3000000, true, false, true, false, true, false, true, false);
            var encoded = sut.Encode(expected);
            foreach (var actual in sut.Decode(encoded))
            {
                Assert.Equal(expected.Time, actual.Time);
                Assert.Equal(expected.X, actual.X);
                Assert.Equal(expected.Y, actual.Y);
                Assert.Equal(expected.Up, actual.Up);
                Assert.Equal(expected.Down, actual.Down);
                Assert.Equal(expected.Left, actual.Left);
                Assert.Equal(expected.Right, actual.Right);
                Assert.Equal(expected.A, actual.A);
                Assert.Equal(expected.B, actual.B);
                Assert.Equal(expected.C, actual.C);
                Assert.Equal(expected.D, actual.D);
            }
        }
    }
}
