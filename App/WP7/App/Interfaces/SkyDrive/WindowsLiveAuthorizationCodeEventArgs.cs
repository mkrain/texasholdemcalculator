using System;

namespace TexasHoldemCalculator.Interfaces.SkyDrive
{
    public class WindowsLiveAuthorizationCodeEventArgs : EventArgs
    {
        private readonly IWindowsLiveAuthorizationCode _windowsLiveAuthorizationCode;

        public IWindowsLiveAuthorizationCode WindowsLiveAuthorizationCode
        {
            get
            {
                return _windowsLiveAuthorizationCode;
            }
        }

        public WindowsLiveAuthorizationCodeEventArgs(IWindowsLiveAuthorizationCode windowsLiveAuthorizationCode)
        {
            _windowsLiveAuthorizationCode = windowsLiveAuthorizationCode;
        }
    }
}