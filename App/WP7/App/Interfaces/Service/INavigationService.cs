using System;

namespace TexasHoldemCalculator.Interfaces.Service
{
    public interface IHoldemNavigationService
    {
        Uri Source
        {
            get;
            set;
        }

        Uri CurrentSource
        {
            get;
        }

        bool CanGoForward
        {
            get;
        }

        bool CanGoBack
        {
            get;
        }

        bool Navigate(Uri source);
        void GoForward();
        void GoBack();
        void StopLoading();

        //event NavigationFailedEventHandler NavigationFailed;
        //event NavigatingCancelEventHandler Navigating;
        //event NavigatedEventHandler Navigated;
        //event NavigationStoppedEventHandler NavigationStopped;
        //event FragmentNavigationEventHandler FragmentNavigation;
    }
}
