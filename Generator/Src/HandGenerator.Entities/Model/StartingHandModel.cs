using System;
using System.Linq;

using Holdem.Interfaces.Configuration;
using Holdem.Interfaces.Model;
using Holdem.Interfaces.Service;
using Holdem.Interfaces.StartingHands;

namespace HandGenerator.Entities.Model
{
    public class StartingHandsModel : IStartingHandsModel
    {
        private readonly string _defaultStartingHand;
		private readonly IConfigurationService _configService;
    	private readonly IStartingHandsManager _handsManager;

        private string SelectedStartingHandConfig
        {
            get
            {
                var savedConfig =
                    Config.Get(ConfigKey.HoldemHandsViewSelectedStartingHand) ??
                    _defaultStartingHand;

                return savedConfig;
            }
        }

        private IHoldemConfiguration<ConfigKey, string> Config
        {
            get
            {
				return _configService.PersistentConfiguration as IHoldemConfiguration<ConfigKey, string>;
            }
        }

        private IStartingHand SelectedStartingHand
        {
            get
            {
                var original = _handsManager.GetHandFromTitle(SelectedStartingHandConfig);

                return original;
            }

        }

		public StartingHandsModel(
			IConfigurationService configService, 
			IStartingHandsManager handsManager)
		{
			_configService = configService;
			_handsManager = handsManager;

            _defaultStartingHand = _handsManager.AvailableStartingHands.FirstOrDefault().Title;
        }

        public event EventHandler<HoldemStartingHandGeneratedEventArgs> StartingHandsGenerated;

        public void GenerateStartingHands()
        {
            if (this.StartingHandsGenerated != null)
            {
                this.StartingHandsGenerated(this, new HoldemStartingHandGeneratedEventArgs(SelectedStartingHand));
            }
        }
    }
}