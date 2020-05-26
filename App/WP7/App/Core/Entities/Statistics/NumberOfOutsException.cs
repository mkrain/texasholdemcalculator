using System;

namespace TexasHoldemCalculator.Core.Entities.Statistics
{
    public class NumberOfOutsException : Exception
    {
        public NumberOfOutsException(string message) : base(message)
        {
            
        }

        public NumberOfOutsException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
