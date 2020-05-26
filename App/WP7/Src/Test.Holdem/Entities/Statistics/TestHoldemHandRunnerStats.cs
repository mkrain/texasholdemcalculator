using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Statistics;

namespace Test.Holdem.Entities.Statistics
{
    [TestFixture]
    public class TestHoldemHandRunnerStats
    {
        [Test]
        public void HandRunnerRunnerStatsRunnerRunnerPercentText()
        {
            var stats =
                new HandRunnerRunnerStats
                {
                    RunnerRunnerPercent = 39.99
                };

            const string expected = "39.99%";

            Assert.AreEqual(expected, stats.RunnerRunnerPercentText);
        }

        [Test]
        public void HandRunnerRunnerStatsRunnerRunnerRatioText()
        {
            var stats =
                new HandRunnerRunnerStats
                {
                    RunnerRunnerRatio = .03999
                };

            const string expected = "0.04:1";

            Assert.AreEqual(expected, stats.RunnerRunnerRatioText);
        }
    }
}
