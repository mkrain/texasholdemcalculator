using System.Collections.Generic;
using NUnit.Framework;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace Test.Holdem.Entities.StartingHands
{
    [TestFixture]
    public class TestHoldemStartingHand
    {
        #region Helper Methods

        public Hand GetHand()
        {
            var hand = new Hand();

            hand.Cards.Add(new CardValue(Suit.Heart, CardName.Ace));
            hand.Cards.Add(new CardValue(Suit.Heart, CardName.King));
            hand.Cards.Add(new CardValue(Suit.Heart, CardName.Queen));
            hand.Cards.Add(new CardValue(Suit.Heart, CardName.Two));
            hand.Cards.Add(new CardValue(Suit.Heart, CardName.Seven));
            hand.Cards.Add(new CardValue(Suit.Heart, CardName.Jack));
            hand.Cards.Add(new CardValue(Suit.Heart, CardName.Ten));

            return hand;
        }

        public Hand GetGridHand()
        {
            var hand = new Hand();

            hand.Cards.Add(new CardValue { Suit = Suit.Heart, Name = CardName.Ace, Parent = CardName.Two });
            hand.Cards.Add(new CardValue { Suit = Suit.Heart, Name = CardName.King, Parent = CardName.Three });
            hand.Cards.Add(new CardValue { Suit = Suit.Heart, Name = CardName.Queen, Parent = CardName.Four });
            hand.Cards.Add(new CardValue { Suit = Suit.Heart, Name = CardName.Jack, Parent = CardName.Five });
            hand.Cards.Add(new CardValue { Suit = Suit.Heart, Name = CardName.Ten, Parent = CardName.Six });
            hand.Cards.Add(new CardValue { Suit = Suit.Heart, Name = CardName.Nine, Parent = CardName.Seven });
            hand.Cards.Add(new CardValue { Suit = Suit.Heart, Name = CardName.Eight, Parent = CardName.Eight });

            return hand;
        }

        public IList<Hand> GetHands(int size)
        {
            var hands = new List<Hand>(size);

            for(int i = 0; i < size; i++)
                hands.Add(GetHand());

            return hands;
        }

        public IList<Hand> GetGridHands(int size)
        {
            var hands = new List<Hand>(size);

            for (int i = 0; i < size; i++)
                hands.Add(GetGridHand());

            return hands;
        }

        #endregion //Helper Methods

        [Test]
        public void StartingHandConstructor()
        {
            var hand = new StartingHand();

            Assert.IsNotNull(hand.AllHands);
        }

        [Test]
        public void StartingHandGetSchema()
        {
            var hand = new StartingHand();

            Assert.IsNull(hand.GetSchema());
        }

        [Test]
        public void StartingHandGetAllHands()
        {
            var hand = new StartingHand();

            var hands = this.GetHands(5);

            hand.Hand = hands;

            var allHands = hand.AllHands;

            Assert.IsNotNull(allHands);
            Assert.AreEqual(7 * 5, allHands.Count);
            Assert.IsFalse(allHands[0].IsVisible);
        }

        [Test]
        public void StartingHandFindHandByColumn()
        {
            var hand = new StartingHand();

            var hands = this.GetGridHands(5);

            hand.Hand = hands;

            var result = hand.FindHandByColumn(CardName.Two, CardName.Queen);

            Assert.IsNotNull(result);
            Assert.AreEqual(CardName.Four, result.Parent);
            Assert.AreEqual(CardName.Queen, result.Name);
        }

        [Test]
        public void StartingHandFindHandByColumnRowExistColumnDoesNot()
        {
            var hand = new StartingHand();

            var hands = this.GetGridHands(5);

            hand.Hand = hands;

            var result = hand.FindHandByColumn(CardName.Two, CardName.Seven);

            Assert.IsNull(result);
        }

        [Test]
        public void StartingHandFindHandByColumnRowDoesNotExist()
        {
            var hand = new StartingHand();

            var hands = this.GetGridHands(5);

            hand.Hand = hands;

            var result = hand.FindHandByColumn(CardName.Seven, CardName.Seven);

            Assert.IsNull(result);
        }

        [Test]
        public void StartingHandFindHandByRow()
        {
            var hand = new StartingHand();

            var hands = this.GetGridHands(5);

            hand.Hand = hands;

            var result = hand.FindHandByRow(CardName.Two);

            Assert.IsNotNull(result);
        }

        [Test]
        public void StartingHandFindHandByRowDoesNotExist()
        {
            var hand = new StartingHand();

            var hands = this.GetGridHands(5);

            hand.Hand = hands;

            var result = hand.FindHandByRow(CardName.Seven);

            Assert.IsNull(result);
        }

        [Test]
        public void StartingHandHasHandTrue()
        {
            var hand = new StartingHand();

            var hands = this.GetGridHands(5);

            hand.Hand = hands;

            var result = hand.HasHand(CardName.Two, CardName.Queen);

            Assert.IsTrue(result);
        }

        [Test]
        public void StartingHandHasHandHasRowFalse()
        {
            var hand = new StartingHand();

            var hands = this.GetGridHands(5);

            hand.Hand = hands;

            var result = hand.HasHand(CardName.Two, CardName.Seven);

            Assert.IsFalse(result);
        }

        [Test]
        public void StartingHandHasHandHasColFalse()
        {
            var hand = new StartingHand();

            var hands = this.GetGridHands(5);

            hand.Hand = hands;

            var result = hand.HasHand(CardName.Seven, CardName.Seven);

            Assert.IsFalse(result);
        }
    }
}
