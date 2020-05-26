using System;

namespace TexasHoldemCalculator.Core.Entities.Statistics
{
    public class NumberOfPlayersException : Exception
    {
        public NumberOfPlayersException(string message) : base(message)
        {
            
        }

        public NumberOfPlayersException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
