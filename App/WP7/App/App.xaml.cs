using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;


namespace TexasHoldemCalculator
{
    public partial class App
    {
        private static NavigationHelper _service;

        // Easy access to the root frame
        private static PhoneApplicationFrame RootFramePage
        {
            get;
            set;
        }

        private static Uri CurrentPage
        {
            get
            {
                return RootFramePage.CurrentSource;
            }
        }

        public App()
        {
#if DEBUG
            //Current.Host.Settings.EnableFrameRateCounter = true;
            //Current.Host.Settings.EnableRedrawRegions = true;
            //Current.Host.Settings.EnableCacheVisualization = true;

            // Display the metro grid helper.
            MetroGridHelper.IsVisible = true;
#endif

            this.UnhandledException += Application_UnhandledException;
            this.Startup += AppStartup;

            this.InitializeComponent();

            this.InitializePhoneApplication();
        }

        private static void AppStartup(object sender, StartupEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// Code to execute when the application is launching (eg, from Start)
        /// This code will not execute when the application is reactivated
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationLaunching(object sender, LaunchingEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// Code to execute when the application is activated (brought to foreground)
        /// This code will not execute when the application is first launched
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationActivated(object sender, ActivatedEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// Code to execute when the application is deactivated (sent to background)
        /// This code will not execute when the application is closing
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationDeactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void ApplicationClosing(object sender, ClosingEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// Code to execute if a navigation fails
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// 
        /// Code to execute on Unhandled Exceptions
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool _phoneApplicationInitialized;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (_phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFramePage = new TransitionFrame();
            RootFramePage.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFramePage.NavigationFailed += RootFrame_NavigationFailed;

            _service = NavigationHelper.Instance;
            _service.InitializeRootFrame(RootFramePage);

            // Ensure we don't initialize again
            _phoneApplicationInitialized = true;

            DispatcherHelper.Initialize();
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            //if (RootVisual != RootFramePage)
            RootVisual = RootFramePage;

            // Remove this handler since it is no longer needed
            RootFramePage.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion

        #region Application Bar Events

        protected virtual void CalculaterMenuItemClick(object sender, EventArgs e)
        {
            NavigateToPageRequest("Calculator");
        }

        protected virtual void HistoryMenuItemClick(object sender, EventArgs e)
        {
            NavigateToPageRequest("History");
        }

        protected virtual void StatisticsMenuItemClick(object sender, EventArgs e)
        {
            NavigateToPageRequest("Statistics");
        }

        protected virtual void StartingHandsSelectionMenuItemClick(object sender, EventArgs e)
        {
            NavigateToPageRequest("StartingHandsSelection");
        }

        protected virtual void HoleOddsMenuItemClick(object sender, EventArgs e)
        {
            NavigateToPageRequest("HoleOdds");
        }

        protected virtual void OptionsMenuItemClick(object sender, EventArgs e)
        {
            NavigateToPageRequest("OptionsMain");
        }

        protected virtual void AboutMenuItemClick(object sender, EventArgs e)
        {
            _service.NavigateToRelativePageRequest("/YourLastAboutDialog;component/AboutPage.xaml");
        }

        private static void NavigateToPageRequest(string page)
        {
            if(CurrentPage.OriginalString.Contains(page))
            {
                return;
            }

            if (_service == null)
            {
                _service = NavigationHelper.Instance;
                _service.InitializeRootFrame(RootFramePage);
            }

            _service.NavigateToRelativePageRequest(string.Format("/View/{0}View.xaml", page));
        }

        #endregion //Application Bar Events
    }
}