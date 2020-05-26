using System;

namespace TexasHoldemCalculator.Interfaces.ViewModel
{
    public class StartingHandsLoadedEventArgs : EventArgs
    {
        public bool HandsLoaded
        {
            get;
            private set;
        }

        public StartingHandsLoadedEventArgs(bool loaded)
        {
            this.HandsLoaded = loaded;
        }
    }
}
