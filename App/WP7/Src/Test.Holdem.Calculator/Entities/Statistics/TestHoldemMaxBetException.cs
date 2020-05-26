using System;
using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Statistics;

namespace Test.Holdem.Entities.Statistics
{
    [TestFixture]
    public class TestHoldemMaxBetException
    {
        [Test]
        public void MaxBetExceptionConstructorMessage()
        {
            const string expected = "An exception occurred";

            var exception = new MaxBetException(expected);

            Assert.AreEqual(expected, exception.Message);
        }

        [Test]
        public void MaxBetExceptionConstructorException()
        {
            const string expected = "An exception occurred";
            var expectedException = new ArgumentException("ArgumentException");

            var exception = new MaxBetException(expected, expectedException);

            Assert.AreEqual(expected, exception.Message);
            Assert.AreEqual(expectedException, exception.InnerException);
        }
    }
}
