using System;
using System.Windows.Media;

namespace TexasHoldemCalculator.Interfaces.Statistics
{
	public interface IHandHistoryStats
	{
		int CurrentIndex
		{
			get;
			set;
		}

		string GameDescription
		{
			get;
			set;
		}

		DateTime GameDateAndTime
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// Returns the Tournament Id for this hand.
		/// 
		/// </summary>
		string TournamentId
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// Returns the Game Id for this hand.
		/// 
		/// </summary>
		string GameId
		{
			get;
			set;
		}

		bool WonHand
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// Returns a string representing the current hand out of all hands.
		/// 
		/// </summary>
		string HandIndex
		{
			get;
			set;
		}

		int GameIndex
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// Gets the amount won for this hand.
		/// 
		/// </summary>
		string WonAmountCurrent
		{
			get;
			set;
		}

		//Amount won [Game]
		/// <summary>
		/// 
		/// Computes the total won for the current game.
		/// 
		/// </summary>
		int AmountWonGame
		{
			get;
			set;
		}

		//Amount won [All Games]
		/// <summary>
		/// 
		/// Computes the total won for all hands.
		/// 
		/// </summary>
		int AmountWonAllGames
		{
			get;
			set;
		}

		//Average won per hand [Game]
		/// <summary>
		/// 
		/// Computes the total won for the current game.
		/// 
		/// </summary>
		double AverageWonPerHandGame
		{
			get;
			set;
		}

		//Average won per hand [All Games]
		/// <summary>
		/// 
		/// Computes the average win for all hands played.
		/// 
		/// </summary>
		double AverageWonPerHandAllGames
		{
			get;
			set;
		}

		//Average won for winning hands [Winning]
		/// <summary>
		/// 
		/// Computes the average win for winning hands only.
		/// 
		/// </summary>
		double AverageWonPerHandWinning
		{
			get;
			set;
		}

		//Hands won this game [Game]
		/// <summary>
		/// 
		/// Computes the total won for the current game.
		/// 
		/// </summary>
		int HandsWonGame
		{
			get;
			set;
		}

		//Hands won for all games [All Games]
		/// <summary>
		/// 
		/// Computes the total won for the current game.
		/// 
		/// </summary>
		int HandsWonAllGames
		{
			get;
			set;
		}

		//Average Hands won for all games [All Games]
		double AverageHandsWonAllGames
		{
			get;
			set;
		}

		int HandCount
		{
			get;
			set;
		}

		Color AmountWonGameLabelBackColor
		{
			get;
			set;
		}
	}
}