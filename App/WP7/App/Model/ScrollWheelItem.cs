using TexasHoldemCalculator.Interfaces.Model;

namespace TexasHoldemCalculator.Model
{
    public class ScrollWheelItem : IScrollWheelItem
    {
        public int Number
        {
            get;
            set;
        }

        public bool IsVisible
        {
            get;
            set;
        }


        public string Text
        {
            get
            {
                return this.Number.ToString();
            }
            set
            {
                
            }
        }

        public ScrollWheelItem(int number)
        {
            this.Number = number;
        }

        public ScrollWheelItem(int number, bool isVisible)
        {
            this.Number = number;
            this.IsVisible = isVisible;
        }
    }
}
