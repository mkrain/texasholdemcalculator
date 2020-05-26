
namespace TexasHoldemCalculator.Interfaces.Model
{
    public interface IStartingHandsContext
    {
        string Title
        {
            get;
            set;
        }

        bool IsSelected
        {
            get;
            set;
        }

        StartingHandContextType HandType { get; set; }
    }

    public enum StartingHandContextType
    {
        Undefined = 0,
        Regular,
        Custom
    }
}