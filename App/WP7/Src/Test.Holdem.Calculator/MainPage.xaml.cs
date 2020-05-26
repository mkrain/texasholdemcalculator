using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Silverlight.Testing;

namespace Test.Holdem
{
    public partial class MainPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        public void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            SystemTray.IsVisible = false;

            // Register NUnit as the provider for test metadata (attributes etc)
            UnitTestSystem.RegisterUnitTestProvider(
                    new Microsoft.Silverlight.Testing.UnitTesting.Metadata.VisualStudio.VsttProvider());

            var testPage = UnitTestSystem.CreateTestPage() as IMobileTestPage;

            if (testPage == null)
            {
                return;
            }

            this.BackKeyPress +=
                (x, xe) =>
                {
                    //testPage.NavigateBack();
                    xe.Cancel = testPage.NavigateBack();

                };

            ((PhoneApplicationFrame)Application.Current.RootVisual).Content = testPage;
        }
    }
}