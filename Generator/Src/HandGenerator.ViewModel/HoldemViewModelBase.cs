//using System;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows.Media.Imaging;

//using Holdem.Entities.Collections;
//using Holdem.Interfaces.Card;
//using Holdem.Interfaces.Configuration;
//using Holdem.Interfaces.HandHistory;
//using Holdem.Interfaces.Service;
//using Holdem.Interfaces.ViewModel;

//namespace HandGenerator.ViewModel
//{
//    public abstract class HoldemViewModelBase : IViewModel, INotifyPropertyChanged
//    {
//        #region Constants

//        protected const int MAX_OUTS_FLOP = 47;
//        protected const int MAX_OUTS_RIVER = 45;
//        protected const int MAX_OUTS_TURN = 46;
//        protected const int MAX_PLAYERS = 22;
//        protected const int MIN_PLAYERS = 2;
//        protected const int MIN_PRECISION = 0;
//        protected const int MAX_PRECISION = 4;

//        private HandHistoryWriterCollection _historyCollection;

//        #endregion //Constants

//        #region Instance Variables

//        private int _numberOfOuts;
//        private int _numberOfPlayers;

//        #endregion //Instance Variables

//        #region Public Properties

//        public virtual string Title
//        {
//            get;
//            set;
//        }

//        public virtual int NumberOfOuts
//        {
//            get
//            {
//                return _numberOfOuts;
//            }
//            set
//            {
//                _numberOfOuts = value;

//                if (this.CurrentState < HoldemCard.Turn && value > MAX_OUTS_FLOP)
//                    _numberOfOuts = MAX_OUTS_FLOP;
//                if (this.CurrentState == HoldemCard.Turn && value > MAX_OUTS_TURN)
//                    _numberOfOuts = MAX_OUTS_TURN;
//                if (this.CurrentState == HoldemCard.River && value > MAX_OUTS_RIVER)
//                    _numberOfOuts = MAX_OUTS_RIVER;
//            }
//        }

//        public virtual int NumberOfPlayers
//        {
//            get
//            {
//                return _numberOfPlayers;
//            }
//            set
//            {
//                _numberOfPlayers = value;

//                if (value < MIN_PLAYERS)
//                    _numberOfPlayers = MIN_PLAYERS;
//                if (value > MAX_PLAYERS)
//                    _numberOfPlayers = MAX_PLAYERS;
//            }
//        }

//        public virtual HoldemCard CurrentState
//        {
//            get;
//            set;
//        }

//        public virtual double SliderValue
//        {
//            get;
//            set;
//        }

//        public virtual int CurrentIndex
//        {
//            get;
//            set;
//        }

//        protected virtual IHandHistory CurrentHand
//        {
//            get
//            {
//                if (HistoryCollection == null || HistoryCollection.Count == 0)
//                    return null;

//                if (CurrentIndex - 1 < 0)
//                    CurrentIndex = 0;
//                if (CurrentIndex - 1 >= HistoryCollection.Count)
//                    CurrentIndex = HistoryCollection.Count - 1;

//                return HistoryCollection[CurrentIndex - 1];
//            }
//        }

//        public virtual BitmapImage HoleCardOne
//        {
//            get
//            {
//                return this.GetCardImage(HoldemCard.Hole1);
//            }
//        }

//        public virtual BitmapImage HoleCardTwo
//        {
//            get
//            {
//                return this.GetCardImage(HoldemCard.Hole2);
//            }
//        }

//        public virtual BitmapImage FlopCardOne
//        {
//            get
//            {
//                return GetCardImageBasedOnState(HoldemCard.Flop3, HoldemCard.Flop1);
//            }
//        }

//        public virtual BitmapImage FlopCardTwo
//        {
//            get
//            {
//                return GetCardImageBasedOnState(HoldemCard.Flop3, HoldemCard.Flop2);
//            }
//        }

//        public virtual BitmapImage FlopCardThree
//        {
//            get
//            {
//                return GetCardImageBasedOnState(HoldemCard.Flop3, HoldemCard.Flop3);
//            }
//        }

//        public virtual BitmapImage TurnCard
//        {
//            get
//            {
//                return GetCardImageBasedOnState(HoldemCard.Turn, HoldemCard.Turn);
//            }
//        }

//        public virtual BitmapImage RiverCard
//        {
//            get
//            {
//                return GetCardImageBasedOnState(HoldemCard.River, HoldemCard.River);
//            }
//        }

//        #endregion //Public Properties

//        #region Protected Properties

//        protected virtual HandHistoryWriterCollection HistoryCollection
//        {
//            get
//            {
//                return _historyCollection;
//            }
//            set
//            {
//                _historyCollection = value;
//            }
//        }

//        protected IHoldemConfiguration<ConfigKey, string> Config
//        {
//            get
//            {
//                return (IHoldemConfiguration<ConfigKey, string>)this.Service.PersistentConfiguration;
//            }
//        }

//        protected IHoldemConfiguration<ConfigKey, string> TransientConfig
//        {
//            get
//            {
//                return this.Service.TransientConfiguration as IHoldemConfiguration<ConfigKey, string>;
//            }
//        }

//        protected IHoldemService Service
//        {
//            get;
//            private set;
//        }

//        #endregion //Protected Properties

//        #region Constructor

//        protected HoldemViewModelBase(IHoldemService service)
//        {
//            this.Service = service;

//            //Default Hand Preview Option.
//            Config.Get(ConfigKey.HoldemOptionsViewPreview, "false");

//            _historyCollection = new HandHistoryWriterCollection();
//        }

//        #endregion //Constructor

//        #region INotifyPropertyChanged Implementation

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void NotifyPropertyChanged(string propertyName)
//        {
//            if (this.PropertyChanged != null)
//            {
//                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
//            }
//        }

//        protected virtual BitmapImage GetCardImage(HoldemCard card)
//        {
//            if (CurrentHand == null)
//                return Service.CardThemeManager.DefaultCardBack;

//            var key = this.CurrentHand.Keys.FirstOrDefault(x => x == card);
//            var cardFromHand = this.CurrentHand[key];

//            return Service.CardThemeManager.GetCardImage(cardFromHand);
//        }

//        protected virtual BitmapImage GetCardImageBasedOnState(HoldemCard expected, HoldemCard card)
//        {
//            if (this.CurrentState >= expected || Config.Cast<bool>(ConfigKey.HoldemOptionsViewPreview))
//                return this.GetCardImage(card);
//            return Service.CardThemeManager.DefaultCardBack;
//        }

//        protected virtual void UpdateCardImages()
//        {
//            this.NotifyPropertyChanged("HoleCardOne");
//            this.NotifyPropertyChanged("HoleCardTwo");

//            this.NotifyPropertyChanged("FlopCardOne");
//            this.NotifyPropertyChanged("FlopCardTwo");
//            this.NotifyPropertyChanged("FlopCardThree");

//            this.NotifyPropertyChanged("TurnCard");

//            this.NotifyPropertyChanged("RiverCard");
//        }

//        #endregion //INotifyPropertyChanged Implementation

//        protected virtual void RunMethodAsync(Action method)
//        {
//            //Application.Current.RootVisual.Dispatcher.BeginInvoke(method);
//        }
//    }
//}
