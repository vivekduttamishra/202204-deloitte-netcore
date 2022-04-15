using System;
using Xunit;

namespace BooksWebUnitTests
{
    public class PrimeTests
    {
        [Fact]
        public void IsPrimeShouldReturnFalseFor15()
        {
            var number = 15;
            var actualResult = PrimeUtils.IsPrime(number);

            Assert.Equal(false, actualResult);

        }
        [Fact]
        public void IsPrimeShouldReturnTruefor2()
        {
            var number = 2;
            var actualResult = PrimeUtils.IsPrime(number);

            Assert.True(actualResult);
        }
        [Fact]
        public void IsPrimeShouldReturnFalseFor0()
        {
            Assert.False(PrimeUtils.IsPrime(0));
        }
    }
}
