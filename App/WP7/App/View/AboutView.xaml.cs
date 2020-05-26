using System;
using System.Net;
using System.Windows.Controls;
using Microsoft.Phone.Tasks;
using TexasHoldemCalculator.Interfaces;
using TexasHoldemCalculator.Service;

namespace TexasHoldemCalculator.View
{
    public partial class AboutView
    {
        private IAdProvider AdProvider
        {
            get
            {
                return Factory.Instance.GetInstance<IAdProvider>();
            }
        }

        public AboutView()
        {
            InitializeComponent();

            this.THCOptionsAd.AdUnitId = AdProvider.AdUnitId;
            this.THCOptionsAd.ApplicationId = AdProvider.ApplicationId;
        }

        private void ThcHyperlinkButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var link = sender as HyperlinkButton;

            if (link == null)
                return;

            var task =
                new WebBrowserTask
                    {
                        Uri = new Uri(HttpUtility.UrlEncode(link.Content as string))
                    };

            task.Show();
        }

        private void ThcEmailHyperlinkButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var link = sender as HyperlinkButton;

            if (link == null)
                return;

            var task =
                new EmailComposeTask
                {
                    Subject = "Texas Holdem Calculator WP7",
                    To = HttpUtility.UrlEncode(link.Content as string),
                    Body = "Comment/Suggestion..."
                };

            task.Show();
        }
    }
}