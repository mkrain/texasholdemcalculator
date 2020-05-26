using System;

namespace TexasHoldemCalculator.Interfaces.Model
{
    public class HoldemHandHistoryLoadedEventArgs : EventArgs
    {
        public bool HistoryLoaded
        {
            get;
            private set;
        }

        public HoldemHandHistoryLoadedEventArgs( bool generated)
        {
            this.HistoryLoaded = generated;
        }
    }
}
