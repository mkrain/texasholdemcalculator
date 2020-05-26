using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TexasHoldemCalculator.Core.Generator;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Generator;

namespace Test.Holdem
{
	[TestFixture]
	public class HoldemTestCardGenerator
	{
		[Test]
		public void CardGenerator_Constructor()
		{
			var numberGenerator = new Mock<INumberGenerator>();

			Assert.DoesNotThrow(
				() => new CardGenerator(numberGenerator.Object));
		}

		[Test]
		public void CardGenerator_Constructor_Invalid()
		{
            //Assert.Throws<ArgumentNullException>(
            //    () => new CardGenerator(null));
		}

		[Test]
		public void CardGenerator_GetCard_CardId_Club()
		{
			var numberGenerator = new Mock<INumberGenerator>();
			var cardGenerator = new CardGenerator(numberGenerator.Object);

			var two = cardGenerator.GetCard(1);
			var ace = cardGenerator.GetCard(13);

			Assert.AreEqual(Suit.Club, two.Suit);
			Assert.AreEqual(Suit.Club, ace.Suit);
			Assert.AreEqual(CardName.Two, two.Name);
			Assert.AreEqual(CardName.Ace, ace.Name);
		}

		[Test]
		public void CardGenerator_GetCard_CardId_Diamond()
		{
			var numberGenerator = new Mock<INumberGenerator>();
			var cardGenerator = new CardGenerator(numberGenerator.Object);

			var two = cardGenerator.GetCard(14);
			var ace = cardGenerator.GetCard(26);

			Assert.AreEqual(Suit.Diamond, two.Suit);
			Assert.AreEqual(Suit.Diamond, ace.Suit);
			Assert.AreEqual(CardName.Two, two.Name);
			Assert.AreEqual(CardName.Ace, ace.Name);
		}

		[Test]
		public void CardGenerator_GetCard_CardId_Heart()
		{
			var numberGenerator = new Mock<INumberGenerator>();
			var cardGenerator = new CardGenerator(numberGenerator.Object);

			var two = cardGenerator.GetCard(40);
			var ace = cardGenerator.GetCard(52);

			Assert.AreEqual(Suit.Heart, two.Suit);
			Assert.AreEqual(Suit.Heart, ace.Suit);
			Assert.AreEqual(CardName.Two, two.Name);
			Assert.AreEqual(CardName.Ace, ace.Name);
		}

		[Test]
		public void CardGenerator_GetCard_CardId_Spade()
		{
			var numberGenerator = new Mock<INumberGenerator>();
			var cardGenerator = new CardGenerator(numberGenerator.Object);

			var two = cardGenerator.GetCard(27);
			var ace = cardGenerator.GetCard(39);

			Assert.AreEqual(Suit.Spade, two.Suit);
			Assert.AreEqual(Suit.Spade, ace.Suit);
			Assert.AreEqual(CardName.Two, two.Name);
			Assert.AreEqual(CardName.Ace, ace.Name);
		}

		[Test]
		public void CardGenerator_GetCard_Card_Suit_Club()
		{
			var numberGenerator = new Mock<INumberGenerator>();
			var cardGenerator = new CardGenerator(numberGenerator.Object);

			var cards =
				new[]
                {
                    cardGenerator.GetCard("2", "club"),
                    cardGenerator.GetCard("3", "club"),
                    cardGenerator.GetCard("4", "club"),
                    cardGenerator.GetCard("5", "club"),
                    cardGenerator.GetCard("6", "club"),
                    cardGenerator.GetCard("7", "club"),
                    cardGenerator.GetCard("8", "club"),
                    cardGenerator.GetCard("9", "club"),
                    cardGenerator.GetCard("Ten", "club"),
                    cardGenerator.GetCard("Jack", "club"),
                    cardGenerator.GetCard("Queen", "club"),
                    cardGenerator.GetCard("King", "club"),
                    cardGenerator.GetCard("Ace", "club")
                };

			var club = ( from c in cards
						 select c.Suit ).Contains(Suit.Club);

			Assert.True(club);

			var validNames = ( from c in cards
							   select c.Name ).Distinct().Count();
			var validSuits = ( from c in cards
							   select c.Suit ).Distinct().Count();

			Assert.AreEqual(13, validNames);
			Assert.AreEqual(1, validSuits);
		}

