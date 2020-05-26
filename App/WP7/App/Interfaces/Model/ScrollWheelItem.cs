
namespace TexasHoldemCalculator.Interfaces.Model
{
    public interface IScrollWheelItem : IIsVisible
    {
        int Number
        {
            get;
            set;
        }

        string Text
        {
            get;
            set;
        }
    }
}
