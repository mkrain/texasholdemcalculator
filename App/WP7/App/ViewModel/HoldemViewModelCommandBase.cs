using Common.Core.Configuration;
using TexasHoldemCalculator.Core.Entities.StartingHands;
using TexasHoldemCalculator.Core.Statistics;

namespace TexasHoldemCalculator.ViewModel
{
	public abstract class HoldemViewModelCommandBase : HoldemViewModelVisibilityBase
	{
		#region Instance Variables

        private readonly IPhoneConfiguration _configuration;
        private readonly IntRangeDataSource _playersData;

		#endregion //Instance Variables

		#region Public Properties

        public IntRangeDataSource PlayersData
        {
            get
            {
                return _playersData;
            }
        }

		#endregion //Public Properties

		#region Protected Properties

		protected IPhoneConfiguration Configuration
		{
			get { return _configuration; }
		}
		
		#endregion //Protected Properties

		#region Constructor

        protected HoldemViewModelCommandBase(IPhoneConfiguration configuration)
		{
            _configuration = configuration;
            
            _playersData = new IntRangeDataSource(2, HoldemStatisticsBase.MaxPlayers);
		}

		#endregion //Constructor
	}
}