
//using System.Collections.Generic;

//using Holdem.Core.Entities.Cards;
//using Holdem.Core.Entities.Collections;
//using Holdem.Interfaces.Card;
//using Holdem.Interfaces.History;

//using NUnit.Framework;

//namespace Test.Holdem.Entities.Collections
//{
//    [TestFixture]
//    public class TestHoldemHandHistoryWriterCollection
//    {
//        #region HelperMethods

//        private static IList<ICardValue> History()
//        {
//            return new List<ICardValue>
//            {
//                new CardValue
//                {
//                    HoldemCard = HoldemCard.Hole1
//                },
//                new CardValue
//                {
//                    HoldemCard = HoldemCard.Hole2
//                },
//                new CardValue
//                {
//                    HoldemCard = HoldemCard.Flop1
//                },
//                new CardValue
//                {
//                    HoldemCard = HoldemCard.Flop2
//                },
//                new CardValue
//                {
//                    HoldemCard = HoldemCard.Flop3
//                },
//                new CardValue
//                {
//                    HoldemCard = HoldemCard.Turn
//                },
//                new CardValue
//                {
//                    HoldemCard = HoldemCard.River
//                },
//            };
//        }

//        #endregion //HelperMethods

//        [Test]
//        public void HandHistoryWriterCollectionGetSchema()
//        {
//            var History = new HandHistoryWriterCollection();

//            Assert.IsNull(History.GetSchema());
//        }

//        [Test]
//        public void HandHistoryWriterCollectionAddRange()
//        {
//            var historyList =
//                new List<IHandHistory>
//                {
//                    new History(History()),
//                    new History(History()),
//                    new History(History()),
//                    new History(History())
//                };

//            var History = new HandHistoryWriterCollection();

//            History.AddRange(historyList);

//            Assert.AreEqual(4, History.Count);
//        }

//        [Test]
//        public void HandHistoryWriterHoldemCardCollection()
//        {
//            var holdemCardCollection =
//                new HoldemCardCollection
//                {
//                    new CardValue(),
//                    new CardValue(),
//                    new CardValue(),
//                    new CardValue()
//                };

//            var History = new HandHistoryWriterCollection();

//            History.AddRange(holdemCardCollection);

//            Assert.AreEqual(1, History.Count);
//        }
//    }
//}
