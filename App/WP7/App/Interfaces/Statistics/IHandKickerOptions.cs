using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.Statistics
{
    public interface IHandKickerOptions
    {
        CardName CardValue
        {
            get;
            set;
        }

        int NumberOfPlayers
        {
            get;
            set;
        }

        int Precision
        {
            get;
            set;
        }
    }
}