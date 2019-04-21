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
                Y = TouchControllerRenderer.BUTTON_LEFT_Y
            };

            touchInputs.GetTouchCollection().Returns(positon.Yield());

            var sut = new TouchControlInputs(touchInputs);
            sut.Update();

            Assert.IsTrue(sut.IsPressingLeft);
            Assert.IsFalse(sut.IsPressingDown);
            Assert.IsFalse(sut.IsPressingRight);
            Assert.IsFalse(sut.IsPressingJump);
        }

        [TestMethod]
        public void pressing_right()
        {
            var touchInputs = Substitute.For<TouchInputs>();

            var positon = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_RIGHT_X+1,
                Y = TouchControllerRenderer.BUTTON_RIGHT_Y
            };

            touchInputs.GetTouchCollection().Returns(positon.Yield());

            var sut = new TouchControlInputs(touchInputs);
            sut.Update();

            Assert.IsTrue(sut.IsPressingRight);
            Assert.IsFalse(sut.IsPressingDown);
            Assert.IsFalse(sut.IsPressingLeft);
            Assert.IsFalse(sut.IsPressingJump);
        }

        [TestMethod]
        public void pressing_down()
        {
            var touchInputs = Substitute.For<TouchInputs>();

            var positon = new Vector2()
            {
                X = TouchControllerRenderer.BUTTON_BOT_X,
                Y = TouchControllerRenderer.BUTTON_BOT_Y
            };

            touchInputs.GetTouchCollection().Returns(positon.Yield());

            var sut = new TouchControlInputs(touchInputs);
            sut.Update();

            Assert.IsTrue(sut.IsPressingDown);
            Assert.IsFalse(sut.IsPressingRight);
            Assert.IsFalse(sut.IsPressingLeft);
            Assert.IsFalse(sut.IsPressingJump);
        }
    }
}
