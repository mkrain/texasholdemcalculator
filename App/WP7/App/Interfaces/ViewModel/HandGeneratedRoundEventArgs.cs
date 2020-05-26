using System;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.ViewModel
{
    public class HandGeneratedRoundEventArgs : EventArgs
    {
        private readonly HoldemCard _round;

        public HoldemCard Round
        {
            get { return _round; }
        }

        public HandGeneratedRoundEventArgs(HoldemCard round)
        {
            _round = round;
        }
    }
}