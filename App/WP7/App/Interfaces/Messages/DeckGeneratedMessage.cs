using TexasHoldemCalculator.Interfaces.HandHistory;

namespace TexasHoldemCalculator.Interfaces.Messages
{
    public class DeckGeneratedMessage : Message<History>
    {
        public DeckGeneratedMessage(History deck)
        {
            base.Data = deck;
        }
    }
}
