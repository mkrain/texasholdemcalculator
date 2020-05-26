using System.Windows;
using System.Windows.Controls;
using TexasHoldemCalculator.Interfaces;
using TexasHoldemCalculator.Service;

namespace TexasHoldemCalculator.View
{
    public partial class OptionsView
    {
        private IAdProvider AdProvider
        {
            get
            {
                return Factory.Instance.GetInstance<IAdProvider>();
            }
        }

        public OptionsView()
        {
            InitializeComponent();
            SetAdInformation();
        }

        #region Private Methods

        private void SetAdInformation()
        {
            this.THCOptionsAd.AdUnitId = AdProvider.AdUnitId;
            this.THCOptionsAd.ApplicationId = AdProvider.ApplicationId;
        }

        private void StatsTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            var box = sender as TextBox;

            if (box != null)
            {
                box.SelectAll();
            }
        }

        #endregion  //Private Methods
    }
}