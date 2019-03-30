using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThirdGame;

namespace UnitTestProject
{
    [TestClass]
    public class ServerIpFinderTests
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var sut = new ServerIpFinder();
            var ips = new[] { "1", "2", "3" };

            var actual = sut.FindIp(ips,"4");
            Assert.AreEqual("1", actual);
        }

        [TestMethod]
        public void MyTestMethod2()
        {
            var sut = new ServerIpFinder();
            var ips = new[] { "4", "2", "3" };

            var actual = sut.FindIp(ips, "1");
            Assert.AreEqual("1", actual);
        }
    }
}