		[Test]
		public void CardGenerator_GetCard_Card_Suit_Diamond()
		{
			var numberGenerator = new Mock<INumberGenerator>();
			var cardGenerator = new CardGenerator(numberGenerator.Object);

			var cards =
				new[]
                {
                    cardGenerator.GetCard("2", "diamond"),
                    cardGenerator.GetCard("3", "diamond"),
                    cardGenerator.GetCard("4", "diamond"),
                    cardGenerator.GetCard("5", "diamond"),
                    cardGenerator.GetCard("6", "diamond"),
                    cardGenerator.GetCard("7", "diamond"),
                    cardGenerator.GetCard("8", "diamond"),
                    cardGenerator.GetCard("9", "diamond"),
                    cardGenerator.GetCard("Ten", "diamond"),
                    cardGenerator.GetCard("Jack", "diamond"),
                    cardGenerator.GetCard("Queen", "diamond"),
                    cardGenerator.GetCard("King", "diamond"),
                    cardGenerator.GetCard("Ace", "diamond")
                };

			var diamond = ( from c in cards
							select c.Suit ).Contains(Suit.Diamond);

			Assert.True(diamond);

			var validNames = ( from c in cards
							   select c.Name ).Distinct().Count();
			var validSuits = ( from c in cards
							   select c.Suit ).Distinct().Count();

			Assert.AreEqual(13, validNames);
			Assert.AreEqual(1, validSuits);
		}

		[Test]
		public void CardGenerator_GetCard_Card_Suit_Heart()
		{
			var numberGenerator = new Mock<INumberGenerator>();
			var cardGenerator = new CardGenerator(numberGenerator.Object);

			var cards =
				new[]
                {
                    cardGenerator.GetCard("2", "heart"),
                    cardGenerator.GetCard("3", "heart"),
                    cardGenerator.GetCard("4", "heart"),
                    cardGenerator.GetCard("5", "heart"),
                    cardGenerator.GetCard("6", "heart"),
                    cardGenerator.GetCard("7", "heart"),
                    cardGenerator.GetCard("8", "heart"),
                    cardGenerator.GetCard("9", "heart"),
                    cardGenerator.GetCard("Ten", "heart"),
                    cardGenerator.GetCard("Jack", "heart"),
                    cardGenerator.GetCard("Queen", "heart"),
                    cardGenerator.GetCard("King", "heart"),
                    cardGenerator.GetCard("Ace", "heart")
                };

			var heart = ( from c in cards
						  select c.Suit ).Contains(Suit.Heart);

			Assert.True(heart);

			var validNames = ( from c in cards
							   select c.Name ).Distinct().Count();
			var validSuits = ( from c in cards
							   select c.Suit ).Distinct().Count();

			Assert.AreEqual(13, validNames);
			Assert.AreEqual(1, validSuits);
		}

		[Test]
		public void CardGenerator_GetCard_Card_Suit_Spade()
		{
			var numberGenerator = new Mock<INumberGenerator>();
			var cardGenerator = new CardGenerator(numberGenerator.Object);

			var cards =
				new[]
                {
                    cardGenerator.GetCard("2", "spade"),
                    cardGenerator.GetCard("3", "spade"),
                    cardGenerator.GetCard("4", "spade"),
                    cardGenerator.GetCard("5", "spade"),
                    cardGenerator.GetCard("6", "spade"),
                    cardGenerator.GetCard("7", "spade"),
                    cardGenerator.GetCard("8", "spade"),
                    cardGenerator.GetCard("9", "spade"),
                    cardGenerator.GetCard("Ten", "spade"),
                    cardGenerator.GetCard("Jack", "spade"),
                    cardGenerator.GetCard("Queen", "spade"),
                    cardGenerator.GetCard("King", "spade"),
                    cardGenerator.GetCard("Ace", "spade")
                };

			var spade = ( from c in cards
						  select c.Suit ).Contains(Suit.Spade);

			Assert.True(spade);

			var validNames = ( from c in cards
							   select c.Name ).Distinct().Count();
			var validSuits = ( from c in cards
							   select c.Suit ).Distinct().Count();

			Assert.AreEqual(13, validNames);
			Assert.AreEqual(1, validSuits);
		}

