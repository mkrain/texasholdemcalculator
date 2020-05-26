using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Common.Core.Configuration;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.HandHistory;
using TexasHoldemCalculator.Interfaces.Messages;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.ViewModel
{
    public class HoldemCalculatorViewModel : HoldemViewModelBase
    {
        #region Instance Variables

        private readonly IHoldemCalculatorModel _model;
		private readonly IHandHistoryReplayEngine _handHistoryReplayEngine;
        private static readonly object _lock = new object();

        private History _currentHistory;
        private History _bestHandHistory;

        #endregion //Instance Variables

        #region Properties

        public RelayCommand GenerateHandCommand
        {
            get;
            private set;
        }

        public RelayCommand ShowFlopCommand
        {
            get;
            private set;
        }

        public RelayCommand ShowTurnCommand
        {
            get;
            private set;
        }

        public RelayCommand ShowRiverCommand
        {
            get;
            private set;
        }

        public RelayCommand<CardContext> CardCommand { get; private set; } 

        public bool IsHandRankVisible
        {
            get
            {
                return Configuration.Cast<bool>(ConfigKey.View.Calc.HandRank);
            }
            set
            {
                if( value )
                {
                    _currentHistory = Current;

                    Current = _bestHandHistory;
                }
                else
                {
                    Current = _currentHistory;
                }

                Configuration[ConfigKey.View.Calc.HandRank] = value;

                this.UpdateCardImages();
                this.UpdateHandRank();
            }
        }

        public Visibility HandRankVisibility
        {
            get { return IsHandRankVisible ? Visibility.Visible : Visibility.Collapsed; }
        }

        public string HandValuation
        {
            get { return _bestHandHistory == null || !IsHandRankVisible ? string.Empty : _bestHandHistory.HandValuation; }
        }

        #endregion //Properties

        #region Constructors

        public HoldemCalculatorViewModel(
			ICardThemeManager themeManager,
			IPhoneConfiguration configuration,
			IHandHistoryReplayEngine handHistoryReplayEngine,
			IHoldemCalculatorModel model) : base(themeManager, configuration)
        {
            _model = model;
			_handHistoryReplayEngine = handHistoryReplayEngine;

            base.CurrentState = HoldemCard.Flop2;

            //Commands
            this.GenerateHandCommand = new RelayCommand(GenerateDeck);
            this.ShowFlopCommand = new RelayCommand(this.ShowFlop);
            this.ShowTurnCommand = new RelayCommand(this.ShowTurn);
            this.ShowRiverCommand = new RelayCommand(this.ShowRiver);
            this.CardCommand = new RelayCommand<CardContext>(this.CardHandler);

            this.IsHandRankVisible = Configuration.Cast<bool>(ConfigKey.View.Calc.HandRank);

            Messenger.Default.Register<DeckGeneratedMessage>(this, m => HoldemDeckGenerated(m.Data));

            this.InitializeHand();
        }

        #endregion //Constructors

        #region IHoldemCalculatorViewModel Implementation

        public void GenerateDeck()
        {
            DispatcherHelper.RunAsync(() => _model.GenerateHoldemDeck(this.NumberOfPlayers));
        }

        public void ShowFlop()
        {
            this.UpdateViewState(
                this.CurrentState == HoldemCard.Flop3 ? HoldemCard.Flop1 : HoldemCard.Flop3);
        }

        public void ShowTurn()
        {
            this.UpdateViewState(
                this.CurrentState == HoldemCard.Turn ? HoldemCard.Flop3 : HoldemCard.Turn);
        }

        public void ShowRiver()
        {
            this.UpdateViewState(
                this.CurrentState == HoldemCard.River ? HoldemCard.Turn : HoldemCard.River);
        }

        public void CardHandler(CardContext card)
        {
            if(card.HoldemCard <= HoldemCard.Flop3)
            {
                this.ShowFlop();
                return;
            }

            if(card.HoldemCard == HoldemCard.Turn)
            {
                this.ShowTurn();
                return;
            }

            if(card.HoldemCard == HoldemCard.River)
            {
                this.ShowRiver();
            }
        }

        #endregion //IHoldemCalculatorViewModel Implementation

        #region Private Methods

        private void UpdateViewState(HoldemCard state)
        {
            //Change which cards are visible.
            this.CurrentState = state;

            this.UpdateHandRank();

            this.UpdateCardImages();

            Messenger.Default.Send(new RoundGeneratedMessage(state));
        }

		private void HoldemDeckGenerated(History deck)
		{
            this.UpdateHandHistory(deck);

            if (this.CurrentState != HoldemCard.Flop1)
            {
                Messenger.Default.Send(new HandGeneratedRoundMessage(this.CurrentState));
            }
		}

        private void UpdateHandHistory(History deck)
        {
            if( Configuration.Cast<bool>(ConfigKey.View.Options.WriteHandHistory) )
            {
                _handHistoryReplayEngine.WriteHandHistory(deck);
                Configuration[ConfigKey.View.Options.WriteHandHistory] = true;
            }

            //Ensures the last generated hand is shown
            lock( _lock )
            {
                _currentHistory = deck;

                var bestHand = deck.GetBestHand().ToList();

                _bestHandHistory = BestHandHistory(bestHand);

                Current = this.IsHandRankVisible ? _bestHandHistory : deck;
            }

            this.UpdateCardImages();
            this.UpdateHandRank();
        }

        private void UpdateHandRank()
        {
            base.RaisePropertyChanged("HandValuation");
            base.RaisePropertyChanged("HandRankVisibility");
        }

        private void InitializeHand()
        {
            //Load the last saved hand
            Current = _handHistoryReplayEngine.LatestHistory;

            //Initial generated hand;
            if( Current == null )
                this.GenerateDeck();
            else
                _bestHandHistory = this.BestHandHistory(Current.GetBestHand().ToList());
        }

        private History BestHandHistory(List<CardValue> handHistory)
        {
           var bestHand = 
               new History
                {
                    FlopCardOne = new CardValue(handHistory[0].Suit, handHistory[0].Name, HoldemCard.Flop1),
                    FlopCardTwo = new CardValue(handHistory[1].Suit, handHistory[1].Name, HoldemCard.Flop2),
                    FlopCardThree = new CardValue(handHistory[2].Suit, handHistory[2].Name, HoldemCard.Flop3),
                    TurnCard = new CardValue(handHistory[3].Suit, handHistory[3].Name, HoldemCard.Turn),
                    RiverCard = new CardValue(handHistory[4].Suit, handHistory[4].Name, HoldemCard.River),
                    HoleCardOne = new CardValue(handHistory[5].Suit, handHistory[5].Name, HoldemCard.Hole1),
                    HoleCardTwo = new CardValue(handHistory[6].Suit, handHistory[6].Name, HoldemCard.Hole2)
                };

            return bestHand;
        }

        #endregion //Private Methods
    }
}