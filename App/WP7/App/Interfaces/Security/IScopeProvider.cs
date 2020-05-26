
using System.Collections.Generic;

namespace TexasHoldemCalculator.Interfaces.Security
{
    public interface IScopeProvider
    {
        IEnumerable<string> Scopes { get; }
    }
}
