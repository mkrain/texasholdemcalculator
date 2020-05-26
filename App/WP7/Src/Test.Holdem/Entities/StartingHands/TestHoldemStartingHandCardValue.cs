using NUnit.Framework;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace Test.Holdem.Entities.StartingHands
{
    [TestFixture]
    public class TestHoldemStartingHandCardValue
    {
        [Test]
        public void StartingHandCardValue()
        {
            var hand = new StartingHandCardValue();

            Assert.AreEqual(Suit.Club, hand.Suit);
            Assert.AreEqual(CardName.Two, hand.Name);
            Assert.AreEqual(HoldemCard.Hole1, hand.HoldemCard);
        }

        [Test]
        public void StartingHandCardValueConstructorSuitCardName()
        {
            var hand = new StartingHandCardValue(Suit.Heart, CardName.Queen);

            Assert.AreEqual(Suit.Heart, hand.Suit);
            Assert.AreEqual(CardName.Queen, hand.Name);
        }

        [Test]
        public void StartingHandCardValueConstructorSuitCardNameHoldemCard()
        {
            var hand = new StartingHandCardValue(Suit.Heart, CardName.Queen, HoldemCard.River);

            Assert.AreEqual(Suit.Heart, hand.Suit);
            Assert.AreEqual(CardName.Queen, hand.Name);
            Assert.AreEqual(HoldemCard.River, hand.HoldemCard);
        }

        [Test]
        public void StartingHandCardValueToString()
        {
            var hand = new StartingHandCardValue(Suit.Spade, CardName.Queen, HoldemCard.River);

            const string expected = "Name[Queen], Suit[Spade].";

            Assert.AreEqual(expected, hand.ToString());
        }
    }
}