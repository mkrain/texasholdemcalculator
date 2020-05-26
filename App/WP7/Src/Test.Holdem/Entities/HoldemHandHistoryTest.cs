
using System.Linq;
using NUnit.Framework;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.HandHistory;

namespace Test.Holdem.Entities
{
    [TestFixture]
    public class HoldemHandHistoryTest
    {
        #region Instance Tests

        [Test]
        public void HandHistory_GetStraight_RoyalFlush_True()
        {
             var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.King),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Queen),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Jack),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Eight)
                };

            var result = handHistoryInARow.GetStraight().ToList();

            Assert.AreEqual(CardName.Ace, result[0].Name);
            Assert.AreEqual(CardName.King, result[1].Name);
            Assert.AreEqual(CardName.Queen, result[2].Name);
            Assert.AreEqual(CardName.Jack, result[3].Name);
            Assert.AreEqual(CardName.Ten, result[4].Name);
        }

        [Test]
        public void HandHistory_GetStraight_True()
        {
             var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Eight),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Nine),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Nine),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var result = handHistoryInARow.GetStraight().ToList();

            Assert.AreEqual(CardName.Six, result[4].Name);
            Assert.AreEqual(CardName.Seven, result[3].Name);
            Assert.AreEqual(CardName.Eight, result[2].Name);
            Assert.AreEqual(CardName.Nine, result[1].Name);
            Assert.AreEqual(CardName.Ten, result[0].Name);
        }

        [Test]
        public void HandHistory_GetStraight_Low_Straight_True()
        {
             var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Four),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Five),
                    TurnCard = new CardValue(Suit.Heart, CardName.Nine),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var result = handHistoryInARow.GetStraight().ToList();

            Assert.AreEqual(CardName.Ace, result[0].Name);
            Assert.AreEqual(CardName.Two, result[1].Name);
            Assert.AreEqual(CardName.Three, result[2].Name);
            Assert.AreEqual(CardName.Four, result[3].Name);
            Assert.AreEqual(CardName.Five, result[4].Name);
        }

        [Test]
        public void HandHistory_GetStraight_False()
        {
             var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Five),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Eight),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Nine),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Nine),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var result = handHistoryInARow.GetStraight().ToList();

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void HandHistory_GetFlush_IsFlush()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven, HoldemCard.Hole1),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Ace, HoldemCard.Hole2),
                    FlopCardOne = new CardValue(Suit.Club, CardName.King, HoldemCard.Flop1),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight, HoldemCard.Flop2),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Two, HoldemCard.Flop3),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King, HoldemCard.Turn),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five, HoldemCard.River)
                };

            var result = handHistoryInARow.GetFlush().ToList();

            Assert.AreEqual(result[4], handHistoryInARow.FlopCardThree);
            Assert.AreEqual(result[3], handHistoryInARow.HoleCardOne);
            Assert.AreEqual(result[2], handHistoryInARow.FlopCardTwo);
            Assert.AreEqual(result[1], handHistoryInARow.FlopCardOne);
            Assert.AreEqual(result[0], handHistoryInARow.HoleCardTwo);
        }

        [Test]
        public void HandHistory_GetFlush_IsNotFlush()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Ace),
                    FlopCardOne = new CardValue(Suit.Heart, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Two),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var result = handHistoryInARow.GetFlush().ToList();

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void HandHistory_GetFlush_NoFlush()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Heart, CardName.Ace),
                    FlopCardOne = new CardValue(Suit.Club, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Two),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var result = handHistoryInARow.GetFlush().ToList();

            Assert.IsTrue(result.Count == 0);
        }

        [Test]
        public void HandHistory_IsPair_True()
        {
             var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Seven),
                    RiverCard = new CardValue(Suit.Heart, CardName.Five)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Three),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Jack),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            var handHistoryMultiple =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Two),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Eight),
                    TurnCard = new CardValue(Suit.Spade, CardName.Eight),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            Assert.IsTrue(handHistoryInARow.IsPair());
            Assert.IsTrue(handHistorySpread.IsPair());
            Assert.IsTrue(handHistoryMultiple.IsPair());
        }

        [Test]
        public void HandHistory_IsPair_False()
        {
             var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Jack),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Seven),
                    RiverCard = new CardValue(Suit.Heart, CardName.Five)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Ace),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Three),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Jack),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            Assert.IsFalse(handHistoryInARow.IsPair());
            Assert.IsFalse(handHistorySpread.IsPair());
        }

        [Test]
        public void HandHistory_IsTwoPair_True()
        {
             var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Eight),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Three),
                    RiverCard = new CardValue(Suit.Heart, CardName.Five)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Three),
                    TurnCard = new CardValue(Suit.Spade, CardName.Eight),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            var handHistoryMultiple =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Two),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Eight),
                    TurnCard = new CardValue(Suit.Spade, CardName.Eight),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            Assert.IsTrue(handHistoryInARow.IsTwoPair());
            Assert.IsTrue(handHistorySpread.IsTwoPair());
            Assert.IsTrue(handHistoryMultiple.IsTwoPair());
        }

        [Test]
        public void HandHistory_IsTwoPair_False()
        {
             var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Eight),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Seven),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Three),
                    RiverCard = new CardValue(Suit.Heart, CardName.Five)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Three),
                    TurnCard = new CardValue(Suit.Spade, CardName.Seven),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            Assert.IsFalse(handHistoryInARow.IsTwoPair());
            Assert.IsFalse(handHistorySpread.IsTwoPair());
        }

        [Test]
        public void HandHistory_IsThreeOfAKind_True()
        {
             var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six),
                    FlopCardOne = new CardValue(Suit.Heart, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Nine),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Nine),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Six),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            var handHistoryMultiple =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Two),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Six),
                    TurnCard = new CardValue(Suit.Spade, CardName.Two),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            Assert.IsTrue(handHistoryInARow.IsThreeOfAKind());
            Assert.IsTrue(handHistorySpread.IsThreeOfAKind());
            Assert.IsTrue(handHistoryMultiple.IsThreeOfAKind());
        }

        [Test]
        public void HandHistory_IsThreeOfAKind_False()
        {
             var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six),
                    FlopCardOne = new CardValue(Suit.Heart, CardName.Two),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Nine),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Nine),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Two),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            Assert.IsFalse(handHistoryInARow.IsThreeOfAKind());
            Assert.IsFalse(handHistorySpread.IsThreeOfAKind());
        }

        [Test]
        public void HandHistory_IsStraight_True()
        {
             var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Eight),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Nine),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Nine),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Heart, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Five),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            Assert.IsTrue(handHistoryInARow.IsStraight());
            Assert.IsTrue(handHistorySpread.IsStraight());
        }

        [Test]
        public void HandHistory_IsStraight_False()
        {
             var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Heart, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Nine),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Four),
                    TurnCard = new CardValue(Suit.Heart, CardName.Nine),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Heart, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Four),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Three)
                };

            var handHistorySequential =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Heart, CardName.Two),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Queen),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.King),
                    TurnCard = new CardValue(Suit.Spade, CardName.Jack),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            Assert.IsFalse(handHistoryInARow.IsStraight());
            Assert.IsFalse(handHistorySpread.IsStraight());
            Assert.IsFalse(handHistorySequential.IsStraight());
        }

        [Test]
        public void HandHistory_IsStraight_Low_True()
        {
             var handHistory =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Heart, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Four),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Five),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Eight)
                };

            Assert.IsTrue(handHistory.IsStraight());
        }

        [Test]
        public void HandHistory_IsStraight_RoyalFlush_True()
        {
             var handHistory =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.King),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Queen),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Jack),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Eight)
                };

            Assert.IsTrue(handHistory.IsStraight());
        }

        [Test]
        public void HandHistory_IsFlush_RoyalFlush_True()
        {
            var handHistory =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.King),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Queen),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Jack),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Eight)
                };

            Assert.IsTrue(handHistory.IsFlush());
        }

        [Test]
        public void HandHistory_Is_Flush_True()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Ace),
                    FlopCardOne = new CardValue(Suit.Club, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Two),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Ace),
                    TurnCard = new CardValue(Suit.Club, CardName.King),
                    RiverCard = new CardValue(Suit.Club, CardName.Five)
                };

            Assert.IsTrue(handHistoryInARow.IsFlush());
            Assert.IsTrue(handHistorySpread.IsFlush());
        }

        [Test]
        public void HandHistory_Is_Flush_False()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Diamond, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Heart, CardName.Ace),
                    FlopCardOne = new CardValue(Suit.Club, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Two),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Ace),
                    TurnCard = new CardValue(Suit.Club, CardName.King),
                    RiverCard = new CardValue(Suit.Heart, CardName.Five)
                };

            Assert.False(handHistoryInARow.IsFlush());
            Assert.False(handHistorySpread.IsFlush());
        }

        [Test]
        public void HandHistory_IsFullHouse_True()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Diamond, CardName.Eight),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Seven),
                    TurnCard = new CardValue(Suit.Club, CardName.King),
                    RiverCard = new CardValue(Suit.Club, CardName.Eight)
                };

            Assert.IsTrue(handHistoryInARow.IsFullHouse());
            Assert.IsTrue(handHistorySpread.IsFullHouse());
        }

        [Test]
        public void HandHistory_IsFullHouse_False()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Five),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Five),
                    FlopCardThree = new CardValue(Suit.Diamond, CardName.Eight),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Five),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Seven),
                    TurnCard = new CardValue(Suit.Club, CardName.King),
                    RiverCard = new CardValue(Suit.Club, CardName.Eight)
                };

            Assert.False(handHistoryInARow.IsFullHouse());
            Assert.False(handHistorySpread.IsFullHouse());
        }

        [Test]
        public void HandHistory_Four_Of_A_Kind_True()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Three),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Three),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Heart, CardName.Three),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Ace),
                    TurnCard = new CardValue(Suit.Spade, CardName.Seven),
                    RiverCard = new CardValue(Suit.Spade, CardName.Six)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Three),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Ace),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Heart, CardName.Four),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Three),
                    TurnCard = new CardValue(Suit.Spade, CardName.Seven),
                    RiverCard = new CardValue(Suit.Spade, CardName.Three)
                };

            Assert.IsTrue(handHistoryInARow.IsFourOfAKind());
            Assert.IsTrue(handHistorySpread.IsFourOfAKind());
        }

        [Test]
        public void HandHistory_Four_Of_A_Kind_False()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Three),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Three),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Heart, CardName.Nine),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Ace),
                    TurnCard = new CardValue(Suit.Spade, CardName.Seven),
                    RiverCard = new CardValue(Suit.Spade, CardName.Six)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Three),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Ace),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Heart, CardName.Four),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Three),
                    TurnCard = new CardValue(Suit.Spade, CardName.Seven),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            Assert.IsFalse(handHistoryInARow.IsFourOfAKind());
            Assert.IsFalse(handHistorySpread.IsFourOfAKind());
        }

        [Test]
        public void HandHistory_StraightFlush_True()
        {
            var handHistory =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Three),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.Four),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Ace),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Five),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Ace),
                    TurnCard = new CardValue(Suit.Spade, CardName.Seven),
                    RiverCard = new CardValue(Suit.Spade, CardName.Six)
                };

            Assert.IsTrue(handHistory.IsStraightFlush());
        }

        [Test]
        public void HandHistory_StraightFlush_Straight_True()
        {
            var handHistory =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Three),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.Four),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Five),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Six),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Seven),
                    TurnCard = new CardValue(Suit.Spade, CardName.Eight),
                    RiverCard = new CardValue(Suit.Spade, CardName.Nine)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Two),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.Three),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Four),
                    FlopCardTwo = new CardValue(Suit.Heart, CardName.Six),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Seven),
                    TurnCard = new CardValue(Suit.Spade, CardName.Five),
                    RiverCard = new CardValue(Suit.Spade, CardName.Six)
                };

            Assert.IsTrue(handHistory.IsStraightFlush());
            Assert.IsTrue(handHistorySpread.IsStraightFlush());
        }

        [Test]
        public void HandHistory_StraightFlush_False()
        {
            var handHistory =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Ten),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Eight),
                    FlopCardTwo = new CardValue(Suit.Heart, CardName.Nine),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Queen),
                    TurnCard = new CardValue(Suit.Club, CardName.Seven),
                    RiverCard = new CardValue(Suit.Spade, CardName.Jack)
                };

            Assert.IsFalse(handHistory.IsStraightFlush());
        }

        [Test]
        public void HandHistory_StraightFlush_Flush_No_Straight()
        {
            var handHistory =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Three),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.King),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Ace),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Five),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Ace),
                    TurnCard = new CardValue(Suit.Spade, CardName.Seven),
                    RiverCard = new CardValue(Suit.Spade, CardName.Six)
                };

            Assert.IsFalse(handHistory.IsStraightFlush());
        }

        [Test]
        public void HandHistory_IsRoyalFlush_Flush_Not_Royal()
        {
            var handHistory =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Ace),
                    FlopCardOne = new CardValue(Suit.Club, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Ace),
                    TurnCard = new CardValue(Suit.Club, CardName.King),
                    RiverCard = new CardValue(Suit.Club, CardName.Five)
                };

            Assert.IsFalse(handHistory.IsRoyalFlush());
        }

        [Test]
        public void HandHistory_IsRoyalFlush_True()
        {
            var handHistory =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.King),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Queen),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Jack),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Eight)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Heart, CardName.King),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Diamond, CardName.Jack),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Queen),
                    TurnCard = new CardValue(Suit.Spade, CardName.Ten),
                    RiverCard = new CardValue(Suit.Spade, CardName.Jack)
                };

            Assert.IsTrue(handHistory.IsRoyalFlush());
            Assert.IsTrue(handHistorySpread.IsRoyalFlush());
        }

        [Test]
        public void HandHistory_IsRoyalFlush_False()
        {
            var handHistory =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Heart, CardName.King),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Queen),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.King),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Jack),
                    TurnCard = new CardValue(Suit.Spade, CardName.Ten),
                    RiverCard = new CardValue(Suit.Spade, CardName.Nine)
                };

            Assert.IsFalse(handHistory.IsRoyalFlush());
        }

        #endregion Instance Tests

        #region Static Method Tests

        [Test]
        public void HandHistory_Static_GetHighCard()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.King),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Queen),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Jack),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Two),
                    RiverCard = new CardValue(Suit.Heart, CardName.Three)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Two),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Three),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Jack),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            var handHistoryMultiple =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Eight),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Five),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Eight),
                    TurnCard = new CardValue(Suit.Spade, CardName.Seven),
                    RiverCard = new CardValue(Suit.Spade, CardName.Nine)
                };

            var list = History.GetHighCard(handHistoryInARow.Cards).ToList();

            Assert.AreEqual(list[0].Name, CardName.Ace);
            Assert.AreEqual(list[0].Suit, Suit.Spade);

            list = History.GetHighCard(handHistorySpread.Cards).ToList();

            Assert.AreEqual(list[0].Name, CardName.Jack);
            Assert.AreEqual(list[0].Suit, Suit.Heart);

            list = History.GetHighCard(handHistoryMultiple.Cards).ToList();

            Assert.AreEqual(list[0].Name, CardName.Nine);
            Assert.AreEqual(list[0].Suit, Suit.Spade);
        }

        [Test]
        public void HandHistory_Static_GetPair_True()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six, HoldemCard.Hole1),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six, HoldemCard.Hole2),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Three, HoldemCard.Flop1),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight, HoldemCard.Flop2),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten, HoldemCard.Flop3),
                    TurnCard = new CardValue(Suit.Heart, CardName.Seven, HoldemCard.Turn),
                    RiverCard = new CardValue(Suit.Heart, CardName.Five, HoldemCard.River)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six, HoldemCard.Hole1),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two, HoldemCard.Hole2),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six, HoldemCard.Flop1),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Three, HoldemCard.Flop2),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Jack, HoldemCard.Flop3),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine, HoldemCard.Turn),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten, HoldemCard.River)
                };

            var handHistoryMultiple =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six, HoldemCard.Hole1),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two, HoldemCard.Hole2),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six, HoldemCard.Flop1),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Two, HoldemCard.Flop2),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Eight, HoldemCard.Flop3),
                    TurnCard = new CardValue(Suit.Spade, CardName.Eight, HoldemCard.Turn),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten, HoldemCard.River)
                };

            var pair = History.GetPair(handHistoryInARow.Cards).ToList();

            Assert.IsNotNull(pair);

            pair = pair.Take(2).ToList();

            Assert.IsTrue(pair.Contains(handHistoryInARow.HoleCardOne));
            Assert.IsTrue(pair.Contains(handHistoryInARow.HoleCardTwo));

            pair = History.GetPair(handHistorySpread.Cards).ToList();

            Assert.IsNotNull(pair);

            pair = pair.Take(2).ToList();

            Assert.IsTrue(pair.Contains(handHistorySpread.HoleCardOne));
            Assert.IsTrue(pair.Contains(handHistorySpread.FlopCardOne));

            pair = History.GetPair(handHistoryMultiple.Cards).ToList();

            Assert.IsNotNull(pair);

            pair = pair.Take(2).ToList();

            Assert.IsTrue(pair.Contains(handHistoryMultiple.FlopCardThree));
            Assert.IsTrue(pair.Contains(handHistoryMultiple.TurnCard));
        }

        [Test]
        public void HandHistory_Static_GetTwoPair_True()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six, HoldemCard.Hole1),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six, HoldemCard.Hole2),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Eight, HoldemCard.Flop1),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight, HoldemCard.Flop2),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten, HoldemCard.Flop3),
                    TurnCard = new CardValue(Suit.Heart, CardName.Three, HoldemCard.Turn),
                    RiverCard = new CardValue(Suit.Heart, CardName.Five, HoldemCard.River)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six, HoldemCard.Hole1),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two, HoldemCard.Hole2),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six, HoldemCard.Flop1),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight, HoldemCard.Flop2),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Three, HoldemCard.Flop3),
                    TurnCard = new CardValue(Suit.Spade, CardName.Eight, HoldemCard.Turn),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten, HoldemCard.River)
                };

            var handHistoryMultiple =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six, HoldemCard.Hole1),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two, HoldemCard.Hole2),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six, HoldemCard.Flop1),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Two, HoldemCard.Flop2),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Eight, HoldemCard.Flop3),
                    TurnCard = new CardValue(Suit.Spade, CardName.Eight, HoldemCard.Turn),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten, HoldemCard.River)
                };

            var pair = History.GetTwoPair(handHistoryInARow.Cards).ToList();

            Assert.IsNotNull(pair);

            pair = pair.Take(4).ToList();

            Assert.IsTrue(pair.Contains(handHistoryInARow.HoleCardOne));
            Assert.IsTrue(pair.Contains(handHistoryInARow.HoleCardTwo));
            Assert.IsTrue(pair.Contains(handHistoryInARow.FlopCardOne));
            Assert.IsTrue(pair.Contains(handHistoryInARow.FlopCardTwo));

            pair = History.GetTwoPair(handHistorySpread.Cards).ToList();

            Assert.IsNotNull(pair);

            pair = pair.Take(4).ToList();

            Assert.IsTrue(pair.Contains(handHistorySpread.HoleCardOne));
            Assert.IsTrue(pair.Contains(handHistorySpread.FlopCardOne));
            Assert.IsTrue(pair.Contains(handHistorySpread.FlopCardTwo));
            Assert.IsTrue(pair.Contains(handHistorySpread.TurnCard));

            pair = History.GetTwoPair(handHistoryMultiple.Cards).ToList();

            Assert.IsNotNull(pair);

            pair = pair.Take(4).ToList();

            Assert.IsTrue(pair.Contains(handHistoryMultiple.HoleCardOne));
            Assert.IsTrue(pair.Contains(handHistoryMultiple.FlopCardOne));
            Assert.IsTrue(pair.Contains(handHistoryMultiple.FlopCardThree));
            Assert.IsTrue(pair.Contains(handHistoryMultiple.TurnCard));
        }

        [Test]
        public void HandHistory_Static_GetThreeOfAKind_True()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six, HoldemCard.Hole1),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six, HoldemCard.Hole2),
                    FlopCardOne = new CardValue(Suit.Heart, CardName.Six, HoldemCard.Flop1),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Nine, HoldemCard.Flop2),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten, HoldemCard.Flop3),
                    TurnCard = new CardValue(Suit.Heart, CardName.Nine, HoldemCard.Turn),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight, HoldemCard.River)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six, HoldemCard.Hole1),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two, HoldemCard.Hole2),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six, HoldemCard.Flop1),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight, HoldemCard.Flop2),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Six, HoldemCard.Flop3),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine, HoldemCard.Turn),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten, HoldemCard.River)
                };

            var handHistoryMultiple =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six, HoldemCard.Hole1),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Eight, HoldemCard.Hole2),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six, HoldemCard.Flop1),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight, HoldemCard.Flop2),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Six, HoldemCard.Flop3),
                    TurnCard = new CardValue(Suit.Spade, CardName.Eight, HoldemCard.Turn),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten, HoldemCard.River)
                };

            var threeOfAKind = History.GetThreeOfAKind(handHistoryInARow.Cards).ToList();

            Assert.IsNotNull(threeOfAKind);

            threeOfAKind = threeOfAKind.Take(3).ToList();

            Assert.IsTrue(threeOfAKind.Contains(handHistoryInARow.HoleCardOne));
            Assert.IsTrue(threeOfAKind.Contains(handHistoryInARow.HoleCardTwo));
            Assert.IsTrue(threeOfAKind.Contains(handHistoryInARow.FlopCardOne));

            threeOfAKind = History.GetThreeOfAKind(handHistorySpread.Cards).ToList();

            Assert.IsNotNull(threeOfAKind);

            threeOfAKind = threeOfAKind.Take(3).ToList();

            Assert.IsTrue(threeOfAKind.Contains(handHistorySpread.HoleCardOne));
            Assert.IsTrue(threeOfAKind.Contains(handHistorySpread.FlopCardOne));
            Assert.IsTrue(threeOfAKind.Contains(handHistorySpread.FlopCardThree));

            threeOfAKind = History.GetThreeOfAKind(handHistoryMultiple.Cards).ToList();

            Assert.IsNotNull(threeOfAKind);

            threeOfAKind = threeOfAKind.Take(3).ToList();

            Assert.IsTrue(threeOfAKind.Contains(handHistoryMultiple.HoleCardTwo));
            Assert.IsTrue(threeOfAKind.Contains(handHistoryMultiple.FlopCardTwo));
            Assert.IsTrue(threeOfAKind.Contains(handHistoryMultiple.TurnCard));
        }

        [Test]
        public void HandHistory_Static_GetStraight_True()
        {
           var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Four),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Five),
                    TurnCard = new CardValue(Suit.Heart, CardName.Nine),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Eight),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Nine),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Nine),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var straight = History.GetStraight(handHistoryInARow.Cards).ToList();

            Assert.IsNotNull(straight);

            straight = straight.Take(5).ToList();

            Assert.IsTrue(straight[0].Name == CardName.Ace);
            Assert.IsTrue(straight[1].Name == CardName.Two);
            Assert.IsTrue(straight[2].Name == CardName.Three);
            Assert.IsTrue(straight[3].Name == CardName.Four);
            Assert.IsTrue(straight[4].Name == CardName.Five);

            straight = History.GetStraight(handHistorySpread.Cards).ToList();

            Assert.IsNotNull(straight);

            straight = straight.Take(5).ToList();

            Assert.IsTrue(straight[4].Name == CardName.Six);
            Assert.IsTrue(straight[3].Name == CardName.Seven);
            Assert.IsTrue(straight[2].Name == CardName.Eight);
            Assert.IsTrue(straight[1].Name == CardName.Nine);
            Assert.IsTrue(straight[0].Name == CardName.Ten);
        }

        [Test]
        public void HandHistory_Static_GetFlush_True()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Ace),
                    FlopCardOne = new CardValue(Suit.Club, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Two),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Ace),
                    TurnCard = new CardValue(Suit.Club, CardName.King),
                    RiverCard = new CardValue(Suit.Club, CardName.Five)
                };

            var list = History.GetFlush(handHistoryInARow.Cards).ToList();

            Assert.AreEqual(list[0].Name, CardName.Ace);
            Assert.AreEqual(list[1].Name, CardName.King);
            Assert.AreEqual(list[2].Name, CardName.Eight);
            Assert.AreEqual(list[3].Name, CardName.Seven);
            Assert.AreEqual(list[4].Name, CardName.Two);
            Assert.AreEqual(list[0].Suit, Suit.Club);
            Assert.AreEqual(list[1].Suit, Suit.Club);
            Assert.AreEqual(list[2].Suit, Suit.Club);
            Assert.AreEqual(list[3].Suit, Suit.Club);
            Assert.AreEqual(list[4].Suit, Suit.Club);

            list = History.GetFlush(handHistorySpread.Cards).ToList();

            Assert.AreEqual(list[0].Name, CardName.Ace);
            Assert.AreEqual(list[1].Name, CardName.King);
            Assert.AreEqual(list[2].Name, CardName.Seven);
            Assert.AreEqual(list[3].Name, CardName.Five);
            Assert.AreEqual(list[4].Name, CardName.Two);
            Assert.AreEqual(list[0].Suit, Suit.Club);
            Assert.AreEqual(list[1].Suit, Suit.Club);
            Assert.AreEqual(list[2].Suit, Suit.Club);
            Assert.AreEqual(list[3].Suit, Suit.Club);
            Assert.AreEqual(list[4].Suit, Suit.Club);
        }

        [Test]
        public void HandHistory_Static_GetFourOfAKind_True()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Heart, CardName.Seven),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Two),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Seven),
                    TurnCard = new CardValue(Suit.Club, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Seven)
                };

            var list = History.GetFourOfAKind(handHistoryInARow.Cards).ToList();

            Assert.AreEqual(list[0].Name, CardName.Seven);
            Assert.AreEqual(list[1].Name, CardName.Seven);
            Assert.AreEqual(list[2].Name, CardName.Seven);
            Assert.AreEqual(list[3].Name, CardName.Seven);
            Assert.AreEqual(list[0].Suit, Suit.Club);
            Assert.AreEqual(list[1].Suit, Suit.Diamond);
            Assert.AreEqual(list[2].Suit, Suit.Spade);
            Assert.AreEqual(list[3].Suit, Suit.Heart);

            list = History.GetFourOfAKind(handHistorySpread.Cards).ToList();

            Assert.AreEqual(list[0].Name, CardName.Seven);
            Assert.AreEqual(list[1].Name, CardName.Seven);
            Assert.AreEqual(list[2].Name, CardName.Seven);
            Assert.AreEqual(list[3].Name, CardName.Seven);
            Assert.AreEqual(list[0].Suit, Suit.Club);
            Assert.AreEqual(list[1].Suit, Suit.Diamond);
            Assert.AreEqual(list[2].Suit, Suit.Heart);
            Assert.AreEqual(list[3].Suit, Suit.Spade);
        }

        [Test]
        public void HandHistory_Static_GetFullHouse_True()
        {
            var handHistoryInARow =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Diamond, CardName.Eight),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Seven),
                    TurnCard = new CardValue(Suit.Club, CardName.King),
                    RiverCard = new CardValue(Suit.Club, CardName.Eight)
                };

            var fullHouse = History.GetFullHouse(handHistoryInARow.Cards).ToList();

            Assert.IsTrue(fullHouse[0].Name == CardName.Seven);
            Assert.IsTrue(fullHouse[1].Name == CardName.Seven);
            Assert.IsTrue(fullHouse[2].Name == CardName.Seven);
            Assert.IsTrue(fullHouse[3].Name == CardName.Eight);
            Assert.IsTrue(fullHouse[4].Name == CardName.Eight);

            fullHouse = History.GetFullHouse(handHistorySpread.Cards).ToList();

            Assert.IsTrue(fullHouse[0].Name == CardName.Seven);
            Assert.IsTrue(fullHouse[1].Name == CardName.Seven);
            Assert.IsTrue(fullHouse[2].Name == CardName.Seven);
            Assert.IsTrue(fullHouse[3].Name == CardName.Eight);
            Assert.IsTrue(fullHouse[4].Name == CardName.Eight);
        }

        [Test]
        public void HandHistory_Static_GetStraightFlush_True()
        {
               var handHistory =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Three),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.Four),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Five),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Six),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Seven),
                    TurnCard = new CardValue(Suit.Spade, CardName.Eight),
                    RiverCard = new CardValue(Suit.Spade, CardName.Nine)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Two),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.Three),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Four),
                    FlopCardTwo = new CardValue(Suit.Heart, CardName.Six),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Seven),
                    TurnCard = new CardValue(Suit.Spade, CardName.Five),
                    RiverCard = new CardValue(Suit.Spade, CardName.Six)
                };

            var straightFlush = History.GetStraightFlush(handHistory.Cards).ToList();

            Assert.AreEqual(CardName.Nine, straightFlush[0].Name);
            Assert.AreEqual(CardName.Eight, straightFlush[1].Name);
            Assert.AreEqual(CardName.Seven, straightFlush[2].Name);
            Assert.AreEqual(CardName.Six, straightFlush[3].Name);
            Assert.AreEqual(CardName.Five, straightFlush[4].Name);
            Assert.AreEqual(Suit.Spade, straightFlush[0].Suit);
            Assert.AreEqual(Suit.Spade, straightFlush[1].Suit);
            Assert.AreEqual(Suit.Spade, straightFlush[2].Suit);
            Assert.AreEqual(Suit.Spade, straightFlush[3].Suit);
            Assert.AreEqual(Suit.Spade, straightFlush[4].Suit);

            straightFlush = History.GetStraightFlush(handHistorySpread.Cards).ToList();

            Assert.AreEqual(CardName.Six, straightFlush[0].Name);
            Assert.AreEqual(CardName.Five, straightFlush[1].Name);
            Assert.AreEqual(CardName.Four, straightFlush[2].Name);
            Assert.AreEqual(CardName.Three, straightFlush[3].Name);
            Assert.AreEqual(CardName.Two, straightFlush[4].Name);
            Assert.AreEqual(Suit.Spade, straightFlush[0].Suit);
            Assert.AreEqual(Suit.Spade, straightFlush[1].Suit);
            Assert.AreEqual(Suit.Spade, straightFlush[2].Suit);
            Assert.AreEqual(Suit.Spade, straightFlush[3].Suit);
            Assert.AreEqual(Suit.Spade, straightFlush[4].Suit);
        }

        [Test]
        public void HandHistory_Static_GetRoyalFlush_True()
        {
            var handHistory =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.King),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Queen),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Jack),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Eight)
                };

            var handHistorySpread =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Heart, CardName.King),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Diamond, CardName.Jack),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Queen),
                    TurnCard = new CardValue(Suit.Spade, CardName.Ten),
                    RiverCard = new CardValue(Suit.Spade, CardName.Jack)
                };

            var royalFlush = History.GetRoyalFlush(handHistory.Cards).ToList();

            Assert.AreEqual(CardName.Ace, royalFlush[0].Name);
            Assert.AreEqual(CardName.King, royalFlush[1].Name);
            Assert.AreEqual(CardName.Queen, royalFlush[2].Name);
            Assert.AreEqual(CardName.Jack, royalFlush[3].Name);
            Assert.AreEqual(CardName.Ten, royalFlush[4].Name);
            Assert.AreEqual(Suit.Spade, royalFlush[0].Suit);
            Assert.AreEqual(Suit.Spade, royalFlush[1].Suit);
            Assert.AreEqual(Suit.Spade, royalFlush[2].Suit);
            Assert.AreEqual(Suit.Spade, royalFlush[3].Suit);
            Assert.AreEqual(Suit.Spade, royalFlush[4].Suit);

            royalFlush = History.GetRoyalFlush(handHistorySpread.Cards).ToList();

            Assert.AreEqual(CardName.Ace, royalFlush[0].Name);
            Assert.AreEqual(CardName.King, royalFlush[1].Name);
            Assert.AreEqual(CardName.Queen, royalFlush[2].Name);
            Assert.AreEqual(CardName.Jack, royalFlush[3].Name);
            Assert.AreEqual(CardName.Ten, royalFlush[4].Name);
            Assert.AreEqual(Suit.Spade, royalFlush[0].Suit);
            Assert.AreEqual(Suit.Spade, royalFlush[1].Suit);
            Assert.AreEqual(Suit.Spade, royalFlush[2].Suit);
            Assert.AreEqual(Suit.Spade, royalFlush[3].Suit);
            Assert.AreEqual(Suit.Spade, royalFlush[4].Suit);
        }

        [Test]
        public void HandHistory_Static_DetermineHandRank()
        {
            var highCard =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Three),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Jack),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            var pair =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Seven),
                    RiverCard = new CardValue(Suit.Heart, CardName.Five)
                };

            var twoPair =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Eight),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Three),
                    RiverCard = new CardValue(Suit.Heart, CardName.Five)
                };

            var threeOfAKind =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six),
                    FlopCardOne = new CardValue(Suit.Heart, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Nine),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Two),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var straight =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Four),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Five),
                    TurnCard = new CardValue(Suit.Heart, CardName.Nine),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var flush =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Ace),
                    FlopCardOne = new CardValue(Suit.Club, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Two),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var fullHouse =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Diamond, CardName.Eight),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var fourOfAKind =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Heart, CardName.Seven),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Two),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var straightFlush =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Three),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.Four),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Five),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Six),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Seven),
                    TurnCard = new CardValue(Suit.Spade, CardName.Eight),
                    RiverCard = new CardValue(Suit.Spade, CardName.Nine)
                };

            var royalFlush =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.King),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Queen),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Jack),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Eight)
                };

            Assert.AreEqual(HandRank.HighCard, History.GetHandRank(highCard.Cards));
            Assert.AreEqual(HandRank.Pair, History.GetHandRank(pair.Cards));
            Assert.AreEqual(HandRank.TwoPair, History.GetHandRank(twoPair.Cards));
            Assert.AreEqual(HandRank.ThreeOfAKind, History.GetHandRank(threeOfAKind.Cards));
            Assert.AreEqual(HandRank.Straight, History.GetHandRank(straight.Cards));
            Assert.AreEqual(HandRank.Flush, History.GetHandRank(flush.Cards));
            Assert.AreEqual(HandRank.FullHouse, History.GetHandRank(fullHouse.Cards));
            Assert.AreEqual(HandRank.FourOfAKind, History.GetHandRank(fourOfAKind.Cards));
            Assert.AreEqual(HandRank.StraightFlush, History.GetHandRank(straightFlush.Cards));
            Assert.AreEqual(HandRank.RoyalFlush, History.GetHandRank(royalFlush.Cards));
        }

        [Test]
        public void HandHistory_Static_GetBestHand()
        {
            var highCard =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Club, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Three),
                    FlopCardThree = new CardValue(Suit.Heart, CardName.Jack),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Ten)
                };

            var pair =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Seven),
                    RiverCard = new CardValue(Suit.Heart, CardName.Five)
                };

            var twoPair =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Eight),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Three),
                    RiverCard = new CardValue(Suit.Heart, CardName.Five)
                };

            var threeOfAKind =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Six),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Six),
                    FlopCardOne = new CardValue(Suit.Heart, CardName.Six),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Nine),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Heart, CardName.Two),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var straight =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Two),
                    FlopCardOne = new CardValue(Suit.Diamond, CardName.Three),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Four),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Five),
                    TurnCard = new CardValue(Suit.Heart, CardName.Nine),
                    RiverCard = new CardValue(Suit.Heart, CardName.Eight)
                };

            var flush =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Club, CardName.Ace),
                    FlopCardOne = new CardValue(Suit.Club, CardName.King),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Two),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var fullHouse =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Club, CardName.Eight),
                    FlopCardThree = new CardValue(Suit.Diamond, CardName.Eight),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var fourOfAKind =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Club, CardName.Seven),
                    HoleCardTwo = new CardValue(Suit.Diamond, CardName.Seven),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Seven),
                    FlopCardTwo = new CardValue(Suit.Heart, CardName.Seven),
                    FlopCardThree = new CardValue(Suit.Club, CardName.Two),
                    TurnCard = new CardValue(Suit.Diamond, CardName.King),
                    RiverCard = new CardValue(Suit.Spade, CardName.Five)
                };

            var straightFlush =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Three),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.Four),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Five),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Six),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Seven),
                    TurnCard = new CardValue(Suit.Spade, CardName.Eight),
                    RiverCard = new CardValue(Suit.Spade, CardName.Nine)
                };

            var royalFlush =
                new History
                {
                    HoleCardOne = new CardValue(Suit.Spade, CardName.Ace),
                    HoleCardTwo = new CardValue(Suit.Spade, CardName.King),
                    FlopCardOne = new CardValue(Suit.Spade, CardName.Queen),
                    FlopCardTwo = new CardValue(Suit.Spade, CardName.Jack),
                    FlopCardThree = new CardValue(Suit.Spade, CardName.Ten),
                    TurnCard = new CardValue(Suit.Spade, CardName.Nine),
                    RiverCard = new CardValue(Suit.Spade, CardName.Eight)
                };

            var bestHand = History.GetBestHand(highCard.Cards).ToList();
            Assert.AreEqual(History.GetHighCard(highCard.Cards).ToList(), bestHand);

            bestHand = History.GetBestHand(pair.Cards).ToList();
            Assert.AreEqual(History.GetPair(pair.Cards).ToList(), bestHand);

            bestHand = History.GetBestHand(twoPair.Cards).ToList();
            Assert.AreEqual(History.GetTwoPair(twoPair.Cards).ToList(), bestHand);

            bestHand = History.GetBestHand(threeOfAKind.Cards).ToList();
            Assert.AreEqual(History.GetThreeOfAKind(threeOfAKind.Cards).ToList(), bestHand);

            bestHand = History.GetBestHand(straight.Cards).ToList();
            Assert.AreEqual(History.GetStraight(straight.Cards).ToList(), bestHand);

            bestHand = History.GetBestHand(flush.Cards).ToList();
            Assert.AreEqual(History.GetFlush(flush.Cards).ToList(), bestHand);

            bestHand = History.GetBestHand(fullHouse.Cards).ToList();
            Assert.AreEqual(History.GetFullHouse(fullHouse.Cards).ToList(), bestHand);

            bestHand = History.GetBestHand(fourOfAKind.Cards).ToList();
            Assert.AreEqual(History.GetFourOfAKind(fourOfAKind.Cards).ToList(), bestHand);

            bestHand = History.GetBestHand(straightFlush.Cards).ToList();
            Assert.AreEqual(History.GetStraightFlush(straightFlush.Cards).ToList(), bestHand);

            bestHand = History.GetBestHand(royalFlush.Cards).ToList();
            Assert.AreEqual(History.GetRoyalFlush(royalFlush.Cards).ToList(), bestHand);
        }

        #endregion
    }
}