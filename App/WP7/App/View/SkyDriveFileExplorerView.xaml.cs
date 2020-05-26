using TexasHoldemCalculator.Interfaces;
using TexasHoldemCalculator.Interfaces.Security;
using TexasHoldemCalculator.Service;
using TexasHoldemCalculator.ViewModel;

namespace TexasHoldemCalculator.View
{
    public partial class SkyDriveFileExplorerView
    {
        public ISkyDriveSecurityProvider SecurityProvider
        {
            get { return Factory.Instance.GetInstance<ISkyDriveSecurityProvider>(); }
        }

        public IAdProvider AdPrivder
        {
            get { return Factory.Instance.GetInstance<IAdProvider>(); }
        }

        public HoldemSkyDriveViewModel ViewModel { get { return Factory.Instance.GetInstance<HoldemSkyDriveViewModel>(); } }

        public SkyDriveFileExplorerView()
        {
            InitializeComponent();

            SetAdInformation();
        }

        private void SetAdInformation()
        {
            this.THCSkyDriveExplorerAd.AdUnitId = AdPrivder.AdUnitId;
            this.THCSkyDriveExplorerAd.ApplicationId = AdPrivder.ApplicationId;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //TODO: Is there a better way to call a method on the view model without having
            //TODO: to add methods to the class?
            if( !ViewModel.IsAtRootFolderListing )
            {
                ViewModel.NavigateToPreviousFolderListing();
                e.Cancel = true;
                return;
            }

            base.OnBackKeyPress(e);
        }
    }
}