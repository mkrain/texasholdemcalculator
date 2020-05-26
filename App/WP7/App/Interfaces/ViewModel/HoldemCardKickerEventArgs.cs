using System;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.ViewModel
{
    public class HoldemCardKickerEventArgs : EventArgs
    {
        public ICardKicker Kicker
        {
            get;
            set;
        }

        public HoldemCardKickerEventArgs(ICardKicker kicker)
        {
            this.Kicker = kicker;
        }
    }
}
