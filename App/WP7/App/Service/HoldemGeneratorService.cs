using System;
using TexasHoldemCalculator.Interfaces.Generator;
using TexasHoldemCalculator.Interfaces.Service;

namespace TexasHoldemCalculator.Service
{
	public class HoldemGeneratorService : IGeneratorService
	{
		#region Implementation of IGeneratorService

		private readonly ICardGenerator _cardGenerator;
		private readonly IDeckGenerator _deckGenerator;
		private readonly INumberGenerator _numberGenerator;
		private readonly IPrimitiveGenerator _primitiveGenerator;

		public ICardGenerator CardGenerator { get { return _cardGenerator; } }

		public IDeckGenerator DeckGenerator { get { return _deckGenerator; } }

		public INumberGenerator NumberGenerator { get { return _numberGenerator; } }

		public IPrimitiveGenerator PrimitiveGenerator { get { return _primitiveGenerator; } }

		#endregion

		public HoldemGeneratorService(
			ICardGenerator cardGenerator,
			IDeckGenerator deckGenerator, 
			INumberGenerator numberGenerator, 
			IPrimitiveGenerator primitiveGenerator)
		{
			if (cardGenerator == null)
				throw new ArgumentNullException("cardGenerator");
			if (deckGenerator == null)
				throw new ArgumentNullException("deckGenerator");
			if (numberGenerator == null)
				throw new ArgumentNullException("numberGenerator");
			if (primitiveGenerator == null)
				throw new ArgumentNullException("primitiveGenerator");

			_cardGenerator = cardGenerator;
			_deckGenerator = deckGenerator;
			_numberGenerator = numberGenerator;
			_primitiveGenerator = primitiveGenerator;
		}
	}
}