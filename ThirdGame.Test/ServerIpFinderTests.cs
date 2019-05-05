using Xunit;

namespace ThirdGame.Tests
{
    public class ServerIpFinderTests
    {
        [Theory, AutoMockData]
        public void MyTestMethod(ServerIpFinder sut)
        {
            var ips = new[] { "1", "2", "3" };

            var actual = sut.FindIp(ips, "4");
            Assert.Equal("1", actual);
        }

        [Theory, AutoMockData]
        public void MyTestMethod2(ServerIpFinder sut)
        {
            var ips = new[] { "4", "2", "3" };

            var actual = sut.FindIp(ips, "1");
            Assert.Equal("1", actual);
        }
    }
}
