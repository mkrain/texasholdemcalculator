using System.Windows;
using System.Windows.Controls;
using TexasHoldemCalculator.Interfaces;
using TexasHoldemCalculator.Service;

namespace TexasHoldemCalculator.View
{
    public partial class HistoryView
    {
        private IAdProvider AdProvider
        {
            get
            {
                return Factory.Instance.GetInstance<IAdProvider>();
            }
        }

        public HistoryView()
        {
            InitializeComponent();

            this.THCHistoryAd.AdUnitId = AdProvider.AdUnitId;
            this.THCHistoryAd.ApplicationId = AdProvider.ApplicationId;
        }

        private void SkipTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            var box = sender as TextBox;

            if (box != null)
            {
                box.SelectAll();
            }
        }
    }
}