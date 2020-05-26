using System.Collections.Generic;
using TexasHoldemCalculator.Interfaces.Configuration;

namespace TexasHoldemCalculator.Core.Entities.Configuration
{
	public sealed class HoldemConfigOptions : IHoldemConfigOptions
	{
		#region Implementation of IHoldemConfigOptions

		public int HoldemHandCalcPrecision
		{
			get;
			set;
		}

		public int HandHistorySkipScale
		{
			get;
			set;
		}

		public int HandHistorySkipLargeChange
		{
			get;
			set;
		}

		public int HandHistorySkipSmallChange
		{
			get;
			set;
		}

		public int HandHistoryPlayerCount
		{
			get;
			set;
		}

		public bool HandHistoryRecursion
		{
			get;
			set;
		}

		public string HandHistoryLastSearchDir
		{
			get;
			set;
		}

		public string HandHistorySearchUsername
		{
			get;
			set;
		}

		public string HoldemHandCardType
		{
			get;
			set;
		}

		public string HoldemHandThemeDirectory
		{
			get;
			set;
		}

		public string HandHistorySearchFilter
		{
			get;
			set;
		}

		public IEnumerable<string> AvailableThemes
		{
			get;
			set;
		}

		public string CurrentTheme
		{
			get;
			set;
		}

		public bool HandHistoryPreview
		{
			get;
			set;
		}

		public bool HoldemHandPreview
		{
			get;
			set;
		}

		public bool GeneralSuppressPopup
		{
			get;
			set;
		}

		public bool HoldemHandAutoWrite
		{
			get;
			set;
		}

		public string HandHistorySaveDirectory
		{
			get;
			set;
		}

		public string HandHistoryReplayEngineAsmDirectory
		{
			get;
			set;
		}

		public string HandHistorySelectedReplayEngine
		{
			get;
			set;
		}

		#endregion
	}
}