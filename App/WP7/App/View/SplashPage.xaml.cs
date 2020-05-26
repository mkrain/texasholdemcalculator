using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;

using Microsoft.Phone.Shell;

namespace TexasHoldemCalculator.View
{
    public partial class SplashPage
    {
        private static BackgroundWorker _worker;
        private static bool _loaded;

        private static IDictionary<string, object> PhoneState
        {
            get
            {
                return PhoneApplicationService.Current.State;
            }
        }

        public SplashPage()
        {
            InitializeComponent();

            if (PhoneState.ContainsKey("Loaded"))
                _loaded = (bool)PhoneState["Loaded"];

            if (!_loaded)
                ShowPopup();
        }

        private void ShowPopup()
        {
            if (_worker == null)
                _worker = new BackgroundWorker();
            if (_loaded)
                return;

            _worker.DoWork +=
                (sender, e) =>
                    {
                        //Thread.Sleep(5000);
                    };

            _worker.RunWorkerCompleted +=
                (sender, e) =>
                    RunMethodAsync(
                        () =>
                        {
                            _loaded = true;

                            PhoneState["Loaded"] = _loaded;

                            NavigationHelper.Instance.NavigateToRelativePageRequest("/View/CalculatorView.xaml");
                        });

            _worker.RunWorkerAsync();
        }

        private static void RunMethodAsync(Action action)
        {
            Deployment.Current.Dispatcher.BeginInvoke(action);
        }
    }
}