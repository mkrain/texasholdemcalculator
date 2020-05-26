using System;
using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Statistics;

namespace Test.Holdem.Entities.Statistics
{
    [TestFixture]
    public class TestHoldemNumberOfOutsException
    {
        [Test]
        public void NumberOfOutsExceptionConstructorMessage()
        {
            const string expected = "An exception occurred";

            var exception = new NumberOfOutsException(expected);

            Assert.AreEqual(expected, exception.Message);
        }

        [Test]
        public void NumberOfOutsExceptionConstructorException()
        {
            const string expected = "An exception occurred";
            var expectedException = new ArgumentException("ArgumentException");

            var exception = new NumberOfOutsException(expected, expectedException);

            Assert.AreEqual(expected, exception.Message);
            Assert.AreEqual(expectedException, exception.InnerException);
        }
    }
}
