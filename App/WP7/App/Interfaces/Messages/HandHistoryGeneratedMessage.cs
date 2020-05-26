using System.Collections.Generic;
using TexasHoldemCalculator.Interfaces.HandHistory;

namespace TexasHoldemCalculator.Interfaces.Messages
{
    public class HandHistoryGeneratedMessage : Message<IEnumerable<History>>
    {
        public HandHistoryGeneratedMessage(IEnumerable<History> history) : base(history)
        {
            
        }
    }
}
