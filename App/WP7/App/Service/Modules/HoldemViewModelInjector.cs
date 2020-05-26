using Ninject.Modules;
using TexasHoldemCalculator.ViewModel;
using TexasHoldemCalculator.ViewModel.Statistics;

namespace TexasHoldemCalculator.Service.Modules
{
    public class HoldemViewModelInjector : NinjectModule
    {
        public override void Load()
        {
            this.Bind<HoldemCalculatorViewModel>().ToSelf().InSingletonScope();
            this.Bind<HoldemHandHistoryViewModel>().ToSelf().InSingletonScope();
            this.Bind<HoldemOddsViewModel>().ToSelf().InSingletonScope();
            this.Bind<HoldemOptionsViewModel>().ToSelf().InSingletonScope();
            this.Bind<HoldemStartingHandsViewModel>().ToSelf().InSingletonScope();
            this.Bind<HoldemInformationViewModel>().ToSelf().InSingletonScope();
            this.Bind<HoldemSkyDriveViewModel>().ToSelf().InSingletonScope();
            this.Bind<HoldemAboutViewModel>().ToSelf().InSingletonScope();

            //Statistics
            this.Bind<HoldemStatisticsPotOddsViewModel>().ToSelf().InSingletonScope();
            this.Bind<HoldemStatisticsKickerViewModel>().ToSelf().InSingletonScope();
            this.Bind<HoldemStatisticsRunnerViewModel>().ToSelf().InSingletonScope();
            this.Bind<HoldemStatisticsPocketPairViewModel>().ToSelf().InSingletonScope();
        }
    }
}