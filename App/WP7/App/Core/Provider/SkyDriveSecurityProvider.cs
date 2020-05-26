using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using GalaSoft.MvvmLight.Command;
using Microsoft.Live;
using Microsoft.Live.Controls;
using TexasHoldemCalculator.Core.Entities.SkyDrive;
using TexasHoldemCalculator.Interfaces.Provider;
using TexasHoldemCalculator.Interfaces.Security;
using TexasHoldemCalculator.Interfaces.SkyDrive;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace TexasHoldemCalculator.Core.Provider
{
    public class SkyDriveSecurityProvider : ISkyDriveSecurityProvider
    {
        #region Instance Variables

        private readonly IScopeProvider _scopeProvider;
        private readonly ISecurityOptionsProvider _securityOptionsProvider;
        private readonly IStartingHandsManager _startingHandsManager;
        private readonly IIconProvider _iconProvider;
        private SkyDriveStorage _storage;

        private const string ROOT_DIRECTORY = "/me/SkyDrive";

        public event EventHandler<LiveConnectSessionChangedEventArgs> WindowsLiveConnected;
        public event EventHandler<SkyDriveFileAddedEventArgs> SkyDriveDirectoryLoaded;
        public event EventHandler<SkyDriveFileAddedEventArgs> SkyDriveFileDownloaded;

        private readonly RelayCommand<LiveConnectSessionChangedEventArgs> _sessionChangedCommand;

        #endregion

        #region Public Properties

        public RelayCommand<LiveConnectSessionChangedEventArgs> SessionChangedCommand
        {
            get { return _sessionChangedCommand; }
        }

        public string CurrentDirectory
        {
            get { return _storage == null || _storage.CurrentDirectory == null ? string.Empty : _storage.CurrentDirectory.FileName; }
        }

        public IEnumerable<SkyDriveFileInformation> Files
        {
            get { return _storage.Files; }
        }

        public string ClientId
        {
            get { return _securityOptionsProvider.ClientId; }
        }

        public string RedirectUri
        {
            get { return _securityOptionsProvider.RedirectUri; }
        }

        public string Scopes
        {
            get { return String.Join(" ", _scopeProvider.Scopes); }
        }

        public bool IsNetworkAvailable
        {
            get { return NetworkInterface.GetIsNetworkAvailable(); }
        }

        public bool IsAtRootFolderListing
        {
            get { return this.CurrentDirectory == ROOT_DIRECTORY; }
        }

        public bool IsConnected
        {
            get { return _storage != null && _storage.IsConnected; }
        }

        #endregion

        #region Private Properties

        private LiveConnectSession Session { get; set; }

        #endregion

        #region Contructors

        public SkyDriveSecurityProvider(
            IScopeProvider scopeProvider,
            ISecurityOptionsProvider securityOptionsProvider,
            IStartingHandsManager startingHandsManager,
            IIconProvider iconProvider)
        {
            if( scopeProvider == null )
                throw new ArgumentNullException("scopeProvider");
            if( securityOptionsProvider == null )
                throw new ArgumentNullException("securityOptionsProvider");
            if( startingHandsManager == null )
                throw new ArgumentNullException("startingHandsManager");
            if( iconProvider == null )
                throw new ArgumentNullException("iconProvider");

            _scopeProvider = scopeProvider;
            _startingHandsManager = startingHandsManager;
            _iconProvider = iconProvider;
            _securityOptionsProvider = securityOptionsProvider;

            _sessionChangedCommand =
                new RelayCommand<LiveConnectSessionChangedEventArgs>(
                    e => this.SkyDriveSignInButtonSessionChanged(this, e));

            if( string.IsNullOrEmpty(_securityOptionsProvider.OAuthAuthorizeUri) )
                throw new ArgumentException("OAuth Authorization Uri Cannot Be Null.");
        }

        #endregion

        #region Public Methods

        public void SkyDriveSignInButtonSessionChanged(object sender, LiveConnectSessionChangedEventArgs e)
        {
            if( e.Status != LiveConnectSessionStatus.Connected )
                return;

            this.Session = e.Session;

            if( this.WindowsLiveConnected != null )
                this.WindowsLiveConnected(this, e);

            if( _storage == null )
            {
                _storage = new SkyDriveStorage(this.Session, _iconProvider);
                _storage.SkyDriveFileAdded += this.SkyDriveFileAdded;
                _storage.SkyDriveFileDownloaded += this.SkyDriveFileDownloadedHandler;
            }

            if( _storage.IsEmpty )
                _storage.NavigateToNextDirectory(ROOT_DIRECTORY);
        }

        public ItemType GetItemType(string itemId)
        {
            if( string.IsNullOrEmpty(itemId) )
                return ItemType.Undefined;
            if( itemId.Contains("folder") || itemId.Contains("/") )
                return ItemType.Folder;
            return ItemType.File;
        }

        /// <summary>
        /// 
        /// Navigates to the selected asset if it's a directory.
        /// 
        /// </summary>
        /// <param name="currentDirectory"></param>
        public void NavigateToNextDirectory(string currentDirectory)
        {
            var itemType = this.GetItemType(currentDirectory);

            if( itemType == ItemType.Folder )
                _storage.NavigateToNextDirectory(currentDirectory);
        }

        public void DownloadFile(string fileId)
        {
            _storage.DownLoadFile(fileId);
        }

        /// <summary>
        /// 
        /// Navigates to the previous directory
        /// 
        /// </summary>
        public void NavigateToPreviousDirectory()
        {
            _storage.NavigateToPreviousDirectory();
        }

        #endregion

        #region Private Methods

        private void SkyDriveFileAdded(object sender, SkyDriveFileAddedEventArgs e)
        {
            this.NotifyListenersFileLoaded(e.File);
        }

        private void SkyDriveFileDownloadedHandler(object sender, SkyDriveFileDownloadedEventArgs e)
        {
            if( e.Exception != null )
            {
                this.NotifyListenersFileDownloaded(null);
                return;
            }

            var result =
                _startingHandsManager.WriteResourceToFile(
                    new StartingHandSaveContext(
                        e.Result,
                        e.FileInformation == null ?
                            "StartingHands." + DateTime.UtcNow.ToString("MM_dd_yyyy_ss") :
                            e.FileInformation.FileName));

            this.NotifyListenersFileDownloaded(result == FileSaveStatus.Succeeded ? e.FileInformation : null);
        }

        private void NotifyListenersFileLoaded(SkyDriveFileInformation fileInformation)
        {
            if( this.SkyDriveDirectoryLoaded != null )
                this.SkyDriveDirectoryLoaded(this, new SkyDriveFileAddedEventArgs(fileInformation));
        }

        private void NotifyListenersFileDownloaded(SkyDriveFileInformation fileInformation)
        {
            if( this.SkyDriveFileDownloaded != null )
                this.SkyDriveFileDownloaded(this, new SkyDriveFileAddedEventArgs(fileInformation));
        }

        #endregion
    }
}