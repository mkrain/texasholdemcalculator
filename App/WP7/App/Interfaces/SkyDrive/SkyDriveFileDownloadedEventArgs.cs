using System;
using System.IO;

namespace TexasHoldemCalculator.Interfaces.SkyDrive
{
    public class SkyDriveFileDownloadedEventArgs : EventArgs
    {
        public Stream Result
        {
            get; private set; 
        }

        public Exception Exception
        {
            get; private set; 
        }

        public SkyDriveFileInformation FileInformation { get; private set; }

        public string FileName
        {
            get; private set; 
        }

        public SkyDriveFileDownloadedEventArgs(Stream stream, string fileName)
        {
            this.Result = stream;
            this.FileName = fileName;
        }

        public SkyDriveFileDownloadedEventArgs(Stream stream, SkyDriveFileInformation fileInformation)
        {
            this.Result = stream;
            this.FileInformation = fileInformation;
        }

        public SkyDriveFileDownloadedEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }
}