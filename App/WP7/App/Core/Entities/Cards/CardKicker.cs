using System.Windows.Media.Imaging;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Core.Entities.Cards
{
    public class CardKicker : ICardKicker
    {
		private static ICardThemeManager _themeManager;

        public int Row
        {
            get
            {
                if (this.CardName == CardName.Ace)
                    return 3;
                if (this.CardName <= CardName.Five)
                    return 0;
                if (this.CardName <= CardName.Nine)
                    return 1;
                if (this.CardName <= CardName.King)
                    return 2;

                return -1;
            }
        }

        public int Column
        {
            get
            {
                if (this.CardName == CardName.Ace)
                    return 1;

                int col = (int)this.CardName % 4;

                return col;
            }
        }

        public int ColumnSpan
        {
            get
            {
                if (this.CardName == CardName.Ace)
                    return 2;
                return 1;
            }
        }

        public CardName CardName
        {
            get;
            set;
        }

        public Suit Suit { get; set; }

        public BitmapImage CardImage
        {
            get
            {
				return _themeManager.GetCardImage(this.CardName, this.Suit);
            }
        }

        public CardKicker(ICardThemeManager themeManager, CardName cardName)
        {
			_themeManager = themeManager;
            this.CardName = cardName;
        }

        public CardKicker(ICardThemeManager themeManager, CardName cardName, Suit suit)
        {
			_themeManager = themeManager;
            this.CardName = cardName;
            this.Suit = suit;
        }
    }
}