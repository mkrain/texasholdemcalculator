using System.Collections.Generic;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.HandHistory
{
	public interface IHandHistory : IHandHistoryGameInfo
    {
		CardValue HoleCardOne { get; set; }
        CardValue HoleCardTwo { get; set; }
        CardValue FlopCardOne { get; set; }
        CardValue FlopCardTwo { get; set; }
        CardValue FlopCardThree { get; set; }
        CardValue TurnCard { get; set; }
        CardValue RiverCard { get; set; }

        IEnumerable<CardValue> Cards { get; }

        string HandValuation { get; }
        HandRank HandRank { get; }

        string DetermineBestPossibleHand();

        bool IsRoyalFlush();
        bool IsStraightFlush();
        bool IsFourOfAKind();
        bool IsFullHouse();
        bool IsFlush();
        bool IsStraight();
        bool IsThreeOfAKind();
        bool IsTwoPair();
        bool IsPair();

        IEnumerable<CardValue> GetBestHand();
        IEnumerable<CardValue> GetRoyalFlush();
        IEnumerable<CardValue> GetStraightFlush();
        IEnumerable<CardValue> GetFourOfAKind();
        IEnumerable<CardValue> GetFullHouse();
        IEnumerable<CardValue> GetFlush();
        IEnumerable<CardValue> GetStraight();
        IEnumerable<CardValue> GetThreeOfAKind();
        IEnumerable<CardValue> GetTwoPair();
        IEnumerable<CardValue> GetPair();
        IEnumerable<CardValue> GetHighCard();
    }
}