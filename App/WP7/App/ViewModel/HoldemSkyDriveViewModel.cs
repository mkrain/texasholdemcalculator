using System;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Microsoft.Live.Controls;
using TexasHoldemCalculator.Interfaces.Security;
using TexasHoldemCalculator.Interfaces.Service;
using TexasHoldemCalculator.Interfaces.SkyDrive;

namespace TexasHoldemCalculator.ViewModel
{
    public class HoldemSkyDriveViewModel : HoldemViewModelVisibilityBase
    {
        #region Variables

        private readonly ISkyDriveSecurityProvider _skyDriveSecurityProvider;
        private readonly IHoldemNavigationService _navService;
        private bool _isDownloading;

        private const string HAND_SELECTION_URL = "/View/StartingHandsSelectionView.xaml";
        private static readonly Uri _navigationUri = new Uri(HAND_SELECTION_URL, UriKind.Relative);

        #endregion

        #region Public Properties

        public string CurrentDirectory
        {
            get { return _skyDriveSecurityProvider.CurrentDirectory; }
        }

        public ObservableCollection<SkyDriveFileInformation> Files
        {
            get { return new ObservableCollection<SkyDriveFileInformation>(_skyDriveSecurityProvider.Files); }
        }

        public RelayCommand<string> SkyDriveFileSelectedCommand
        {
            get;
            private set;
        }

        public RelayCommand SkyDriveFileDownloadConfirmedCommand
        {
            get;
            private set;
        }

        public RelayCommand<LiveConnectSessionChangedEventArgs> SessionChangedCommand
        {
            get { return _skyDriveSecurityProvider.SessionChangedCommand; }
        }

        public bool IsAtRootFolderListing
        {
            get { return _skyDriveSecurityProvider != null && _skyDriveSecurityProvider.IsAtRootFolderListing; }
        }

        public string StatusText
        {
            get
            {
                return _isDownloading ? "Downloading..." : "Loading...";
            }
        }

        #endregion

        #region Private Properties

        private string SelectedFileToDownload { get; set; }

        #endregion

        #region Constructors

        public HoldemSkyDriveViewModel(
            ISkyDriveSecurityProvider skyDriveSecurityProvider,
            IHoldemNavigationService navService)
        {
            if (skyDriveSecurityProvider == null)
                throw new ArgumentNullException("skyDriveSecurityProvider");
            if (navService == null)
                throw new ArgumentNullException("navService");

            _skyDriveSecurityProvider = skyDriveSecurityProvider;
            _navService = navService;

            _skyDriveSecurityProvider.SkyDriveDirectoryLoaded += this.SkyDirectoryLoaded;
            _skyDriveSecurityProvider.SkyDriveFileDownloaded += this.SkyFileDownLoaded;

            this.SkyDriveFileSelectedCommand = new RelayCommand<string>(this.SkyDriveFileSelected);
            this.SkyDriveFileDownloadConfirmedCommand = new RelayCommand(this.SkyDriveFileDownloadConfirmed);
        }

        #endregion

        #region Public Methods

        public void NavigateToPreviousFolderListing()
        {
            if (base.Visibility2 == Visibility.Collapsed)
                base.VisibilityChanged("Visibility2");

            _skyDriveSecurityProvider.NavigateToPreviousDirectory();
        }

        public void SkyDriveFileSelected(string fileToDownLoad)
        {
            var itemType = _skyDriveSecurityProvider.GetItemType(fileToDownLoad);

            if (itemType == ItemType.Undefined)
                return;

            if (itemType == ItemType.File)
            {
                this.SelectedFileToDownload = fileToDownLoad;
                if( base.Visibility1 == Visibility.Collapsed )
                    base.VisibilityChanged("Visibility1");

                return;
            }

            if (itemType == ItemType.Folder)
            {
                if( base.Visibility2 == Visibility.Collapsed )
                    base.VisibilityChanged("Visibility2");

                _skyDriveSecurityProvider.NavigateToNextDirectory(fileToDownLoad);
            }
        }

        #endregion

        #region Private Methods

        private void SkyDirectoryLoaded(object sender, SkyDriveFileAddedEventArgs fileAddedEventArgs)
        {
            RefreshSkyDriveHands();

            if (base.Visibility2 == Visibility.Visible)
            {
                base.VisibilityChanged("Visibility2");
            }
        }

        private void SkyFileDownLoaded(object sender, SkyDriveFileAddedEventArgs fileAddedEventArgs)
        {
            _isDownloading = false;

            if( base.Visibility2 == Visibility.Visible )
            {
                base.VisibilityChanged("Visibility2");
                _navService.Navigate(_navigationUri);
            }
        }

        private void SkyDriveFileDownloadConfirmed()
        {
            //Hide the confirmation dialog
            if( base.Visibility1 == Visibility.Visible )
                base.VisibilityChanged("Visibility1");
            //Show the progress dialog
            if( base.Visibility2 == Visibility.Collapsed )
                base.VisibilityChanged("Visibility2");

            //Start asynchronous download
            _isDownloading = true;

            _skyDriveSecurityProvider.DownloadFile(this.SelectedFileToDownload);
        }
          
        private void RefreshSkyDriveHands()
        {
            base.RaisePropertyChanged("Files");
            base.RaisePropertyChanged("StatusText");
            base.RaisePropertyChanged("CurrentDirectory");
        }

        #endregion
    }
}