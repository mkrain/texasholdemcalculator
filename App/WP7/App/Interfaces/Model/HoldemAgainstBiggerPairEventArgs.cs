using System;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Interfaces.Model
{
    public class HoldemAgainstBiggerPairEventArgs : EventArgs
    {
        public IHandPocketPairStats AgainstBiggerPair
        {
            get;
            set;
        }

        public HoldemAgainstBiggerPairEventArgs(IHandPocketPairStats pairInfo)
        {
            this.AgainstBiggerPair = pairInfo;
        }
    }
}
