
using TexasHoldemCalculator.Interfaces.HandHistory;

namespace TexasHoldemCalculator.Interfaces.ReplayEngine
{
    public interface IReplayEngineInfo
    {
        IHandReplayInfo HandReplayInformation
        {
            get;
            set;
        }

        ReplayAction GetReplayAction(string lineToCheck);

        string GetGameId(string lineInfo);
        string GetTournamentId(string lineInfo);
        string GetTableId(string lineInfo);
        string GetGameDescription(string lineInfo);
        string GetDateTime(string lineInfo);
        void SetHoleCards(IHandHistory history, string lineInfo);
        void SetFlop(IHandHistory history, string lineInfo);
        void SetTurn(IHandHistory history, string lineInfo);
        void SetRiver(IHandHistory history, string lineInfo);
        string GetShowdown(string lineInfo);
        string GetSummary(string lineInfo);
        string GetFinalBoard(string lineInfo);
        string GetGameHeader(string lineInfo);
        string GetWonPotAmount(string userName, string lineInfo);
        string GetTotalPotAmount(string lineInfo);
    }

    public enum ReplayAction
    {
        None,
        GameId,
        TournamentId,
        GameDescription,
        TableId,
        HoleCards,
        Flop,
        Turn,
        River,
        Showdown,
        Summary,
        FinalBoard,
        GameHeader,
        WonPotAmount,
        TotalPotAmount
    };

    public enum ReplayEngineType
    {
        PokerStarsEngine,
        FullTiltEngine
    };
}
