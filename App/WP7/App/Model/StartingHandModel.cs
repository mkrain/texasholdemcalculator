using System.Linq;
using Common.Core.Configuration;
using GalaSoft.MvvmLight.Messaging;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Messages;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace TexasHoldemCalculator.Model
{
	public class StartingHandsModel : IStartingHandsModel
	{
		private readonly string _defaultStartingHand;
        private readonly IStartingHandsManager _startingHandsManager;
        private readonly IPhoneConfiguration _configuration;

		private string SelectedStartingHandConfig
		{
			get
			{
				var savedConfig =
                    _configuration.Get<string>(ConfigKey.View.StartingHands.SelectedStartingHand) ??
					_defaultStartingHand;

				return savedConfig;
			}
		}

		public StartingHandsModel(
			IStartingHandsManager startingHandsManager,
            IPhoneConfiguration configuration)
		{
            _startingHandsManager = startingHandsManager;
            _configuration = configuration;

            _defaultStartingHand = _startingHandsManager.AvailableStartingHandsByTitle.FirstOrDefault();
        }
		
        public void GenerateStartingHands()
        {
            var hand = _startingHandsManager.GetHandFromTitle(SelectedStartingHandConfig);

            Messenger.Default.Send(new StartingHandGeneratedMessage(hand));
        }
	}
}