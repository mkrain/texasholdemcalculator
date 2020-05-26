using System;
using System.Linq;
using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.ReplayEngine
{
	public class ReplayEngineDisplayNameStrategy : IReplayEngineStrategy<string, IReplayEngine>
	{
		private readonly IReplayEngineHost _hosts;

		public ReplayEngineDisplayNameStrategy(IReplayEngineHost hosts)
		{
			if( hosts == null )
				throw new ArgumentNullException("hosts");

			_hosts = hosts;
		}

        public IReplayEngine GetStrategy(string criterion)
        {
            var foundEngine =
                ( from engine in _hosts.ReplayEngines
                  where engine.DisplayName.Equals(criterion)
                  select engine ).FirstOrDefault();

            return foundEngine;
        }
	}
}