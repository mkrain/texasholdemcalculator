using System;

namespace TexasHoldemCalculator.Core.Entities.Statistics
{
    public class MaxBetException : Exception
    {
        public MaxBetException(string message) : base(message)
        {
            
        }

        public MaxBetException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
