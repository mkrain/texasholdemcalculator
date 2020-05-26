using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace TexasHoldemCalculator.Interfaces.Service
{
    public interface IProviderService
    {
        ICardThemeManager CardThemeManager
        {
            get;
        }

        IStartingHandsManager StartingHandsManager
        {
            get;
        }
    }
}
