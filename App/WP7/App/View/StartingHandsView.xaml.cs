using System.Windows;
using TexasHoldemCalculator.Service;
using TexasHoldemCalculator.ViewModel;


namespace TexasHoldemCalculator.View
{
	public partial class StartingHandsView
	{
        public HoldemStartingHandsViewModel HandContext
	    {
            get { return Factory.Instance.GetInstance<HoldemStartingHandsViewModel>(); }
	    }

        public HoldemStartingHandsViewModel ViewModel
	    {
	        get { return HandContext as HoldemStartingHandsViewModel; }
	    }

		public StartingHandsView()
		{
		    InitializeComponent();

            this.Loaded += this.StartingHandsViewLoaded;
		}

        private void StartingHandsViewLoaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(
                () =>
                {
                    this.StartingHandsGrid.DataContext = ViewModel.AllHands;
                    this.HandPopup.Visibility = Visibility.Collapsed;
                });
        }

	    protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            ViewModel.OnBackKeyPress(e);
        }
	}
}
