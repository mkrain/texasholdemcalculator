using System;
using System.Globalization;
using System.Linq;
using System.Resources;
using Moq;

using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Core.Resource;
using TexasHoldemCalculator.Interfaces.Provider;

namespace Test.Holdem
{
    [TestFixture]
    public class HoldemTestResources
    {
        private const string ODDS_PERCENT_FORMAT_STRING = "Hand{0}_Percent_PocketPairMatchup";
        private const string ODDS_DESCRIPTION_FORMAT_STRING = "Hand{0}_Description_PocketPairMatchup";
        private const string ODDS_ODDS_FORMAT_STRING = "Hand{0}_Odds_PocketPairMatchup";

        [Test]
        public void HoldemResources_Constructor()
        {
            var iconProvider = new Mock<IIconProvider>().Object;
            var resources = new HoldemResource(iconProvider);

            Assert.NotNull(resources);
        }

        [Test]
        public void HoldemResources_HoleOdds()
        {
            var iconProvider = new Mock<IIconProvider>().Object;
            var resources = new HoldemResource(iconProvider);

            Assert.NotNull(resources.HoleOdds());
        }

        [Test]
        public void HoldemResources_HoleOdds_Odds_Percent()
        {
            var iconProvider = new Mock<IIconProvider>().Object;
            var resources = new HoldemResource(iconProvider);

            int count = resources.HoleOdds().Count - 1;

            while( count >= 0 )
            {
                var odds = string.Format(CultureInfo.InvariantCulture, ODDS_ODDS_FORMAT_STRING, count);
                var percent = string.Format(CultureInfo.InvariantCulture, ODDS_PERCENT_FORMAT_STRING, count);
                var description = string.Format(CultureInfo.InvariantCulture, ODDS_DESCRIPTION_FORMAT_STRING, count--);

                odds = resources.GetString(odds);
                percent = resources.GetString(percent);
                description = resources.GetString(description);

                Assert.IsNotNull(odds);
                Assert.IsNotNull(percent);
                Assert.IsNotNull(description);

                var holeOdds =
                    new HoleOdds(iconProvider)
                    {
                        Description = description,
                        Details = 
                            new HoleOddsDetails
                            {
                                Odds = odds,
                                Percent = percent
                            }
                    };

                var found = resources.HoleOdds().FirstOrDefault(
                    x => string.Compare(x.Description, holeOdds.Description) == 0
                         && string.Compare(x.Details.Odds, holeOdds.Details.Odds) == 0
                         && string.Compare(x.Details.Percent, holeOdds.Details.Percent) == 0);

                Assert.NotNull(found);
            }
        }

        [Test]
        public void HoldemResource_GetString_Exception()
        {
            var rm = new Mock<ResourceManager>();
            
            rm
                .Setup(m => m.GetString(It.IsAny<string>()))
                .Throws(new ArgumentException("name"));

            var hm = new HoldemResource(rm.Object);

            string result = "not null";

            Assert.DoesNotThrow(
                () =>
                {
                    result = hm.GetString("name");
                } );

            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result);
        }

    }
}
