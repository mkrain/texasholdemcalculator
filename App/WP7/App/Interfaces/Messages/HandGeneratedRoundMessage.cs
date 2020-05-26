using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.Messages
{
    public class HandGeneratedRoundMessage : Message<HoldemCard>
    {
        public HandGeneratedRoundMessage(HoldemCard card) : base(card)
        {
            
        }
    }
}