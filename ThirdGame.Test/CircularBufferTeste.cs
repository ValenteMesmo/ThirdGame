using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ThirdGame.Tests
{
    public class CircularBufferTeste
    {
        [Theory, AutoMockData]
        public void Teste1()
        {
            CircularBuffer sut = new CircularBuffer(3);
            sut.Add(1);
            var actual = sut.Get().Last();

            Assert.Equal(1, actual);
        }

        [Theory, AutoMockData]
        public void Teste2()
        {
            CircularBuffer sut = new CircularBuffer(3);
            sut.Add(1);
            sut.Add(2);
            sut.Add(3);
            var actual = sut.Get().ToArray();

            Assert.Equal(1, actual[0]);
            Assert.Equal(2, actual[1]);
            Assert.Equal(3, actual[2]);
        }
    }
}
