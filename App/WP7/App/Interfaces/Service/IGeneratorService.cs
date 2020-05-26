using TexasHoldemCalculator.Interfaces.Generator;

namespace TexasHoldemCalculator.Interfaces.Service
{
    public interface IGeneratorService
    {
        ICardGenerator CardGenerator
        {
            get;
        }

        IDeckGenerator DeckGenerator
        {
            get;
        }

        INumberGenerator NumberGenerator
        {
            get;
        }

        IPrimitiveGenerator PrimitiveGenerator
        {
            get;
        }
    }
}