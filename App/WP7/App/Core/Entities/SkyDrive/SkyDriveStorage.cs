using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.Live;
using TexasHoldemCalculator.Interfaces.Extensions;
using TexasHoldemCalculator.Interfaces.Provider;
using TexasHoldemCalculator.Interfaces.SkyDrive;

namespace TexasHoldemCalculator.Core.Entities.SkyDrive
{
    public class SkyDriveStorage
    {
        #region Instance Variables

        private readonly List<SkyDriveFileInformation> _rootDirectory = new List<SkyDriveFileInformation>();
        private readonly IIconProvider _iconProvider;
        private readonly Stack<SkyDriveFileInformation> _folderListings = new Stack<SkyDriveFileInformation>();
        private SkyDriveFileInformation _requestedDownloadFile;

        private IList<string> _supportedFileTypes = new List<string> { ".xml" };

        public event EventHandler<SkyDriveFileAddedEventArgs> SkyDriveFileAdded;
        public event EventHandler<SkyDriveFileDownloadedEventArgs> SkyDriveFileDownloaded;

        #endregion

        #region Public Properties

        public SkyDriveFileInformation CurrentDirectory { get; private set; }

        public IEnumerable<SkyDriveFileInformation> Files
        {
            get { return _rootDirectory; }
        }

        public ReadOnlyCollection<string> SupportedFileTypes
        {
            get { return new ReadOnlyCollection<string>(_supportedFileTypes); }
            set { _supportedFileTypes = new List<string>(value); }
        }

        public bool IsEmpty { get { return _rootDirectory.Count == 0; } }

        public bool IsConnected
        {
            get { return this.Client != null && this.Client.Session != null && this.Session.Expires.DateTime >= DateTime.UtcNow; }
        }

        #endregion

        #region Private Properties

        private LiveConnectSession Session { get; set; }

        private LiveConnectClient Client { get; set; }

        #endregion

        #region Contructors

        public SkyDriveStorage(LiveConnectSession session, IIconProvider iconProvider)
        {
            _iconProvider = iconProvider;
            this.Session = session;
            this.Client = new LiveConnectClient(session);

            this.Client.GetCompleted += this.LiveClientGetCompleted;
        }

        #endregion

        #region Public Methods

        public void NavigateToNextDirectory(string directoryToTraverse)
        {
            var findDirectory =
                (from f in _rootDirectory                
                 where f.FileId == directoryToTraverse || f.FileName == directoryToTraverse
                 select new
                 {
                     Directory = f,
                 }).FirstOrDefault();

            if( this.CurrentDirectory != null )
                _folderListings.Push(this.CurrentDirectory);

            this.CurrentDirectory = findDirectory == null ? 
                new SkyDriveFileInformation
                {
                    FileName = directoryToTraverse,
                    FileId = directoryToTraverse
                } : findDirectory.Directory;

            var directoryId = this.CurrentDirectory.FileId;

            if( _rootDirectory.Count > 0 )
                _rootDirectory.Clear();

            this.GetFileListing(directoryId);
        }

        public void NavigateToPreviousDirectory()
        {
            if( _folderListings.Count == 0)
                return;

            this.CurrentDirectory = _folderListings.Pop();

            var directoryId = this.CurrentDirectory.FileId;

            if (_rootDirectory.Count > 0)
                _rootDirectory.Clear();

            this.GetFileListing(directoryId);
        }

        /// <summary>
        /// 
        /// In order to retrieve the results you must subscribe to the file
        /// downloaded eventargs.
        /// 
        /// </summary>
        /// <param name="fileId"></param>
        public void DownLoadFile(string fileId)
        {
            this.Client.DownloadCompleted += this.LiveClientDownloadCompleted;

            var getFileInfo = this.GetFileInformation(fileId);

            _requestedDownloadFile = getFileInfo;

            if (getFileInfo == null)
            {
                this.SkyDriveFileDownloaded(this, null);
                return;
            }

            this.Client.DownloadAsync(getFileInfo.FileId + "/content");
        }

        public SkyDriveFileInformation GetFileInformation(string fileNameOrId)
        {
            var file = ( from f in _rootDirectory
                         where f.FileName == fileNameOrId 
                            || f.FileId == fileNameOrId
                         select f ).FirstOrDefault();

            return file;
        }

        #endregion

        #region Private Methods

        private void GetFileListing(string directoryToTraverse)
        {
            this.Client.GetAsync(directoryToTraverse + "/files");
        }

        private IEnumerable<SkyDriveFileInformation> DirectoryListing(IEnumerable<object> skyDriveData)
        {
            var directories =
                from element in skyDriveData.Cast<IDictionary<string, object>>()
                where element.ContainsKey("type")
                let type = element.ValueOrDefault("type")
                select
                    type == "photo" || type == "video" || type == "file" ? this.FileInformation(element) :
                    type == "album" ? this.AlbumInformation(element) :
                    type == "folder" ? this.FolderInformation(element) : null;

            return directories.Where(e => e != null);
        }

        private SkyDriveFileInformation FileInformation(IDictionary<string, object> fileListing)
        {
            var fileInfo =
                new SkyDriveFileInformation(fileListing)
                {
                    IconProvider = _iconProvider
                };

            var extension = Path.GetExtension(fileInfo.FileName);

            if (extension != null && this.SupportedFileTypes.Contains(extension.ToLower()))
                return fileInfo;

            return null;
        }

        private SkyDriveFileInformation AlbumInformation(IDictionary<string, object> fileListing)
        {
            var album =
                new SkyDriveFileInformation(fileListing)
                {
                    EntryImage = "album.png", 
                    IconProvider = _iconProvider
                };

            return album;
        }

        private SkyDriveFileInformation FolderInformation(IDictionary<string, object> fileListing)
        {
            var fileInfo =
                new SkyDriveFileInformation(fileListing)
                {
                    EntryImage = "folder_blue.png",
                    IconProvider = _iconProvider
                };

            return fileInfo;
        }

        private void LiveClientGetCompleted(object sender, LiveOperationCompletedEventArgs e)
        {
            try
            {
                var data = (List<object>)e.Result["data"];

                var listings = this.DirectoryListing(data).ToList();

                if (listings.Count <= 0)
                {
                    this.NotifyListenersFileAdded(null);
                    return;
                }

                var first = listings.FirstOrDefault();

                _rootDirectory.AddRange(listings);

                this.NotifyListenersFileAdded(first);
            }
            catch( Exception )
            {
                this.NotifyListenersFileAdded(null);
            }
        }

        private void LiveClientDownloadCompleted(object sender, LiveDownloadCompletedEventArgs args)
        {
            if( this.SkyDriveFileDownloaded == null )
                return;

            if (args == null)
            {
                this.SkyDriveFileDownloaded(this, null);
                return;
            }

            if( args.Error != null )
            {
                this.SkyDriveFileDownloaded(this, new SkyDriveFileDownloadedEventArgs(args.Error));
                return;
            }

            this.SkyDriveFileDownloaded(
                this, 
                new SkyDriveFileDownloadedEventArgs(
                    args.Result, 
                    _requestedDownloadFile));
        }

        private void NotifyListenersFileAdded(SkyDriveFileInformation information)
        {
            if (this.SkyDriveFileAdded != null)
                this.SkyDriveFileAdded(this, new SkyDriveFileAddedEventArgs(information));
        }

        #endregion
    }
}