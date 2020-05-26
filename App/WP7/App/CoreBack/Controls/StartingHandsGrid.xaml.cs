using System.Windows;
using System.Windows.Controls;
using Holdem.Interfaces.ViewModel;

namespace TexasHoldemCalculator.Core.Controls
{
    public partial class StartingHandsGrid : UserControl
    {
        private IStartingHandsViewModel _model;

        public static readonly DependencyProperty SelectedButtonProperty =
            DependencyProperty.Register(
                "SelectedButton",
                typeof(Button),
                typeof(StartingHandsGrid),
                new PropertyMetadata(null));

        public event RoutedEventHandler Click;

        public Button SelectedButton
        {
            get
            {
                return (Button)GetValue(SelectedButtonProperty);
            }
            set
            {
                SetValue(SelectedButtonProperty, value);
            }
        }

        public IStartingHandsViewModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                this.DataContext = _model.AllHands;
            }
        }

        public StartingHandsGrid()
        {
            InitializeComponent();
        }

        private void SelectedButonClick(object sender, RoutedEventArgs e)
        {
            this.SelectedButton = e.OriginalSource as Button;

            if (this.Click != null)
                this.Click(sender, e);
        }
    }
}
