using System.Collections.Generic;
using Common.Core.Configuration;
using TexasHoldemCalculator.Core.Statistics;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.HandHistory;

namespace TexasHoldemCalculator.ViewModel
{
    public abstract class HoldemViewModelBase : HoldemViewModelCommandBase
    {
        #region Constants

    	protected const double EPSILON = 0.005;

        #endregion //Constants

        #region Instance Variables

    	private readonly ICardThemeManager _themeManager;

        #endregion //Instance Variables

        #region Public Properties

        public virtual string Title
        {
            get;
            set;
        }

        public virtual int NumberOfPlayers
        {
            get
            {
                return Configuration.Cast<int>(ConfigKey.View.Options.NumberOfPlayers);
            }
            set
            {
                Configuration[ConfigKey.View.Options.NumberOfPlayers] = value;

                base.RaisePropertyChanged("NumberOfPlayers");
            }
        }

        public HoldemCard CurrentState
        {
            get;
            set;
        }

        public int CurrentIndex { get; set; }

        protected virtual History Current { get; set; }

        public List<CardContext> HoldemHandCards
        {
            get
            {
                return
                    new List<CardContext>
                    {
                        GetCardImageBasedOnState(HoldemCard.Flop3, HoldemCard.Flop1),
                        GetCardImageBasedOnState(HoldemCard.Flop3, HoldemCard.Flop2),
                        GetCardImageBasedOnState(HoldemCard.Flop3, HoldemCard.Flop3),
                        GetCardImageBasedOnState(HoldemCard.Turn, HoldemCard.Turn),
                        GetCardImageBasedOnState(HoldemCard.River, HoldemCard.River)
                    };
            }
        }

        public List<CardContext> HoldemHoleCards
        {
            get
            {
                return
                    new List<CardContext>
                    {
                        this.GetCardImage(HoldemCard.Hole1),
                        this.GetCardImage(HoldemCard.Hole2)
                    };
            }
        }

        public int Precision
        {
            get
            {
                var precisionKey = ConfigKey.View.Options.Precision;

                var precision = Configuration.Cast<int>(precisionKey);

                if(precision > HoldemStatisticsBase.MaxPrecision)
                {
                    Configuration[precisionKey] = (int)HoldemStatisticsBase.MaxPrecision;
                }

                if(precision < HoldemStatisticsBase.MinPrecision)
                {
                    Configuration[precisionKey] = (int)HoldemStatisticsBase.MinPrecision;
                }

                return Configuration.Cast<int>(precisionKey);
            }
            set
            {
                var precisionKey = ConfigKey.View.Options.Precision;

                if(value > HoldemStatisticsBase.MaxPrecision)
                {
                    Configuration[precisionKey] = (int)HoldemStatisticsBase.MaxPrecision;
                }

                if(value < HoldemStatisticsBase.MinPrecision)
                {
                    Configuration[precisionKey] = (int)HoldemStatisticsBase.MinPrecision;
                }
                else
                {
                    Configuration[precisionKey] = value;
                }

                base.RaisePropertyChanged("Precision");
            }
        }

        #endregion //Public Properties

        #region Protected Properties

		protected ICardThemeManager ThemeManager { get { return _themeManager; } }

        #endregion //Protected Properties

        #region Constructor

        protected HoldemViewModelBase(
			ICardThemeManager themeManager, 
			IPhoneConfiguration configuration)
            : base(configuration)
        {
			_themeManager = themeManager;

            this.LoadSavedConfiguration();
        }

        #endregion //Constructor

        #region INotifyPropertyChanged Implementation

        protected virtual CardContext GetCardImage(HoldemCard card)
        {
            if(this.Current == null)
            {
                return
                    new CardContext
                        {
                            UriSource = _themeManager.DefaultCardBack.UriSource.OriginalString
                        };
            }

            var cardFromHand = this.Current.FindCard(card);

            var uriSource = _themeManager.GetCardImage(cardFromHand).UriSource.OriginalString;

            return
                new CardContext
                    {
                        Card = cardFromHand,
                        UriSource = uriSource,
                        HoldemCard = card
                    };
        }

        protected virtual CardContext GetCardImageBasedOnState(HoldemCard expected, HoldemCard card)
        {
            string uriSource = _themeManager.DefaultCardBack.UriSource.OriginalString;

            if(this.CurrentState >= expected || Configuration.Cast<bool>(ConfigKey.View.Options.Preview))
            {
                return this.GetCardImage(card);
            }

            return
                new CardContext
                    {
                        UriSource = uriSource,
                        HoldemCard = card
                    };
        }

        protected void UpdateCardImages()
        {
            base.RaisePropertyChanged("HoldemHandCards");
            base.RaisePropertyChanged("HoldemHoleCards");
        }

        #endregion //INotifyPropertyChanged Implementation

        #region Protected Methods

        private void LoadSavedConfiguration()
        {
            CurrentIndex = 0;
            CurrentState = HoldemCard.Flop2;

            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            this.NumberOfPlayers = Configuration.Cast<int>(ConfigKey.View.Options.NumberOfPlayers);

            if(this.NumberOfPlayers < HoldemStatisticsBase.MinPlayers)
            {
                this.NumberOfPlayers = HoldemStatisticsBase.MinPlayers;
            }

            this.Precision = Configuration.Get<int>(ConfigKey.View.Options.Precision);
        }

        #endregion
    }

    public class CardContext
    {
        public CardValue Card { get; set; }
        public string UriSource { get; set; }
        public HoldemCard HoldemCard { get; set; }
    }
}