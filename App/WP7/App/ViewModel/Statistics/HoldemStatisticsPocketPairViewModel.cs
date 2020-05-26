using System;
using Common.Core.Configuration;
using GalaSoft.MvvmLight.Threading;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Model.Statistics;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.ViewModel.Statistics
{
    public class HoldemStatisticsPocketPairViewModel : HoldemStatisticsKickerBaseViewModel
    {
        #region Variables
        
        private static readonly IHandPocketPairOptions _pocketPairOptions;
		private readonly IHoldemStatisticsPocketPairModel _model;

        #endregion //Variables

        #region Public Properties

        public IHandKickerStats HandKickerStats
        {
            get;
            set;
        }

        public override Suit CardOneSuit
        {
            get { return Suit.Heart; }
        }

        public override Suit CardTwoSuit
        {
            get { return Suit.Spade; }
        }

        public override CardName CardOneName
        {
            get { return _pocketPairOptions.CardValue; }
        }

        public override CardName CardTwoName
        {
            get { return _pocketPairOptions.CardValue; }
        }

        public IHandPocketPairStats HandPocketPairStats
        {
            get;
            set;
        }

        #region Implementation of IHoldemStatisticsPocketPairViewModel

        public string PocketPairProbability
        {
            get
            {
                return HandPocketPairStats.ProbabilityText;
            }
        }

        public string PocketPairRatio
        {
            get
            {
                return HandPocketPairStats.RatioText;
            }
        }

        public string PocketPairPercent
        {
            get
            {
                return HandPocketPairStats.PercentageText;
            }
        }

        #endregion

        #endregion //Public Properties

        #region Constructors

        static HoldemStatisticsPocketPairViewModel()
        {
            _pocketPairOptions =
                new HandPocketPairOptions
                {
                    NumberOfPlayers = 9,
                    CardValue = CardName.Two,
                    Precision = 4
                };
        }

        public HoldemStatisticsPocketPairViewModel(
			ICardThemeManager themeManager,
			IPhoneConfiguration configuration,
			IHoldemStatisticsPocketPairModel model)
            : base(themeManager, configuration)
        {
			if (model == null)
				throw new ArgumentNullException("model");

            _model = model;
            
            this.HandPocketPairStats = new HandPocketPairStats();

            UpdateStatOptionsFromConfig();

            _model.HoldemAgainstBiggerPairGenerated += GenerateHoldemAgainstBiggerPair;
        }

        #endregion //Constructors

        #region IHoldemStatisticsPocketPairViewModel Implementation

        public override void GenerateOdds()
        {
            _pocketPairOptions.CardValue = this.SelectedKicker;
            _pocketPairOptions.NumberOfPlayers = base.NumberOfPlayers;
            _pocketPairOptions.Precision = base.Precision;

            Configuration[ConfigKey.View.Stats.SelectedPocketPair] = this.SelectedKicker;

            DispatcherHelper.RunAsync(
                () => _model.GenerateAgainstBiggerPair(_pocketPairOptions));
        }

        #endregion //IHoldemStatisticsPocketPairViewModel Implementation

        #region Private Methods

        private void GenerateHoldemAgainstBiggerPair(object sender, HoldemAgainstBiggerPairEventArgs e)
        {
            this.HandPocketPairStats = e.AgainstBiggerPair;

            base.RaisePropertyChanged("PocketPairProbability");
            base.RaisePropertyChanged("PocketPairRatio");
            base.RaisePropertyChanged("PocketPairPercent");

            base.UpdateCardImages();
        }

        #endregion  //Private Methods

        #region Protected Methods

        private void UpdateStatOptionsFromConfig()
        {
            if(!Configuration.ContainsKey(ConfigKey.View.Stats.SelectedPocketPair))
            {
                Configuration.Add(ConfigKey.View.Stats.SelectedPocketPair, CardName.Two);
            }

            this.SelectedKicker = Configuration.Cast<CardName>(ConfigKey.View.Stats.SelectedPocketPair);
            _pocketPairOptions.NumberOfPlayers = base.NumberOfPlayers;
            _pocketPairOptions.Precision = base.Precision;
        }

        #endregion //Protected Methods
    }
}