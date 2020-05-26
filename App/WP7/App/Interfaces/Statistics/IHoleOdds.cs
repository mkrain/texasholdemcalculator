using System.Windows.Media.Imaging;

namespace TexasHoldemCalculator.Interfaces.Statistics
{
    public interface IHoleOdds
    {
        string Description
        {
            get;
            set;
        }

        IHoleOddsDetail Details
        {
            get;
            set;
        }

        BitmapImage DisplayIcon { get; }
    }

    public interface  IHoleOddsDetail
    {
        string Percent
        {
            get;
            set;
        }

        string Odds
        {
            get;
            set;
        }
    }
}
