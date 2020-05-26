using System.Windows.Media.Imaging;

namespace TexasHoldemCalculator.Interfaces.Card
{
    public interface ICardKicker
    {
        int Row
        {
            get;
        }

        int Column
        {
            get;
        }

        int ColumnSpan
        {
            get;
        }

        CardName CardName
        {
            get;
            set;
        }

        BitmapImage CardImage
        {
            get;
        }
    }
}
