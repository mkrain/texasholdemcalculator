//using System;
//using System.Collections.Generic;
//using System.Linq;

//using HandGenerator.Entities.Model;

//using Holdem.Interfaces.Card;
//using Holdem.Interfaces.Configuration;
//using Holdem.Interfaces.Model;
//using Holdem.Interfaces.Service;
//using Holdem.Interfaces.StartingHands;
//using Holdem.Interfaces.ViewModel;
//using Holdem.Model;

//namespace HandGenerator.ViewModel
//{
//    public class HoldemStartingHandsViewModel : HoldemViewModelBase, IStartingHandsViewModel
//    {
//        #region Variables

//        private readonly IStartingHandsModel _model;

//        public event EventHandler<StartingHandsLoadedEventArgs> OnStartingHandsLoaded;

//        #endregion //Variables

//        #region Constructors

//        public HoldemStartingHandsViewModel(IHoldemService service)
//            : this(service, new StartingHandsModel(service))
//        {

//        }

//        public HoldemStartingHandsViewModel(IHoldemService service, IStartingHandsModel model)
//            : base(service)
//        {
//            _model = model;
//            _model.StartingHandsGenerated += this.StartingHandsLoaded;

//            Config.Get(ConfigKey.HoldemHandsViewSelectedStartingHand, string.Empty);
//        }

//        #endregion //Constructors

//        #region Public Properties

//        public override string Title
//        {
//            get
//            {
//                return Config.Get(ConfigKey.HoldemHandsViewSelectedStartingHand);
//            }
//            set
//            {
//                Config.Set(ConfigKey.HoldemHandsViewSelectedStartingHand, value);
//            }
//        }

//        public IStartingHand StartingHands
//        {
//            get;
//            set;
//        }

//        public IEnumerable<ICardValue> AllHands
//        {
//            get
//            {
//                return StartingHands.AllHands;
//            }
//            set
//            {

//            }
//        }

//        public IEnumerable<IStartingHandsContext> StartingHandsByTitle
//        {
//            get
//            {
//                return from hand in Service.StartingHandsManager.AvailableStartingHands
//                       select new StartingHandContext(hand, false);
//            }
//        }

//        public string SelectedStartingHand
//        {
//            get;
//            set;
//        }

//        #endregion //Public Properties

//        #region Public Methods

//        public void LoadStartingHands()
//        {
//            _model.GenerateStartingHands();
//        }

//        #endregion //Public Methods

//        #region Private Methods

//        private void StartingHandsLoaded(object sender, HoldemStartingHandGeneratedEventArgs e)
//        {
//            this.StartingHands = e.StartingHands;

//            if (OnStartingHandsLoaded != null)
//                OnStartingHandsLoaded(this, new StartingHandsLoadedEventArgs(true));

//            this.NotifyPropertyChanged("StartingHands");
//        }

//        #endregion //Private Methods
//    }
//}
