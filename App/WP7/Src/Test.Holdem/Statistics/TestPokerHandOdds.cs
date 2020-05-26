using System;
using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Core.Statistics;
using TexasHoldemCalculator.Interfaces.Card;

namespace Test.Holdem.Statistics
{
    [TestFixture]
    public class TestPokerHandOdds
    {
        [Test]
        public void PokerHandOdds_Precision()
        {
            var odds = new PokerHandOdds();

            Assert.AreEqual(8, odds.Precision);
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsProbability()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 23,
                    Precision = 8
                };

            var num = (159 - (12 * ((int)info.CardValue + 2)));
            var expected = (num / (double)1225);

            expected = 1 - Math.Pow(1 - expected, info.NumberOfPlayers);

            expected = Math.Round(100 * Math.Round(expected, 8), info.Precision) / 100;

            Assert.AreEqual(expected, odds.HandWithBiggerAceAsProbability(info));
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsProbability_Single_Opponent()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 2,
                    Precision = 8
                };

            var num = (159 - (12 * ((int)info.CardValue + 2)));
            var expected = (num / (double)1225);

            expected = Math.Round(100 * Math.Round(expected, 8), info.Precision) / 100;

            Assert.AreEqual(expected, odds.HandWithBiggerAceAsProbability(info));
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsProbability_Precision_Less_Zero()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 23,
                    Precision = -1
                };

            var num = (159 - (12 * ((int)info.CardValue + 2)));
            var expected = (num / (double)1225);

            expected = 1 - Math.Pow(1 - expected, info.NumberOfPlayers);

            expected = Math.Round(100 * Math.Round(expected, 8), 8) / 100;

            Assert.AreEqual(expected, odds.HandWithBiggerAceAsProbability(info));
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsProbability_Precision_Greater_Max()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 23,
                    Precision = 9
                };

            var num = (159 - (12 * ((int)info.CardValue + 2)));
            var expected = (num / (double)1225);

            expected = 1 - Math.Pow(1 - expected, info.NumberOfPlayers);

            expected = Math.Round(100 * Math.Round(expected, 8), 8) / 100;

            Assert.AreEqual(expected, odds.HandWithBiggerAceAsProbability(info));
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsProbability_Ace()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.Ace,
                    NumberOfPlayers = 23,
                    Precision = 8
                };

            var expected = 0;

            Assert.AreEqual(expected, odds.HandWithBiggerAceAsProbability(info));
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsRatio()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 23,
                    Precision = 8
                };

            var expected = odds.HandWithBiggerAceAsProbability(info);

            expected = 1 / expected;

            expected = Math.Round(Math.Round(expected, 8) - 1, info.Precision);

            Assert.AreEqual(expected, odds.HandWithBiggerAceAsRatio(info));
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsRatio_Precision_Less_Zero()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 23,
                    Precision = -1
                };

            var expected = odds.HandWithBiggerAceAsProbability(info);

            expected = 1 / expected;

            expected = Math.Round(Math.Round(expected, 8) - 1, 8);

            info.Precision = -1;

            Assert.AreEqual(expected, odds.HandWithBiggerAceAsRatio(info));
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsRatio_Precision_Greater_Max()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 23,
                    Precision = 9
                };

            var expected = odds.HandWithBiggerAceAsProbability(info);

            expected = 1 / expected;

            expected = Math.Round(Math.Round(expected, 8) - 1, 8);

            info.Precision = 9;

            Assert.AreEqual(expected, odds.HandWithBiggerAceAsRatio(info));
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsRatio_Ace()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.Ace,
                    NumberOfPlayers = 23,
                    Precision = 8
                };

            var expected = 0;

            Assert.AreEqual(expected, odds.HandWithBiggerAceAsRatio(info));
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsPercentage()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 23,
                    Precision = 8
                };

            var expected = odds.HandWithBiggerAceAsProbability(info);

            expected = Math.Round(100 * expected, info.Precision);

            Assert.AreEqual(expected, odds.HandWithBiggerAceAsPercentage(info));
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsPercentage_Ace()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.Ace,
                    NumberOfPlayers = 23,
                    Precision = 8
                };

            Assert.AreEqual(0, odds.HandWithBiggerAceAsPercentage(info));
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsPercentage_Precision_Less_Zero()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 23,
                    Precision = 8
                };

            var expected = odds.HandWithBiggerAceAsProbability(info);

            expected = Math.Round(100 * expected, info.Precision);

            info.Precision = -1;

            Assert.AreEqual(expected, odds.HandWithBiggerAceAsPercentage(info));
        }

        [Test]
        public void PokerHandOdds_HandWithBiggerAceAsPercentage_Precision_Greater_Max()
        {
            var odds = new PokerHandOdds();
            var info =
                new HandKickerOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 23,
                    Precision = 8
                };

            var expected = odds.HandWithBiggerAceAsProbability(info);

            expected = Math.Round(100 * expected, info.Precision);

            info.Precision = 9;

            Assert.AreEqual(expected, odds.HandWithBiggerAceAsPercentage(info));
        }
    }
}
