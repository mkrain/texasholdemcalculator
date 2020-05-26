
using System;

using Microsoft.Phone.Controls;

namespace TexasHoldemCalculator
{
    public class NavigationHelper
    {
        private static readonly NavigationHelper _instance = new NavigationHelper();
        private static PhoneApplicationFrame _root;

        private NavigationHelper()
        {

        }

        public NavigationHelper InitializeRootFrame(PhoneApplicationFrame root)
        {
            _root = root;

            return _instance;
        }

        public static NavigationHelper Instance
        {
            get
            {
                return _instance;
            }
        }

        public void NavigateToRelativePageRequest(string page)
        {
            NavigateToPage(page, UriKind.Relative);
        }

        public void NavigateToAbsolutePageRequest(string page)
        {
            NavigateToPage(page, UriKind.Absolute);
        }

        private static void NavigateToPage(string page, UriKind kind)
        {
			if (_root.CurrentSource.OriginalString.Contains(page))
				return;

            _root.Navigate(new Uri(page, kind));
        }
    }
}