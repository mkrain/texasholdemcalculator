using System;
using TexasHoldemCalculator.Interfaces.Service;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Service
{
	public class HoldemStatisticsService : IHoldemStatisticsService
	{
		#region Implementation of IHoldemStatisticsService

		private readonly IPotOdds _potOdds;
		private readonly IPokerHandOdds _pokerHandOdds;
		private readonly IPocketPairOdds _pocketPairOdds;

		public IPotOdds PotOdds { get { return _potOdds; } }
		public IPokerHandOdds PokerHandOdds { get { return _pokerHandOdds; } }
		public IPocketPairOdds PocketPairOdds { get { return _pocketPairOdds; } }

		#endregion


		public HoldemStatisticsService(IPotOdds potOdds, IPokerHandOdds handOdds, IPocketPairOdds pairOdds)
		{
			if (potOdds == null)
				throw new ArgumentNullException("potOdds");
			if (handOdds == null)
				throw new ArgumentNullException("handOdds");
			if (pairOdds == null)
				throw new ArgumentNullException("pairOdds");

			_potOdds = potOdds;
			_pokerHandOdds = handOdds;
			_pocketPairOdds = pairOdds;
		}
	}
}