
namespace TexasHoldemCalculator.Interfaces.Statistics
{
	public interface IHandKickerStats
	{
		double Probability
		{
			get;
			set;
		}

		double Ratio
		{
			get;
			set;
		}

		double Percentage
		{
			get;
			set;
		}

		string ProbabilityText
		{
			get;
		}

		string RatioText
		{
			get;
		}

		string PercentageText
		{
			get;
		}
	}
}