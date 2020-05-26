using System;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Generator;

namespace TexasHoldemCalculator.Core.Generator
{
	public class CardGenerator : ICardGenerator
	{
		private readonly INumberGenerator _numberGenerator;

		public CardGenerator(INumberGenerator numberGenerator)
		{
			if( numberGenerator == null )
				throw  new ArgumentNullException("numberGenerator");

			_numberGenerator = numberGenerator;
		}

		#region ICardGenerator Members

		/// <summary>
		/// 
		/// Returns and CardValue which represents a card.
		/// 
		/// Ranges for card values (colors are staggered):
		/// 
		/// Club:    [1,13]
		/// Diamond: [14,26]
		/// Spade:   [27,39] 
		/// Heart:   [40,52]
		/// 
		/// </summary>
		/// <param name="cardId"></param>
		/// <returns></returns>
		public CardValue GetCard(int cardId)
		{
            if (cardId == 0)
                throw new ArgumentException("CardId must be non zero");

		    Suit suit;
		    CardName name;

            if (cardId <= 13)
                suit = Suit.Club;
            else if (cardId <= 26)
                suit = Suit.Diamond;
            else if (cardId <= 39)
                suit = Suit.Spade;
            else
                suit = Suit.Heart;

            if (cardId % 13 == 0)
                name = CardName.Ace;
            else
                name = (CardName)( ( cardId - 1 ) % 13 );

            return new CardValue(suit, name);
		}

		/// <summary>
		/// 
		/// Returns and ICardValue based on the card type and it's suit.
		/// 
		/// </summary>
		/// <param name="card">Value 2 - 9, Ten, Jack, Queen, Kind, Ace.</param>
		/// <param name="suit">Suit: (C)lub, (D)iamond, (H)eart, (S)pade.</param>
		/// <returns></returns>
		public virtual CardValue GetCard(string card, string suit)
		{
			if( string.IsNullOrEmpty(card) )
				throw new ArgumentNullException("card");
            if (string.IsNullOrEmpty(suit))
                throw new ArgumentNullException("suit");

			var value = new CardValue();

			var cardValue = Converter.Converter.StringToInt(card.Trim()) - 2;
			CardName cardName;

			if( Converter.Converter.IsNumber(card.Trim()) )
				cardName = (CardName)cardValue;
				//This is not a number, i.e. not 2-9
			else
			{
				card = card.Substring(0, 1);

				switch( card )
				{
					case "T":
					case "t":
						cardName = CardName.Ten;
						break;
					case "J":
					case "j":
						cardName = CardName.Jack;
						break;
					case "Q":
					case "q":
						cardName = CardName.Queen;
						break;
					case "K":
					case "k":
						cardName = CardName.King;
						break;
						//case "A":
						//case "a":
					default:
						cardName = CardName.Ace;
						break;
				}
			}

			value.Suit = this.GenerateSuit(suit);
			value.Name = cardName;

			return value;
		}

        public virtual CardValue GetCard(string card, string suit, HoldemCard holdemCard)
        {
            var generatedCard = this.GetCard(card, suit);

            generatedCard.HoldemCard = holdemCard;

            return generatedCard;
        }

		/// <summary>
		/// 
		/// Generates a random suit.
		/// 
		/// </summary>
		/// <returns></returns>
		public Suit GenerateSuit()
		{
			return (Suit)_numberGenerator.Next((int)Suit.Club, ((int)Suit.Spade) + 1);
		}

		/// <summary>
		/// 
		/// Generates a random suit based on the string passed in.
		/// 
		/// </summary>
        /// <param name="suit">Suit: (C)lub, (D)iamond, (H)eart, (S)pade.</param>
		/// <returns></returns>
		public Suit GenerateSuit(string suit)
		{
			if( string.IsNullOrEmpty(suit) )
				throw new ArgumentNullException("suit");
			if( suit.Trim().Length < 1 )
				throw new ArgumentException("suit.Trim().Length < 1", "suit");

			var suitPrefix = suit.Trim().Substring(0, 1);

			switch( suitPrefix )
			{
				case "C":
				case "c":
					return Suit.Club;
				case "D":
				case "d":
					return Suit.Diamond;
				case "H":
				case "h":
					return Suit.Heart;
					//case "S":
					//case "s":
				default:
					return Suit.Spade;
			}
		}

		#endregion
	}
}