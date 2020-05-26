using System;
using System.Collections.Generic;
using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.Interfaces.Service
{
	public interface IReplayEngineService
	{
		IList<string> AvailableReplayEngines
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// The callback for resolving IReplayEngine.  This is defined elsewhere.
		/// 
		/// </summary>
		Func<string, IReplayEngine> ReplayEngineByString
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// The callback for resolving IReplayEngine.  This is defined elsewhere.
		/// 
		/// </summary>
		Func<Type, IReplayEngine> ReplayEngineByType
		{
			get;
			set;
		}

		IReplayEngine GetReplayEngineByString(string typeOfEngine);
		IReplayEngine GetReplayEngineByType(Type typeOfEngine);
	}
}