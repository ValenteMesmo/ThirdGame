using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThirdGame;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = "loltrollei";
            var sut = new MyBlueToothChannel();
            var encoded = sut.Encoding2(input);
            var output = sut.Decoding(encoded);

        }
    }
}
