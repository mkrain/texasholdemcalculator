using System;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Interfaces.Model.Statistics
{
    public interface IHoldemStatisticsKickerModel
    {
        void GenerateAceAgainstBiggerKicker(IHandKickerOptions info);

        event EventHandler<HoldemAceAgainstBiggerKickerEventArgs> HoldemAceAgainstBiggerKickerGenerated;
    }
}
