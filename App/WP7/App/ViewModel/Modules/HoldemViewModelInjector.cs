using TexasHoldemCalculator.Interfaces.ViewModel;

namespace TexasHoldemCalculator.ViewModel.Modules
{
    public class HoldemViewModelInjector : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IHoldemCalculatorViewModel>().To<HoldemCalculatorViewModel>().InSingletonScope();
            this.Bind<IHoldemHandHistoryViewModel>().To<HoldemHandHistoryViewModel>().InSingletonScope();
            this.Bind<IHoldemOddsViewModel>().To<HoldemOddsViewModel>().InSingletonScope();
            this.Bind<IHoldemOptionsViewModel>().To<HoldemOptionsViewModel>().InSingletonScope();
            this.Bind<IStartingHandsViewModel>().To<HoldemStartingHandsViewModel>().InSingletonScope();
            this.Bind<IHoldemInformationViewModel>().To<HoldemInformationViewModel>().InSingletonScope();
            this.Bind<IHoldemSkyDriveViewModel>().To<HoldemSkyDriveViewModel>().InSingletonScope();
            this.Bind<IHoldemAboutTipViewModel>().To<HoldemAboutViewModel>().InSingletonScope();

            //Statistics
            this.Bind<IHoldemStatisticsPotOddsViewModel>().To<HoldemStatisticsPotOddsViewModel>().InSingletonScope();
            this.Bind<IHoldemStatisticsPocketPairViewModel>().To<HoldemStatisticsPocketPairViewModel>().InSingletonScope();
            this.Bind<IHoldemStatisticsKickerViewModel>().To<HoldemStatisticsKickerViewModel>().InSingletonScope();
            this.Bind<IHoldemStatisticsRunnerViewModel>().To<HoldemStatisticsRunnerViewModel>().InSingletonScope();
            this.Bind<IViewModel>().To<HoldemStatisticsPocketPairViewModel>().InSingletonScope();
        }
    }
}