
using Holdem.Interfaces.Service;

namespace HandGenerator.Entities.Model
{
    public abstract class HoldemModel
    {
		private readonly IConfigurationService _configService;
    	private readonly IHoldemStatisticsService _statsService;

		public IConfigurationService ConfigService
        {
			get { return _configService; }
        }

		public IHoldemStatisticsService HoldemStatistics
        {
            get { return _statsService; }
        }

        protected HoldemModel(IConfigurationService configService, IHoldemStatisticsService statsService)
        {
        	_configService = configService;
        	_statsService = statsService;
        }
    }
}
