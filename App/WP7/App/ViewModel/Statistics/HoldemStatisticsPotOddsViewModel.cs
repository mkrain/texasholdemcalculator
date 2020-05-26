using System;
using System.Windows.Media;
using Common.Core.Configuration;
using GalaSoft.MvvmLight.Threading;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.ViewModel.Statistics
{
    public class HoldemStatisticsPotOddsViewModel : HoldemStatisticsBaseViewModel
    {
        #region Variables

        private static IFullHandStatsInfo _fullHandStats;
        private readonly IHoldemStatisticsPotOddsModel _model;

        #endregion //Variables

        #region Public Properties

        public Brush NegativeExpectationColor
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// Initialized in ViewModelLocator
        /// 
        /// </summary>
        public Brush PositiveExpectationColor
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// Initialized in ViewModelLocator
        /// 
        /// </summary>
        public Brush CurrentBorderBrush
        {
            get
            {
                return _fullHandStats.FlopHandStats.Expectation ?
                    this.PositiveExpectationColor :
                    this.NegativeExpectationColor;
            }
        }

        public string PotOddsText
        {
            get
            {
                return _fullHandStats.FlopHandStats.PotOddsText;
            }
        }

        public string MaxBetText
        {
            get
            {
                return _fullHandStats.FlopHandStats.MaxBetText;
            }
        }

        public string HandOddsFlopText
        {
            get
            {
                return _fullHandStats.FlopHandStats.HandOddsText;
            }
        }

        public string HandOddsTurnText
        {
            get
            {
                return _fullHandStats.TurnHandStats.HandOddsText;
            }
        }

        public string HandOddsRiverText
        {
            get
            {
                return _fullHandStats.RiverHandStats.HandOddsText;
            }
        }

        public string FlopPercentText
        {
            get
            {
                return _fullHandStats.FlopHandStats.MakeHandText;
            }
        }

        public string TurnPercentText
        {
            get
            {
                return _fullHandStats.TurnHandStats.MakeHandText;
            }
        }

        public string RiverPercentText
        {
            get
            {
                return _fullHandStats.RiverHandStats.MakeHandText;
            }
        }

        public double PotSize
        {
            get
            {
                return HandInfo.PotSize;
            }
            set
            {
                if(Math.Abs(HandInfo.PotSize - value) < EPSILON)
                {
                    return;
                }

                HandInfo.PotSize = value;

                Configuration[ConfigKey.View.Calc.PotSize] = value;

                GenerateOdds();
            }
        }

        public double MaxBet
        {
            get
            {
                return HandInfo.MaxBet;
            }
            set
            {
                if(Math.Abs(HandInfo.MaxBet - value) < EPSILON)
                {
                    return;
                }

                HandInfo.MaxBet = value;

                //Although betting more than what is in the pot is possible
                //it doesn't make sense, since change will be returned.
                if (HandInfo.MaxBet > HandInfo.PotSize)
                    HandInfo.MaxBet = HandInfo.PotSize;

                Configuration[ConfigKey.View.Calc.PotSize] = value;

                GenerateOdds();
            }
        }

        #endregion

        #region Constructors

        static HoldemStatisticsPotOddsViewModel()
        {
            _fullHandStats =
                new FullHandStatsInfo
                {
                    FlopHandStats = new HandStatsInfo(),
                    RiverHandStats = new HandStatsInfo(),
                    TurnHandStats = new HandStatsInfo()
                };
        }

        public HoldemStatisticsPotOddsViewModel(
			ICardThemeManager themeManager,
			IPhoneConfiguration configuration,
			IHoldemStatisticsPotOddsModel model)
			: base(themeManager, configuration)
        {
            _model = model;

            _model.HoldemPotOddsGenerated += PotsOddsGenerated;

            if(!Configuration.ContainsKey(ConfigKey.View.Calc.BetAmount))
            {
                Configuration.Add(ConfigKey.View.Calc.BetAmount, MaxBet);
            }

            if(!Configuration.ContainsKey(ConfigKey.View.Calc.PotSize))
            {
                Configuration.Add(ConfigKey.View.Calc.PotSize, PotSize);
            }

            this.MaxBet = configuration.Cast<double>(ConfigKey.View.Calc.BetAmount);
            this.PotSize = configuration.Cast<double>(ConfigKey.View.Calc.PotSize);

            GenerateOdds();
        }

        #endregion

        private void UpdateStatOptionsFromConfig()
        {
            HandInfo.PotSize = this.PotSize;
        }

        private void PotsOddsGenerated(object sender, HoldemPotOddsEventArgs e)
        {
            _fullHandStats = e.FullHandStatsInfo;

            base.RaisePropertyChanged("CurrentBorderBrush");
            base.RaisePropertyChanged("NumberOfPlayers");
            base.RaisePropertyChanged("NumberOfOuts");
            base.RaisePropertyChanged("MaxBet");
            base.RaisePropertyChanged("MaxBetText");
            base.RaisePropertyChanged("PotSize");
            base.RaisePropertyChanged("HandOddsFlopText");
            base.RaisePropertyChanged("HandOddsTurnText");
            base.RaisePropertyChanged("HandOddsRiverText");
            base.RaisePropertyChanged("PotOddsText");
            base.RaisePropertyChanged("CurrentState");
            base.RaisePropertyChanged("FlopPercentText");
            base.RaisePropertyChanged("TurnPercentText");
            base.RaisePropertyChanged("RiverPercentText");

            base.UpdateCardImages();
        }

        public override sealed void GenerateOdds()
        {
            UpdateStatOptionsFromConfig();

            DispatcherHelper.RunAsync(() => _model.GeneratePotOdds(this.HandInfo));
        }
    }
}