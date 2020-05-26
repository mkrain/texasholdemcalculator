using System.ComponentModel;

namespace TexasHoldemCalculator.Interfaces.Card
{
    //public interface ICardValue : IPersistable, IXmlSerializable
    //{
    //    long GameId { get; set; }

    //    Suit Suit { get; set; }

    //    CardName Parent { get; set; }

    //    CardName Name { get; set; }

    //    HoldemCard HoldemCard { get; set; }

    //    Color Highlight { get; set; }

    //    int Strength { get; set; }

    //    bool IsSuited { get; set; }

    //    bool IsVisible { get; set; }

    //    Visibility Visibility { get; set; }
    //}

    public enum HandRank : int
    {
        Undefined = 0,
        [Description("High Card")]
        HighCard,
        [Description("Pair")]
        Pair,
        [Description("Two Pair")]
        TwoPair,
        [Description("Three of A Kind")]
        ThreeOfAKind,
        [Description("Straight")]
        Straight,
        [Description("Flush")]
        Flush,
        [Description("Full House")]
        FullHouse,
        [Description("Four of A Kind")]
        FourOfAKind,
        [Description("Straight Flush")]
        StraightFlush,
        [Description("Royal Flush")]
        RoyalFlush
    }

	public enum Suit : int
	{
		Club = 0,
		Diamond = 1,
		Heart = 2,
		Spade = 3
	};

	public enum CardName : int
	{
		Two = 0,
		Three = 1,
		Four = 2,
		Five = 3,
		Six = 4,
		Seven = 5,
		Eight = 6,
		Nine = 7,
		Ten = 8,
		Jack = 9,
		Queen = 10,
		King = 11,
		Ace = 12
	};
}