using System;

namespace TexasHoldemCalculator.Interfaces.Database
{
    public class HandHistoryAddedEventArgs : EventArgs
    {
        private HandHistory.History _history;

        public HandHistory.History History
        {
            get { return _history; }
        }

        public HandHistoryAddedEventArgs(HandHistory.History history)
        {
            _history = history;
        }
    }
}