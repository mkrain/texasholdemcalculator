using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows;
using Common.Core.Configuration.Attributes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Marketplace;
using Ninject.Modules;
using TexasHoldemCalculator.Core.Generator;
using TexasHoldemCalculator.Core.Provider;
using TexasHoldemCalculator.Core.Provider.Ads;
using TexasHoldemCalculator.Core.Provider.Database;
using TexasHoldemCalculator.Core.Resource;
using TexasHoldemCalculator.Core.Security;
using TexasHoldemCalculator.Core.Statistics;
using TexasHoldemCalculator.Interfaces;
using TexasHoldemCalculator.Interfaces.Database;
using TexasHoldemCalculator.Interfaces.Generator;
using TexasHoldemCalculator.Interfaces.Resource;
using TexasHoldemCalculator.Interfaces.Security;
using TexasHoldemCalculator.Interfaces.Service;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Service.Modules
{
    public sealed class HoldemServiceCommonInjector : NinjectModule
    {
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
        private const string AD_UNIT_ID_SMALL = "10024302";
        private const string APPLICATION_ID_SMALL = "a3cfac8c-8dea-40c7-8b43-e42043da7356";
        private const string AD_UNIT_ID_LARGE = "10024571";
        private const string APPLICATION_ID_LARGE = "a3cfac8c-8dea-40c7-8b43-e42043da7356";
#endif

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
            this.NavigationService = Application.Current.RootVisual as PhoneApplicationFrame;

            _smallAd = new SmallAdProvider(AD_UNIT_ID_SMALL, APPLICATION_ID_SMALL);
            _largeAd = new LargeAdProvider(AD_UNIT_ID_LARGE, APPLICATION_ID_LARGE);
        }

        public override void Load()
        {
            if(!this._inDesignMode)
            {
                this.SetDefaultVaues();
            }

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
            this.Bind<IEncryptionProvider>().To<EncryptionProvider>().InSingletonScope();

            if( !_inDesignMode )
			{
                this.Bind<IHoldemNavigationService>()
				    .ToConstant(new HoldemNavigationService(this.NavigationService)).InSingletonScope();
            }

			this.Bind<IGeneratorService>().To<HoldemGeneratorService>().InSingletonScope();
			this.Bind<IProviderService>().To<HoldemProviderService>().InSingletonScope();
			this.Bind<IHoldemStatisticsService>().To<HoldemStatisticsService>().InSingletonScope();

            //Default Ad Provider
            this.Bind<IAdProvider>()
                .ToConstant(_smallAd)
                .WhenTargetHas<SmallAdAttribute>()
                .InSingletonScope()
                .Named(AdProviderMapping.AD_PROVIDER_BINDING_SMALL);
            this.Bind<IAdProvider>()
                .ToConstant(_largeAd)
                .WhenTargetHas<LargeAdAttribute>()
                .InSingletonScope();
            this.Bind<IAdProvider>()
                .ToConstant(_largeAd)
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