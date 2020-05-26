using NUnit.Framework;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace Test.Holdem.Entities.StartingHands
{
    [TestFixture]
    public class TestHoldemHandStrength
    {
        [Test]
        public void HandStrengthGetSchema()
        {
            var strength = new HandStrength();

            Assert.IsNull(strength.GetSchema());
        }
    }
}