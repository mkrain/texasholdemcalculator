namespace TexasHoldemCalculator.Interfaces.Messages
{
    public class HandHistoryLoadedMessage : Message<bool>
    {
        public HandHistoryLoadedMessage(bool loaded) : base(loaded)
        {
            
        }
    }
}