using System;
using System.Windows.Controls;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
    public class StartingHandsSelectedHandEventArgs : EventArgs
    {
        private readonly Button _selectedHand;

        public Button SelectedHand
        {
            get { return _selectedHand; }
        }

        public StartingHandsSelectedHandEventArgs(Button selectedHand)
        {
            _selectedHand = selectedHand;
        }
    }
}
