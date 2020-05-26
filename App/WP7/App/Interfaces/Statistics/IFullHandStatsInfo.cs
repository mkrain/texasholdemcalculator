
namespace TexasHoldemCalculator.Interfaces.Statistics
{
    public interface IFullHandStatsInfo
    {
        IHandStats FlopHandStats
        {
            get;
            set;
        }

        IHandStats TurnHandStats
        {
            get;
            set;
        }

        IHandStats RiverHandStats
        {
            get;
            set;
        }
    }
}
