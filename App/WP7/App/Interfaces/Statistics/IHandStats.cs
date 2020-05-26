
namespace TexasHoldemCalculator.Interfaces.Statistics
{
	public interface IHandStats
	{
		bool Expectation
		{
			get;
			set;
		}

		double PotOdds
		{
			get;
			set;
		}

		double HandOdds
		{
			get;
			set;
		}

		double MaxBet
		{
			get;
			set;
		}

		double MakeHandPercent
		{
			get;
			set;
		}

		string PotOddsText
		{
			get;
		}

		string HandOddsText
		{
			get;
		}

		string MakeHandText
		{
			get;
		}

		string MaxBetText
		{
			get;
		}
	}
}