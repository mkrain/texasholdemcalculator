using System;
using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Core.Statistics;
using TexasHoldemCalculator.Interfaces.Card;

namespace Test.Holdem.Statistics
{
    [TestFixture]
    public class TestPokerPairOdds
    {
        #region Property Tests

        [Test]
        public void PocketPairOdds_Constructor()
        {
            var odds = new PocketPairOdds();

            Assert.AreEqual(1000, odds.RoundFactor);
        }

        #endregion //Property Tests

        #region PocketPairOdds Members

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherSingleProbability_Null_Info()
        {
            var odds = new PocketPairOdds();
            
            Assert.Throws(typeof(ArgumentNullException), () => odds.ProbabilityHandFacesHigherSingleProbability(null));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherSingleProbability()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.Ace,
                    NumberOfPlayers = 10,
                    Precision = 8
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8);
            expected = Math.Round(100 * expected, 3) / 100;

            Assert.AreEqual(expected,  odds.ProbabilityHandFacesHigherSingleProbability(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherSingleRatio_Null_Info()
        {
            var odds = new PocketPairOdds();

            Assert.Throws(typeof(ArgumentNullException), () => odds.ProbabilityHandFacesHigherSingleRatio(null));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherSingleRatio()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = 6
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8);
            expected = Math.Round(1 / expected, info.Precision);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherSingleRatio(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherSingleRatio_Precision_Less_Zero()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = -1
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8);
            expected = Math.Round(1 / expected, 8);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherSingleRatio(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherSingleRatio_Precision_Greater_Max()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = 9
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8);
            expected = Math.Round(1 / expected, 8);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherSingleRatio(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherSingleRatio_Zero_Probability()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.Ace,
                    NumberOfPlayers = 10,
                    Precision = 8
                };

            Assert.AreEqual(0, odds.ProbabilityHandFacesHigherSingleRatio(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherSinglePercentage()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = 8
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8);
            expected = Math.Round(100 * expected, info.Precision);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherSinglePercentage(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherSinglePercentage_Null_Info()
        {
            var odds = new PocketPairOdds();

            Assert.Throws(typeof(ArgumentNullException), () => odds.ProbabilityHandFacesHigherSinglePercentage(null));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherSinglePercentage_Precision_Less_Zero()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = -1
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8);
            expected = Math.Round(100 * expected, 8);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherSinglePercentage(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherSinglePercentage_Precision_Greater_Max()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = 9
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8);
            expected = Math.Round(100 * expected, 8);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherSinglePercentage(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiProbability()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = 8
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8);
            expected = Math.Round(100 * expected * info.NumberOfPlayers, 8);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherMultiProbability(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiProbability_Null_Info()
        {
            var odds = new PocketPairOdds();

            Assert.Throws(typeof(ArgumentNullException), () => odds.ProbabilityHandFacesHigherMultiProbability(null));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiProbability_NumberOfPlayers_Less_Zero()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = -1,
                    Precision = 8
                };

            Assert.Throws(typeof(ArgumentException), () => odds.ProbabilityHandFacesHigherMultiProbability(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiProbability_NumberOfPlayers_Greater_Max()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 23,
                    Precision = 8
                };

            Assert.Throws(typeof(ArgumentException), () => odds.ProbabilityHandFacesHigherMultiProbability(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiRatio()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = 8
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8) * info.NumberOfPlayers;
            expected = Math.Round(1 / expected, info.Precision);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherMultiRatio(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiRatio_Null_Info()
        {
            var odds = new PocketPairOdds();
            
            Assert.Throws(typeof(ArgumentNullException), () => odds.ProbabilityHandFacesHigherMultiRatio(null));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiRatio_Precision_Less_Zero()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = -1
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8) * info.NumberOfPlayers;
            expected = Math.Round(1 / expected, 8);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherMultiRatio(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiRatio_Precision_Greater_Max()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = 9
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8) * info.NumberOfPlayers;
            expected = Math.Round(1 / expected, 8);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherMultiRatio(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiRatio_Denominator_Zero()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.Ace,
                    NumberOfPlayers = 10,
                    Precision = 9
                };

            Assert.AreEqual(0, odds.ProbabilityHandFacesHigherMultiRatio(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiPercentage()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = 8
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8) * info.NumberOfPlayers;
            expected = Math.Round(100 * expected, info.Precision);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherMultiPercentage(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiPercentage_Null_Info()
        {
            var odds = new PocketPairOdds();
            
            Assert.Throws(typeof(ArgumentNullException), () => odds.ProbabilityHandFacesHigherMultiPercentage(null));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiPercentage_NumberOfPlayers_Less_Zero()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = -1,
                    Precision = 8
                };

            Assert.Throws(typeof(ArgumentException), () => odds.ProbabilityHandFacesHigherMultiPercentage(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiPercentage_Greater_Max()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = -1,
                    Precision = 8
                };

            Assert.Throws(typeof(ArgumentException), () => odds.ProbabilityHandFacesHigherMultiPercentage(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiPercentage_Precision_Less_Zero()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = -1
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8) * info.NumberOfPlayers;
            expected = Math.Round(100 * expected, 8);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherMultiPercentage(info));
        }

        [Test]
        public void PocketPairOdds_ProbabilityHandFacesHigherMultiPercentage_Precision_Greater_Max()
        {
            var odds = new PocketPairOdds();
            var info =
                new HandPocketPairOptions
                {
                    CardValue = CardName.King,
                    NumberOfPlayers = 10,
                    Precision = 9
                };

            const double singleDenom = 1225;
            const double singleHandProb = 84 / singleDenom;

            var expected = (6 * ((int)info.CardValue + 2)) / singleDenom;
            expected = Math.Round(singleHandProb - expected, 8) * info.NumberOfPlayers;
            expected = Math.Round(100 * expected, 8);

            Assert.AreEqual(expected, odds.ProbabilityHandFacesHigherMultiPercentage(info));
        }

        #endregion //PocketPairOdds Members
    }
}