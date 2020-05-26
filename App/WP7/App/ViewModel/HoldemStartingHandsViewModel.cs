using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Common.Core.Configuration;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Live;
using Microsoft.Live.Controls;
using Microsoft.Phone.Reactive;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Extensions;
using TexasHoldemCalculator.Interfaces.Messages;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Security;
using TexasHoldemCalculator.Interfaces.Service;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace TexasHoldemCalculator.ViewModel
{
	public class HoldemStartingHandsViewModel : HoldemViewModelCommandBase
	{
		#region Variables

		private const string NAVIGATION_URL = "/View/StartingHandsView.xaml";
        private const string SKY_DRIVE_URL = "/View/SkyDriveFileExplorerView.xaml";
		private const string CUSTOM_STARTING_HAND = "<< Custom >>";
		private static readonly Uri _navigationUri = new Uri(NAVIGATION_URL, UriKind.Relative);
        private static readonly Uri _skyDriveUri = new Uri(SKY_DRIVE_URL, UriKind.Relative);
		private readonly IList<StartingHand> _availableHands;
		private readonly IStartingHandsModel _model;
		private readonly ICardThemeManager _themeManager;
		private readonly IStartingHandsManager _startingHandsManager;
		private readonly IHoldemNavigationService _navService;
		private StartingHand _selectedStartingHand;
        private Button _selectedStartingHandButton;
        private bool _isNetworkAvailable;

        private readonly ISkyDriveSecurityProvider _skyDriveSecurityProvider;

		#endregion //Variables

		#region Public Properties

		public string Title
		{
			get
			{
				return this.Configuration.Cast<string>(ConfigKey.View.StartingHands.SelectedStartingHand);
			}
			set
			{
                this.Configuration[ConfigKey.View.StartingHands.SelectedStartingHand] = value;
			}
		}

		public string Description
		{
			get
			{
				if(this.StartingHands == null || string.IsNullOrEmpty(this.StartingHands.Description))
				{
				    return "No description from author." + Environment.NewLine;
				}
				return this.StartingHands.Description.Replace("\n", Environment.NewLine);
			}
		}

        public string SignOnText
        {
            get
            {
                return IsNetworkAvailable ?
                    "This application uses custom hands stored in your SkyDrive account.  You need to log in to open and store them  on your device." :
                    "No network connection is available, enable the provider network or use wifi in order to sign-in.";
            }
        }

        public bool IsNetworkAvailable
        {
            get
            {
                var isAvailable = CheckIfNetworkIsAvailable();


                if (_isNetworkAvailable != isAvailable)
                {
                    _isNetworkAvailable = isAvailable;
                    RaisePropertyChanged("IsNetworkAvailable");
                    RaisePropertyChanged("SignOnText");
                }

                return _isNetworkAvailable;
            }
        }

		public BitmapImage SelectedHandHoleCardOne
		{
			get
			{
			    if(_selectedStartingHandButton == null)
			    {
			        return new BitmapImage();
			    }

			    var card = _selectedStartingHandButton.DataContext as CardValue;

			    if(card == null)
			    {
			        return _themeManager.DefaultCardBack;
			    }

			    var image = _themeManager.GetCardImage(card.Parent, card.IsSuited ? Suit.Spade : Suit.Heart);

			    return image;
			}
		}

		public BitmapImage SelectedHandHoleCardTwo
		{
			get
			{
			    if(_selectedStartingHandButton == null)
			    {
			        return new BitmapImage();
			    }

			    var card = _selectedStartingHandButton.DataContext as CardValue;

			    if(card == null)
			    {
			        return _themeManager.DefaultCardBack;
			    }

			    var image = _themeManager.GetCardImage(card.Name, Suit.Spade);

			    return image;
			}
		}

		public string SelectedHandDescription
		{
			get
			{
				if(this.StartingHands == null || _selectedStartingHandButton == null)
				{
				    return "No description from author." + Environment.NewLine;
				}

				var query = from strength in this.StartingHands.HandHighlight.HandStrength
							where strength.Key == (int)_selectedStartingHandButton.Content
							select strength.Value.Description;

				string description = query.FirstOrDefault() ?? "No description from author.";

				return description.Replace("\n", Environment.NewLine) + Environment.NewLine;
			}
		}

		public string SelectedHandHeader
		{
			get
			{
				if(_selectedStartingHandButton == null)
				{
				    return string.Empty;
				}
				return "Hands with strength " + (int)_selectedStartingHandButton.Content + ":" + Environment.NewLine;
			}
		}

        public StartingHand SelectedStartingHand
        {
            get
            {
                return _selectedStartingHand;
            }
            set
            {
                if(value == null)
                {
                    return;
                }

                _selectedStartingHand = value;
                this.Configuration[ConfigKey.View.StartingHands.SelectedStartingHand] = value.Title;
            }
        }

	    public RelayCommand StartingHandChangedCommand
		{
			get; set;
		}

        public RelayCommand<Button> StartingHandSelectedCommand
		{
			get; set;
        }

        public RelayCommand<string> DeleteHandCommand { get; private set; }
            

		private IStartingHand StartingHands
		{
			get;
			set;
		}

		public IEnumerable<CardValue> AllHands
		{
			get
			{
				if(this.StartingHands == null)
				{
				    return null;
				}
				return this.StartingHands.AllHands;
			}
		}

        public IList<StartingHand> StartingHandsByTitle
		{
			get
			{
			    var available = _startingHandsManager.AvailableStartingHands;

			    //-1 for the custom tag
			    if(available.Count + 1 != _availableHands.Count)
			    {
			        this.InitializeAvailableHands();
			    }

			    return _availableHands;
			}
		}

	    public bool IsHandInformationVisible
	    {
	        get { return Visibility1 == Visibility.Visible || Visibility2 == Visibility.Visible || OutsVisibility == Visibility.Visible; }
	    }

	    public bool IsCustomHandVisible
	    {
	        get { return Visibility == Visibility.Visible; }
	    }

	    #endregion //Public Properties

		#region Constructors

		public HoldemStartingHandsViewModel(
			IPhoneConfiguration configuration,
			ICardThemeManager themeManager,
			IStartingHandsManager startingHandsManager,
			IHoldemNavigationService navService,
            ISkyDriveSecurityProvider skyDriveSecurityProvider,
            IStartingHandsModel model)
            : base(configuration)
		{
			_startingHandsManager = startingHandsManager;
			_themeManager = themeManager;
			_navService = navService;
		    _skyDriveSecurityProvider = skyDriveSecurityProvider;

            _model = model;

            Messenger.Default.Register<StartingHandGeneratedMessage>(
                this, m => this.StartingHandsLoaded(m.Data));

			_availableHands = new ObservableCollection<StartingHand>();

		    _skyDriveSecurityProvider.WindowsLiveConnected += this.WindowsLiveConnected;

            Messenger.Default.Register<DeletedCustomHandMessage>(this, m => this.CustomHandDeleted(m.Data));

			this.StartingHandChangedCommand = new RelayCommand(this.StartingHandChanged);
			this.StartingHandSelectedCommand = new RelayCommand<Button>(this.StartingHandSelected);
            this.DeleteHandCommand = new RelayCommand<string>(
                this.DeleteStartingHand,
                x => _availableHands.First(h => h.Title == x).HandType == HandType.Custom);

			this.InitializeAvailableHands();
		}

		#endregion //Constructors

        #region Public Methods

        public void LoadStartingHands()
        {
            _model.GenerateStartingHands();
        }

        public void StartingHandChanged()
        {
            if( _selectedStartingHand.HandType != HandType.Undefined )
            {
                Scheduler.Dispatcher.Schedule(this.LoadStartingHands);

                UpdateStartingHandsVisibility();

                _navService.Navigate(_navigationUri);
                return;
            }

            //If we're already connect navigate to the skydrive file explorer
            if( _skyDriveSecurityProvider.IsConnected )
            {
                _navService.Navigate(_skyDriveUri);
            }
            //Ask the user to sign into skydrive.
            else if( base.Visibility == Visibility.Collapsed )
            {
                base.VisibilityChanged("Visibility");
            }
        }

	    public void StartingHandSelected(Button sender)
        {
            _selectedStartingHandButton = sender;

            base.RaisePropertyChanged("SelectedHandDescription");
            base.RaisePropertyChanged("SelectedHandHeader");
            base.RaisePropertyChanged("SelectedHandHoleCardOne");
            base.RaisePropertyChanged("SelectedHandHoleCardTwo");
            VisibilityChanged("OutsVisibility");
        }

        public void UpdateStartingHandsVisibility()
        {
            if(OutsVisibility == Visibility.Visible)
            {
                VisibilityChanged("OutsVisibility");
            }

            if(Visibility1 == Visibility.Visible)
            {
                VisibilityChanged("Visibility1");
            }

            if(Visibility2 == Visibility.Visible)
            {
                VisibilityChanged("Visibility2");
            }
        }

        public void UpdateStartingHandsSelectionVisibility()
        {
            if(Visibility == Visibility.Visible)
            {
                VisibilityChanged("Visibility");
            }
        }

        #endregion

        #region Private Methods

        private bool CheckIfNetworkIsAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

	    private void SetDefaultSelectedHand()
	    {
	        var saved = this.Configuration.Get<string>(ConfigKey.View.StartingHands.SelectedStartingHand);

	        if(_availableHands.Count == 0)
	        {
	            return;
	        }

	        if(string.IsNullOrEmpty(saved))
	        {
	            saved = _availableHands[0].Title;
                this.Configuration[ConfigKey.View.StartingHands.SelectedStartingHand] = saved;
	        }

	        var selected = _availableHands.FirstOrDefault(x => String.CompareOrdinal(x.Title, saved) == 0);

	        var selectedHand = selected ?? _availableHands[0];
	        selectedHand.IsSelected = true;

	        this.SelectedStartingHand = selectedHand;
	    }

	    private void InitializeAvailableHands()
		{
			if(_availableHands == null)
			{
			    return;
			}

			_availableHands.Clear();

            _startingHandsManager
                .AvailableStartingHands
                .ForEach(hand => _availableHands.Add(hand));

            //For the custom option
			_availableHands.Add(
                new StartingHand 
                { 
                    Title = CUSTOM_STARTING_HAND, 
                    HandType = HandType.Undefined 
                });

		    SetDefaultSelectedHand();
		}

        private void DeleteStartingHand(string handToDelete)
        {
            _startingHandsManager.DeleteCustomStartingHand(_availableHands.First(h => h.Title == handToDelete));
        }

        private void CustomHandDeleted(StartingHandDeletedDescription description)
        {
            base.RaisePropertyChanged("StartingHandsByTitle");               
        }

		private void StartingHandsLoaded(IStartingHand startingHand)
		{
            this.StartingHands = startingHand;
		}

	    private void WindowsLiveConnected(object sender, LiveConnectSessionChangedEventArgs e)
	    {
	        var isConnected = e.Status == LiveConnectSessionStatus.Connected;

	        if(isConnected)
	        {
	            if(base.Visibility == Visibility.Visible)
	            {
	                base.VisibilityChanged("Visibility");
	                _navService.Navigate(_skyDriveUri);
	            }
	            return;
	        }

	        //Connected, close the dialog, navigate to the selection page.
	        //TODO: Navigate to the selection page.
	        base.VisibilityChanged("Visibility");
	    }

	    public void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if(!IsHandInformationVisible)
            {
                return;
            }

            UpdateStartingHandsVisibility();

            e.Cancel = true;
        }

	    #endregion //Private Methods
	}
}