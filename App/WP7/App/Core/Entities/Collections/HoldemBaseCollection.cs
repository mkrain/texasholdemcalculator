using System.Collections.Generic;

namespace TexasHoldemCalculator.Core.Entities.Collections
{
    public class HoldemBaseCollection<T> : List<T>
    {
        public HoldemBaseCollection()
        {

        }

        public HoldemBaseCollection(int capacity) : base(capacity)
        {

        }

        public HoldemBaseCollection(IEnumerable<T> initial) : base(initial)
        {

        }
    }
}