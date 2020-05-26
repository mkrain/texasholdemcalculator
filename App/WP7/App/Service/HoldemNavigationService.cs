using System;
using Microsoft.Phone.Controls;
using TexasHoldemCalculator.Interfaces.Service;

namespace TexasHoldemCalculator.Service
{
    public class HoldemNavigationService : IHoldemNavigationService
    {
        private static PhoneApplicationFrame _nService;

        public HoldemNavigationService(PhoneApplicationFrame nService)
        {
        	if( nService == null )
        		throw new ArgumentNullException("nService");

			_nService = nService;
        }

        public Uri Source
        {
            get
            {
                return _nService.Source;
            }
            set
            {
                _nService.Source = value;
            }
        }

        public Uri CurrentSource
        {
            get
            {
                return _nService.CurrentSource;
            }
        }

        public bool CanGoForward
        {
            get
            {
                return _nService.CanGoForward;
            }
        }

        public bool CanGoBack
        {
            get
            {
                return _nService.CanGoBack;
            }
        }

        public bool Navigate(Uri source)
        {
            return _nService.Navigate(source);
        }

        public void GoForward()
        {
            _nService.GoForward();
        }

        public void GoBack()
        {
            _nService.GoBack();
        }

        public void StopLoading()
        {
            _nService.StopLoading();
        }
    }
}
