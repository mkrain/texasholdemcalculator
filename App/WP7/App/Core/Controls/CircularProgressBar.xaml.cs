using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace TexasHoldemCalculator.Core.Controls
{
    public partial class CircularProgressBar : INotifyPropertyChanged
    {
        private readonly DispatcherTimer _timer;

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached(
                "Value",
                typeof(double),
                typeof(CircularProgressBar),
                new PropertyMetadata(null));

        public static readonly DependencyProperty IsRunningProperty =
            DependencyProperty.RegisterAttached(
                "IsRunning",
                typeof(bool),
                typeof(CircularProgressBar),
                new PropertyMetadata(null));

        public static readonly DependencyProperty RefreshRatioProperty =
            DependencyProperty.RegisterAttached(
                "RefreshRatio",
                typeof(double),
                typeof(CircularProgressBar),
                new PropertyMetadata(null));

        public double Value
        {
            get
            {
                return (double)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public bool IsRunning
        {
            get
            {
                return (bool)GetValue(IsRunningProperty);
            }
            set
            {
                SetValue(IsRunningProperty, value);

                if (value && !_timer.IsEnabled)
                    _timer.Start();
                else
                    _timer.Stop();
            }
        }

        public double RefreshRatio
        {
            get
            {
                return (double)GetValue(RefreshRatioProperty);
            }
            set
            {
                SetValue(RefreshRatioProperty, (value * 100) % 100);
            }
        }

        public CircularProgressBar()
        {
            InitializeComponent();

            this.InitializeComponent();

            _timer =
                new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(50)
                };
            _timer.Tick += this.TimerTick;

            CirProgressIndicator.DataContext = this;

            this.IsRunning = true;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            double newValue = this.Value + RefreshRatio;

            if (newValue > 100)
                newValue = 0;

            this.Value = newValue;

            this.OnPropertyChanged("Value");
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if( this.PropertyChanged != null )
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion
    }
}