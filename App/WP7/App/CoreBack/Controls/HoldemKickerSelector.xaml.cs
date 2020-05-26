using System;
using System.Windows.Controls;
using Holdem.Interfaces.Card;
using Holdem.Interfaces.ViewModel;
using TexasHoldemCalculator.Core.Entities.Cards;

namespace TexasHoldemCalculator.Core.Controls
{
    public partial class HoldemKickerSelector : UserControl
    {
        public delegate void ClickHandler(object sender, EventArgs e);

        public event ClickHandler Click;
        private CardKicker _selectedKicker;

        public CardName CardName
        {
            get { return _selectedKicker.CardName; }
        }

        public HoldemKickerSelector()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button == null)
                return;

            var kicker = button.DataContext as CardKicker;

            _selectedKicker = kicker;

            if (this.Click != null)
                this.Click(kicker, new HoldemCardKickerEventArgs(kicker));
        }
    }
}
