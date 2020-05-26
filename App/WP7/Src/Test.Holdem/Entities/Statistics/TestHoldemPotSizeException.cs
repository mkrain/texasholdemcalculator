using System;
using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Statistics;

namespace Test.Holdem.Entities.Statistics
{
    [TestFixture]
    public class TestHoldemPotSizeException
    {
        [Test]
        public void PotSizeExceptionConstructorMessage()
        {
            const string expected = "An exception occurred";

            var exception = new PotSizeException(expected);

            Assert.AreEqual(expected, exception.Message);
        }

        [Test]
        public void PotSizeExceptionConstructorException()
        {
            const string expected = "An exception occurred";
            var expectedException = new ArgumentException("ArgumentException");

            var exception = new PotSizeException(expected, expectedException);

            Assert.AreEqual(expected, exception.Message);
            Assert.AreEqual(expectedException, exception.InnerException);
        }
    }
}
