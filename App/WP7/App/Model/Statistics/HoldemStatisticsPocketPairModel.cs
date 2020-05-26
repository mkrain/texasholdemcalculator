using System;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Model.Statistics;
using TexasHoldemCalculator.Interfaces.Service;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Model.Statistics
{
	public class HoldemStatisticsPocketPairModel : IHoldemStatisticsPocketPairModel
	{
		private readonly IHoldemStatisticsService _statistics;
        public event EventHandler<HoldemAgainstBiggerPairEventArgs> HoldemAgainstBiggerPairGenerated;

		public HoldemStatisticsPocketPairModel(IHoldemStatisticsService statistics)
		{
			if (statistics == null)
				throw new ArgumentNullException("statistics");

			_statistics = statistics;
		}

		#region IHoldemStatistics Implementation

		public void GenerateAgainstBiggerPair(IHandPocketPairOptions info)
		{
			var pocketPairStats = new HandPocketPairStats();

			if( info != null )
			{
				pocketPairStats =
					new HandPocketPairStats
					{
						Percentage = _statistics.PocketPairOdds.ProbabilityHandFacesHigherSinglePercentage(info),
						Probability = _statistics.PocketPairOdds.ProbabilityHandFacesHigherSingleProbability(info),
						Ratio = _statistics.PocketPairOdds.ProbabilityHandFacesHigherSingleRatio(info)
					};
			}

			if( HoldemAgainstBiggerPairGenerated != null )
				HoldemAgainstBiggerPairGenerated(this, new HoldemAgainstBiggerPairEventArgs(pocketPairStats));
		}

		#endregion //IHoldemStatistics Implementation
	}
}