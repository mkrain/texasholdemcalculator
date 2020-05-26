namespace TexasHoldemCalculator.Interfaces.HandHistory
{
	public interface IHandReplayInfo
	{
		string GameIdTag
		{
			get;
			set;
		}

		string TournamentIdTag
		{
			get;
			set;
		}

		string TableIdTag
		{
			get;
			set;
		}

		string HoleCardsTag
		{
			get;
			set;
		}

		string FlopTag
		{
			get;
			set;
		}

		string TurnTag
		{
			get;
			set;
		}

		string RiverTag
		{
			get;
			set;
		}

		string ShowdownTag
		{
			get;
			set;
		}

		string SummaryTag
		{
			get;
			set;
		}

		string FinalBoardTag
		{
			get;
			set;
		}

		string GameHeaderTag
		{
			get;
			set;
		}

		string WonPotAmountTag
		{
			get;
			set;
		}

		string FinalPotAmountTag
		{
			get;
			set;
		}
	}
}