using System;

namespace TexasHoldemCalculator.Interfaces.SkyDrive
{
    public class WindowsLiveAuthorizationFailedEventArgs : EventArgs
    {
        public string Error { get; private set; }
        public string ErrorDescription { get; private set; }


        public WindowsLiveAuthorizationFailedEventArgs(string error, string errorDescription)
        {
            this.Error = error;
            this.ErrorDescription = errorDescription;
        }
    }
}