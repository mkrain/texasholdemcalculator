using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.Statistics
{
    public interface IHandOptions
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

        int NumberOfOuts
        {
            get;
            set;
        }

        double MaxBet
        {
            get;
            set;
        }

        double PotSize
        {
            get;
            set;
        }

        PotOddsSelection OddsSelection
        {
            get;
            set;
        }

        HoldemCard HandState
        {
            get;
            set;
        }

        IHandOptions Clone();
    }

	public enum PotOddsSelection
	{
		River,
		Turn,
		Combined,
		PotOdds,
		PotOddsRatio
	};
}