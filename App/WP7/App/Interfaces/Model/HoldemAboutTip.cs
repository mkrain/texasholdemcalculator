using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using TexasHoldemCalculator.Interfaces.Provider;

namespace TexasHoldemCalculator.Interfaces.Model
{
    public class HoldemAboutTip : INotifyPropertyChanged
    {
        private readonly IIconProvider _iconProvider;
        private bool _isExpanded;

        public string SummaryText { get; set; }

        public string HeaderText { get; set; }

        public List<string> Tips { get; set; }

        public BitmapImage DisplayIcon
        {
            get
            {
                if( Tips == null || Tips.Count == 0 )
                    return new BitmapImage();
                if( IsExpanded )
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

                    if (PropertyChanged != null )
                        PropertyChanged(this, new PropertyChangedEventArgs("DisplayIcon"));
                }
            }
        }

        public HoldemAboutTip(IIconProvider iconProvider)
        {
            if( iconProvider == null )
                throw new ArgumentNullException("iconProvider");

            _iconProvider = iconProvider;
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}