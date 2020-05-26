using System.Collections.Generic;
using Common.Core.Configuration;
using GalaSoft.MvvmLight.Messaging;
using TexasHoldemCalculator.Core.Entities.Collections;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.HandHistory;
using TexasHoldemCalculator.Interfaces.Messages;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.Model
{
	public class HoldemHandHistoryModel : IHoldemHandHistoryModel
	{
		private readonly IReplayEngineStrategy<string, IReplayEngine> _engineStrategy;
		private readonly IReplayEngineProvider _engineProvider;
		private readonly IPhoneConfiguration _configuration;

		/// <summary>
		/// 
		/// Returns the last loaded engine, after calling GetSelectedReplayEngine().
		/// 
		/// </summary>
		public IReplayEngine LastLoadedEngine
		{
			get;
			private set;
		}

		#region Constructor

		public HoldemHandHistoryModel(
			IReplayEngineStrategy<string, IReplayEngine> engineStrategy,
			IReplayEngineProvider engineProvider,
			IPhoneConfiguration configuration)
		{
			_engineStrategy = engineStrategy;
			_engineProvider = engineProvider;
			_configuration = configuration;
		}

		#endregion //Constructor

		#region IHoldemHandHistoryModel Implementation

	    public bool HasHistory()
	    {
	        this.SetCurrentReplayEngine();

	        return this.LastLoadedEngine.HasHandReplay();
	    }

	    public void GetHandHistory()
        {
            IEnumerable<History> handHistory = new HandHistoryWriterCollection();

            SetCurrentReplayEngine();

            if(this.LastLoadedEngine != null)
            {
                handHistory = this.LastLoadedEngine.GetHandReplay(_engineProvider);
            }

            Messenger.Default.Send(new HandHistoryGeneratedMessage(handHistory));
        }

	    private void SetCurrentReplayEngine()
        {
            var savedSelection = _configuration.Get<string>(ConfigKey.View.Options.SelectedReplayEngine);

            this.LastLoadedEngine = _engineStrategy.GetStrategy(savedSelection);
        }

		#endregion //IHoldemHandHistoryModel Implementation
	}
}