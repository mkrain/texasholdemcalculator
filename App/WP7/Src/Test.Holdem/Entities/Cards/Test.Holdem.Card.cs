
using System;
using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Cards;
using TexasHoldemCalculator.Interfaces.Card;

namespace Test.Holdem.Entities.Cards
{
    [TestFixture]
    public class TestHoldemCard
    {
        [Test]
        public void CardConstructor()
        {
            Assert.DoesNotThrow(() => new Card());
        }

        [Test]
        public void CardICardValueConstructor()
        {
            Card card = null;
            var cardValue = new CardValue(Suit.Heart, CardName.Queen);

            Assert.DoesNotThrow(() => card = new Card(cardValue));
            Assert.IsNotNull(card);
            Assert.AreEqual(Suit.Heart, card.CardSuit);
            Assert.AreEqual(CardName.Queen, card.Name);
        }

        [Test]
        public void CardGetImage()
        {
            var card = new Card();

            //Assert.Throws<NotImplementedException>(
            //    () => card.Image = null);
        }

        [Test]
        public void CardSetImage()
        {
            var card = new Card();

            //Assert.Throws<NotImplementedException>(
            //    () =>
            //    {
            //        var image = card.Image;
            //    });
        }

        [Test]
        public void CardGetCardPath()
        {
            var card = new Card();

            //Assert.Throws<NotImplementedException>(
            //    () => card.CardPath = null);
        }

        [Test]
        public void CardSetCardPath()
        {
            var card = new Card();

            //Assert.Throws<NotImplementedException>(
            //    () =>
            //    {
            //        var path = card.CardPath;
            //    });
        }

        [Test]
        public void CardSetCardId()
        {
            var card = new Card();

            card.CardId = (long)CardName.Queen;

            Assert.AreEqual(card.Name, (CardName)card.CardId);
        }

        [Test]
        public void CardGetCardId()
        {
            var card = new Card();

            Assert.AreEqual(CardName.Two, (CardName)card.CardId);
        }

        [Test]
        public void CardSetCardValue()
        {
            var card = new Card();

            card.CardValue = (long)CardName.Queen;

            Assert.AreEqual(card.Name, (CardName)card.CardValue);
        }

        [Test]
        public void CardGetCardValue()
        {
            var card = new Card();

            Assert.AreEqual(CardName.Two, (CardName)card.CardValue);
        }

        [Test]
        public void CardEqualsCardTrue()
        {
            var card = new Card(CardName.Queen, Suit.Heart);
            var card1 = new Card(CardName.Queen, Suit.Heart);

            Assert.IsTrue(card.Equals(card1));
        }

        [Test]
        public void CardEqualsCardFalse()
        {
            var card = new Card(CardName.Queen, Suit.Heart);
            var card1 = new Card(CardName.Ace, Suit.Heart);

            Assert.IsFalse(card.Equals(card1));
        }

        [Test]
        public void CardEqualsObjectTrue()
        {
            var card = new Card(CardName.Queen, Suit.Heart);
            object card1 = new Card(CardName.Queen, Suit.Heart);

            Assert.IsTrue(card.Equals(card1));
        }

        [Test]
        public void CardEqualsObjectFalse()
        {
            var card = new Card(CardName.Queen, Suit.Heart);
            object card1 = new Card(CardName.Ace, Suit.Heart);

            Assert.IsFalse(card.Equals(card1));
        }

        [Test]
        public void CardEqualsObjectInvalidFalse()
        {
            var card = new Card(CardName.Queen, Suit.Heart);
            object card1 = "Not a card";

            Assert.IsFalse(card.Equals(card1));
        }

        [Test]
        public void CardGetHashCode()
        {
            var expected = ((long)CardName.Queen).GetHashCode();
            expected = (expected * 397) ^ Suit.Heart.GetHashCode();
            expected = (expected * 397) ^ CardName.Queen.GetHashCode();

            var card = new Card(CardName.Queen, Suit.Heart);

            Assert.AreEqual(expected, card.GetHashCode());
        }

        [Test]
        public void CardGetCardTextLessJack()
        {
            var card = new Card(CardName.Ten, Suit.Heart);

            const string expected = "10";

            Assert.AreEqual(expected, card.CardText);

            card.Name = CardName.Two;

            const string expectedTwo = "2";

            Assert.AreEqual(expectedTwo, card.CardText);
        }

        [Test]
        public void CardGetCardTextGreaterJack()
        {
            var card = new Card(CardName.Jack, Suit.Heart);

            const string expectedJack = "J";

            Assert.AreEqual(expectedJack, card.CardText);

            card.Name = CardName.Ace;

            const string expectedAce = "A";

            Assert.AreEqual(expectedAce, card.CardText);
        }

        [Test]
        public void CardSetCardText()
        {
            var card = new Card(CardName.Jack, Suit.Heart);

            card.CardText = string.Empty;

            const string expected = "J";

            Assert.AreEqual(expected, card.CardText);
        }


    }
}
