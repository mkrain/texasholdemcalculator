using System.Windows;
using System.Windows.Controls;

namespace TexasHoldemCalculator.Core.Controls
{
    public partial class RoundedButton : UserControl
    {
        public string Text
        {
            get
            {
                return this.RoundInnerButton.Content as string;
            }
            set
            {
                this.RoundInnerButton.Content = value;
            }
        }

        public event RoutedEventHandler Click;

        public RoundedButton()
        {
            // Required to initialize variables
            InitializeComponent();

            this.RoundInnerButton.Click += this.RoundInnerButtonClick;
        }

        void RoundInnerButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
                this.Click(sender, e);
        }
    }
}
