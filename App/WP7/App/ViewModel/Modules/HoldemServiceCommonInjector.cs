using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Marketplace;
using TexasHoldemCalculator.Interfaces;
using TexasHoldemCalculator.Interfaces.Attributes.Ad;
using TexasHoldemCalculator.Interfaces.Attributes.Config;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Database;
using TexasHoldemCalculator.Interfaces.Generator;
using TexasHoldemCalculator.Interfaces.Resource;
using TexasHoldemCalculator.Interfaces.Security;
using TexasHoldemCalculator.Interfaces.Service;
using TexasHoldemCalculator.Interfaces.Statistics;

//using RSA;

namespace TexasHoldemCalculator.ViewModel.Modules
{
    public sealed class HoldemServiceCommonInjector : NinjectModule
    {
        //private const int RSA_KEY_SIZE = 2048;
        private readonly bool _inDesignMode;

        private const string DB_CONNECTION_STRING = "Data Source=isostore:/History.sdf";

        private IAdProvider _smallAd;
        private IAdProvider _largeAd;

#if DEBUG
        private const string AD_UNIT_ID_SMALL = "Image300_50";
        private const string APPLICATION_ID_SMALL = "test_client";
        private const string AD_UNIT_ID_LARGE = "Image480_80";
        private const string APPLICATION_ID_LARGE = "test_client";
#else
        private const string AD_UNIT_ID_SMALL = "REDACTED";
        private const string APPLICATION_ID_SMALL = "REDACTED";
        private const string AD_UNIT_ID_LARGE = "REDACTED";
        private const string APPLICATION_ID_LARGE = "REDACTED";
#endif
        private IsolatedStorageFile Storage
        {
            get;
            set;
        }

        private IDictionary<string, object> Settings
        {
            get;
            set;
        }

        private IDictionary<string, object> State
        {
            get;
            set;
        }

        private PhoneApplicationFrame NavigationService
        {
            get;
            set;
        }

        public HoldemServiceCommonInjector()
        {
            bool isInDesignMode = false;

            try
            {
                isInDesignMode = DesignerProperties.GetIsInDesignMode(Application.Current.RootVisual);
            }
            catch( Exception )
            {

            }

            _inDesignMode = isInDesignMode;
        }

        private void SetDefaultVaues()
        {
            this.Storage = IsolatedStorageFile.GetUserStoreForApplication();
            this.Settings = IsolatedStorageSettings.ApplicationSettings;
            this.State = Microsoft.Phone.Shell.PhoneApplicationService.Current.State;

            this.NavigationService = Application.Current.RootVisual as PhoneApplicationFrame;

            this._smallAd = new SmallAdProvider(AD_UNIT_ID_SMALL, APPLICATION_ID_SMALL);
            this._largeAd = new LargeAdProvider(AD_UNIT_ID_LARGE, APPLICATION_ID_LARGE);
        }

        public override void Load()
        {
            if( !this._inDesignMode )
                this.SetDefaultVaues();

            //Bind Isolated File Settings
            //This is the default if the target is null.
            this.Bind<IHoldemPhoneConfiguration>()
                .To<HoldemAppSettings>()
                .WhenTargetHas<PersistentConfigurationAttribute>()
                .InSingletonScope()
                .WithConstructorArgument("dictionary", this.Settings);

            this.Bind<IHoldemPhoneConfiguration>()
                .To<HoldemAppSettings>()
                .WhenTargetHas<TransientConfigurationAttribute>()
                .InSingletonScope()
                .WithConstructorArgument("dictionary", this.State);

            this.Bind<IHoldemPhoneConfiguration>()
                .To<HoldemAppSettings>()
                .InSingletonScope()
                .WithConstructorArgument("dictionary", this.Settings);

            this.Bind<IHoldemIsolatedStorageFile>().ToConstant(new HoldemIsolatedStorageFile(this.Storage)).InSingletonScope();

            //Load the odds
            this.Bind<IPocketPairOdds>().To<PocketPairOdds>().InSingletonScope();
            this.Bind<IPotOdds>().To<PotOdds>().InSingletonScope();
            this.Bind<IPokerHandOdds>().To<PokerHandOdds>().InSingletonScope();

            //Generators
            this.Bind<ICardGenerator>().To<CardGenerator>().InSingletonScope();
            this.Bind<IDeckGenerator>().To<DeckGenerator>().InSingletonScope();
            this.Bind<INumberGenerator>().To<NumberGenerator>().InSingletonScope();
            this.Bind<IPrimitiveGenerator>().To<PrimitiveGenerator>().InSingletonScope();

            //Resource Manager
            this.Bind<IHoldemResource>().To<HoldemResource>().InSingletonScope();

            //Security Provider
            this.Bind<IScopeProvider>().To<ScopeProvider>().InSingletonScope();
            this.Bind<ISecurityOptionsProvider>().To<SecurityOptionsProvider>().InSingletonScope();
            this.Bind<ISkyDriveSecurityProvider>().To<SkyDriveSecurityProvider>().InSingletonScope();
            this.Bind<RandomNumberGenerator>().ToConstant(new RNGCryptoServiceProvider()).InSingletonScope();
            //this.Bind<RSACrypto>().ToConstant(new RSACrypto(RSA_KEY_SIZE)).InSingletonScope();
            this.Bind<IEncryptionProvider>().To<EncryptionProvider>().InSingletonScope();

			//Service Bindings
			this.Bind<IConfigurationService>().To<HoldemConfigurationService>().InSingletonScope();

            if( !_inDesignMode )
			{
                this.Bind<IHoldemNavigationService>()
				    .ToConstant(new HoldemNavigationService(this.NavigationService)).InSingletonScope();
            }

			this.Bind<IGeneratorService>().To<HoldemGeneratorService>().InSingletonScope();
			this.Bind<IProviderService>().To<HoldemProviderService>().InSingletonScope();
			this.Bind<IHoldemStatisticsService>().To<HoldemStatisticsService>().InSingletonScope();

            //License Provider
            this.Bind<LicenseInformation>().To<LicenseInformation>().InSingletonScope();
            this.Bind<ITrialProvider>().To<TriaProvider>().InSingletonScope();

            //Default Ad Provider
            this.Bind<IAdProvider>()
                .ToConstant(this._smallAd)
                .WhenTargetHas<SmallAdAttribute>()
                .InSingletonScope()
                .Named(AdProviderMapping.AD_PROVIDER_BINDING_SMALL);
            this.Bind<IAdProvider>()
                .ToConstant(this._largeAd)
                .WhenTargetHas<LargeAdAttribute>()
                .InSingletonScope();
            this.Bind<IAdProvider>()
                .ToConstant(this._largeAd)
                .InSingletonScope();

            //Sensors
            //this.Bind<IAccelerometerHelper>().To<AccelerometerHelper>().InSingletonScope();

            //Database
            this.Bind<IDatabaseConfiguration>().ToConstant(new DatabaseConfiguration(DB_CONNECTION_STRING)).InSingletonScope();

            //Get a new context on every request
            this.Bind<IHandHistoryDataContext>().To<HandHistoryDataContextWrapper>().InSingletonScope();
        }
    }
}
