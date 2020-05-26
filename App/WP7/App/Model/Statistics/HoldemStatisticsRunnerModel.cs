using System;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Model.Statistics;
using TexasHoldemCalculator.Interfaces.Service;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Model.Statistics
{
    public class HoldemStatisticsRunnerModel : IHoldemStatisticsRunnerModel
    {
        public event EventHandler<HoldemRunnerRunnerEventArgs> HoldemRunnerRunnerGenerated;
        private readonly IHoldemStatisticsService _statistics;

		public HoldemStatisticsRunnerModel(IHoldemStatisticsService statistics)
		{
			if (statistics == null)
				throw new ArgumentNullException("statistics");

			_statistics = statistics;
		}

        public void GenerateRunerRuunerOdds(IHandRunnerRunnerOptions info)
		{
			var runnerStats = new HandRunnerRunnerStats();

			if( info != null )
			{
				runnerStats =
					new HandRunnerRunnerStats
					{
						RunnerRunnerPercent = _statistics.PotOdds.RunnerRunnerAsPercentage(info),
						RunnerRunnerRatio = _statistics.PotOdds.RunnerRunnerAsRatio(info)
					};
			}

			if( HoldemRunnerRunnerGenerated != null )
				HoldemRunnerRunnerGenerated(this, new HoldemRunnerRunnerEventArgs(runnerStats));
		}
    }
}