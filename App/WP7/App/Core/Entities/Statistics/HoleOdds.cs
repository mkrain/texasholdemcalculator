using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using TexasHoldemCalculator.Interfaces.Provider;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Core.Entities.Statistics
{
    public class HoleOdds : IHoleOdds, INotifyPropertyChanged
    {
        private readonly IIconProvider _iconProvider;
        private bool _isExpanded;

        public string Description
        {
            get;
            set;
        }

        public IHoleOddsDetail Details
        {
            get;
            set;
        }

        public BitmapImage DisplayIcon
        {
            get
            {
                if (this.IsExpanded)
                    return _iconProvider.MinusIcon;
                return _iconProvider.PlusIcon;
            }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if( _isExpanded != value )
                {
                    _isExpanded = value;

                    this.RaisePropertyChanged("DisplayIcon");
                }
            }
        }

        public HoleOdds(IIconProvider iconProvider)
        {
            if (iconProvider == null)
                throw new ArgumentNullException("iconProvider");

            _iconProvider = iconProvider;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class HoleOddsDetails : IHoleOddsDetail
    {
        public string Percent
        {
            get;
            set;
        }

        public string Odds
        {
            get;
            set;
        }
    }
}