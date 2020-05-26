using System.Collections.Generic;

namespace TexasHoldemCalculator.Interfaces.ReplayEngine
{
	public interface IReplayEngineHost
	{
		IEnumerable<string> EngineNames { get; }

		IEnumerable<IReplayEngine> ReplayEngines { get; }
	}
}