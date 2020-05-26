namespace TexasHoldemCalculator.Interfaces.ReplayEngine
{
	public interface IReplayEngineStrategy<T, TK>
	{
		TK GetStrategy(T criterion);
	}
}