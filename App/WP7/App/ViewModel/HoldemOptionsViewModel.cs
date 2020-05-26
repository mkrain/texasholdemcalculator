using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Core.Configuration;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Reactive;
using TexasHoldemCalculator.Core.Entities.StartingHands;
using TexasHoldemCalculator.Core.Statistics;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Database;
using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.ViewModel
{
    public class HoldemOptionsViewModel : HoldemViewModelBase
    {
        #region Instance Variables

        private static readonly ObservableCollection<string> _searchFilters =
            new ObservableCollection<string>
			{
				".xml",
				".txt",
				"*.*"
			};

		private readonly IReplayEngineHost _engineHost;
        private readonly IHandHistoryDataContext _dataContext;

        private IntRangeDataSource _precisionData;

        #endregion

        #region Public Properties

        public RelayCommand DeleteHandHsitoryCommand
        {
            get;
            private set;
        }

		public IEnumerable<string> AvailableEngines { get { return _engineHost.EngineNames; } }

        public ObservableCollection<string> SearchFilter
        {
            get
            {
                return _searchFilters;
            }
        }

        public int SkipScaleChange
        {
            get
            {
                return Configuration.Get<int>(ConfigKey.View.Options.SkipScale);
            }
            set
            {
                Configuration[ConfigKey.View.Options.SkipScale] = value;
                base.RaisePropertyChanged("SkipScaleChange");
            }
        }

        public string UserName
        {
            get
            {
                return Configuration.Get<string>(ConfigKey.View.Options.UserName);
            }
            set
            {
                Configuration[ConfigKey.View.Options.UserName] = value;
                base.RaisePropertyChanged("UserName");
            }
        }

        public string SelectedReplayEngine
        {
            get
            {
                return Configuration.Get<string>(ConfigKey.View.Options.SelectedReplayEngine);
            }
            set
            {
                Configuration[ConfigKey.View.Options.SelectedReplayEngine] = value;
                base.RaisePropertyChanged("SelectedReplayEngine");
            }
        }

        public string SelectedSearchFilter
        {
            get
            {
                return Configuration.Get<string>(ConfigKey.View.Options.SelectedSearchFilter);
            }
            set
            {
                Configuration[ConfigKey.View.Options.SelectedSearchFilter] = value;
                base.RaisePropertyChanged("SelectedSearchFilter");
            }
        }

        public bool ResursiveSearch
        {
            get
            {
                return Configuration.Get<bool>(ConfigKey.View.Options.RecursiveSearch);
            }
            set
            {
                Configuration[ConfigKey.View.Options.RecursiveSearch] = value;
                base.RaisePropertyChanged("ResursiveSearch");
            }
        }

        public bool HandPreview
        {
            get
            {
                return Configuration.Get<bool>(ConfigKey.View.Options.Preview);
            }
            set
            {
                Configuration[ConfigKey.View.Options.Preview] = value;
                base.RaisePropertyChanged("HandPreview");
            }
        }

        public bool WriteHandHistory
        {
            get
            {
                return Configuration.Get<bool>(ConfigKey.View.Options.WriteHandHistory);
            }
            set
            {
                Configuration[ConfigKey.View.Options.WriteHandHistory] = value;
                base.RaisePropertyChanged("WriteHandHistory");
            }
        }
        
        public bool IsHandHistorySaved
        {
            get { return !_dataContext.IsEmpty(); }
        }

        public IntRangeDataSource PrecisionData
        {
            get
            {
                return _precisionData;
            }
        }

        #endregion

        #region Constructor

        public HoldemOptionsViewModel(
            IHandHistoryDataContext dataContext,
			IReplayEngineHost engineHost,
			ICardThemeManager themeManager,
			IPhoneConfiguration configuration)
            : base(themeManager, configuration)
        {
            _dataContext = dataContext;
            _dataContext.HandHistoryDeletedAllEvent += this.HandHistoryDeleted;

			_engineHost = engineHost;

            DeleteHandHsitoryCommand = 
                new RelayCommand(
                    () =>
                    {
                        base.VisibilityChanged("Visibility3");

                        Scheduler.Dispatcher.Schedule(
                            () => _dataContext.DeleteHandHistory());
                    });

            LoadSavedConfiguration();
        }

        #endregion

        #region Private Methods

        private void HandHistoryDeleted(object sender, HandHistoryDeletedAllEventArgs e)
        {
            Scheduler.Dispatcher.Schedule(
                () =>
                {
                    RaisePropertyChanged("IsHandHistorySaved");
                    base.VisibilityChanged("Visibility3");
                });
        }

        private void LoadSavedConfiguration()
        {
            this.SelectedReplayEngine = Configuration.Get<string>(ConfigKey.View.Options.SelectedReplayEngine);
            this.SkipScaleChange = Configuration.Get<int>(ConfigKey.View.Options.SkipScale);
            base.NumberOfPlayers = Configuration.Cast<int>(ConfigKey.View.Options.NumberOfPlayers);
            this.UserName = Configuration.Get<string>(ConfigKey.View.Options.UserName);
            this.WriteHandHistory = Configuration.Get<bool>(ConfigKey.View.Options.WriteHandHistory);

            this.PlayersData.SelectedItem = base.NumberOfPlayers;

            _precisionData = new IntRangeDataSource(1, (int)HoldemStatisticsBase.MaxPrecision);

            base.PlayersData.SelectionChanged 
                += (o, e) =>
                {
                    base.NumberOfPlayers = (int)e.AddedItems[0];
                };
            _precisionData.SelectionChanged
                += (o, e) =>
                {
                    this.Precision = (int)e.AddedItems[0];
                };
        }

        #endregion
    }
}