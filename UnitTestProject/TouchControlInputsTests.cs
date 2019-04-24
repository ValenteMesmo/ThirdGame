using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using NSubstitute;
using ThirdGame;

namespace UnitTestProject
{
    [TestClass]
    public class TouchControlInputsTests
    {
        [TestMethod]
        public void pressing_left()
        {
            var touchInputs = Substitute.For<TouchInputs>();

            var positon = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_LEFT_X,
                Y = TouchControllerRenderer.BUTTON_LEFT_Y + 20
            };

            touchInputs.GetTouchCollection().Returns(positon.Yield());

            var sut = new TouchControlInputs(touchInputs);
            sut.Update();

            Assert.AreEqual(Direction.Left, sut.Direction);
        }

        [TestMethod]
        public void pressing_right()
        {
            var touchInputs = Substitute.For<TouchInputs>();

            var positon = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_RIGHT_X + 60,
                Y = TouchControllerRenderer.BUTTON_RIGHT_Y + 60
            };

            touchInputs.GetTouchCollection().Returns(positon.Yield());

            var sut = new TouchControlInputs(touchInputs);
            sut.Update();

            Assert.AreEqual(Direction.Right, sut.Direction);
        }

        [TestMethod]
        public void pressing_down()
        {
            var touchInputs = Substitute.For<TouchInputs>();

            var positon = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_BOT_X + TouchControllerRenderer.BUTTON_WIDTH / 2,
                Y = TouchControllerRenderer.BUTTON_BOT_Y + TouchControllerRenderer.BUTTON_HEIGHT / 2
            };

            touchInputs.GetTouchCollection().Returns(positon.Yield());

            var sut = new TouchControlInputs(touchInputs);
            sut.Update();

            Assert.AreEqual(Direction.Down, sut.Direction);
        }

        [TestMethod]
        public void pressing_up()
        {
            var touchInputs = Substitute.For<TouchInputs>();

            var positon = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_TOP_X + 20,
                Y = TouchControllerRenderer.BUTTON_TOP_Y
            };

            touchInputs.GetTouchCollection().Returns(positon.Yield());

            var sut = new TouchControlInputs(touchInputs);
            sut.Update();

            Assert.AreEqual(Direction.Up, sut.Direction);
        }

        [TestMethod]
        public void pressing_center_with_left_intention()
        {
            var touchInputs = Substitute.For<TouchInputs>();

            var positon_right = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_RIGHT_X + TouchControllerRenderer.BUTTON_WIDTH /2,
                Y = TouchControllerRenderer.BUTTON_RIGHT_Y + TouchControllerRenderer.BUTTON_HEIGHT / 2
            };

            var positon_center = new Vector2()
            {
                X = -443.7177f,
                Y = 137.63858f
            };

            touchInputs.GetTouchCollection().Returns(positon_right.Yield());

            var sut = new TouchControlInputs(touchInputs);
            sut.Update();
            touchInputs.GetTouchCollection().Returns(positon_center.Yield());
            sut.Update();


            Assert.AreEqual(Direction.Left, sut.Direction);
        }

        [TestMethod]
        public void pressing_center_with_right_intention()
        {
            var touchInputs = Substitute.For<TouchInputs>();

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

            touchInputs.GetTouchCollection().Returns(positon_left.Yield());

            var sut = new TouchControlInputs(touchInputs);
            sut.Update();
            touchInputs.GetTouchCollection().Returns(positon_center.Yield());
            sut.Update();


            Assert.AreEqual(Direction.Right, sut.Direction);
        }
    }
}
