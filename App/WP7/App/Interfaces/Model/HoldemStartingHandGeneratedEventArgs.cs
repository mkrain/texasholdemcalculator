using System;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace TexasHoldemCalculator.Interfaces.Model
{
	public class HoldemStartingHandGeneratedEventArgs : EventArgs
	{
		public IStartingHand StartingHands
		{
			get;
			private set;
		}

		public HoldemStartingHandGeneratedEventArgs(IStartingHand startingHand)
		{
			this.StartingHands = startingHand;
		}
	}
}
