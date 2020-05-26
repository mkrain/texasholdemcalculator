using TexasHoldemCalculator.Interfaces.StartingHands;

namespace TexasHoldemCalculator.Interfaces.Messages
{
    public class StartingHandGeneratedMessage : Message<IStartingHand>
    {
        public StartingHandGeneratedMessage(IStartingHand startingHand) : base(startingHand)
        {
            
        }
    }
}