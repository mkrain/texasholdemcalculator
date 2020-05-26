using System;
using System.Data.Linq;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Database;
using TexasHoldemCalculator.Interfaces.HandHistory;

namespace TexasHoldemCalculator.Core.Provider.Database
{
    public class HandHistoryDataContext : DataContext
    {
        public Table<History> History;
        public Table<CardValue> Cards;

        #region Implementation of IHandHistoryDataContext

        public Table<History> HandHistoryTable
        {
            get { return this.GetTable<History>(); }
        }

        public Table<CardValue> CardsTable
        {
            get { return this.GetTable<CardValue>(); }
        }

        #endregion
            
        public HandHistoryDataContext(IDatabaseConfiguration databaseConfiguration) : base(databaseConfiguration.ConnectionString)
        {
            //base.ObjectTrackingEnabled = false;

            if( databaseConfiguration == null )
                throw new ArgumentNullException("databaseConfiguration");
        }
    }
}