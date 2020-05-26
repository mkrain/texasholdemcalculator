using System;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Interfaces.Model
{
    public class HoldemAceAgainstBiggerKickerEventArgs : EventArgs
    {
        public IHandKickerStats AgainstBiggerKicker
        {
            get;
            set;
        }

        public HoldemAceAgainstBiggerKickerEventArgs(IHandKickerStats kickerInfo)
        {
            this.AgainstBiggerKicker = kickerInfo;
        }
    }
}
