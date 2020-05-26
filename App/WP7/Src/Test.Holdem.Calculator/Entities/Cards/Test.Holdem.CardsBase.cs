using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;
using NUnit.Framework;
using TexasHoldemCalculator.Interfaces.Card;

namespace Test.Holdem.Entities.Cards
{
    [TestFixture]
    public class HoldemTestCards
    {
        [Test]
        public void CardBaseConstructor()
        {
            var cardbase = new CardBase();

            Assert.AreEqual(HoldemCard.Hole1, cardbase.HoldemCard);
        }

        [Test]
        public void CardBaseConstructorSuitAndName()
        {
            var cardbase = new CardBase(Suit.Spade, CardName.Ace);

            Assert.AreEqual(Suit.Spade, cardbase.Suit);
            Assert.AreEqual(CardName.Ace, cardbase.Name);
        }

        [Test]
        public void CardBaseConstructorSuitNameAndHoldemCard()
        {
            var cardbase = new CardBase(Suit.Spade, CardName.Ace, HoldemCard.River);

            Assert.AreEqual(HoldemCard.River, cardbase.HoldemCard);
            Assert.AreEqual(Suit.Spade, cardbase.Suit);
            Assert.AreEqual(CardName.Ace, cardbase.Name);
        }

        [Test]
        public void CardBaseSetRow()
        {
            var cardbase =
                new CardBase(Suit.Spade, CardName.Ace)
                {
                    Parent = CardName.King
                };

            const int expected = 12 - (int)CardName.Queen;

            cardbase.Row = (int)CardName.Queen;

            Assert.AreEqual(expected, cardbase.Row);
        }

        [Test]
        public void CardBaseGetRow()
        {
            var cardbase =
                new CardBase(Suit.Spade, CardName.Ace)
                {
                    Parent = CardName.Queen
                };

            var expected = 12 - (int)cardbase.Parent;

            Assert.AreEqual(expected, cardbase.Row);
        }

        [Test]
        public void CardBaseSetCol()
        {
            var cardbase =
                new CardBase(Suit.Spade, CardName.Ace)
                {
                    Name = CardName.King
                };

            var expected = 12 - (int)CardName.Queen;

            cardbase.Column = (int)CardName.Queen;

            Assert.AreEqual(expected, cardbase.Column);
        }

        [Test]
        public void CardBaseGetCol()
        {
            var cardbase =
                new CardBase(Suit.Spade, CardName.Ace)
                {
                    Name = CardName.Queen
                };

            var expected = 12 - (int)cardbase.Name;

            Assert.AreEqual(expected, cardbase.Column);
        }

        [Test]
        public void CardBaseToString()
        {
            var cardbase =
                new CardBase(Suit.Spade, CardName.Ace)
                {
                    Name = CardName.Ace,
                    Suit = Suit.Heart
                };

            var expected = "Ah, GameId: 0";

            Assert.AreEqual(expected, cardbase.ToString());
        }

        [Test]
        public void CardBaseGetSchema()
        {
            var cardbase = new CardBase();

            Assert.AreEqual(null, cardbase.GetSchema());
        }

        [Test]
        public void CardBaseEqualsOverrideEqual()
        {
            var cardbase1 = new CardBase();
            var cardbase2 = new CardBase();

            var areEqual = cardbase1 == cardbase2;

            Assert.IsTrue(areEqual);
        }

        [Test]
        public void CardBaseEqualsOverrideLeftNull()
        {
            CardBase cardbase1 = null;
            var cardbase2 = new CardBase();

            var areEqual = cardbase1 == cardbase2;

            Assert.IsFalse(areEqual);
        }

        [Test]
        public void CardBaseEqualsOverrideRightNull()
        {
            var cardbase1 = new CardBase();
            CardBase cardbase2 = null;

            var areEqual = cardbase1 == cardbase2;

            Assert.IsFalse(areEqual);
        }

        [Test]
        public void CardBaseEqualsOverrideNotEqual()
        {
            var cardbase1 = new CardBase(Suit.Heart, CardName.Two);
            var cardbase2 = new CardBase(Suit.Heart, CardName.Ace);

            var areEqual = cardbase1 != cardbase2;

            Assert.IsTrue(areEqual);
        }

        //[Test]
        //public void CardBaseEqualsICardValueTrue()
        //{
        //    CardValue cardbase1 = new CardBase(Suit.Heart, CardName.Ace);
        //    CardValue cardbase2 = new CardBase(Suit.Heart, CardName.Ace);

        //    var areEqual = cardbase1.Equals(cardbase2);

        //    Assert.IsTrue(areEqual);
        //}

        //[Test]
        //public void CardBaseEqualsNotCardValueFalse()
        //{
        //    CardValue cardbase1 = new CardBase(Suit.Heart, CardName.Ace);
        //    const string cardbase2 = "Test";

        //    var areEqual = cardbase1.Equals(cardbase2);

        //    Assert.IsFalse(areEqual);
        //}

        [Test]
        public void CardBaseGetHashCode()
        {
            var cardbase = new CardBase(Suit.Heart, CardName.Ace);

            var expected = (Suit.Heart.GetHashCode() * 397) ^ CardName.Ace.GetHashCode();

            Assert.AreEqual(expected, cardbase.GetHashCode());
        }

        [Test]
        public void CardBaseReadWriteXml()
        {
            var cardbase =
                new CardBase
                {
                    Suit = Suit.Heart,
                    Name = CardName.Ace,
                    HoldemCard = HoldemCard.Turn,
                    Strength = 9,
                    IsSuited = true
                };
            var cardbase1 = new CardBase();

            var fileToCreate = "./Test.CardBase.xml";

            var storage = IsolatedStorageFile.GetUserStoreForApplication();

            var xmlSettings =
                new XmlWriterSettings
                {
                    ConformanceLevel = ConformanceLevel.Fragment
                };

            var xmlReaderSettings =
                new XmlReaderSettings
                {
                    ConformanceLevel = ConformanceLevel.Fragment
                };

            var writerStream = storage.CreateFile(fileToCreate);

            using (var writer = XmlWriter.Create(writerStream, xmlSettings))
            {
                cardbase.WriteXml(writer);
            }

            writerStream.Close();
            writerStream.Dispose();

            string value = null;

            using (var fileStream = storage.OpenFile(fileToCreate, FileMode.Open, FileAccess.Read))
            {
                using (var reader = XmlReader.Create(fileStream, xmlReaderSettings))
                {

                    if (reader.Read() && reader.HasValue)
                        value = reader.Value;
                }
            }

            if (storage.FileExists(fileToCreate))
                storage.DeleteFile(fileToCreate);

            Assert.IsNotNull(value);
            Assert.IsTrue(value.Contains(cardbase.Suit.ToString()));
            Assert.IsTrue(value.Contains(cardbase.Name.ToString()));
            Assert.IsTrue(value.Contains(cardbase.HoldemCard.ToString()));
            Assert.IsTrue(value.Contains(cardbase.Strength.ToString()));
            Assert.IsTrue(value.Contains(cardbase.IsSuited.ToString()));
            Assert.IsTrue(value.Contains("Card"));
            Assert.IsTrue(value.Contains("Name"));
            Assert.IsTrue(value.Contains("Suit"));
            Assert.IsTrue(value.Contains("Strength"));
            Assert.IsTrue(value.Contains("Suited"));
        }
    }
}
