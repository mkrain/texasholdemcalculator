using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Core.Configuration;
using TexasHoldemCalculator.Core.Entities.Collections;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Database;
using TexasHoldemCalculator.Interfaces.HandHistory;
using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.ReplayEngine
{
	public abstract class HoldemHandHistoryWriterBase : IHandHistoryReplayEngine
	{
        private readonly IHandHistoryDataContext _dataContext;
        private IReplayEngineProvider _provider;
        private readonly List<History> _cachedHands = new List<History>();
        private readonly IPhoneConfiguration _configuration;

		public abstract string DisplayName
		{
			get;
		}

		public IReplayEngineProvider Provider
		{
			get { return _provider; }
		}

	    public bool FlushHandHistory { get; set; }

        protected HoldemHandHistoryWriterBase(
            IHandHistoryDataContext dataContext,
            IPhoneConfiguration configuration,
            IReplayEngineProvider replayEngineProvider)
        {
            _provider = replayEngineProvider;
            _configuration = configuration;
            _dataContext = dataContext;
            _dataContext.HandHistoryDeletedAllEvent += this.HandHistoryDeleted;

            if(!_configuration.ContainsKey(ConfigKey.View.Options.UserName))
            {
                _configuration.Add(ConfigKey.View.Options.UserName, string.Empty);
            }
        }

        public bool HasHandReplay()
        {
            return !_dataContext.IsEmpty();
        }

	    #region IReplayEngine Implementation

		/// <summary>
		/// 
		/// 
		/// </summary>
		/// <param name="provider"></param>
		public void SetEngineProvider(IReplayEngineProvider provider)
		{
			if( provider == null )
				throw new ArgumentException("provider");

			_provider = provider;
		}

		/// <summary>
		/// 
		/// Returns a list of hand history based on the stream provided by the replay
		/// engine provider.
		/// 
		/// </summary>
		/// <returns>A list of hand history from the associated stream</returns>
        public IEnumerable<History> GetHandReplay(IReplayEngineProvider replayProvider)
		{
			return this.HandHistory;
		}

		#endregion //IReplayEngine Implementation

		#region Implementation of IHandHistoryWriter

        public IEnumerable<History> HandHistory
		{
            get { return _dataContext.GetAllHandHistory(); }
		}

	    public History LatestHistory
	    {
	        get { return _dataContext.GetLatestHandHistory(); }
	    }

	    public int HandCount
	    {
	        get { return HandHistory.Count(); }
	    }

        public void WriteHandHistory(History history)
        {
            if( history == null )
                return;

            if( history.Id != 0 )
                return;

            if( _dataContext.IsDeleting )
                _cachedHands.Add(history);
            else
                _dataContext.AddHandHistory(history);
        } 

	    public void WriteHandHistory(IEnumerable<History> handHistory)
        {
            if( handHistory == null )
                return;

            if( _dataContext.IsDeleting )
                _cachedHands.AddRange(handHistory);
            else
                _dataContext.AddHandHistories(handHistory);
        }

		public void DeleteHandHistory()
		{
            _dataContext.DeleteHandHistory();
		}

        public void DeleteHandHistory(History toDelete)
        {
            throw new NotImplementedException();
        }

		protected abstract HandHistoryWriterCollection LoadExistingHandHistory(Stream fileStream);

		protected abstract void SaveExistingHandHistory(Stream fileStream);

        private void HandHistoryDeleted(object sender, HandHistoryDeletedAllEventArgs e)
        {
            if( _cachedHands.Count <= 0 )
                return;

            this.WriteHandHistory(this._cachedHands);
            _cachedHands.Clear();
        }

		#endregion
	}
}