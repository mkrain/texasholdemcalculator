using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Threading;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Database;
using TexasHoldemCalculator.Interfaces.HandHistory;

namespace TexasHoldemCalculator.Core.Provider.Database
{
    public class HandHistoryDataContextWrapper : IHandHistoryDataContext
    {
        private readonly IDatabaseConfiguration _databaseConfiguration;
        private bool _isDeleting;
        
        public HandHistoryDataContextWrapper(IDatabaseConfiguration databaseConfiguration)
        {
            if (databaseConfiguration == null)
                throw new ArgumentNullException("databaseConfiguration");

            _databaseConfiguration = databaseConfiguration;

            _currentContext = new HandHistoryDataContext(databaseConfiguration);

                if( !_currentContext.DatabaseExists() )
                     _currentContext.CreateDatabase();
        }

        #region Implementation of IHandHistoryDataContext

        public event EventHandler<DatabaseDeletedEventArgs> DatabaseDeletedEvent;
        public event EventHandler<HandHistoryDeletedAllEventArgs> HandHistoryDeletedAllEvent;
        public event EventHandler<HandHistoryDeletedEventArgs> HandHistoryDeletedEvent;
        public event EventHandler<HandHistoryAddAllEventArgs> HandHistoryAddAllEvent;
        public event EventHandler<HandHistoryAddedEventArgs> HandHistoryAddedEvent;

        private HandHistoryDataContext _currentContext;

        public int Count { get { return Queryable.Count(_currentContext.HandHistoryTable); } }

        public bool IsDeleting { get { return _isDeleting; } }

        public Table<History> HandHistoryTable
        {
            get { return _currentContext.HandHistoryTable; }
        }

        public Table<CardValue> CardsTable
        {
            get { return _currentContext.CardsTable; }
        }

        public void AddHandHistory(History history)
        {
            try
            {
                _currentContext = new HandHistoryDataContext(_databaseConfiguration);

                _currentContext.HandHistoryTable.InsertOnSubmit(history);
                _currentContext.SubmitChanges();

                if (this.HandHistoryAddedEvent != null)
                    this.HandHistoryAddedEvent(this, new HandHistoryAddedEventArgs(history));
            }
            catch (Exception e)
            {

            }
        }

        public void AddHandHistories(IEnumerable<History> handHistories)
        {
            try
            {
                using( var context = new HandHistoryDataContext(_databaseConfiguration) )
                {
                    context.HandHistoryTable.InsertAllOnSubmit(handHistories);
                    context.SubmitChanges();

                    if( this.HandHistoryAddAllEvent != null )
                        this.HandHistoryAddAllEvent(this, new HandHistoryAddAllEventArgs());
                }
            }
            catch (Exception e)
            {

            }
        }

        public void DeleteHandHistory()
        {
            try
            {
                _currentContext = new HandHistoryDataContext(_databaseConfiguration);

                if( this.IsEmpty() )
                    return;

                ThreadPool.QueueUserWorkItem(
                    x =>
                    {
                        _isDeleting = true;

                        var cards = ( from c in _currentContext.CardsTable
                                      select c );

                        _currentContext.CardsTable.DeleteAllOnSubmit(cards);
                        _currentContext.SubmitChanges();

                        var history = ( from s in _currentContext.HandHistoryTable
                                        select s );

                        _currentContext.HandHistoryTable.DeleteAllOnSubmit(history);
                        _currentContext.SubmitChanges();

                        _isDeleting = false;

                        if( this.HandHistoryDeletedAllEvent != null )
                            this.HandHistoryDeletedAllEvent(this, new HandHistoryDeletedAllEventArgs());
                    });
            }
            catch (Exception e)
            {

            }
        }

        public void DeleteHandHistory(History history)
        {
            try
            {
                _currentContext = new HandHistoryDataContext(_databaseConfiguration);

                var foundHistory = ( from s in _currentContext.HandHistoryTable
                                     where s.Id == history.Id
                                     select s ).FirstOrDefault();

                if( foundHistory == null )
                    return;

                _isDeleting = true;

                var cards = ( from s in _currentContext.CardsTable
                              where s.History.Id == history.Id
                              select s ).FirstOrDefault();

                if( cards != null )
                {
                    _currentContext.CardsTable.DeleteOnSubmit(cards);
                    _currentContext.SubmitChanges();
                }

                _currentContext.HandHistoryTable.DeleteOnSubmit(foundHistory);
                _currentContext.SubmitChanges();

                _isDeleting = false;

                if( this.HandHistoryDeletedAllEvent != null )
                    this.HandHistoryDeletedAllEvent(this, new HandHistoryDeletedAllEventArgs());
            }
            catch( Exception e )
            {

            }
        }

        public IEnumerable<History> GetAllHandHistory()
        {
            try
            {
                var context = new HandHistoryDataContext(_databaseConfiguration);

                var history = ( from s in context.HandHistoryTable
                                select s ).ToList();

                return history;
            }
            catch( Exception e )
            {
                return new List<History>();
            }
        }

        public History GetLatestHandHistory()
        {
            try
            {
                var context = new HandHistoryDataContext(_databaseConfiguration);

                var history = ( from s in context.HandHistoryTable
                                orderby s.Id descending
                                select s ).FirstOrDefault();

                return history;
            }
            catch( Exception e )
            {
                return null;
            }
        }

        public bool IsEmpty()
        {
            try
            {
                //If hand history is being deleted hide and existing hands
                return _isDeleting || !Queryable.Any(_currentContext.HandHistoryTable);    
            }
            catch( Exception e)
            {
                return false;
            }
        }

        #endregion
    }
}