using System;

using FluentAssertions;
using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Core.Statistics;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace Test.Holdem.Statistics
{
    [TestFixture]
    public class TestPotOdds
    {
        private const double EPSILON = .000005;

        #region Constructor Tests

        [Test]
        public void PotOdds_Constructor()
        {
            var odds = new PotOdds();

            Assert.IsNotNull(odds);
            Assert.AreEqual(8, odds.Precision);
        }

        #endregion //Constructor Tests

        #region Property Tests

        [Test]
        public void PotOdds_Precision()
        {
            var odds = new PotOdds {Precision = 16};

            Assert.AreEqual(16, odds.Precision);
        }

        #endregion

        #region Expectation Tests

        [Test]
        public void PotOdds_IsPositiveExpectation_Empty_HandInfo()
        {
            var odds = new PotOdds();

            Assert.Throws(typeof(ArgumentNullException), () => odds.IsPositiveExpectation(null));
        }

        [Test]
        public void PotOdds_IsPositiveExpectation_False_Both()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 1000,
                    PotSize = 10,
                    NumberOfOuts = 23,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            Assert.Throws(typeof(ArgumentException), () => odds.IsPositiveExpectation(handInfo));
        }

        [Test]
        public void PotOdds_IsPositiveExpectation_False_River()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 1000,
                    PotSize = 10,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.River
                };

            Assert.IsFalse(odds.IsPositiveExpectation(handInfo));
        }

        [Test]
        public void PotOdds_IsPositiveExpectation_False_Turn()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 1000,
                    PotSize = 10,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Turn
                };

            Assert.IsFalse(odds.IsPositiveExpectation(handInfo));
        }

        [Test]
        public void PotOdds_IsPositiveExpectation_True_River()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 10,
                    PotSize = 1000,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.River
                };

            Assert.IsTrue(odds.IsPositiveExpectation(handInfo));
        }

        [Test]
        public void PotOdds_IsPositiveExpectation_True_Turn()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 10,
                    PotSize = 1000,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Turn
                };

            Assert.IsTrue(odds.IsPositiveExpectation(handInfo));
        }

        [Test]
        public void PotOdds_IsPositiveExpectation_True_Default()
        {
            var odds = new PotOdds();
            var handInfo = new HandConfigOptions();

            Assert.IsTrue(odds.IsPositiveExpectation(handInfo));
        }

        #endregion //Expectation Tests

        #region PotOdds Tests

        [Test]
        public void PotOdds_CalculateMaxCallableBet_Null_Options()
        {
            var odds = new PotOdds();

            Assert.Throws(typeof(ArgumentNullException), () => odds.CalculateMaxCallableBet(null));
        }

        [Test]
        public void PotOdds_CalculateMaxCallableBet_Zero_PotSize()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 1000,
                    PotSize = 0,
                    NumberOfOuts = 23,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            Assert.AreEqual(0, odds.CalculateMaxCallableBet(handInfo));
        }

        [Test]
        public void PotOdds_CalculateMaxCallableBet_Negative_PotSize()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = -100,
                    NumberOfOuts = 23,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            Assert.AreEqual(0, odds.CalculateMaxCallableBet(handInfo));
        }

        [Test]
        public void PotOdds_CalculateMaxCallableBet_Valid_PotSize()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                    {
                        MaxBet = 20,
                        PotSize = 150,
                        NumberOfOuts = PotOdds.MaxOuts,
                        NumberOfPlayers = 10,
                        Precision = 8,
                        OddsSelection = PotOddsSelection.Combined
                    };

            var prob = odds.CalculatePokerOddsAsProbability(handInfo);
            var expected = Math.Floor(Math.Round(handInfo.PotSize*prob, handInfo.Precision));

            Assert.AreEqual(expected, odds.CalculateMaxCallableBet(handInfo));
        }

        [Test]
        public void PotOdds_CalculatePotOddsPercent_Null_Info()
        {
            var odds = new PotOdds();

            Assert.Throws(typeof(ArgumentNullException), () => odds.CalculatePotOddsPercent(null));
        }

        [Test]
        public void PotOdds_CalculatePotOddsPercent()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = 23,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            var ratio = odds.CalculatePotOddsAsRatio(handInfo);
            var expected = Math.Round(100 * ratio, handInfo.Precision);

            Assert.AreEqual(expected, odds.CalculatePotOddsPercent(handInfo));
        }

        [Test]
        public void PotOdds_CalculatePotOddsPercent_Zero_PotSize()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 0,
                    NumberOfOuts = 23,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            Assert.AreEqual(0, odds.CalculatePotOddsPercent(handInfo));
        }

        [Test]
        public void PotOdds_CalculatePotOddsPercent_Negative_PotSize()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = -150,
                    NumberOfOuts = 23,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            Assert.Throws(typeof(ArgumentException), () => odds.CalculatePotOddsPercent(handInfo));
        }

        [Test]
        public void PotOdds_CalculatePotOddsAsRatio()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = 23,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            var expected = Math.Round(handInfo.PotSize / handInfo.MaxBet, handInfo.Precision);

            Assert.AreEqual(expected, odds.CalculatePotOddsAsRatio(handInfo));
        }

        [Test]
        public void PotOdds_CalculatePotOddsAsRatio_Null_Info()
        {
            var odds = new PotOdds();
            
            Assert.Throws(typeof(ArgumentNullException), () => odds.CalculatePotOddsAsRatio(null));
        }

        [Test]
        public void PotOdds_CalculatePotOddsAsRatio_Negative_PotSize()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = -150,
                    NumberOfOuts = 23,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            Assert.Throws(typeof(ArgumentException), () => odds.CalculatePotOddsAsRatio(handInfo));
        }

        [Test]
        public void PotOdds_CalculatePotOddsAsRatio_Zero_MaxBet()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 0,
                    PotSize = 150,
                    NumberOfOuts = 23,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            Assert.AreEqual(0, odds.CalculatePotOddsAsRatio(handInfo));
        }

        #endregion //PotOdds Tests

        #region Probabilities Tests

        [Test]
        public void PotOdds_TurnOrRiverProbability()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            var flopToTurn = (PotOdds.CardsLeftAtFlop - handInfo.NumberOfOuts) / (double)PotOdds.CardsLeftAtFlop;
            var turnToRiver = (PotOdds.CardsLeftAtTurn - handInfo.NumberOfOuts) / (double)PotOdds.CardsLeftAtTurn;

            var value = flopToTurn * turnToRiver;
            value = 1 - value;
            value = odds.RoundWithScale(value, handInfo.Precision);

            Math.Abs( odds.TurnOrRiverProbability(handInfo) - value ).Should().BeLessOrEqualTo(EPSILON);
        }

        [Test]
        public void PotOdds_TurnOrRiverProbability_NumberOfOuts_Zero()
        {
            var odds = new PotOdds();

            IHandOptions info = 
                new HandConfigOptions 
                {
                    NumberOfOuts = 0
                };

            Assert.AreEqual(0, odds.TurnOrRiverProbability(info));
        }

        [Test]
        public void PotOdds_TurnOrRiverProbability_NumberOfOuts_Greater_Max()
        {
            var odds = new PotOdds();

            IHandOptions info = 
                new HandConfigOptions 
                {
                    NumberOfOuts = PotOdds.MaxOuts + 1
                };

            Assert.Throws(typeof(ArgumentException), () => odds.TurnOrRiverProbability(info));
        }

        [Test]
        public void PotOdds_TurnOrRiverProbability_NumberOfOuts_Less_Zero()
        {
            var odds = new PotOdds();

            IHandOptions info = 
                new HandConfigOptions 
                {
                    NumberOfOuts = PotOdds.MaxOuts + 1
                };

            Assert.Throws(typeof(ArgumentException), () => odds.TurnOrRiverProbability(info));
        }

        [Test]
        public void PotOdds_TurnProbability()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            var prob = Math.Round(handInfo .NumberOfOuts * PotOdds.TurnRatio, odds.Precision);

            Math.Abs(odds.TurnProbability(handInfo) - prob).Should().BeLessOrEqualTo(EPSILON);
        }

        [Test]
        public void PotOdds_TurnProbability_NumberOfOuts_Zero()
        {
            var odds = new PotOdds();

            IHandOptions info = 
                new HandConfigOptions 
                {
                    NumberOfOuts = 0
                };

            Assert.AreEqual(0, odds.TurnProbability(info));
        }

        [Test]
        public void PotOdds_TurnProbability_NumberOfOuts_Less_Zero()
        {
            var odds = new PotOdds();

            IHandOptions info = 
                new HandConfigOptions 
                {
                    NumberOfOuts = -1
                };

            Assert.Throws(typeof(ArgumentException), () => odds.TurnProbability(info));
        }

        [Test]
        public void PotOdds_TurnProbability_NumberOfOuts_Greater_Max()
        {
            var odds = new PotOdds();

            IHandOptions info = 
                new HandConfigOptions 
                {
                    NumberOfOuts = PotOdds.MaxOuts + 1
                };

            Assert.Throws(typeof(ArgumentException), () => odds.TurnOrRiverProbability(info));
        }

        [Test]
        public void fPotOdds_RiverProbability()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            var prob = Math.Round(handInfo.NumberOfOuts * PotOdds.RiverRatio, odds.Precision);

            Math.Abs(odds.RiverProbability(handInfo) - prob).Should().BeLessOrEqualTo(EPSILON);
        }

        [Test]
        public void PotOdds_RiverProbability_NumberOfOuts_Zero()
        {
            var odds = new PotOdds();

            IHandOptions info = 
                new HandConfigOptions 
                {
                    NumberOfOuts = 0
                };

            Assert.AreEqual(0, odds.RiverProbability(info));
        }

        [Test]
        public void PotOdds_RiverProbability_NumberOfOuts_Less_Zero()
        {
            var odds = new PotOdds();

            IHandOptions info = 
                new HandConfigOptions 
                {
                    NumberOfOuts = -1
                };

            Assert.Throws(typeof(ArgumentException), () => odds.RiverProbability(info));
        }

        [Test]
        public void PotOdds_RiverProbability_NumberOfOuts_Greater_Max()
        {
            var odds = new PotOdds();

            IHandOptions info = 
                new HandConfigOptions 
                {
                    NumberOfOuts = PotOdds.MaxOuts + 1
                };

            Assert.Throws(typeof(ArgumentException), () => odds.RiverProbability(info));
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsProbability_Null_Info()
        {
            var odds = new PotOdds();

            Assert.Throws(typeof(ArgumentNullException), () => odds.CalculatePokerOddsAsProbability(null));
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsProbability_Turn()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Turn
                };

            var result = Math.Round(odds.TurnProbability(handInfo), 8);

            Math.Abs(odds.CalculatePokerOddsAsProbability(handInfo) - result).Should().BeLessOrEqualTo(EPSILON);
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsProbability_River()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.River
                };

            var result = Math.Round(odds.RiverProbability(handInfo), 8);

            Math.Abs(odds.CalculatePokerOddsAsProbability(handInfo) - result).Should().BeLessOrEqualTo(EPSILON);
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsProbability_Combined()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            var result = odds.RoundWithScale(odds.TurnOrRiverProbability(handInfo), 8);

            odds.CalculatePokerOddsAsProbability(handInfo).Should().BeGreaterOrEqualTo(result);
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsProbability_Default()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.PotOdds
                };

            var result = 0.0;

            Assert.AreEqual(result, odds.CalculatePokerOddsAsProbability(handInfo));
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsPercent_Null_Info()
        {
            var odds = new PotOdds();

            Assert.Throws(typeof(ArgumentNullException), () => odds.CalculatePokerOddsAsPercent(null));
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsPercent_Turn()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Turn
                };

            var result = Math.Round(100 * odds.TurnProbability(handInfo), handInfo.Precision);

            Assert.AreEqual(result, odds.CalculatePokerOddsAsPercent(handInfo));
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsPercent_River()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.River
                };

            var result = Math.Round(100 * odds.RiverProbability(handInfo), handInfo.Precision);

            Assert.AreEqual(result, odds.CalculatePokerOddsAsPercent(handInfo));
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsPercent_Combined()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            var result = Math.Round(100 * odds.TurnOrRiverProbability(handInfo), handInfo.Precision);

            Assert.AreEqual(result, odds.CalculatePokerOddsAsPercent(handInfo));
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsPercent_Default()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.PotOddsRatio
                };

            var result = 0.0;

            Assert.AreEqual(result, odds.CalculatePokerOddsAsPercent(handInfo));
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsRatio_Null_Info()
        {
            var odds = new PotOdds();

            Assert.Throws(typeof(ArgumentNullException), () => odds.CalculatePokerOddsAsRatio(null));
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsRatio_Turn()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Turn
                };

            var value = odds.TurnProbability(handInfo);
            var result = Math.Round(Math.Round(1 / value, odds.Precision) - 1, handInfo.Precision);

            Math.Abs(odds.CalculatePokerOddsAsRatio(handInfo) - result).Should().BeLessOrEqualTo(EPSILON);
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsRatio_River()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.River
                };

            var value = odds.RiverProbability(handInfo);
            var result = Math.Round(Math.Round(1 / value, odds.Precision) - 1, handInfo.Precision);

            Math.Abs(odds.CalculatePokerOddsAsRatio(handInfo) - result).Should().BeLessOrEqualTo(EPSILON);
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsRatio_Combined()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.Combined
                };

            var value = odds.TurnOrRiverProbability(handInfo);
            var result = Math.Round(Math.Round(1 / value, odds.Precision) - 1, handInfo.Precision);

            Math.Abs(odds.CalculatePokerOddsAsRatio(handInfo) - result).Should().BeLessOrEqualTo(EPSILON);
        }

        [Test]
        public void PotOdds_CalculatePokerOddsAsRatio_Default()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandConfigOptions
                {
                    MaxBet = 20,
                    PotSize = 150,
                    NumberOfOuts = PotOdds.MaxOuts,
                    NumberOfPlayers = 10,
                    Precision = 8,
                    OddsSelection = PotOddsSelection.PotOddsRatio
                };

            var result = 0.0;

            Assert.AreEqual(result, odds.CalculatePokerOddsAsRatio(handInfo));
        }

        [Test]
        public void PotOdds_RunnerRunnerAsPercentage()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandRunnerRunnerOptions
                    {
                        NumberOfOuts = PotOdds.MaxOuts, 
                        Precision = 8
                    };

            var expected = Math.Round(100*odds.RunnerRunnerProbability(handInfo), handInfo.Precision);

            Assert.AreEqual(expected, odds.RunnerRunnerAsPercentage(handInfo));
        }

        [Test]
        public void PotOdds_RunnerRunnerProbability_Null_Info()
        {
            var odds = new PotOdds();

            Assert.Throws(typeof(ArgumentNullException), () => odds.RunnerRunnerProbability(null));
        }

        [Test]
        public void PotOdds_RunnerRunnerProbability_NumberOfOuts_Less_Zero()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandRunnerRunnerOptions
                {
                    NumberOfOuts = -1,
                    Precision = 8
                };

            Assert.Throws(typeof(ArgumentException), () => odds.RunnerRunnerProbability(handInfo));
        }

        [Test]
        public void PotOdds_RunnerRunnerProbability_NumberOfOuts_Greater_Max()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandRunnerRunnerOptions
                {
                    NumberOfOuts = PotOdds.MaxOuts + 1,
                    Precision = 8
                };

            Assert.Throws(typeof(ArgumentException), () => odds.RunnerRunnerProbability(handInfo));
        }

        [Test]
        public void PotOdds_RunnerRunnerProbability()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandRunnerRunnerOptions
                {
                    NumberOfOuts = PotOdds.MaxOuts,
                    Precision = 8
                };

            var expected = Math.Round(100 * odds.RunnerRunnerProbability(handInfo), handInfo.Precision);

            Assert.AreEqual(expected, odds.RunnerRunnerAsPercentage(handInfo));
        }

        [Test]
        public void PotOdds_RunnerRunnerProbability_NumberOfOuts_Zero()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandRunnerRunnerOptions
                {
                    NumberOfOuts = 0,
                    Precision = 8
                };

            Assert.AreEqual(0, odds.RunnerRunnerAsPercentage(handInfo));
        }

        [Test]
        public void PotOdds_RunnerRunnerAsRatio_Null_Info()
        {
            var odds = new PotOdds();

            Assert.Throws(typeof(ArgumentNullException), () => odds.RunnerRunnerAsRatio(null));
        }

        [Test]
        public void PotOdds_RunnerRunnerAsRatio_NumberOfOuts_Less_Zero()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandRunnerRunnerOptions
                {
                    NumberOfOuts = -1,
                    Precision = 8
                };

            Assert.Throws(typeof(ArgumentException), () => odds.RunnerRunnerAsRatio(handInfo));
        }

        [Test]
        public void PotOdds_RunnerRunnerAsRatio_NumberOfOuts_Greater_Max()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandRunnerRunnerOptions
                {
                    NumberOfOuts = PotOdds.MaxOuts + 1,
                    Precision = 8
                };

            Assert.Throws(typeof(ArgumentException), () => odds.RunnerRunnerAsRatio(handInfo));
        }

        [Test]
        public void PotOdds_RunnerRunnerAsRatio()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandRunnerRunnerOptions
                {
                    NumberOfOuts = PotOdds.MaxOuts,
                    Precision = 8
                };

            var ratio = 1 / odds.RunnerRunnerProbability(handInfo);
            var expected = Math.Round(ratio - 1, handInfo.Precision);

            Math.Abs(odds.RunnerRunnerAsRatio(handInfo) - expected).Should().BeLessOrEqualTo(EPSILON);
        }

        [Test]
        public void PotOdds_RunnerRunnerAsRatio_NumberOfOuts_Zero()
        {
            var odds = new PotOdds();
            var handInfo =
                new HandRunnerRunnerOptions
                {
                    NumberOfOuts = 0,
                    Precision = 8
                };

            Assert.AreEqual(0, odds.RunnerRunnerAsRatio(handInfo));
        }

        #endregion //Probabilities Tests
    }
}
