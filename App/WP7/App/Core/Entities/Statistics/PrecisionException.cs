using System;

namespace TexasHoldemCalculator.Core.Entities.Statistics
{
    public class PrecisionException : Exception
    {
        public PrecisionException(string message) : base(message)
        {
            
        }

        public PrecisionException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}
