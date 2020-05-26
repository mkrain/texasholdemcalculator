using System.Collections.Generic;
using NUnit.Framework;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace Test.Holdem.Entities.StartingHands
{
    [TestFixture]
    public class TestHoldemHand
    {
        [Test]
        public void HandConstructor()
        {
            var hand = new Hand();

            Assert.AreEqual(0, hand.Cards.Count);
        }

        [Test]
        public void HandGetSchema()
        {
            var hand = new Hand();

            Assert.IsNull(hand.GetSchema());
        }

        [Test]
        public void HandFindByHandColumn()
        {
            var hand = new Hand();

            var hands =
                new List<CardValue>
                {
                    new CardValue(Suit.Club, CardName.Two, HoldemCard.River),
                    new CardValue(Suit.Club, CardName.Three, HoldemCard.River),
                    new CardValue(Suit.Club, CardName.Four, HoldemCard.River),
                    new CardValue(Suit.Spade, CardName.Five, HoldemCard.River),
                    new CardValue(Suit.Club, CardName.Six, HoldemCard.River),
                    new CardValue(Suit.Club, CardName.Seven, HoldemCard.River),
                    new CardValue(Suit.Club, CardName.Ace, HoldemCard.River)
                };

            hand.Cards = hands;

            Assert.AreEqual(HoldemCard.River, hand.FindHandByColumn(CardName.Two).HoldemCard);
            Assert.AreEqual(Suit.Spade, hand.FindHandByColumn(CardName.Five).Suit);
            Assert.AreEqual(CardName.Ace, hand.FindHandByColumn(CardName.Ace).Name);
        }
    }
}
