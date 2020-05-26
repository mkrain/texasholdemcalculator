using System.Collections.Generic;

namespace TexasHoldemCalculator.Interfaces.Model
{
    public interface IHoldemAboutTipModel
    {
        IEnumerable<HoldemAboutTip> Tips { get; }
    }
}