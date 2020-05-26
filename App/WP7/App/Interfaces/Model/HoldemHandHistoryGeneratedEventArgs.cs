using System;
using System.Collections.Generic;
using TexasHoldemCalculator.Interfaces.HandHistory;
using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.Interfaces.Model
{
    public class HoldemHandHistoryGeneratedEventArgs : EventArgs
    {
		public IEnumerable<History> HandHistory
        {
            get;
            private set;
        }

        public HoldemHandHistoryGeneratedEventArgs(IEnumerable<HandHistory.History> handHistory)
        {
            this.HandHistory = handHistory;
        }
    }

    public class HoldemHandHistoryEngineGenerated : EventArgs
    {
        public IReplayEngine ReplayEngine
        {
            get;
            set;
        }

        public HoldemHandHistoryEngineGenerated(IReplayEngine engine)
        {
            this.ReplayEngine = engine;
        }
    }
}
