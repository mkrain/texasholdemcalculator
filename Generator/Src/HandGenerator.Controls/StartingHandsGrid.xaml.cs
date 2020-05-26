using System.Windows;
using System.Windows.Controls;

namespace HandGenerator.Controls
{
    public partial class StartingHandsGrid
    {
        public event RoutedEventHandler StartingHandButtonClick;

        public ItemsControl ItemsControl
        {
            get
            {
                return this.StartingHandsItemControl;
            }
        }

        public StartingHandsGrid()
        {
            InitializeComponent();
        }

        private void StartingHandSelectionClick(object sender, RoutedEventArgs e)
        {
            if (StartingHandButtonClick != null)
                StartingHandButtonClick(sender, e);
        }

        public int GetButtonRow(Button button)
        {
            return Grid.GetRow(button);
        }

        public int GetButtonCol(Button button)
        {
            return Grid.GetColumn(button);
        }
    }
}