using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Interfaces.Provider;
using TexasHoldemCalculator.Interfaces.Resource;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Core.Resource
{
	public class HoldemResource : IHoldemResource
	{
		private const string ODDS_PERCENT_FORMAT_STRING = "Hand{0}_Percent_PocketPairMatchup";
		private const string ODDS_DESCRIPTION_FORMAT_STRING = "Hand{0}_Description_PocketPairMatchup";
		private const string ODDS_ODDS_FORMAT_STRING = "Hand{0}_Odds_PocketPairMatchup";
        private static ResourceManager _rm;
        private readonly IIconProvider _iconProvider;
		private static readonly IList<IHoleOdds> _holeOdds = new List<IHoleOdds>();


        public HoldemResource(IIconProvider iconProvider)
            : this(new ResourceManager(typeof(HoldemResource).FullName, typeof(HoldemResource).Assembly))
        {
            if (iconProvider == null)
                throw new ArgumentNullException("iconProvider");

            _iconProvider = iconProvider;
        }

        public HoldemResource(ResourceManager manager)
        {
            _rm = manager;
        }

		public string GetString(string resource)
		{
			try
			{
				return _rm == null ? string.Empty : _rm.GetString(resource);
			}
			catch
			{
				return string.Empty;
			}
		}

		public IList<IHoleOdds> HoleOdds()
		{
			if (_holeOdds.Count > 0)
				return _holeOdds;

			var num = 0;
			var odds = this.GetString(string.Format(CultureInfo.InvariantCulture, ODDS_ODDS_FORMAT_STRING, num));
			var percent = this.GetString(string.Format(CultureInfo.InvariantCulture, ODDS_PERCENT_FORMAT_STRING, num));
			var description = this.GetString(string.Format(CultureInfo.InvariantCulture, ODDS_DESCRIPTION_FORMAT_STRING, num));

			while( !string.IsNullOrEmpty(percent) )
			{
                var holeOdds =
                    new HoleOdds(_iconProvider)
                    {
                        Description = description,
                        Details =
                            new HoleOddsDetails
                            {
                                Odds = odds.Trim(),
                                Percent = percent.Trim()
                            }
                    };

				_holeOdds.Add(holeOdds);

				odds = this.GetString(string.Format(CultureInfo.InvariantCulture, ODDS_ODDS_FORMAT_STRING, ++num));
				percent = this.GetString(string.Format(CultureInfo.InvariantCulture, ODDS_PERCENT_FORMAT_STRING, num));
				description = this.GetString(string.Format(CultureInfo.InvariantCulture, ODDS_DESCRIPTION_FORMAT_STRING, num));
			}

			return _holeOdds;
		}
	}
}