using Common.Core.Configuration;
using Common.Core.Configuration.Service;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Security;
using TexasHoldemCalculator.Service;
using TexasHoldemCalculator.ViewModel;
using IAdProvider = TexasHoldemCalculator.Interfaces.IAdProvider;

namespace TexasHoldemCalculator.View
{
	public partial class StartingHandsSelectionView
	{
        public ISkyDriveSecurityProvider SecurityProvider
        {
            get
            {
                return Factory.Instance.GetInstance<ISkyDriveSecurityProvider>();
            }
        }

	    private HoldemStartingHandsViewModel HandContext
	    {
	        get { return DataContext as HoldemStartingHandsViewModel; }
	    }

	    public ITrialProvider TrialProvider
	    {
	        get
	        {
                return Factory.Instance.GetInstance<ITrialProvider>();
	        }
	    }

        public IPhoneConfiguration TransientConfig
		{
			get
			{
                return Factory.Instance.GetInstance<IConfigurationService>().TransientConfiguration;
			}
		}

        private IAdProvider AdProvider
        {
            get
            {
                return Factory.Instance.GetInstance<IAdProvider>();
            }
        }

		public StartingHandsSelectionView()
		{
		    InitializeComponent();

		    SetAdInformation();

            this.SkyDriveSignInButton.SessionChanged += SecurityProvider.SkyDriveSignInButtonSessionChanged;

		    if(TrialProvider.IsTrial)
		    {
		        return;
		    }

		    if(TransientConfig.ContainsKey(ConfigKey.View.StartingHands.SelectionEventRegistration))
		    {
		        return;
		    }

            TransientConfig[ConfigKey.View.StartingHands.SelectionEventRegistration] = true;
		}

        private void SetAdInformation()
        {
            this.THCStartingHandsAd.AdUnitId = AdProvider.AdUnitId;
            this.THCStartingHandsAd.ApplicationId = AdProvider.ApplicationId;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if(!HandContext.IsCustomHandVisible)
            {
                return;
            }

            HandContext.UpdateStartingHandsSelectionVisibility();

            e.Cancel = true;
        }
	}
}