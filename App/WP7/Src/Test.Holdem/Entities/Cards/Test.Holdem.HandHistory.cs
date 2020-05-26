
using System;
using NUnit.Framework;
using TexasHoldemCalculator.Interfaces.HandHistory;

namespace Test.Holdem.Entities.Cards
{
    [TestFixture]
    public class TestHoldemHandHistory
    {
        #region HelperMethods

        //private IList<ICardValue> HandHistory()
        //{
        //    return new List<ICardValue>
        //    {
        //        new CardValue
        //        {
        //            HoldemCard = HoldemCard.Hole1
        //        },
        //        new CardValue
        //        {
        //            HoldemCard = HoldemCard.Hole2
        //        },
        //        new CardValue
        //        {
        //            HoldemCard = HoldemCard.Flop1
        //        },
        //        new CardValue
        //        {
        //            HoldemCard = HoldemCard.Flop2
        //        },
        //        new CardValue
        //        {
        //            HoldemCard = HoldemCard.Flop3
        //        },
        //        new CardValue
        //        {
        //            HoldemCard = HoldemCard.Turn
        //        },
        //        new CardValue
        //        {
        //            HoldemCard = HoldemCard.River
        //        },
        //    };
        //}

        #endregion HelperMethods

        [Test]
        public void HandHistoryConstructor()
        {
            Assert.DoesNotThrow(() => new History());
        }

        [Test]
        public void HandHistoryDateTime()
        {
            var expected = DateTime.Now.ToString("HH-mm-dd-MM-yyyy");

            var handHistory = new History();

            var result = handHistory.Date.ToString("HH-mm-dd-MM-yyyy");

            Assert.AreEqual(expected, result);
        }

        //[Test]
        //public void HandHistoryIListICardValue()
        //{
        //    var list = HandHistory();

        //    Assert.DoesNotThrow(() => new HandHistory(list));
        //}

        //[Test]
        //public void HandHistoryIEnumerableICardValue()
        //{
        //    IEnumerable<ICardValue> list = HandHistory();

        //    Assert.DoesNotThrow(() => new HandHistory(list));
        //}

        //[Test]
        //public void HandHistoryHoldemCardCollection()
        //{
        //    var list = new HoldemCardCollection(HandHistory());

        //    Assert.DoesNotThrow(() => new HandHistory(list));
        //}

        //[Test]
        //public void HandHistoryGetHandHistoryCollection()
        //{
        //    var list = new HoldemCardCollection(HandHistory());

        //    var handHistory = new HandHistory(list);

        //    var newList = HandHistory();

        //    newList[0].IsSuited = true;

        //    handHistory.HandHistoryCollection = newList;

        //    Assert.IsTrue(handHistory[HoldemCard.Hole1].IsSuited);
        //}

        //[Test]
        //public void HandHistoryDerivedCollection()
        //{
        //    var list = new HoldemCardCollection(HandHistory());

        //    var handHistory = new HandHistory(list);

        //    var derived = handHistory.DerivedCollection;

        //    Assert.IsNotNull(derived);
        //    Assert.AreEqual(7, derived.Count);
        //}

        //[Test]
        //public void HandHistoryIndexerSet()
        //{
        //    var list = new HoldemCardCollection(HandHistory());

        //    var handHistory = new HandHistory(list);

        //    ICardValue newCard =
        //        new CardValue(Suit.Diamond, CardName.Six)
        //        {
        //            HoldemCard = HoldemCard.Flop1
        //        };

        //    handHistory[HoldemCard.Flop1] = newCard;

        //    Assert.AreEqual(newCard, handHistory[HoldemCard.Flop1]);
        //}

        [Test]
        public void HandHistoryGetSchema()
        {
            var handHistory = new History();

            Assert.IsNull(handHistory.GetSchema());
        }
    }
}
