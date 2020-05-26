using System.Collections.Generic;

namespace TexasHoldemCalculator.Interfaces.ReplayEngine
{
	public interface IReplayEngine
	{
		string DisplayName
		{
			get;
		}

		IReplayEngineProvider Provider { get; }

		#region Interface Methods

		void SetEngineProvider(IReplayEngineProvider provider);

	    bool HasHandReplay();

		/// <summary>
		/// 
		/// Returns a list of hand history based on the stream provided by the replay
		/// engine provider.
		/// 
		/// </summary>
		/// <returns>A list of hand history from the associated stream</returns>
		IEnumerable<HandHistory.History> GetHandReplay(IReplayEngineProvider provider);

		#endregion //Interface Methods
	}
}