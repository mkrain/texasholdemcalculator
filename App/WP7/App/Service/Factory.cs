using System.Collections.Generic;
using System.Linq;
using Common.Core.ServiceFactory;
using Ninject.Modules;
using TexasHoldemCalculator.Interfaces.ReplayEngine;
using TexasHoldemCalculator.Service.Modules;

namespace TexasHoldemCalculator.Service
{
	public sealed class Factory : Container, IReplayEngineHost
    {
        private static readonly Factory _factory;
		private static IList<string> _engineNames;


        static Factory()
        {
            _factory = new Factory();
        }

        public static Factory Instance
        {
            get
            {
                return _factory;
            }
        }

	    protected override IEnumerable<INinjectModule> Injectors()
	    {
	        return
	            new List<INinjectModule>
	                {
	                    new HoldemServiceFactoryInjector(),
	                    new HoldemCardThemeInjector(),
	                    new HoldemServiceCommonInjector(),
	                    new HoldemReplayEngineProviderInjector(this),
	                    new HoldemModelInjector(),
	                    new HoldemViewModelInjector()
	                };
	    }

		#region IReplayEngineHost

		public IEnumerable<IReplayEngine> ReplayEngines
		{
            get { return _factory.GetAllInstances<IReplayEngine>(); }
		}

		public IEnumerable<string> EngineNames
		{
			get
			{
				if(_engineNames == null || !_engineNames.Any())
				{
				    _engineNames = new List<string>(this.ReplayEngines.Select(x => x.DisplayName).Distinct());
				}

				return _engineNames;
			}
		}

		#endregion //IReplayEngineHost
    }
}