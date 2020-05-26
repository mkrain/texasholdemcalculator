using System.Collections.Generic;
using TexasHoldemCalculator.Interfaces.Security;

namespace TexasHoldemCalculator.Core.Security
{
    public class ScopeProvider : IScopeProvider
    {
        private static readonly IEnumerable<string> _scopes = new[] { "wl.signin, wl.basic, wl.offline_access, wl.skydrive" };

        public IEnumerable<string> Scopes
        {
            get
            {
                return _scopes;
            }
        }
    }
}
