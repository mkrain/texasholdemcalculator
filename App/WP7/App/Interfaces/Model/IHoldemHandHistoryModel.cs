using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.Interfaces.Model
{
	public interface IHoldemHandHistoryModel
	{
	    IReplayEngine LastLoadedEngine
	    {
	        get;
	    }

	    //void GetSelectedReplayEngine();

	    bool HasHistory();

        void GetHandHistory();

        //event EventHandler<HoldemHandHistoryGeneratedEventArgs> HandHistoryGenerated;
	    //event EventHandler<HoldemHandHistoryEngineGenerated> HandHistoryEngineGenerated;
	}
}
