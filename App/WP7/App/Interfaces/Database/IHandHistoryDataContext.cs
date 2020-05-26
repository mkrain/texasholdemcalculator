using System;
using System.Collections.Generic;
using System.Data.Linq;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.HandHistory;

namespace TexasHoldemCalculator.Interfaces.Database
{
    public interface IHandHistoryDataContext
    {
        int Count { get; }

        bool IsEmpty();

        bool IsDeleting { get; }

        Table<History> HandHistoryTable { get; }
        Table<CardValue> CardsTable { get; }

        void AddHandHistory(History history);
        void AddHandHistories(IEnumerable<History> handHistories);

        void DeleteHandHistory();
        void DeleteHandHistory(History history);

        IEnumerable<History> GetAllHandHistory();
        History GetLatestHandHistory();

        event EventHandler<DatabaseDeletedEventArgs> DatabaseDeletedEvent;
        event EventHandler<HandHistoryDeletedAllEventArgs> HandHistoryDeletedAllEvent;
        event EventHandler<HandHistoryDeletedEventArgs> HandHistoryDeletedEvent;
        event EventHandler<HandHistoryAddAllEventArgs> HandHistoryAddAllEvent;
        event EventHandler<HandHistoryAddedEventArgs> HandHistoryAddedEvent;
    }
}