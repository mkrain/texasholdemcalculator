namespace TexasHoldemCalculator.Interfaces.Statistics
{
	public interface IPotOdds
	{
		int Precision
		{
			get;
			set;
		}

		double RiverProbability(IHandOptions options);
		double TurnProbability(IHandOptions options);
		double TurnOrRiverProbability(IHandOptions options);
        double RunnerRunnerProbability(IHandRunnerRunnerOptions info);
        double RunnerRunnerAsPercentage(IHandRunnerRunnerOptions info);
        double RunnerRunnerAsRatio(IHandRunnerRunnerOptions info);
		double CalculatePokerOddsAsRatio(IHandOptions info);
		double CalculatePokerOddsAsPercent(IHandOptions info);
		double CalculatePokerOddsAsProbability(IHandOptions info);

		double CalculatePotOddsAsRatio(IHandOptions options);
		double CalculatePotOddsAsPercentage(IHandOptions options);
		double CalculatePotOddsPercent(IHandOptions options);
		double CalculateMaxCallableBet(IHandOptions options);

		bool IsPositiveExpectation(IHandOptions options);
	}
}