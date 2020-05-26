using TexasHoldemCalculator.Interfaces.StartingHands;

namespace TexasHoldemCalculator.Interfaces.Messages
{
    public class DeletedCustomHandMessage : Message<StartingHandDeletedDescription>
    {
        public DeletedCustomHandMessage(StartingHandDeletedDescription description) : base(description)
        {
            
        }
    }
}