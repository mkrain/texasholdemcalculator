using TexasHoldemCalculator.Interfaces.HandHistory;

namespace TexasHoldemCalculator.Core.Entities.Cards
{
    public sealed class HandReplayInfo : IHandReplayInfo
    {
        public string GameIdTag
        {
            get;
            set;
        }

        public string TournamentIdTag
        {
            get;
            set;
        }

        public string TableIdTag
        {
            get;
            set;
        }

        public string HoleCardsTag
        {
            get;
            set;
        }

        public string FlopTag
        {
            get;
            set;
        }

        public string TurnTag
        {
            get;
            set;
        }

        public string RiverTag
        {
            get;
            set;
        }

        public string ShowdownTag
        {
            get;
            set;
        }

        public string SummaryTag
        {
            get;
            set;
        }

        public string FinalBoardTag
        {
            get;
            set;
        }

        public string GameHeaderTag
        {
            get;
            set;
        }

        public string WonPotAmountTag
        {
            get;
            set;
        }

        public string FinalPotAmountTag
        {
            get;
            set;
        }
    }
}