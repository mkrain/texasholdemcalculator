using Common.Core.Configuration;
using GalaSoft.MvvmLight.Command;
using TexasHoldemCalculator.Core.Entities.StartingHands;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Core.Statistics;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.ViewModel.Statistics
{
    public abstract class HoldemStatisticsBaseViewModel : HoldemViewModelBase
    {
        private readonly RelayCommand _generateOddsCommand;
        private readonly RelayCommand<int> _statsChanged;

        private readonly IntRangeDataSource _outsData;

        private readonly IHandOptions _handInfo =
                new HandConfigOptions
                {
                    NumberOfPlayers = 9,
                    MaxBet = 500,
                    NumberOfOuts = 4,
                    OddsSelection = PotOddsSelection.Combined,
                    Precision = 4,
                    PotSize = 1000
                };

        #region Protected Properties

        public IntRangeDataSource OutsData
        {
            get
            {
                return _outsData;
            }
        }

        public RelayCommand GenerateOddsCommand
        {
            get { return _generateOddsCommand; }
        }

        public RelayCommand<int> SelectedStatsChanged
        {
            get { return _statsChanged; }
        }

        protected IHandOptions HandInfo
        {
            get
            {
                _handInfo.Precision = this.Precision;
                _handInfo.NumberOfOuts = this.NumberOfOuts;

                return _handInfo;
            }
        }

        #endregion //Protected Properties

        #region Public Properties

        public int NumberOfOuts
        {
            get
            {
                var outs = Configuration.Cast<int>(ConfigKey.View.Stats.NumberOfOuts);

                if(outs < HoldemStatisticsBase.MinOuts)
                {
                    Configuration[ConfigKey.View.Stats.NumberOfOuts] = HoldemStatisticsBase.MinOuts;
                }

                if(outs > HoldemStatisticsBase.MaxOuts)
                {
                    Configuration[ConfigKey.View.Stats.NumberOfOuts] = HoldemStatisticsBase.MaxOuts;
                }

                return Configuration.Cast<int>(ConfigKey.View.Stats.NumberOfOuts);
            }
            set
            {
                if(value < HoldemStatisticsBase.MinOuts)
                {
                    Configuration[ConfigKey.View.Stats.NumberOfOuts] = HoldemStatisticsBase.MinOuts;
                }

                if(value > HoldemStatisticsBase.MaxOuts)
                {
                    Configuration[ConfigKey.View.Stats.NumberOfOuts] = HoldemStatisticsBase.MaxOuts;
                }
                else
                {
                    Configuration[ConfigKey.View.Stats.NumberOfOuts] = value;
                }

                RaisePropertyChanged("NumberOfOuts");
            }
        }

        #endregion

        protected HoldemStatisticsBaseViewModel(
            ICardThemeManager themeManager,
            IPhoneConfiguration configuration)
            : base(themeManager, configuration)
        {
            _generateOddsCommand = new RelayCommand(this.GenerateOdds);
            _statsChanged = new RelayCommand<int>(PanoramIndexChanged);
            _outsData = new IntRangeDataSource(maximum: HoldemStatisticsBase.MaxOuts);

            _outsData.SelectionChanged 
                += (o, e) =>
                {
                    this.NumberOfOuts = (int)e.AddedItems[0];

                    GenerateOdds();
                };

            LoadConfiguration();
        }

        public abstract void GenerateOdds();

        private void LoadConfiguration()
        {
            if(!Configuration.ContainsKey(ConfigKey.View.Stats.SelectedPanoramoItem))
            {
                Configuration.Add(ConfigKey.View.Stats.SelectedPanoramoItem, 0);
            }

            if(!Configuration.ContainsKey(ConfigKey.View.Options.Precision))
            {
                Configuration.Add(ConfigKey.View.Options.Precision, HoldemStatisticsBase.MinPlayers);
            }

            if(!Configuration.ContainsKey(ConfigKey.View.Options.NumberOfPlayers))
            {
                Configuration.Add(ConfigKey.View.Options.NumberOfPlayers, HoldemStatisticsBase.MinPlayers);
            }

            var numberOfPlayers = HoldemStatisticsBase.MinPlayers;
            var savedPlayers = Configuration.Cast<int>(ConfigKey.View.Options.NumberOfPlayers);

            if(savedPlayers < numberOfPlayers)
            {
                this.NumberOfPlayers = numberOfPlayers;
            }

            this.Precision = Configuration.Get<int>(ConfigKey.View.Options.Precision);
            this.NumberOfOuts = Configuration.Get<int>(ConfigKey.View.Calc.NumberOfOuts);
        }

        private void PanoramIndexChanged(int index)
        {
            if(index == -1)
            {
                return;
            }

            Configuration[ConfigKey.View.Stats.SelectedPanoramoItem] = index;
        }
    }
}