using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using Common.Core.Configuration;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using TexasHoldemCalculator.Core.Entities.Collections;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.HandHistory;
using TexasHoldemCalculator.Interfaces.Messages;
using TexasHoldemCalculator.Interfaces.Model;

namespace TexasHoldemCalculator.ViewModel
{
    public class HoldemHandHistoryViewModel : HoldemViewModelBase
    {
        #region Instance Variables

        private int _currentHand;
        private readonly IHoldemHandHistoryModel _model;
        private const string TOURNAMENT_LABEL = "Tournament: {0}";
        private const string HAND_RANGE_LABEL = "Hand {0} of {1}, {2} won this game";
        private HandHistoryWriterCollection _historyCollection = new HandHistoryWriterCollection();

        #endregion //Instance Variables

        #region Public Properties

        public RelayCommand LoadHandHistoryCommand
        {
            get;
            set;
        }

        public RelayCommand LoadHandHistoryStatsCommand
        {
            get;
            set;
        }

        public RelayCommand NextHistoryCommand
        {
            get;
            set;
        }

        public RelayCommand PreviousHistoryCommand
        {
            get;
            set;
        }

        public string TournamentLabel
        {
            get
            {
                var label = "Auto Generated by THC: ";
                
                label += this.Current == null ? 
                    DateTime.Now.ToString(CultureInfo.InvariantCulture) : 
                    this.Current.Date.ToString(CultureInfo.InvariantCulture);

                if(this.Current != null && !string.IsNullOrEmpty(this.Current.GameDescription))
                {
                    label = this.Current.GameDescription;
                }

                return string.Format(TOURNAMENT_LABEL, label);
            }
        }

        public string HandRangeLabel
        {
            get
            {
                int start = 1, end = 1, won = 0;

                if(this.Current != null)
                {
                    start = _currentHand + 1;
                    end = _historyCollection.Count; 

                    if(!string.IsNullOrEmpty(this.Current.GameDescription))
                    {
                        won = _historyCollection.Count(hand => hand.WonHand);
                    }
                }

                return string.Format(HAND_RANGE_LABEL, start, end, won);
            }
        }

        protected override History Current
        {
            get
            {
                if (_historyCollection == null || _historyCollection.Count == 0)
                    return null;

                return _historyCollection[_currentHand];
            }
        }

        public int SkipScaleChange
        {
            get
            {
                return Configuration.Cast<int>(ConfigKey.View.Options.SkipScale);
            }
            set
            {
                Configuration[ConfigKey.View.Options.SkipScale] = value;
            }
        }

        public int CurrentHandIndex
        {
            get
            {
                return _currentHand + 1;
            }
            set
            {
                _currentHand = value - 1;

                this.ValidateCurrentHandIndex();

                this.UpdateCardImages();
            }
        }

        public bool HistoryLoaded
        {
            get
            {
                return ( _historyCollection != null && _historyCollection.Any() );
            }
        }

        public bool HistoryEnabled
        {
            get
            {
                //TODO Maybe move this to an OnNavigated method?
                if( !_model.HasHistory() && _historyCollection.Any() )
                {
                    _historyCollection.Clear();
                    this.UpdateCardImages();
                }

                return _model.HasHistory();
            }
        }

        public bool StatsEnabled
        {
            get
            {
                return this.Current != null 
                    && !string.IsNullOrEmpty(this.Current.GameDescription)
                    && !this.Current.GameDescription.Contains("Auto");
            }
        }

        #endregion //Public Properties

        #region	Constructors

        public HoldemHandHistoryViewModel(
			ICardThemeManager themeManager,
			IPhoneConfiguration configuration,
			IHoldemHandHistoryModel model)
			: base(themeManager, configuration)
        {
            _model = model;

            LoadSavedConfiguration();

            Messenger.Default.Register<HandHistoryGeneratedMessage>(this, m => this.HandHistoryGenerated(m.Data));

            this.LoadHandHistoryCommand = new RelayCommand(this.LoadHandHistoryAsync);
            this.LoadHandHistoryStatsCommand = new RelayCommand(this.LoadHandHistoryStatsAsync);
            this.NextHistoryCommand = new RelayCommand(this.NextHistorySelected);
            this.PreviousHistoryCommand = new RelayCommand(this.PreviousHistorySelected);

            base.Visibility = Visibility.Collapsed;
        }

        #endregion //Constructors

        #region IHoldemHandHistoryViewModel Implementation

        private void LoadHandHistoryStatsAsync()
        {
            if(this.Current != null)
            {
                return;
            }

            base.VisibilityChanged("Visibility");
        }

        private void LoadHandHistoryAsync()
        {
            DispatcherHelper.RunAsync(
                () =>
                {
                    if( !_model.HasHistory() )
                    {
                        _historyCollection.Clear();
                        UpdateCardImages();
                        return;
                    }

                    _model.GetHandHistory();

                    //Notify listeners if hand history was found.
                    Messenger.Default.Send(new HandHistoryLoadedMessage(this.StatsEnabled));

                    //The base property uses CurrentIndex-1 as the current index.
                    _currentHand = 0;

                    this.UpdateCardImages();
                });
        }

        #endregion //IHoldemHandHistoryViewModel Implementation

        #region Private Methods

        private void HandHistoryGenerated(IEnumerable<History> history)
        {
            _historyCollection = new HandHistoryWriterCollection(history);
        }

        private void NextHistorySelected()
        {
            if( _currentHand > _historyCollection.Count - 1 )
            {
                this.UpdateCardImages();
                return;
            }

            if(_currentHand + SkipScaleChange >= _historyCollection.Count - 1)
            {
                _currentHand = _historyCollection.Count - 1;
            }
            else
            {
                _currentHand += SkipScaleChange;
            }

            this.UpdateCardImages();
        }

        private new void UpdateCardImages()
        {
            base.UpdateCardImages();
            base.RaisePropertyChanged("HistoryLoaded");
            base.RaisePropertyChanged("CurrentHandIndex");
            base.RaisePropertyChanged("TournamentLabel");
            base.RaisePropertyChanged("HandRangeLabel");
        }

        private void PreviousHistorySelected()
        {
            if( _currentHand <= 0 )
            {
                this.UpdateCardImages();
                return;
            }

            if(_currentHand - SkipScaleChange < 0)
            {
                _currentHand = 0;
            }

            else
            {
                _currentHand -= SkipScaleChange;
            }

            this.UpdateCardImages();
        }

        private void ValidateCurrentHandIndex()
        {
            if(_currentHand < 0)
            {
                _currentHand = 0;
            }

            if(_currentHand >= _historyCollection.Count)
            {
                _currentHand = _historyCollection.Count - 1;
            }
        }

        private void LoadSavedConfiguration()
        {
            this.SkipScaleChange = Configuration.Get<int>(ConfigKey.View.Options.SkipScale);
        }

        #endregion //Private Methods

        #region Protected Methods

        /// <summary>
        /// 
        /// There is no state when replaying hand history, just show all cards.
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        protected override CardContext GetCardImageBasedOnState(HoldemCard expected, HoldemCard card)
        {
            return this.GetCardImage(card);
        }

        #endregion
    }
}