using System;
using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Statistics;

namespace Test.Holdem.Entities.Statistics
{
    [TestFixture]
    public class TestHoldemNumberOfPlayersException
    {
        [Test]
        public void NumberOfPlayersExceptionConstructorMessage()
        {
            const string expected = "An exception occurred";

            var exception = new NumberOfPlayersException(expected);

            Assert.AreEqual(expected, exception.Message);
        }

        [Test]
        public void NumberOfPlayersExceptionConstructorException()
        {
            const string expected = "An exception occurred";
            var expectedException = new ArgumentException("ArgumentException");

            var exception = new NumberOfPlayersException(expected, expectedException);

            Assert.AreEqual(expected, exception.Message);
            Assert.AreEqual(expectedException, exception.InnerException);
        }
    }
}
