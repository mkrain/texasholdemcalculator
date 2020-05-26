using TexasHoldemCalculator.Interfaces;
using TexasHoldemCalculator.Service;

namespace TexasHoldemCalculator.View
{
    public partial class HoleOddsView
    {
        private IAdProvider AdProvider
        {
            get
            {
                return Factory.Instance.GetInstance<IAdProvider>();
            }
        }

        public HoleOddsView()
        {
            InitializeComponent();

            this.THCHoleOddsAd.AdUnitId = AdProvider.AdUnitId;
            this.THCHoleOddsAd.ApplicationId = AdProvider.ApplicationId;
        }
    }
}