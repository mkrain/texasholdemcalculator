using System;

namespace TexasHoldemCalculator.Interfaces.Database
{
    public class HandHistoryDeletedEventArgs : EventArgs
    {
        private readonly HandHistory.History _history;

        public HandHistory.History History
        {
            get { return _history; }
        }

        public HandHistoryDeletedEventArgs(HandHistory.History history)
        {
            _history = history;
        }
    }
}