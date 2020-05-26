using NUnit.Framework;
using TexasHoldemCalculator.Interfaces.Card;

namespace Test.Holdem.Entities.Cards
{
    [TestFixture]
    public class CardValueTest
    {
        [Test]
        public void CardValueConstructor()
        {
            var cardValue = new CardValue();

            Assert.AreEqual(Suit.Club, cardValue.Suit);
            Assert.AreEqual(CardName.Two, cardValue.Name);
            Assert.AreEqual(HoldemCard.Hole1, cardValue.HoldemCard);
        }

        [Test]
        public void CardValueConstructorICardValue()
        {
            CardValue iCardValue = new CardValue(Suit.Heart, CardName.Two, HoldemCard.Turn);
            var cardValue = new CardValue(iCardValue);

            Assert.AreEqual(Suit.Heart, cardValue.Suit);
            Assert.AreEqual(CardName.Two, cardValue.Name);
            Assert.AreEqual(HoldemCard.Turn, cardValue.HoldemCard);
        }

        [Test]
        public void CardValueConstructorSuitAndCardName()
        {
            var cardValue = new CardValue(Suit.Heart, CardName.Ace);

            Assert.AreEqual(Suit.Heart, cardValue.Suit);
            Assert.AreEqual(CardName.Ace, cardValue.Name);
        }

        [Test]
        public void CardValueConstructorSuitCardNameAndHoldemCard()
        {
            var cardValue = new CardValue(Suit.Heart, CardName.Ace, HoldemCard.Turn);

            Assert.AreEqual(Suit.Heart, cardValue.Suit);
            Assert.AreEqual(CardName.Ace, cardValue.Name);
            Assert.AreEqual(HoldemCard.Turn, cardValue.HoldemCard);
        }
    }
}
