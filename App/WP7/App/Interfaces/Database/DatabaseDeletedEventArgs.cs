using System;

namespace TexasHoldemCalculator.Interfaces.Database
{
    public class DatabaseDeletedEventArgs : EventArgs
    {
        private readonly DeleteStatus _deleteStatus;

        public DeleteStatus DeleteStatus
        {
            get { return _deleteStatus; }
        }

        public DatabaseDeletedEventArgs(DeleteStatus deleteStatus)
        {
            _deleteStatus = deleteStatus;
        }
    }

    public enum DeleteStatus
    {
        NoStatus,
        Deleted,
        NotDeletedDoesNotExist,
        NotDeletedError,
    }
}