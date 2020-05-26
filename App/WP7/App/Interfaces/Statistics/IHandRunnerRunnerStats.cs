
namespace TexasHoldemCalculator.Interfaces.Statistics
{
    public interface IHandRunnerRunnerStats
    {
        double RunnerRunnerPercent
        {
            get;
            set;
        }

        double RunnerRunnerRatio
        {
            get;
            set;
        }

        string RunnerRunnerPercentText
        {
            get;
        }

        string RunnerRunnerRatioText
        {
            get;
        }
    }
}