		[Test]
		public void CardGenerator_GetCard_Card_Suit_Null_Card()
		{
			var numberGenerator = new Mock<INumberGenerator>();
			var cardGenerator = new CardGenerator(numberGenerator.Object);

            //Assert.Throws<ArgumentNullException>(
            //    () => cardGenerator.GetCard(string.Empty, string.Empty));
		}

		[Test]
		public void CardGenerator_Constructor_GetCard_Card_Suit_Null_Suit()
		{
			var numberGenerator = new Mock<INumberGenerator>();
			var cardGenerator = new CardGenerator(numberGenerator.Object);

			//Assert.Throws<ArgumentNullException>(
			//	() => cardGenerator.GetCard("two", string.Empty));
		}

		[Test]
		public void CardGenerator_Generate_Suit()
		{
			var numberGenerator = new NumberGenerator();
			var cardGenerator = new CardGenerator(numberGenerator);

			var suit = cardGenerator.GenerateSuit();

			Assert.GreaterOrEqual(suit, Suit.Club);
			Assert.LessOrEqual(suit, Suit.Spade);
		}

		[Test]
		public void CardGenerator_GenerateSuit_ArgmentNullException()
		{
			var numberGenerator = new NumberGenerator();
			var cardGenerator = new CardGenerator(numberGenerator);

			//Assert.Throws<ArgumentNullException>(() => cardGenerator.GenerateSuit(null));
		}

		[Test]
		public void CardGenerator_GenerateSuit_ArgmentException()
		{
			var numberGenerator = new NumberGenerator();
			var cardGenerator = new CardGenerator(numberGenerator);

			//Assert.Throws<ArgumentException>(() => cardGenerator.GenerateSuit(" "));
		}
	}

	[TestFixture]
	public class HoldemTestDeckGenerator
	{
		[Test]
		public void DeckGenerator_Constructor()
		{
			var numGen = new NumberGenerator();
			var cardGen = new CardGenerator(numGen);
			var primGen = new PrimitiveGenerator(numGen);

			Assert.DoesNotThrow(() => new DeckGenerator(cardGen, primGen));
		}

		[Test]
		public void DeckGenerator_Constructor_Null_Card_Gen()
		{
			var numGen = new NumberGenerator();
			var primGen = new PrimitiveGenerator(numGen);

			//Assert.Throws<ArgumentNullException>(() => new DeckGenerator(null, primGen));
		}

		[Test]
		public void DeckGenerator_Constructor_Null_Primitive_Gen()
		{
			var numGen = new NumberGenerator();
			var cardGen = new CardGenerator(numGen);

			//Assert.Throws<ArgumentNullException>(() => new DeckGenerator(cardGen, null));
		}

		[Test]
		public void DeckGenerator_Generate_Holdem_Deck_Too_Many_Players()
		{
			var numGen = new NumberGenerator();
			var cardGen = new CardGenerator(numGen);
			var primGen = new PrimitiveGenerator(numGen);

			var deckGen = new DeckGenerator(cardGen, primGen);

			//Assert.Throws<ArgumentException>(() => deckGen.GenerateHoldemDeck(23));
		}

