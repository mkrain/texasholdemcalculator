using Ninject.Modules;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Model.Statistics;
using TexasHoldemCalculator.Model;
using TexasHoldemCalculator.Model.Statistics;

namespace TexasHoldemCalculator.Service.Modules
{
	public class HoldemModelInjector : NinjectModule
	{
		#region Overrides of NinjectModule

		public override void Load()
		{
		    this.Bind<IHoldemCalculatorModel>().To<HoldemCalculatorModel>().InSingletonScope();
			this.Bind<IHoldemInformationModel>().To<HoldemInformationModel>().InSingletonScope();
			this.Bind<IStartingHandsModel>().To<StartingHandsModel>().InSingletonScope();
			this.Bind<IHoldemHandHistoryModel>().To<HoldemHandHistoryModel>().InSingletonScope();
		    this.Bind<IHoldemAboutTipModel>().To<HoldemAboutTipModel>().InSingletonScope();

            //Statistics
		    this.Bind<IHoldemStatisticsPotOddsModel>().To<HoldemStatisticsPotOddsModel>().InSingletonScope();
            this.Bind<IHoldemStatisticsPocketPairModel>().To<HoldemStatisticsPocketPairModel>().InSingletonScope();
            this.Bind<IHoldemStatisticsRunnerModel>().To<HoldemStatisticsRunnerModel>().InSingletonScope();
			this.Bind<IHoldemStatisticsKickerModel>().To<HoldemStatisticsKickerModel>().InSingletonScope();
		}

		#endregion
	}
}