using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Statistics;

namespace Test.Holdem.Entities.Statistics
{
    [TestFixture]
    public class TestHoldemHandKickerStats
    {
        [Test]
        public void HandKickerStatsProbabilityText()
        {
            var stats =
                new HandKickerStats
                {
                    Probability = 0.4123
                };

            string expected = "0.41";

            Assert.AreEqual(expected, stats.ProbabilityText);

            stats.Probability = 0.0120;

            expected = "0.01";

            Assert.AreEqual(expected, stats.ProbabilityText);
        }

        [Test]
        public void HandKickerStatsPercentageText()
        {
            var stats =
                new HandKickerStats
                {
                    Percentage = 39.12345
                };

            const string expected = "39.12%";

            Assert.AreEqual(expected, stats.PercentageText);
        }

        [Test]
        public void HandKickerStatsRatioText()
        {
            var stats =
                new HandKickerStats
                {
                    Ratio = 0.4123
                };

            string expected = "0.41:1";

            Assert.AreEqual(expected, stats.RatioText);

            stats.Ratio = 0.0120;

            expected = "0.01:1";

            Assert.AreEqual(expected, stats.RatioText);
        }
    }
}
