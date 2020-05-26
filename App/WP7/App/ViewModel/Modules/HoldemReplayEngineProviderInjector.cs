using TexasHoldemCalculator.Interfaces;
using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.ViewModel.Modules
{
	public class HoldemReplayEngineProviderInjector : NinjectModule
	{
		private readonly IReplayEngineHost _replayEngineHost;

		public HoldemReplayEngineProviderInjector(IReplayEngineHost host)
		{
			this._replayEngineHost = host;
		}

		#region Overrides of NinjectModule

		public override void Load()
		{
			//For now this is used for all replay engines.
			//TODO: Create one for web based hand information,
			//      the pokerstars replay engine.

			Bind<IReplayEngineHost>().ToConstant(this._replayEngineHost).InSingletonScope();

			Bind<IReplayEngineConfiguration>().To<ReplayEngineConfiguration>().InSingletonScope();

			//History ReplayEngine Provider
			Bind<IReplayEngineProvider>()
				.To<HandHistoryReplayEngineProvider>()
				.InSingletonScope()
				.Named(ReplayEngineProviderMapping.FILE_SYSTEM);

			Bind<IReplayEngineStrategy<string, IReplayEngine>>().To<ReplayEngineDisplayNameStrategy>().InSingletonScope();

			Bind<IHandHistoryReplayEngine>().To<HoldemHandHistoryWriter>().InSingletonScope();
		}

		#endregion
	}
}