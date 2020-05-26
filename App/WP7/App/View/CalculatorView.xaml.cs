using System;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Reactive;
using Microsoft.Phone.Shell;
using TexasHoldemCalculator.Interfaces;
using TexasHoldemCalculator.Service;
using SplashPopup = TexasHoldemCalculator.Core.Controls.SplashPopup;

namespace TexasHoldemCalculator.View
{
    public partial class CalculatorView
    {
        #region Variables

        private static bool _loaded;
        private Popup _popup;

        #endregion

        #region Properties

        private static IDictionary<string, object> PhoneState
        {
            get
            {
                return PhoneApplicationService.Current.State;
            }
        }

        private IAdProvider AdProvider
        {
            get
            {
                return Factory.Instance.GetInstance<IAdProvider>();
            }
        }

        #endregion //Properties

        #region Constructor

        public CalculatorView()
        {
            if (PhoneState.ContainsKey("Loaded"))
            {
                _loaded = (bool) PhoneState["Loaded"];
            }

            if (!_loaded)
            {
                this.ShowPopup();
            }
            else
            {
                this.InitializeComponent();
                this.SetAdInformation();
            }
        }

        private void SetAdInformation()
        {
            this.THCStatisticsAd.AdUnitId = AdProvider.AdUnitId;
            this.THCStatisticsAd.ApplicationId = AdProvider.ApplicationId;
        }

        #endregion //Constructor

        #region Private Methods

        private void ShowPopup()
        {
            _popup =
                new Popup
                {
                    Child = new SplashPopup(),
                    IsOpen = true
                };

            StartLoadingData();
        }

        private void StartLoadingData()
        {
            if( _loaded )
                return;

            Scheduler.Dispatcher.Schedule(
                () =>
                {
                    if( _loaded )
                        return;

                    this.InitializeComponent();
                    this.SetAdInformation();

                    Scheduler.Dispatcher.Schedule(
                        this.ClosePop,
                        TimeSpan.FromSeconds(2));
                });
        }

        private void ClosePop()
        {
            _loaded = true;
            _popup.IsOpen = false;

            PhoneState["Loaded"] = _loaded;

            this.ApplicationBar.IsVisible = true;
        }

        #endregion //Private Methods
    }
}