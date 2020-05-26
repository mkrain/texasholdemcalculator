using System.Collections.Generic;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.HandHistory
{
    public interface IHandHistoryDictionary : IDictionary<HoldemCard, CardValue>
	{
		
	}
}