using System;
using System.Windows.Media;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Core.Entities.Statistics
{
	public sealed class HandHistoryStats : IHandHistoryStats
	{
		#region IHandHistoryStats Members

		public int CurrentIndex
		{
			get;
			set;
		}

		public string GameDescription
		{
			get;
			set;
		}

		public DateTime GameDateAndTime
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// Returns the Tournament Id for this hand.
		/// 
		/// </summary>
		public string TournamentId
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// Returns the Game Id for this hand.
		/// 
		/// </summary>
		public string GameId
		{
			get;
			set;
		}

		public bool WonHand
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// Returns a string representing the current hand out of all hands.
		/// 
		/// </summary>
		public string HandIndex
		{
			get;
			set;
		}

		public int GameIndex
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// Gets the amount won for this hand.
		/// 
		/// </summary>
		public string WonAmountCurrent
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
		public int AmountWonGame
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
		public int AmountWonAllGames
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
		public double AverageWonPerHandGame
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
		public double AverageWonPerHandAllGames
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
		public double AverageWonPerHandWinning
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
		public int HandsWonGame
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
		public int HandsWonAllGames
		{
			get;
			set;
		}

		//Average Hands won for all games [All Games]
		public double AverageHandsWonAllGames
		{
			get;
			set;
		}

		public int HandCount
		{
			get;
			set;
		}

		public Color AmountWonGameLabelBackColor
		{
			get;
			set;
		}

		#endregion
	}
}