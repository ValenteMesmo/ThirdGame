using System.Linq;
using Xunit;

namespace ThirdGame.Tests
{
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
