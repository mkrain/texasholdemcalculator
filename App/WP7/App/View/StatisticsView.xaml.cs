using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Common.Core.Configuration;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Service;
using IAdProvider = TexasHoldemCalculator.Interfaces.IAdProvider;

namespace TexasHoldemCalculator.View
{
    public partial class StatsView
    {
        private IPhoneConfiguration Config
        {
            get
            {
                return Factory
                    .Instance
                    .GetInstance<IPhoneConfiguration>();
            }
        }

        private IAdProvider AdProvider
        {
            get
            {
                return Factory.Instance.GetInstance<IAdProvider>();
            }
        }

        #region Constructor

        public StatsView()
        {
            this.InitializeComponent();

            THCStatisticsAd.AdUnitId = AdProvider.AdUnitId;
            THCStatisticsAd.ApplicationId = AdProvider.ApplicationId;
//#if DEBUG
//            THCStatisticsAd.ErrorOccurred += this.THCStatisticsAd_ErrorOccurred;
//#endif
        }

//#if DEBUG
//        private void THCStatisticsAd_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
//        {
//            MessageBox.Show(e.Error.Message, "Ad Error", MessageBoxButton.OK);
//        }        
//#endif

        #endregion //Constructor

        #region  Protected Methods

    	protected override void OnNavigatedTo(NavigationEventArgs e)
    	{
    	    var index = Config.Cast<int>(ConfigKey.View.Stats.SelectedPanoramoItem);
    	    StatisticsPanorama.DefaultItem = StatisticsPanorama.Items[index];
    	}

        #endregion  //Protected Methods

        private void StatsTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            var box = sender as TextBox;

            if (box != null)
                box.SelectAll();
        }
    }
}