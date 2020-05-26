
//using System.Collections.Generic;

//using Holdem.Core.Entities.Cards;
//using Holdem.Interfaces.Card;

//using NUnit.Framework;

//namespace Test.Holdem.Entities.Collections
//{
//    [TestFixture]
//    public class TestHoldemCardCollection
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

//        //[Test]
//        //public void HoldemCardCollectionICollectionCountLessSeven()
//        //{
//        //    var hand = History();
//        //    var icollection = new Collection<ICardValue>(hand);
//        //    icollection.RemoveAt(0);

//        //    Assert.DoesNotThrow(() => new HoldemCardCollection(icollection));
//        //}

//        //[Test]
//        //public void HoldemCardCollectionICollectionCountGreaterSeven()
//        //{
//        //    var hand = History();
//        //    var icollection = new Collection<ICardValue>(hand);
//        //    icollection.Add(new CardValue());

//        //    Assert.Throws<ArgumentException>(() => new HoldemCardCollection(icollection));
//        //}

//        //[Test]
//        //public void HoldemCardCollectionGetHandHistory()
//        //{
//        //    var hand = History();

//        //    var collection = new HoldemCardCollection(hand);

//        //    var History = collection.History;

//        //    Assert.IsNotNull(History);
//        //}
//    }
//}