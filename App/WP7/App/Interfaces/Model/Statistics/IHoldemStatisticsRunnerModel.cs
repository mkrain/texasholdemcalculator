using System;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Interfaces.Model.Statistics
{
    public interface IHoldemStatisticsRunnerModel
    {
        void GenerateRunerRuunerOdds(IHandRunnerRunnerOptions info);

        event EventHandler<HoldemRunnerRunnerEventArgs> HoldemRunnerRunnerGenerated;
    }
}