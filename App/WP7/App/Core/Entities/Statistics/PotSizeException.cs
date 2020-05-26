using System;

namespace TexasHoldemCalculator.Core.Entities.Statistics
{
    public class PotSizeException : Exception
    {
        public PotSizeException(string message) : base(message)
        {
            
        }

        public PotSizeException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
