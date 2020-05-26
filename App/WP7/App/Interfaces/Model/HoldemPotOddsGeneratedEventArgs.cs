using System;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Interfaces.Model
{
	public class HoldemPotOddsEventArgs : EventArgs
	{
        public IFullHandStatsInfo FullHandStatsInfo
		{
			get;
			set;
		}

        public HoldemPotOddsEventArgs(IFullHandStatsInfo statsInfo)
		{
            this.FullHandStatsInfo = statsInfo;
		}
	}
}
