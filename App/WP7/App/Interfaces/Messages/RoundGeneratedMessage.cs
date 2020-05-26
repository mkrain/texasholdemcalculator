using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.Messages
{
    public class RoundGeneratedMessage : Message<HoldemCard>
    {
        public RoundGeneratedMessage(HoldemCard card) : base(card)
        {
            
        }
    }
}