		[Test]
		public void DeckGenerator_Generate_Holdem_Deck_Too_Small()
		{
			var numGen = new NumberGenerator();
			var cardGen = new CardGenerator(numGen);
			var primGen = new Mock<IPrimitiveGenerator>();

			primGen
				.Setup(x => x.GenerateList(It.IsAny<byte>(), It.IsAny<byte>()))
				.Returns(new byte[] { 0, 1, 2, 3, 4, 5, 6 });

			var deckGen = new DeckGenerator(cardGen, primGen.Object);

			//Assert.Throws<ArgumentException>(() => deckGen.GenerateHoldemDeck(23));
		}

		[Test]
		public void DeckGenerator_Generate_Holdem_Deck()
		{
			var numGen = new NumberGenerator();
			var cardGen = new CardGenerator(numGen);
			var primGen = new Mock<IPrimitiveGenerator>();

			var deck = new byte[52];
			deck[0] = 14;
			deck[1] = 1;
			deck[2] = 2;
			deck[3] = 3;
			deck[4] = 4;
			deck[5] = 5;
			deck[6] = 6;
			deck[7] = 7;
			deck[8] = 8;
			deck[9] = 9;
			deck[10] = 10;
			deck[11] = 11;
			deck[12] = 12;
			deck[13] = 13;

			primGen
				.Setup(x => x.GenerateList(It.IsAny<byte>(), It.IsAny<byte>()))
				.Returns(deck);

			var deckGen = new DeckGenerator(cardGen, primGen.Object);

			var generated = deckGen.GenerateHoldemDeck(2);

			Assert.IsNotNull(generated.HoleCardOne);
			Assert.IsNotNull(generated.HoleCardTwo);
			Assert.IsNotNull(generated.FlopCardOne);
			Assert.IsNotNull(generated.FlopCardTwo);
			Assert.IsNotNull(generated.FlopCardThree);
			Assert.IsNotNull(generated.TurnCard);
			Assert.IsNotNull(generated.RiverCard);

			Assert.AreEqual(generated.HoleCardOne.HoldemCard, HoldemCard.Hole1);
			Assert.AreEqual(generated.HoleCardTwo.HoldemCard, HoldemCard.Hole2);
			Assert.AreEqual(generated.FlopCardOne.HoldemCard, HoldemCard.Flop1);
			Assert.AreEqual(generated.FlopCardTwo.HoldemCard, HoldemCard.Flop2);
			Assert.AreEqual(generated.FlopCardThree.HoldemCard, HoldemCard.Flop3);
			Assert.AreEqual(generated.TurnCard.HoldemCard, HoldemCard.Turn);
			Assert.AreEqual(generated.RiverCard.HoldemCard, HoldemCard.River);
		}

        //[Test]
        //[Ignore]
        //public void DeckGenerator_Generate_Holdem_Deck_Stress_Test()
        //{
        //    var numGen = new NumberGenerator();
        //    var primGen = new PrimitiveGenerator(numGen);
        //    var cardGen = new CardGenerator(numGen);

        //    var deckGen = new DeckGenerator(cardGen, primGen);

        //    var waitHandles = 
        //        new WaitHandle[]
        //        {
        //            new AutoResetEvent(false),
        //            new AutoResetEvent(false),
        //            new AutoResetEvent(false),
        //            new AutoResetEvent(false),
        //            new AutoResetEvent(false),
        //            new AutoResetEvent(false),
        //            new AutoResetEvent(false),
        //            new AutoResetEvent(false)
        //        };

        //    int threadCount = 0;

        //    for( int i = 0; i < 400000; i++ )
        //    {
        //        var numPlayers = numGen.Next(2, 22);

        //        int count = i;
                

        //        if (threadCount < 8)
        //        {
        //            int myCount = threadCount++;

        //            ThreadPool.QueueUserWorkItem(
        //                x =>
        //                {
        //                    var deck = deckGen.GenerateHoldemDeck(numPlayers);
        //                    Console.WriteLine(
        //                        string.Format("Iteration: {0}, NumPlayers: {1}, HandRank: {2}",
        //                                      count,
        //                                      numPlayers,
        //                                      deck.HandRank));

