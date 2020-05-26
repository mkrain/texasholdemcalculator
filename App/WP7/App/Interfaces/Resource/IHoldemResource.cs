using System.Collections.Generic;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Interfaces.Resource
{
    public interface IHoldemResource
    {
       string GetString(string resource);

        IList<IHoleOdds> HoleOdds();
    }
}