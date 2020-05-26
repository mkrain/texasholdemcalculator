using System;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Service;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Model.Statistics
{
	public class HoldemStatisticsPotOddsModel : IHoldemStatisticsPotOddsModel
	{
        public event EventHandler<HoldemPotOddsEventArgs> HoldemPotOddsGenerated;
		private readonly IHoldemStatisticsService _statistics;

		public HoldemStatisticsPotOddsModel(IHoldemStatisticsService statistics)
		{
			if (statistics == null)
				throw new ArgumentNullException("statistics");

			_statistics = statistics;
		}

		#region IHoldemCalculatorController Members
		
		public void GeneratePotOdds(IHandOptions info)
		{
			var handStats = new FullHandStatsInfo();

			if( info != null )
			{
				var flopInfo = info.Clone();
				flopInfo.OddsSelection = PotOddsSelection.Combined;

				var turnInfo = info.Clone();
				turnInfo.OddsSelection = PotOddsSelection.Turn;

				var riverInfo = info.Clone();
				riverInfo.OddsSelection = PotOddsSelection.River;

				handStats =
					new FullHandStatsInfo
					{
						FlopHandStats =
							new HandStatsInfo
							{
								Expectation = _statistics.PotOdds.IsPositiveExpectation(flopInfo),
								HandOdds = _statistics.PotOdds.CalculatePokerOddsAsRatio(flopInfo),
								MakeHandPercent = _statistics.PotOdds.CalculatePokerOddsAsPercent(flopInfo),
								MaxBet = _statistics.PotOdds.CalculateMaxCallableBet(flopInfo),
								PotOdds = _statistics.PotOdds.CalculatePotOddsAsRatio(flopInfo)
							},
						TurnHandStats =
							new HandStatsInfo
							{
								Expectation = _statistics.PotOdds.IsPositiveExpectation(turnInfo),
								HandOdds = _statistics.PotOdds.CalculatePokerOddsAsRatio(turnInfo),
								MakeHandPercent = _statistics.PotOdds.CalculatePokerOddsAsPercent(turnInfo),
								MaxBet = _statistics.PotOdds.CalculateMaxCallableBet(turnInfo),
								PotOdds = _statistics.PotOdds.CalculatePotOddsAsRatio(turnInfo)
							},
						RiverHandStats =
							new HandStatsInfo
							{
								Expectation = _statistics.PotOdds.IsPositiveExpectation(riverInfo),
								HandOdds = _statistics.PotOdds.CalculatePokerOddsAsRatio(riverInfo),
								MakeHandPercent = _statistics.PotOdds.CalculatePokerOddsAsPercent(riverInfo),
								MaxBet = _statistics.PotOdds.CalculateMaxCallableBet(riverInfo),
								PotOdds = _statistics.PotOdds.CalculatePotOddsAsRatio(riverInfo)
							}
					};
			}

			if( HoldemPotOddsGenerated != null )
				HoldemPotOddsGenerated(this, new HoldemPotOddsEventArgs(handStats));
		}

		#endregion
	}
}