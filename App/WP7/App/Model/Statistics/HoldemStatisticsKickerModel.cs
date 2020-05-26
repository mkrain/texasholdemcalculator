using System;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Model.Statistics;
using TexasHoldemCalculator.Interfaces.Service;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Model.Statistics
{
    public class HoldemStatisticsKickerModel : IHoldemStatisticsKickerModel
    {
        private readonly IHoldemStatisticsService _statistics;

        #region Implementation of IHoldemStatisticsKickerModel

        public void GenerateAceAgainstBiggerKicker(IHandKickerOptions info)
		{
			var runnerStats = new HandKickerStats();

			if( info != null )
			{
				runnerStats =
					new HandKickerStats
					{
						Percentage = _statistics.PokerHandOdds.HandWithBiggerAceAsPercentage(info),
						Probability = _statistics.PokerHandOdds.HandWithBiggerAceAsProbability(info),
						Ratio = _statistics.PokerHandOdds.HandWithBiggerAceAsRatio(info)
					};
			}

			if( HoldemAceAgainstBiggerKickerGenerated != null )
				HoldemAceAgainstBiggerKickerGenerated(this, new HoldemAceAgainstBiggerKickerEventArgs(runnerStats));
		}

        public event EventHandler<HoldemAceAgainstBiggerKickerEventArgs> HoldemAceAgainstBiggerKickerGenerated;

        #endregion

        #region Constructor

        public HoldemStatisticsKickerModel(IHoldemStatisticsService statistics)
		{
			if (statistics == null)
				throw new ArgumentNullException("statistics");

			_statistics = statistics;
		}

        #endregion
    }
}