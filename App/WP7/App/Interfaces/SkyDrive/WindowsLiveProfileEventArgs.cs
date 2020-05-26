using System;

namespace TexasHoldemCalculator.Interfaces.SkyDrive
{
    public class WindowsLiveProfileEventArgs : EventArgs
    {
        private readonly IWindowsLiveProfile _windowsLiveProfile;

        public IWindowsLiveProfile WindowsLiveProfile
        {
            get
            {
                return _windowsLiveProfile;
            }
        }

        public WindowsLiveProfileEventArgs(IWindowsLiveProfile profile)
        {
            _windowsLiveProfile = profile;
        }
    }
}
