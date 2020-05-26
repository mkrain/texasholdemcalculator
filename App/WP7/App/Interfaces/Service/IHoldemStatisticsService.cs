using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Interfaces.Service
{
	public interface IHoldemStatisticsService
	{
		IPotOdds PotOdds
		{
			get;
		}

		IPokerHandOdds PokerHandOdds
		{
			get;
		}

		IPocketPairOdds PocketPairOdds
		{
			get;
		}
	}
}