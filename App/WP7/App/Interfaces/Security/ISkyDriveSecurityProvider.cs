using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using Microsoft.Live.Controls;
using TexasHoldemCalculator.Interfaces.SkyDrive;

namespace TexasHoldemCalculator.Interfaces.Security
{
	public interface ISkyDriveSecurityProvider
	{
	    string CurrentDirectory { get; }

	    IEnumerable<SkyDriveFileInformation> Files { get; }

	    bool IsAtRootFolderListing { get; }

	    bool IsConnected { get; }

        RelayCommand<LiveConnectSessionChangedEventArgs> SessionChangedCommand { get; }

	    event EventHandler<LiveConnectSessionChangedEventArgs> WindowsLiveConnected;

	    event EventHandler<SkyDriveFileAddedEventArgs> SkyDriveDirectoryLoaded;

        event EventHandler<SkyDriveFileAddedEventArgs> SkyDriveFileDownloaded;

	    void SkyDriveSignInButtonSessionChanged(object sender, LiveConnectSessionChangedEventArgs e);

        void NavigateToNextDirectory(string currentDirectory);

	    void DownloadFile(string fileId);
	    
        void NavigateToPreviousDirectory();

	    ItemType GetItemType(string itemId);
	}

    public enum ItemType
    {
        Undefined = 0,
        File,
        Folder
    }
}