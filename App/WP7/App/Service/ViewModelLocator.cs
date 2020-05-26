using System.Linq;
using System.Windows.Media;
using Common.Core.Configuration;
using TexasHoldemCalculator.Core.Statistics;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Database;
using TexasHoldemCalculator.Interfaces.ReplayEngine;
using TexasHoldemCalculator.Interfaces.Security;
using TexasHoldemCalculator.Interfaces.StartingHands;
using TexasHoldemCalculator.ViewModel;
using TexasHoldemCalculator.ViewModel.Statistics;
using IAdProvider = TexasHoldemCalculator.Interfaces.IAdProvider;

namespace TexasHoldemCalculator.Service
{
    public class ViewModelLocator
    {
        private static bool _initialized;
        private static bool _setDefaultSettings;

        public IAdProvider AdProvider
        {
            get
            {
                this.SetDefaultSettings();

                return Factory.Instance.GetInstance<IAdProvider>();
            }
        }

        public ISkyDriveSecurityProvider SecurityProvider
        {
            get
            {
                this.SetDefaultSettings();

                return Factory.Instance.GetInstance<ISkyDriveSecurityProvider>();
            }
        }

        public HoldemInformationViewModel Information
        {
            get
            {
                this.SetDefaultSettings();

                return Factory.Instance.GetInstance<HoldemInformationViewModel>();
            }
        }

        public HoldemCalculatorViewModel Main
        {
            get
            {
                this.SetDefaultSettings();

				return Factory.Instance.GetInstance<HoldemCalculatorViewModel>();
            }
        }

        public HoldemHandHistoryViewModel HandHistory
        {
            get
            {
                this.SetDefaultSettings();

                return Factory.Instance.GetInstance<HoldemHandHistoryViewModel>();
            }
        }

        public HoldemOddsViewModel Odds
        {
            get
            {
                this.SetDefaultSettings();

                return Factory.Instance.GetInstance<HoldemOddsViewModel>();
            }
        }

        public HoldemOptionsViewModel Options
        {
            get
            {
                this.SetDefaultSettings();

				return Factory.Instance.GetInstance<HoldemOptionsViewModel>();
            }
        }

        public HoldemStartingHandsViewModel StartingHands
        {
            get
            {
                this.SetDefaultSettings();

                return Factory.Instance.GetInstance<HoldemStartingHandsViewModel>();
            }
        }

        public HoldemSkyDriveViewModel SkyDrive
        {
            get { return Factory.Instance.GetInstance<HoldemSkyDriveViewModel>(); }
        }

        public HoldemStatisticsKickerViewModel KickerStatistics
        {
            get
            {
                this.SetDefaultSettings();

				return Factory.Instance.GetInstance<HoldemStatisticsKickerViewModel>();
            }
        }

        public HoldemStatisticsRunnerViewModel RunnerStatistics
        {
            get
            {
                this.SetDefaultSettings();

				return Factory.Instance.GetInstance<HoldemStatisticsRunnerViewModel>();
            }
        }

        public HoldemStatisticsPocketPairViewModel PairStatistics
        {
            get
            {
                this.SetDefaultSettings();

				return Factory.Instance.GetInstance<HoldemStatisticsPocketPairViewModel>();
            }
        }

        public HoldemStatisticsPotOddsViewModel OddsStatistics
        {
            get
            {
                this.SetDefaultSettings();

                var oddsViewModel = Factory.Instance.GetInstance<HoldemStatisticsPotOddsViewModel>();

                if (!_initialized)
                {
                    oddsViewModel.PositiveExpectationColor = new SolidColorBrush(ColorNames.Green.FromName());
                    oddsViewModel.NegativeExpectationColor = new SolidColorBrush(ColorNames.Red.FromName());
                    _initialized = true;
                }

                return oddsViewModel;
            }
        }

        public HoldemAboutViewModel About
        {
            get
            {
                this.SetDefaultSettings();

                return Factory.Instance.GetInstance<HoldemAboutViewModel>();
            }
        }

        private IPhoneConfiguration Configuration
        {
            get
            {
                return Factory.Instance.GetInstance<IPhoneConfiguration>();
            }
        }

        private void SetDefaultSettings()
        {
            if( _setDefaultSettings )
                return;

            //Calculator ViewModel
            if(!this.Configuration.ContainsKey(ConfigKey.View.Calc.HandRank))
            {
                this.Configuration.Add(ConfigKey.View.Calc.HandRank, false);
            }

            if(!this.Configuration.ContainsKey(ConfigKey.View.Options.HandHistorySaved))
            {
                this.Configuration[ConfigKey.View.Options.HandHistorySaved] = false;
            }

            //Hand History ViewModel
            if(!this.Configuration.ContainsKey(ConfigKey.View.Options.SkipScale))
            {
                this.Configuration.Add(ConfigKey.View.Options.SkipScale, 1);
            }

            if(!this.Configuration.ContainsKey(ConfigKey.View.Options.HandHistorySaved))
            {
                this.Configuration.Add(ConfigKey.View.Options.HandHistorySaved, false);
            }

            //Options ViewModel
            if(!this.Configuration.ContainsKey(ConfigKey.View.Options.NumberOfPlayers))
            {
                this.Configuration.Add(ConfigKey.View.Options.NumberOfPlayers, HoldemStatisticsBase.MinPlayers);
            }

            if(!this.Configuration.ContainsKey(ConfigKey.View.Options.Precision))
            {
                this.Configuration.Add(ConfigKey.View.Options.Precision, (int)HoldemStatisticsBase.MinPrecision);
            }

            if(!this.Configuration.ContainsKey(ConfigKey.View.Options.SkipScale))
            {
                this.Configuration.Add(ConfigKey.View.Options.SkipScale, 1);
            }

            if(!this.Configuration.ContainsKey(ConfigKey.View.Options.UserName))
            {
                this.Configuration.Add(ConfigKey.View.Options.UserName, string.Empty);
            }

            if(!this.Configuration.ContainsKey(ConfigKey.View.Options.WriteHandHistory))
            {
                this.Configuration.Add(ConfigKey.View.Options.WriteHandHistory, false);
            }

            if(!this.Configuration.ContainsKey(ConfigKey.View.Options.HandHistorySaved))
            {
                this.Configuration.Add(ConfigKey.View.Options.HandHistorySaved, true);
            }

            this.Configuration[ConfigKey.View.Options.HandHistorySaved] =
                !Factory.Instance.GetInstance<IHandHistoryDataContext>().IsEmpty();

            if(!this.Configuration.ContainsKey(ConfigKey.View.Options.SelectedReplayEngine))
            {
                var engines = Factory.Instance.GetInstance<IReplayEngineHost>().EngineNames.FirstOrDefault();

                if(engines != null)
                {
                    this.Configuration.Add(ConfigKey.View.Options.SelectedReplayEngine, engines);
                }
            }

            _setDefaultSettings = true;
        }
    }
}