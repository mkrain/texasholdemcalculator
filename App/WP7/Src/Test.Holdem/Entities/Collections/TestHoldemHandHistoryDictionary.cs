//using Holdem.Core.Entities.Collections;

//using NUnit.Framework;

//namespace Test.Holdem.Entities.Collections
//{
//    [TestFixture]
//    public class TestHoldemHandHistoryDictionary
//    {
//        [Test]
//        public void HandHistoryDictionaryGetWonHandTrue()
//        {
//            var dictionary =
//                new HandHistoryDictionary
//                {
//                    WonPotAmount = "1000"
//                };

//            Assert.IsTrue(dictionary.WonHand);
//        }

//        [Test]
//        public void HandHistoryDictionaryGetWonHandFalse()
//        {
//            var dictionary = new HandHistoryDictionary();

//            Assert.IsFalse(dictionary.WonHand);
//        }

//        [Test]
//        public void HandHistoryDictionaryToStringEmpty()
//        {
//            var dictionary = new HandHistoryDictionary();

//            const string expected = "Hand history for game: , tournament: , description , date and time: ";

//            Assert.AreEqual(expected, dictionary.ToString());
//        }

//        [Test]
//        public void HandHistoryDictionaryToString()
//        {
//            var dictionary =
//                new HandHistoryDictionary
//                {
//                    GameId = "12345",
//                    TournamentId = "2222",
//                    GameDescription = "Description",
//                    DateAndTime = "yyyy-mm-dd"
//                };

//            string expected =
//                string.Format(
//                    "Hand history for game: {0}, tournament: {1}, description {2}, date and time: {3}",
//                    dictionary.GameId, dictionary.TournamentId, dictionary.GameDescription, dictionary.DateAndTime);

//            Assert.AreEqual(expected, dictionary.ToString());
//        }
//    }
//}
