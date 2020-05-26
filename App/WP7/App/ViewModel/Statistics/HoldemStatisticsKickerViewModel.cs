using System.Windows.Controls;
using Common.Core.Configuration;
using GalaSoft.MvvmLight.Threading;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Core.Statistics;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Model.Statistics;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.ViewModel.Statistics
{
    public class HoldemStatisticsKickerViewModel : HoldemStatisticsKickerBaseViewModel
    {
        #region Variables

        private static readonly IHandKickerOptions _kickerOptions;
        private readonly IHoldemStatisticsKickerModel _model;

        #endregion

        #region Public Properties

        public override Suit CardOneSuit
        {
            get { return Suit.Spade; }
        }

        public override Suit CardTwoSuit
        {
            get { return Suit.Diamond; }
        }

        public override CardName CardOneName
        {
            get { return CardName.Ace; }
        }

        public override CardName CardTwoName
        {
            get { return _kickerOptions.CardValue; }
        }

        public IHandKickerStats HandKickerStats
        {
            get;
            set;
        }

        public override int NumberOfPlayers
        {
            get
            {
                return _kickerOptions.NumberOfPlayers;
            }
            set
            {
                if(_kickerOptions.NumberOfPlayers == value)
                {
                    return;
                }

                _kickerOptions.NumberOfPlayers = value;

                if(value < HoldemStatisticsBase.MinPlayers)
                {
                    _kickerOptions.NumberOfPlayers = HoldemStatisticsBase.MinPlayers;
                }

                if(value > HoldemStatisticsBase.MaxPlayers)
                {
                    _kickerOptions.NumberOfPlayers = HoldemStatisticsBase.MaxPlayers;
                }

                Configuration[ConfigKey.View.Stats.NumberOfPlayers] = value;

                RaisePropertyChanged("NumberOfPlayers");
            }
        }

        #endregion

        #region Implementation of IHoldemStatisticsKickerViewModel

        public string KickerProbability
        {
            get
            {
                return HandKickerStats.ProbabilityText;
            }
        }

        public string KickerRatio
        {
            get
            {
                return HandKickerStats.RatioText;
            }
        }

        public string KickerPercent
        {
            get
            {
                return HandKickerStats.PercentageText;
            }
        }

        #endregion

        #region Constructors

        static HoldemStatisticsKickerViewModel()
        {
            _kickerOptions =
                new HandKickerOptions
                {
                    NumberOfPlayers = 9,
                    Precision = 2,
                    CardValue = CardName.Two
                };
        }

        public HoldemStatisticsKickerViewModel(
            ICardThemeManager themeManager, 
            IPhoneConfiguration configuration,
            IHoldemStatisticsKickerModel model)
            : base(themeManager, configuration)
        {
            this.HandKickerStats = new HandKickerStats();

            _model = model;

            LoadSavedConfiguration();

            _model.HoldemAceAgainstBiggerKickerGenerated += GenerateAceAgainstBiggerKicker;
            base.PlayersData.SelectionChanged += this.PlayersSelectionChangedHandler;
        }
        
        #endregion

        #region Private Method

        public override void GenerateOdds()
        {
            _kickerOptions.CardValue = this.SelectedKicker;
            _kickerOptions.Precision = base.Precision;

            Configuration[ConfigKey.View.Stats.SelectedKicker] = this.SelectedKicker;

            DispatcherHelper.RunAsync(
                () => _model.GenerateAceAgainstBiggerKicker(_kickerOptions));
        }

        private void GenerateAceAgainstBiggerKicker(object sender, HoldemAceAgainstBiggerKickerEventArgs e)
        {
            this.HandKickerStats = e.AgainstBiggerKicker;

            base.RaisePropertyChanged("KickerProbability");
            base.RaisePropertyChanged("KickerRatio");
            base.RaisePropertyChanged("KickerPercent");
            base.RaisePropertyChanged("KickerImage");

            base.UpdateCardImages();
        }

        private void LoadSavedConfiguration()
        {
            if(Configuration.ContainsKey(ConfigKey.View.Stats.SelectedKicker))
            {
                this.SelectedKicker = Configuration.Get<CardName>(ConfigKey.View.Stats.SelectedKicker);
            }

            this.PlayersData.SelectedItem = _kickerOptions.NumberOfPlayers;
        }

        private void PlayersSelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            this.NumberOfPlayers = (int)e.AddedItems[0];
        }

        #endregion
    }
}