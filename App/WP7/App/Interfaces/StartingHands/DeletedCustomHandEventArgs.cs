using System;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
    public class DeletedCustomHandEventArgs : EventArgs
    {
        public string Title
        {
            get; private set; 
        }

        public FileDeleteStatus Status
        {
            get; private set;
        }

        public DeletedCustomHandEventArgs(string title, FileDeleteStatus status = FileDeleteStatus.Succeeded)
        {
            this.Title = title;
            this.Status = status;
        }
    }
}