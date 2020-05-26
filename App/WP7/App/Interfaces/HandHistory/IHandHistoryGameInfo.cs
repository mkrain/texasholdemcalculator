using System;
using TexasHoldemCalculator.Interfaces.Database;

namespace TexasHoldemCalculator.Interfaces.HandHistory
{
    public interface IHandHistoryGameInfo : IPersistable
    {
        string TournamentId { get; set; }

        string TableId { get; set; }

        bool WonHand { get; set; }

        string WonPotAmount { get; set; }

        string TotalPotAmount { get; set; }

        string GameDescription { get; set; }

        DateTime Date { get; set; }
    }
}