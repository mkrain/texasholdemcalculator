using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.Generator
{
	public interface ICardGenerator
	{
		CardValue GetCard(int cardId);

		CardValue GetCard(string card, string suit);

        CardValue GetCard(string card, string suit, HoldemCard holdemCard);

		/// <summary>
		/// 
		/// Generates a random suit.
		/// 
		/// </summary>
		/// <returns></returns>
		Suit GenerateSuit();

		Suit GenerateSuit(string suit);
	}
}