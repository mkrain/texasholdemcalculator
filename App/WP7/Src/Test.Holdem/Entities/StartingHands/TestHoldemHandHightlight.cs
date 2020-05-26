using NUnit.Framework;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace Test.Holdem.Entities.StartingHands
{
    [TestFixture]
    public class TestHoldemHandHightlight
    {
        [Test]
        public void HandHightlightConstructor()
        {
            var highlight = new HandHighlight();

            Assert.AreEqual(0, highlight.HandStrength.Count);
        }

        [Test]
        public void HandHightlightGetSchema()
        {
            var highlight = new HandHighlight();

            Assert.IsNull(highlight.GetSchema());
        }
    }
}
