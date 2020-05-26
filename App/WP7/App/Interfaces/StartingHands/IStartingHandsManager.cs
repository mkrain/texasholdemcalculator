using System.Collections.Generic;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
    public interface IStartingHandsManager
    {
        IList<string> AvailableStartingHandsByTitle { get; }

        IList<StartingHand> AvailableStartingHands { get; }

        StartingHand GetHandFromResource(string resourceName);

        StartingHand GetHandFromTitle(string title);

        /// <summary>
        ///   TODO: This writes a stream from HTTP to a file.
        ///   TODO: Should take some type of stream.
        /// </summary>
        FileSaveStatus WriteResourceToFile(StartingHandSaveContext context);

        void DeleteCustomStartingHand(StartingHand customHand);

        //event EventHandler<DeletedCustomHandEventArgs> CustomStartingHandDeleted;
    }

    public enum FileSaveStatus
    {
        Undefined = 0,
        Succeeded,
        FailedExisting,
        FailedWrite,
        FailedRead,
        FailedOther
    }

    public enum FileDeleteStatus
    {
        Undefined = 0,
        Succeeded,
        FailedMissing,
        FailedStorageError,
        FailedOther
    }
}
