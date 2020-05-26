using System;

namespace TexasHoldemCalculator.Interfaces.SkyDrive
{
    public class SkyDriveFileAddedEventArgs : EventArgs
    {
        private readonly SkyDriveFileInformation _skyDriveFileInformation;

        public SkyDriveFileInformation File
        {
            get { return _skyDriveFileInformation; }
        }

        public SkyDriveFileAddedEventArgs(SkyDriveFileInformation fileAdded)
        {
            _skyDriveFileInformation = fileAdded;
        }
    }
}