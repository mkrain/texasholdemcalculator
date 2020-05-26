using System.Collections.Generic;

namespace TexasHoldemCalculator.Interfaces.ReplayEngine
{
	public interface IHandHistoryReplayEngine : IReplayEngine
	{
        IEnumerable<HandHistory.History> HandHistory
        {
            get;
        }

	    HandHistory.History LatestHistory { get; }

        bool FlushHandHistory { get; set; }

	    int HandCount { get; }

	    /// <summary>
	    /// 
	    /// Writes the holdem hand.
	    /// 
	    /// </summary>
	    /// <param name="generated"> </param>
	    void WriteHandHistory(HandHistory.History generated);

        /// <summary>
        /// 
        /// Writes a collection of hands to a store.
        /// 
        /// </summary>
        /// <param name="handHistory"></param>
        void WriteHandHistory(IEnumerable<HandHistory.History> handHistory);

        /// <summary>
        /// 
        /// Delete all hand history
        /// 
        /// </summary>
        void DeleteHandHistory();

        /// <summary>
        /// 
        /// Deletes the specified hand history.
        /// 
        /// </summary>
        /// <param name="toDelete"></param>
        void DeleteHandHistory(HandHistory.History toDelete);
	}
}