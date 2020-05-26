using System.Collections.Generic;
using TexasHoldemCalculator.Interfaces.HandHistory;
using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.ReplayEngine
{
	/// <summary>
	/// 
	/// By default this defines the GetReplayAction(string lineToCheck) method of
	/// IReplayEngine.  The lineToCheck input is compared against each IdTag property
	/// for a match and the one matching is returned as a ReplayAction.  The method
	/// has been made virtual if this default behavior is not sufficient.
	/// 
	/// </summary>
	public abstract class ReplayEngineBase : IReplayEngine, IReplayEngineInfo
	{
		protected ReplayEngineBase()
		{
		}

		#region Abstract Properties

		public abstract IHandReplayInfo HandReplayInformation
		{
			get;
			set;
		}

		#endregion //Abstract Properties

		#region Abstract Methods

		public abstract string DisplayName
		{
			get;
		}

	    public abstract bool HasHandReplay();

		public abstract IReplayEngineProvider Provider { get; }

		public abstract void SetEngineProvider(IReplayEngineProvider provider);

		/// <summary>
		/// 
		/// Returns a list of hand history based on the steam provided by the replay
		/// engine provider.
		/// 
		/// </summary>
		/// <returns>A list of hand history from the associated stream</returns>
        public abstract IEnumerable<History> GetHandReplay(IReplayEngineProvider provider);

		public abstract string GetGameId(string lineInfo);
		public abstract string GetTournamentId(string lineInfo);
		public abstract string GetTableId(string lineInfo);
		public abstract string GetGameDescription(string lineInfo);
		public abstract string GetDateTime(string lineInfo);
		public abstract void SetHoleCards(IHandHistory history, string lineInfo);
		public abstract void SetFlop(IHandHistory history, string lineInfo);
		public abstract void SetTurn(IHandHistory history, string lineInfo);
		public abstract void SetRiver(IHandHistory history, string lineInfo);
		public abstract string GetShowdown(string lineInfo);
		public abstract string GetSummary(string lineInfo);
		public abstract string GetFinalBoard(string lineInfo);
		public abstract string GetGameHeader(string lineInfo);
		public abstract string GetWonPotAmount(string userName, string lineInfo);
		public abstract string GetTotalPotAmount(string lineInfo);

		#endregion //Abstract Methods

		#region IReplayEngine Members

		/// <summary>
		/// 
		/// Parses the line to find a given action.  If the given
		/// action is found it is returned as ReplayAction.
		/// 
		/// </summary>
		/// <param name="lineToCheck"></param>
		/// <returns></returns>
		public virtual ReplayAction GetReplayAction(string lineToCheck)
		{
			if (string.IsNullOrEmpty(lineToCheck))
				return ReplayAction.None;

			if (!string.IsNullOrEmpty(this.HandReplayInformation.GameHeaderTag) && lineToCheck.Contains(this.HandReplayInformation.GameHeaderTag))
				return ReplayAction.GameHeader;

			if (!string.IsNullOrEmpty(this.HandReplayInformation.GameIdTag) && lineToCheck.Contains(this.HandReplayInformation.GameIdTag))
				return ReplayAction.GameId;

			if (!string.IsNullOrEmpty(this.HandReplayInformation.TournamentIdTag) && lineToCheck.Contains(this.HandReplayInformation.TournamentIdTag))
				return ReplayAction.TournamentId;

			if (!string.IsNullOrEmpty(this.HandReplayInformation.TableIdTag) && lineToCheck.Contains(this.HandReplayInformation.TableIdTag))
				return ReplayAction.TableId;

			if (!string.IsNullOrEmpty(this.HandReplayInformation.HoleCardsTag) && lineToCheck.Contains(this.HandReplayInformation.HoleCardsTag))
				return ReplayAction.HoleCards;

			if (!string.IsNullOrEmpty(this.HandReplayInformation.FlopTag) && lineToCheck.Contains(this.HandReplayInformation.FlopTag))
				return ReplayAction.Flop;

			if (!string.IsNullOrEmpty(this.HandReplayInformation.TurnTag) && lineToCheck.Contains(this.HandReplayInformation.TurnTag))
				return ReplayAction.Turn;

			if (!string.IsNullOrEmpty(this.HandReplayInformation.RiverTag) && lineToCheck.Contains(this.HandReplayInformation.RiverTag))
				return ReplayAction.River;

			if (!string.IsNullOrEmpty(this.HandReplayInformation.SummaryTag) && lineToCheck.Contains(this.HandReplayInformation.SummaryTag))
				return ReplayAction.Summary;

			if (!string.IsNullOrEmpty(this.HandReplayInformation.FinalBoardTag) && lineToCheck.Contains(this.HandReplayInformation.FinalBoardTag))
				return ReplayAction.FinalBoard;

			if (!string.IsNullOrEmpty(this.HandReplayInformation.WonPotAmountTag) && lineToCheck.Contains(this.HandReplayInformation.WonPotAmountTag))
				return ReplayAction.WonPotAmount;

			if (!string.IsNullOrEmpty(this.HandReplayInformation.FinalPotAmountTag) && lineToCheck.Contains(this.HandReplayInformation.FinalPotAmountTag))
				return ReplayAction.TotalPotAmount;

			return ReplayAction.None;
		}

		#endregion
	}
}