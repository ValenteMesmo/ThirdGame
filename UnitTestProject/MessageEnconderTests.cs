using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ThirdGame;

namespace UnitTestProject
{
    [TestClass]
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

        [TestMethod]
        public void MyTestMethod2()
        {
            var sut = new MyMessageEncoder();

            var expected = new Message(-100, -200, 3, true, false, true, false, true, false, true, false);
            var encoded = sut.Encode(expected);
            foreach (var actual in sut.Decode(encoded))
            {
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

        [TestMethod]
        public void MyTestMethod3()
        {
            var sut = new MyMessageEncoder();

            var expected = new Message(10000, 20000, 3000000, true, false, true, false, true, false, true, false);
            var encoded = sut.Encode(expected);
            foreach (var actual in sut.Decode(encoded))
            {
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
}
