using System;

namespace TexasHoldemCalculator.Interfaces.Exceptions
{
    public class DuplicateHandException : Exception
    {
        public DuplicateHandException() : base("Duplicate starting hand")
        {

        }
    }
}