        //                    ( (AutoResetEvent)( waitHandles[myCount] ) ).Set();
        //                });
        //        }
        //        else
        //        {
        //            WaitHandle.WaitAll(waitHandles);
        //            threadCount = 0;
        //        }
        //    }

        //    Console.WriteLine("Done");
        //}

	    [Test]
		public void DeckGenerator_GenerateHoldemDeck_InvalidLength()
		{
			var numGen = new NumberGenerator();
			var cardGen = new CardGenerator(numGen);
			var primGen = new Mock<IPrimitiveGenerator>();

			var deck = new byte[51];
			deck[0] = 0;
			deck[1] = 1;
			deck[2] = 2;
			deck[3] = 3;
			deck[4] = 4;
			deck[5] = 5;
			deck[6] = 6;
			deck[7] = 7;
			deck[8] = 8;
			deck[9] = 9;
			deck[10] = 10;
			deck[11] = 11;
			deck[12] = 12;
			deck[13] = 13;

			primGen
				.Setup(x => x.GenerateList(It.IsAny<byte>(), It.IsAny<byte>()))
				.Returns(deck);

			var deckGen = new DeckGenerator(cardGen, primGen.Object);

			//Assert.Throws<ArgumentException>(() => deckGen.GenerateHoldemDeck(2));
		}
	}

	[TestFixture]
	public class HoldemTestNumberGenerator
	{
		[Test]
		public void NumberGenerator_RandomBytes_Negative_Size()
		{
			var numGenerator = new NumberGenerator();

            //Assert.Throws<ArgumentException>(
            //    () => numGenerator.RandomBytes(-1));
		}

		[Test]
		public void NumberGenerator_RandomBytes_Empty_Size()
		{
			var numGenerator = new NumberGenerator();

			//Assert.Throws<ArgumentException>(
			//	() => numGenerator.RandomBytes(0));
		}

		[Test]
		public void NumberGenerator_RandomBytes()
		{
			var numGenerator = new NumberGenerator();

			var size = new List<int> { 1, 52, 200, 20000 };

			size.ForEach(
				x =>
				{
					var generated = numGenerator.RandomBytes(x);

					Assert.AreEqual(x, generated.Length);
				});
		}

		[Test]
		public void NumberGenerator_Next_Negative()
		{
			var numGenerator = new NumberGenerator();

			//Assert.Throws<ArgumentOutOfRangeException>(
			//	() => numGenerator.Next(-1));
		}

		[Test]
		public void NumberGenerator_Next_Zero()
		{
			var numGenerator = new NumberGenerator();

            //Assert.Throws<ArgumentOutOfRangeException>(
            //    () => numGenerator.Next(0));
		}

		[Test]
		public void NumberGenerator_Next()
		{
			var numGenerator = new NumberGenerator();

			var size = new List<int> { 1, 52, 200, 20000 };

			size.ForEach(
				x =>
				{
					var next = numGenerator.Next(x);

					Assert.LessOrEqual(next, x);
					Assert.GreaterOrEqual(next, 0);
				});
		}

		[Test]
		public void NumberGenerator_Next_Range()
		{
			var numGenerator = new NumberGenerator();

			var size = new List<int> { 1, 52, 200, 20000 };

			size.ForEach(
				x =>
				{
					var next = numGenerator.Next(x, x * x);

					Assert.LessOrEqual(next, x * x);
					Assert.GreaterOrEqual(next, x);
				});
		}

