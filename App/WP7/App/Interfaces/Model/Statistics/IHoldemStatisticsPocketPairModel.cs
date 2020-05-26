using System;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Interfaces.Model.Statistics
{
    public interface IHoldemStatisticsPocketPairModel
    {
        void GenerateAgainstBiggerPair(IHandPocketPairOptions info);

        event EventHandler<HoldemAgainstBiggerPairEventArgs> HoldemAgainstBiggerPairGenerated;
    }
}