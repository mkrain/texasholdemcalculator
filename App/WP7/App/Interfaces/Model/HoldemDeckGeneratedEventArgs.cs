using System;

namespace TexasHoldemCalculator.Interfaces.Model
{
	public class HoldemDeckGeneratedEventArgs : EventArgs
	{
		public HandHistory.History Deck
		{
			get; private set; 
		}

		public HoldemDeckGeneratedEventArgs(HandHistory.History deck)
		{
			this.Deck = deck;
		}
	}
}
