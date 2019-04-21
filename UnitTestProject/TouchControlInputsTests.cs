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
                Y = TouchControllerRenderer.BUTTON_LEFT_Y+20
            };

            touchInputs.GetTouchCollection().Returns(positon.Yield());

            var sut = new TouchControlInputs(touchInputs);
            sut.Update();

            Assert.IsTrue(sut.IsPressingLeft);
            Assert.IsFalse(sut.IsPressingDown);
            Assert.IsFalse(sut.IsPressingRight);
            Assert.IsFalse(sut.IsPressingUp);
            Assert.IsFalse(sut.IsPressingJump);
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

            Assert.IsTrue(sut.IsPressingRight);
            Assert.IsFalse(sut.IsPressingDown);
            Assert.IsFalse(sut.IsPressingLeft);
            Assert.IsFalse(sut.IsPressingJump);
            Assert.IsFalse(sut.IsPressingUp);
        }

        [TestMethod]
        public void pressing_down()
        {
            var touchInputs = Substitute.For<TouchInputs>();

            var positon = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_BOT_X + 60,
                Y = TouchControllerRenderer.BUTTON_BOT_Y+20
            };

            touchInputs.GetTouchCollection().Returns(positon.Yield());

            var sut = new TouchControlInputs(touchInputs);
            sut.Update();

            Assert.IsTrue(sut.IsPressingDown);
            Assert.IsFalse(sut.IsPressingRight);
            Assert.IsFalse(sut.IsPressingLeft);
            Assert.IsFalse(sut.IsPressingJump);
            Assert.IsFalse(sut.IsPressingUp);
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

            Assert.IsTrue(sut.IsPressingUp);
            Assert.IsFalse(sut.IsPressingDown);
            Assert.IsFalse(sut.IsPressingRight);
            Assert.IsFalse(sut.IsPressingLeft);
            Assert.IsFalse(sut.IsPressingJump);
        }
    }
}
