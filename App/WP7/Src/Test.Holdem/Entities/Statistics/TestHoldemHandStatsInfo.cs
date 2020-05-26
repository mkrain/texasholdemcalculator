using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Statistics;

namespace Test.Holdem.Entities.Statistics
{
    [TestFixture]
    public class TestHoldemHandStatsInfo
    {
        [Test]
        public void HandStatsInfoMakeHandText()
        {
            var stats =
                new HandStatsInfo
                {
                    MakeHandPercent = 39.12345
                };

            const string expected = "39.12%";

            Assert.AreEqual(expected, stats.MakeHandText);
        }

        [Test]
        public void HandStatsInfoPotOddsText()
        {
            var stats =
                new HandStatsInfo
                {
                    PotOdds = 0.4123
                };

            string expected = "0.41";

            Assert.AreEqual(expected, stats.PotOddsText);

            stats.PotOdds = 0.0120;

            expected = "0.01";

            Assert.AreEqual(expected, stats.PotOddsText);
        }

        [Test]
        public void HandStatsInfoMaxBetText()
        {
            var stats =
                new HandStatsInfo
                {
                    MaxBet = 1234
                };

            var expected = "1234";

            Assert.AreEqual(expected, stats.MaxBetText);

        }

        [Test]
        public void HandStatsInfoHandOddsText()
        {
            var stats =
                new HandStatsInfo
                {
                    HandOdds = 23.99
                };

            var expected = "23.99";

            Assert.AreEqual(expected, stats.HandOddsText);

        }
    }
}
