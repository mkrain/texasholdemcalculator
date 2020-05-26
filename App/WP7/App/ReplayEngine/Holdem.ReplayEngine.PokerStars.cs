using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Common.Core.Configuration;
using TexasHoldemCalculator.Core.Entities.Cards;
using TexasHoldemCalculator.Core.Entities.Collections;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Generator;
using TexasHoldemCalculator.Interfaces.HandHistory;
using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.ReplayEngine
{
	public class PokerStarsReplayEngine : ReplayEngineBase
	{
		private readonly ICardGenerator _cardGenerator;
		private IReplayEngineProvider _provider;
	    private readonly IPhoneConfiguration _configuration;

		#region PokerStars Properties

		public override string DisplayName
		{
			get
			{
				return "Poker Stars";
			}
		}

		public override IReplayEngineProvider Provider { get { return _provider; } }

		public override IHandReplayInfo HandReplayInformation
		{
			get;
			set;
		}

		#endregion PokerStars Properties

		#region PokerStars Constructor

		/// <summary>
		/// 
		/// This engine needs a way to determine how to generate an CardValue object.
		/// Injection is used here in order to get access to the card generator.
		/// 
		/// </summary>
		public PokerStarsReplayEngine(ICardGenerator cardGenerator, IPhoneConfiguration configuration)
		{
			_cardGenerator = cardGenerator;
		    _configuration = configuration;

			this.UpdateHandTagInfo();
		}

		#endregion PokerStars Constructor

		#region PokerStars Methods

		/// <summary>
		/// 
		/// Set the tags for the game information.
		/// 
		/// </summary>
		private void UpdateHandTagInfo()
		{
			this.HandReplayInformation =
				new HandReplayInfo
				{
					TournamentIdTag = "Tournament #",
					GameIdTag = "Game #",
					TableIdTag = "Table '",
					HoleCardsTag = "Dealt to",
					FlopTag = "*** FLOP",
					TurnTag = "*** TURN",
					RiverTag = "*** RIVER",
					ShowdownTag = "*** SHOW DOWN",
					SummaryTag = "*** SUM",
					FinalBoardTag = "Board [",
					GameHeaderTag = "PokerStars Game #",
					WonPotAmountTag = " collected ",
					FinalPotAmountTag = "Total pot "
				};
		}

		public override void SetEngineProvider(IReplayEngineProvider provider)
		{
			_provider = provider;
		}

		/// <summary>
		/// 
		/// Returns a list of hand history based on the steam provided by the replay
		/// engine provider.
		/// 
		/// </summary>
		/// <returns>A list of hand history from the associated stream</returns>
        public override IEnumerable<History> GetHandReplay(IReplayEngineProvider provider)
		{
			if(provider == null)
			{
			    throw new ArgumentNullException("provider");
			}

			var handsToReplay = new HandHistoryWriterCollection();
            var handList = new List<History>();

			using (TextReader reader = new StreamReader(provider.ReadableStream()))
			{
				var userName = _configuration.Get<string>(ConfigKey.View.Options.UserName);

				this.FindHandHistroy(reader, handList, userName);
			}

			handsToReplay.AddRange(handList);

			return handsToReplay;
		}

        public override bool HasHandReplay()
        {
            throw new NotImplementedException();
        }

        private void FindHandHistroy(TextReader reader, IList<History> handList, string userName)
		{
			string line;
			var summaryHit = false;

			while ((line = reader.ReadLine()) != null)
			{
				var history = new History();

				//Get each game hand.  This includes hole cards, flop/turn/river
				while (line != null)
				{
					//Only exit when the summary has been hit, otherwise,
					//not all information will have been extracted.
					if (summaryHit)
					{
						handList.Add(history);
						summaryHit = false;
						break;
					}

					var action = this.GetReplayAction(line);

					//Which action have we seen?
					switch (action)
					{
						case ReplayAction.GameHeader:
							history.TableId = this.GetGameId(line);
							history.TournamentId = this.GetTournamentId(line);
                            history.Date = ConvertToDateTime(GetDateTime(line));
							history.GameDescription = this.GetGameDescription(line);
							break;
						case ReplayAction.TableId:
							history.TableId = this.GetTableId(line);
							break;
						case ReplayAction.HoleCards:
							this.SetHoleCards(history, line);
							break;
						case ReplayAction.Flop:
							this.SetFlop(history, line);
							break;
						case ReplayAction.Turn:
							this.SetTurn(history, line);
							break;
						case ReplayAction.River:
							this.SetRiver(history, line);
							break;
						case ReplayAction.WonPotAmount:
							history.WonPotAmount = this.GetWonPotAmount(userName, line);
							break;
						case ReplayAction.TotalPotAmount:
							history.TotalPotAmount = this.GetTotalPotAmount(line);
							summaryHit = true;
							break;
						case ReplayAction.Summary:
							summaryHit = true;
							break;
					}

					//Next line of file
					line = reader.ReadLine();
				}
			}
		}

		/// <summary>
		/// 
		/// Returns the string that contains the game id.
		/// 
		/// A Sample header looks like this:
		///		PokerStars Game #20882538122: Tournament #111797084, 10000+325 Hold'em No Limit - Level I (10/20) - 2008/10/02 22:47:07 ET
		/// 
		/// 20882538122 is returned as a string or empty if there is not one.
		///
		/// </summary>
		/// <param name="lineInfo"></param>
		/// <returns>GameId for the current hand</returns>
		public override string GetGameId(string lineInfo)
		{
			return GetId(lineInfo, this.HandReplayInformation.GameIdTag, ":");
		}

		/// <summary>
		/// 
		/// Returns the string that contains the tournament id.
		/// 
		/// A Sample header looks like this:
		///		PokerStars Game #20882538122: Tournament #111797084, 10000+325 Hold'em No Limit - Level I (10/20) - 2008/10/02 22:47:07 ET
		/// 
		/// 111797084 is returned as a string or empty if there is not one.
		///
		/// </summary>
		/// <param name="lineInfo"></param>
		/// <returns>TournamentId for the current hand</returns>
		public override string GetTournamentId(string lineInfo)
		{
			return GetId(lineInfo, this.HandReplayInformation.TournamentIdTag, ",");
		}

		/// <summary>
		/// 
		/// Returns the string that contains the tournament id.
		/// 
		/// A Sample table id line looks like this:
		///		Table '111797084 1' 9-max Seat #1 is the button
		/// 
		/// 111797084 is returned as a string or empty if there is not one.
		///
		/// </summary>
		/// <param name="lineInfo"></param>
		/// <returns>TableId for the current hand</returns>
		public override string GetTableId(string lineInfo)
		{
			return GetId(lineInfo, this.HandReplayInformation.TableIdTag, "'");
		}

		private static string GetId(string lineInfo, string tag, string token)
		{
			if (string.IsNullOrEmpty(lineInfo)
			|| string.IsNullOrEmpty(tag)
			|| string.IsNullOrEmpty(token))
				return string.Empty;

			var index = lineInfo.IndexOf(tag, StringComparison.OrdinalIgnoreCase) + tag.Length;

			if (index == -1 || index < tag.Length)
				return string.Empty;

			lineInfo = lineInfo.Substring(index);

			index = lineInfo.IndexOf(token, StringComparison.OrdinalIgnoreCase);

			if (index < 0 || index > lineInfo.Length)
				return string.Empty;

			var tokenStr = lineInfo.Substring(0, index);

			return tokenStr.Substring(0, tokenStr.Length);
		}

		/// <summary>
		/// 
		/// Tournament games have a tournament header whereas as ring games don't.
		/// Check for both cases here:
		/// 
		/// Ring Game:
		///		PokerStars Game #21125116748:  Hold'em No Limit (5/10) - 2008/10/12 1:41:47 ET
		///		
		/// Tournament Game:
		///		PokerStars Game #21149350545: Tournament #113842050, 500+30 Hold'em No Limit - Level I (10/20) - 2008/10/12 22:14:50 ET
		/// </summary>
		/// <param name="lineInfo"></param>
		/// <returns>A string representing the game title description</returns>
		public override string GetGameDescription(string lineInfo)
		{
			if (string.IsNullOrEmpty(lineInfo))
				return string.Empty;

			if (!string.IsNullOrEmpty(this.GetTournamentId(lineInfo)))
				return this.GetTournamentGameDescription(lineInfo);
			if (!string.IsNullOrEmpty(this.GetGameId(lineInfo)))
				return this.GetTableGameDescription(lineInfo);

			return string.Empty;
		}

		private string GetTournamentGameDescription(string lineInfo)
		{
			var index = lineInfo.IndexOf(this.HandReplayInformation.TournamentIdTag, StringComparison.OrdinalIgnoreCase) + this.HandReplayInformation.TournamentIdTag.Length;

			var token = lineInfo.Substring(index).Trim();

			index = token.IndexOf(",", StringComparison.OrdinalIgnoreCase);

			token = token.Substring(index + 1).Trim();

			index = token.LastIndexOf("-", StringComparison.OrdinalIgnoreCase);

			if (index < 0)
				return string.Empty;

			token = token.Substring(0, index).Trim();

			return token;
		}

		private string GetTableGameDescription(string lineInfo)
		{
			int index = lineInfo.IndexOf(":", StringComparison.OrdinalIgnoreCase) + 1;

			if (index < this.HandReplayInformation.GameIdTag.Length)
				return string.Empty;

			var token = lineInfo.Substring(index).Trim();

			index = token.LastIndexOf("-", StringComparison.OrdinalIgnoreCase);

			if (index < 0 || index > token.Length - 1)
				return string.Empty;

			token = token.Substring(0, index).Trim();

			return token;
		}

		/// <summary>
		/// 
		/// Returns the date portion of the game title using the game header:
		/// 
		/// Ring Game:
		///		PokerStars Game #21125116748:  Hold'em No Limit (5/10) - 2008/10/12 1:41:47 ET
		///		
		/// Tournament Game:
		///		PokerStars Game #21149350545: Tournament #113842050, 500+30 Hold'em No Limit - Level I (10/20) - 2008/10/12 22:14:50 ET
		/// 
		/// </summary>
		/// <param name="lineInfo"></param>
		/// <returns>String representing the Date and Time with Zone.</returns>
		public override string GetDateTime(string lineInfo)
		{
			if (string.IsNullOrEmpty(lineInfo))
				return string.Empty;

			var index = lineInfo.LastIndexOf("-", StringComparison.OrdinalIgnoreCase);

			if (index == -1)
				return string.Empty;

			var datetime = lineInfo.Substring(index + 1).Trim();

			return datetime;
		}

		private static DateTime ConvertToDateTime(string timeToConvert)
		{
			DateTime outTime;

			DateTime.TryParse(timeToConvert, out outTime);

			return outTime;
		}

		/// <summary>
		/// 
		/// Takes the lineInfo string and determines which hole cards the user is playing.
		/// 
		/// </summary>
		/// <param name="history">IHandHistoryDictionary containing the added hole cards.</param>
		/// <param name="lineInfo"></param>
		public override void SetHoleCards(IHandHistory history, string lineInfo)
		{
			if (string.IsNullOrEmpty(lineInfo) || history == null)
				return;

			var cards = lineInfo.Substring(lineInfo.IndexOf("[", StringComparison.OrdinalIgnoreCase));

			var cardInfo = cards.Split(new[] { ' ' });

			if (cardInfo.Length < 2)
				return;

			var cardOne = cardInfo[0].Replace('[', ' ').Trim();
			var cardTwo = cardInfo[1].Replace(']', ' ').Trim();

			if (string.IsNullOrEmpty(cardOne) || string.IsNullOrEmpty(cardTwo))
				return;

			history.HoleCardOne = _cardGenerator.GetCard(cardOne[0].ToString(), cardOne[1].ToString(), HoldemCard.Hole1);
			history.HoleCardTwo = _cardGenerator.GetCard(cardTwo[0].ToString(), cardTwo[1].ToString(), HoldemCard.Hole2);
		}

		/// <summary>
		/// 
		/// Takes the lineInfo string and determines flop for this game.
		/// 
		/// </summary>
		/// <param name="history">IHandHistoryDictionary containing the added flop cards.</param>
		/// <param name="lineInfo"></param>
		public override void SetFlop(IHandHistory history, string lineInfo)
		{
			if (string.IsNullOrEmpty(lineInfo) || history == null)
				return;

			var cards =
				lineInfo.Substring(lineInfo.IndexOf("[", StringComparison.OrdinalIgnoreCase) + 1).Replace(']', ' ').Trim();

			var cardInfo = cards.Split(new[] { ' ' });

			if (cardInfo.Length < 3)
				return;

			var cardOne = cardInfo[0];
			var cardTwo = cardInfo[1];
			var cardThree = cardInfo[2];

			if (string.IsNullOrEmpty(cardOne) || string.IsNullOrEmpty(cardTwo) || string.IsNullOrEmpty(cardThree))
				return;

			history.FlopCardOne = _cardGenerator.GetCard(cardOne[0].ToString(), cardOne[1].ToString(), HoldemCard.Flop1);
			history.FlopCardTwo = _cardGenerator.GetCard(cardTwo[0].ToString(), cardTwo[1].ToString(), HoldemCard.Flop2);
			history.FlopCardThree = _cardGenerator.GetCard(cardThree[0].ToString(), cardThree[1].ToString(), HoldemCard.Flop3);
		}

		/// <summary>
		/// 
		/// Takes the lineInfo string and determines turn for this game.
		/// 
		/// </summary>
		/// <param name="history">IHandHistoryDictionary containing the added turn card.</param>
		/// <param name="lineInfo"></param>
		public override void SetTurn(IHandHistory history, string lineInfo)
		{
			this.SetCard(history, lineInfo, HoldemCard.Turn);
		}

		/// <summary>
		/// 
		/// Takes the lineInfo string and determines river for this game.
		/// 
		/// </summary>
		/// <param name="history">IHandHistoryDictionary containing the added river card.</param>
		/// <param name="lineInfo"></param>
		public override void SetRiver(IHandHistory history, string lineInfo)
		{
			this.SetCard(history, lineInfo, HoldemCard.River);
		}

		private void SetCard(IHandHistory history, string lineInfo, HoldemCard turnOrRiver)
		{
			if (string.IsNullOrEmpty(lineInfo) || history == null)
				return;

			var index = lineInfo.LastIndexOf("[", StringComparison.OrdinalIgnoreCase);

			if (index == -1)
				return;

			var cards = lineInfo.Substring(index + 1);

			var cardInfo = cards.Split(new[] { ' ' });

			var cardOne = cardInfo[0].Replace(']', ' ').Trim();

			if (string.IsNullOrEmpty(cardOne) || cardOne.Length < 2)
				return;

			if (turnOrRiver == HoldemCard.Turn)
				history.TurnCard = _cardGenerator.GetCard(cardOne[0].ToString(), cardOne[1].ToString(), HoldemCard.Turn);
			if (turnOrRiver == HoldemCard.River)
				history.RiverCard = _cardGenerator.GetCard(cardOne[0].ToString(), cardOne[1].ToString(), HoldemCard.River);
		}

		/// <summary>
		/// 
		/// Returns the showdown information for this hand.
		/// 
		/// </summary>
		/// <param name="lineInfo"></param>
		/// <returns></returns>
		public override string GetShowdown(string lineInfo)
		{
			return string.Empty;
		}

		/// <summary>
		/// 
		/// Returns the game summary for this hand.
		/// 
		/// </summary>
		/// <param name="lineInfo"></param>
		/// <returns></returns>
		public override string GetSummary(string lineInfo)
		{
			return string.Empty;
		}

		/// <summary>
		/// 
		/// For now this has no use as the final board can be found by
		/// finding the flop/turn/river if they have been dealt
		/// 
		/// </summary>
		/// <param name="lineInfo"></param>
		/// <returns></returns>
		public override string GetFinalBoard(string lineInfo)
		{
			return string.Empty;
		}

		/// <summary>
		/// 
		/// Returns the game header for this hand.
		/// 
		/// </summary>
		/// <param name="lineInfo"></param>
		/// <returns></returns>
		public override string GetGameHeader(string lineInfo)
		{
			return string.Empty;
		}

		/// <summary>
		/// 
		/// Returns the won pot amount for the current user and hand.
		/// 
		/// </summary>
		/// <param name="userName">Current user.</param>
		/// <param name="lineInfo"></param>
		/// <returns></returns>
		public override string GetWonPotAmount(string userName, string lineInfo)
		{
			if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(lineInfo))
				return string.Empty;

			var line = lineInfo.ToUpper(CultureInfo.InvariantCulture);

			if (!line.Contains(userName.ToUpper(CultureInfo.InvariantCulture)))
				return string.Empty;

			var index = lineInfo.IndexOf(
				this.HandReplayInformation.WonPotAmountTag, StringComparison.OrdinalIgnoreCase)
						+ this.HandReplayInformation.WonPotAmountTag.Length;

			if (index < this.HandReplayInformation.WonPotAmountTag.Length)
				return string.Empty;

			line = lineInfo.Substring(index);

			if (line.Length < 1)
				return "0";

			return line.Split(new[] { ' ' })[0];
		}

		/// <summary>
		/// 
		/// Returns the total pot amount for this hand.
		/// 
		/// </summary>
		/// <param name="lineInfo"></param>
		/// <returns></returns>
		public override string GetTotalPotAmount(string lineInfo)
		{
			if (string.IsNullOrEmpty(lineInfo))
				return string.Empty;

			var index =
				lineInfo.IndexOf(this.HandReplayInformation.FinalPotAmountTag, StringComparison.OrdinalIgnoreCase)
				+ this.HandReplayInformation.FinalPotAmountTag.Length;

			if (index < this.HandReplayInformation.FinalPotAmountTag.Length)
				return string.Empty;

			lineInfo = lineInfo.Substring(index).Trim();

			if (string.IsNullOrEmpty(lineInfo))
				return string.Empty;

			var amount = lineInfo.Split(new[] { '|' })[0];

			if (amount.Length < 1)
				return string.Empty;

			return amount.Trim();
		}

		#endregion PokerStars Methods
	}
}