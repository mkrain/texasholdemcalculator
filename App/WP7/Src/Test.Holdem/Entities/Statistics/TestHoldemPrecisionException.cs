using System;
using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Statistics;

namespace Test.Holdem.Entities.Statistics
{
    [TestFixture]
    public class TestHoldemPrecisionException
    {
        [Test]
        public void PrecisionExceptionConstructorMessage()
        {
            const string expected = "An exception occurred";

            var exception = new PrecisionException(expected);

            Assert.AreEqual(expected, exception.Message);
        }

        [Test]
        public void PrecisionExceptionConstructorException()
        {
            const string expected = "An exception occurred";
            var expectedException = new ArgumentException("ArgumentException");

            var exception = new PrecisionException(expected, expectedException);

            Assert.AreEqual(expected, exception.Message);
            Assert.AreEqual(expectedException, exception.InnerException);
        }
    }
}