		[Test]
		public void NumberGenerator_NextByte_Range_Min_Equal_Max()
		{
			var numGenerator = new NumberGenerator();

			var expected = 4;

			var result = numGenerator.NextByte(4, 4);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void NumberGenerator_NextByte_Range_Max_Less_Min()
		{
			var numGenerator = new NumberGenerator();

			//Assert.Throws<ArgumentException>(() => numGenerator.NextByte(4, 3));
		}

		[Test]
		public void NumberGenerator_Next_Range_Max_Gt_Int32()
		{
			var numGenerator = new NumberGenerator();

			//Assert.Throws<ArgumentException>(
			//	() => numGenerator.Next(7, Int32.MaxValue));
		}

		[Test]
		public void NumberGenerator_Next_Range_Min_Gt_Max()
		{
			var numGenerator = new NumberGenerator();

            //Assert.Throws<ArgumentException>(
            //    () => numGenerator.Next(99, 2));
		}

		[Test]
		public void NumberGenerator_Next_Range_Min_Lt_Zero()
		{
			var numGenerator = new NumberGenerator();

            //Assert.Throws<ArgumentException>(
            //    () => numGenerator.Next(-1, 2));
		}

		[Test]
		public void NumberGenerator_Next_Range_Max_Lt_Zero()
		{
			var numGenerator = new NumberGenerator();

			//Assert.Throws<ArgumentException>(
			//	() => numGenerator.Next(1, -1));
		}

		[Test]
		public void NumberGenerator_NextByte_Equal()
		{
			var numGenerator = new NumberGenerator();

			var result = numGenerator.Next(0, 0);

			Assert.AreEqual(0, result);
		}

		[Test]
		public void NumberGenerator_NextByte_Max_Less_Min()
		{
			var numGenerator = new NumberGenerator();

            //Assert.Throws<ArgumentException>(
            //    () => numGenerator.Next(200, 75));
		}

		[Test]
		public void NumberGenerator_NextByte()
		{
			var numGenerator = new NumberGenerator();

			var size = new List<byte> { 2, 7, 14 };

			size.ForEach(
				x =>
				{
					var next = numGenerator.NextByte(x, (byte)( x * x ));

					Assert.LessOrEqual(next, x * x);
					Assert.GreaterOrEqual(next, x);
				});
		}
	}

	[TestFixture]
	public class HoldemTestPrimitiveGenerator
	{
        [Test]
		public void PrimitiveGenerator_Constructor_Max_Less_Min()
        {
            var numberGenerator = new Mock<INumberGenerator>().Object;
            var primitiveGenerator = new PrimitiveGenerator(numberGenerator);

			//Assert.Throws<ArgumentException>(() => primitiveGenerator.GenerateList((byte)7, (byte)1));
		}

		[Test]
		public void PrimitiveGenerator_Constructor_Null_Num_Gen()
		{
            //Assert.Throws<ArgumentNullException>(
            //    () => new PrimitiveGenerator(null));
		}

        [Test]
		public void PrimitiveGenerator_Constructor_Min_Equal_Max()
        {
            var numberGenerator = new Mock<INumberGenerator>().Object;
            var primitiveGenerator = new PrimitiveGenerator(numberGenerator);

            var expected = new[] { 99 };

            var actual = primitiveGenerator.GenerateList(99, 99);

            Assert.AreEqual(expected, actual);
        }

		[Test]
		public void PrimitiveGenerator_Constructor_Non_Null_Num_Gen()
		{
			Assert.DoesNotThrow(
				() => new PrimitiveGenerator(new NumberGenerator()));
		}

		/// <summary>
		/// 
		/// TODO: Figure out an automated test for this.
		/// Cannot be tested using NUnit, emulator only.
		/// 
		/// </summary>
		[Test]
		public void PrimitiveGenerator_Byte_Generate_List()
		{
			IPrimitiveGenerator primGen = new PrimitiveGenerator(new NumberGenerator());

			var listSize = new List<byte>
                           {
                               1,
                               2,
                               4,
                               8,
                               16,
                               32,
                               52,
                               64,
                               254
                           };

			listSize.ForEach(
				x =>
				{
					var generated = primGen.GenerateList(0, x);

					var count = ( from i in generated
								  select i ).Distinct().Count();

					Assert.AreEqual(count, x + 1);
				});
		}
	}
}
