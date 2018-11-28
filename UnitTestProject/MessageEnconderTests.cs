using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ThirdGame;

namespace UnitTestProject
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class MessageEnconderTests
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var sut = new MyMessageEncoder();

            var expected = new Message(1, 2, 3, true, false, true, false, true, false, true, false);
            var encoded = sut.Encode(expected);
            var actual = sut.Decode(encoded).First();

            Assert.AreEqual(expected.Time, actual.Time);
            Assert.AreEqual(expected.X, actual.X);
            Assert.AreEqual(expected.Y, actual.Y);
            Assert.AreEqual(expected.Up, actual.Up);
            Assert.AreEqual(expected.Down, actual.Down);
            Assert.AreEqual(expected.Left, actual.Left);
            Assert.AreEqual(expected.Right, actual.Right);
            Assert.AreEqual(expected.A, actual.A);
            Assert.AreEqual(expected.B, actual.B);
            Assert.AreEqual(expected.C, actual.C);
            Assert.AreEqual(expected.D, actual.D);
        }
    }
}
