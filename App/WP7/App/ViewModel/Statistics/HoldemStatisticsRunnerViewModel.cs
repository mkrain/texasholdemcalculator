using System;
using Common.Core.Configuration;
using GalaSoft.MvvmLight.Threading;
using TexasHoldemCalculator.Core.Entities.Statistics;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Model.Statistics;
using TexasHoldemCalculator.Interfaces.Resource;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.ViewModel.Statistics
{
    public class HoldemStatisticsRunnerViewModel : HoldemStatisticsBaseViewModel
    {
        #region Variables

        private static readonly IHandRunnerRunnerOptions _runnerOptions;
        private readonly IHoldemStatisticsRunnerModel _model;
        private readonly IHoldemResource _resource;

        #endregion

        #region Public Properties

        public IHandRunnerRunnerStats HandRunnerStats
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// You have a {0} or 1 in {1} chance of drawing cards back-to-back with {2} outs.
        /// 
        /// </summary>
        public string RunnerRunnerPercentExplanation
        {
            get
            {
                return string.Format(
                    _resource.GetString("THC_Stat_Runner_Explanation"),
                        RunnerProbability,
                        string.Format("{0:0.00}", 
                            Math.Abs(this.HandRunnerStats.RunnerRunnerRatio - 0.0) < EPSILON ? 0 :
                            HandRunnerStats.RunnerRunnerRatio + 1),
                        base.NumberOfOuts);
            }
        }

        #region Implementation of IHoldemStatisticsRunnerViewModel

        public string RunnerProbability
        {
            get
            {
                return HandRunnerStats.RunnerRunnerPercentText;
            }
        }

        public string RunnerRatio
        {
            get
            {
                return HandRunnerStats.RunnerRunnerRatioText;
            }
        }

        #endregion

        #endregion

        #region Constructor

        static HoldemStatisticsRunnerViewModel()
        {
            _runnerOptions =
                new HandRunnerRunnerOptions
                {
                    NumberOfOuts = 2,
                    Precision = 4
                };
        }

        public HoldemStatisticsRunnerViewModel(
            ICardThemeManager themeManager, 
            IPhoneConfiguration configuration,
            IHoldemStatisticsRunnerModel model,
            IHoldemResource resource)
            : base(themeManager, configuration)
        {
            _model = model;
            _resource = resource;

            this.HandRunnerStats = new HandRunnerRunnerStats();

            UpdateStatOptionsFromConfig();

            _model.HoldemRunnerRunnerGenerated += GenerateHoldemRunnerRunner;
        }

        #endregion

        public override void GenerateOdds()
        {
            _runnerOptions.NumberOfOuts = this.NumberOfOuts;
            _runnerOptions.Precision = base.Precision;

            DispatcherHelper.RunAsync(
                () => _model.GenerateRunerRuunerOdds(_runnerOptions));
        }

        private void GenerateHoldemRunnerRunner(object sender, HoldemRunnerRunnerEventArgs e)
        {
            this.HandRunnerStats = e.RunnerRunner;

            base.RaisePropertyChanged("RunnerProbability");
            base.RaisePropertyChanged("RunnerRatio");
            base.RaisePropertyChanged("RunnerRunnerPercentExplanation");
            base.RaisePropertyChanged("NumberOfOuts");
            base.RaisePropertyChanged("NumberOfPlayers");
        }

        private void UpdateStatOptionsFromConfig()
        {
            _runnerOptions.NumberOfOuts = this.NumberOfOuts;
            _runnerOptions.Precision = base.Precision;
        }
    }
}