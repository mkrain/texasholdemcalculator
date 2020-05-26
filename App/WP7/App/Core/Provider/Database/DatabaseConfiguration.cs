using System;
using TexasHoldemCalculator.Interfaces.Database;

namespace TexasHoldemCalculator.Core.Provider.Database
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        private readonly string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public DatabaseConfiguration(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            _connectionString = connectionString;
        }
    }
}