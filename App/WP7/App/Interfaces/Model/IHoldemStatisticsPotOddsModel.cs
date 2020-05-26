using System;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Interfaces.Model
{
	public interface IHoldemStatisticsPotOddsModel
	{
		
	    void GeneratePotOdds(IHandOptions info);
		
		event EventHandler<HoldemPotOddsEventArgs> HoldemPotOddsGenerated;
	}
}