namespace TexasHoldemCalculator.Interfaces.ReplayEngine
{
	public interface IReplayEngineFactory<T, TK>
	{
		TK GetReplayEngine(IReplayEngineStrategy<T, TK> strategy);
	}
}
