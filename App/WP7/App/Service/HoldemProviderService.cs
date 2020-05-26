using System;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Service;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace TexasHoldemCalculator.Service
{
	public class HoldemProviderService : IProviderService
	{
		#region Implementation of IProviderService

		private readonly ICardThemeManager _cardThemeManager;
		private readonly IStartingHandsManager _startingHandsManager;

		public ICardThemeManager CardThemeManager { get { return _cardThemeManager; } }
		public IStartingHandsManager StartingHandsManager { get { return _startingHandsManager; } }

		#endregion

		public HoldemProviderService(ICardThemeManager cardThemeManager, IStartingHandsManager startingHandsManager)
		{
			if (cardThemeManager == null)
				throw new ArgumentNullException("cardThemeManager");
			if (startingHandsManager == null)
				throw new ArgumentNullException("startingHandsManager");

			_cardThemeManager = cardThemeManager;
			_startingHandsManager = startingHandsManager;
		}
	}
}