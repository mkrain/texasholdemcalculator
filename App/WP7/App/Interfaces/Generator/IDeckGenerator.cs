
namespace TexasHoldemCalculator.Interfaces.Generator
{
	public interface IDeckGenerator
	{
		HandHistory.History GenerateHoldemDeck(int players);
	}
}