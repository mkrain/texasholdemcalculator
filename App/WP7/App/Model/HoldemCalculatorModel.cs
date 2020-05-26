using System;
using GalaSoft.MvvmLight.Messaging;
using TexasHoldemCalculator.Interfaces.Messages;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Service;

namespace TexasHoldemCalculator.Model
{
    public class HoldemCalculatorModel : IHoldemCalculatorModel
    {
		private readonly IGeneratorService _generator;


        public HoldemCalculatorModel(IGeneratorService generator)
		{
			if (generator == null)
				throw new ArgumentNullException("generator");

			_generator = generator;
		}

        public void GenerateHoldemDeck(int playerCount)
        {
            var generatedDeck = _generator.DeckGenerator.GenerateHoldemDeck(playerCount);

            Messenger.Default.Send(new DeckGeneratedMessage(generatedDeck));
        }
    }
}