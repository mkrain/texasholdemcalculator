using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.HandHistory
{
	public static class HandHistoryExtension
	{
		public static CardValue FindCard(this IHandHistory cardHand, HoldemCard card)
		{
			switch (card)
			{
				case HoldemCard.Hole1:
					return cardHand.HoleCardOne;
				case HoldemCard.Hole2:
					return cardHand.HoleCardTwo;
				case HoldemCard.Flop1:
					return cardHand.FlopCardOne;
				case HoldemCard.Flop2:
					return cardHand.FlopCardTwo;
				case HoldemCard.Flop3:
					return cardHand.FlopCardThree;
				case HoldemCard.Turn:
					return cardHand.TurnCard;
				case HoldemCard.River:
					return cardHand.RiverCard;
				default:
					return null;
			}
		}
	}
}