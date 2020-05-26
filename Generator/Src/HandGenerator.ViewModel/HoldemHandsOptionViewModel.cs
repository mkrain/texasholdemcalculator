using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

using HandGenerator.Entities.Model;
using HandGenerator.Phone.Supported;
using HandGenerator.Phone.Supported.Extensions;

using Holdem.Interfaces.Card;

namespace HandGenerator.ViewModel
{
    public class HoldemHandsOptionViewModel : INotifyPropertyChanged
    {
        private readonly IList<HoldemColor> _supportedColors;
        private CardValue _selectedCard;

        public event PropertyChangedEventHandler PropertyChanged;

        public HoldemColor SelectedPaletteColor
        {
            get;
            set;
        }

        public HoldemColor SelectedHandColor
        {
            get;
            set;
        }

        public IList<HoldemColor> SupportedColors
        {
            get
            {
                return _supportedColors;
            }
        }

        public int ColorCount
        {
            get
            {
                return _supportedColors.Count();
            }
        }

        public int Strength
        {
            get
            {
                return _selectedCard == null ? 0 : _selectedCard.Strength;
            }
            set
            {
                if (_selectedCard == null)
                    return;
                if (SelectedButton != null)
                    SelectedButton.Content = value.ToString();

                _selectedCard.Strength = value;


                this.NotifyPropertyChanged("Strength");
            }
        }

        public Color Highlight
        {
            get
            {
                return _selectedCard == null ? ColorNames.Transparent.FromName() : _selectedCard.Highlight;
            }
            set
            {
                if (_selectedCard == null)
                    return;
                if (SelectedButton != null)
                    SelectedButton.Background = new SolidColorBrush(value);

                _selectedCard.Highlight = value;

                this.NotifyPropertyChanged("Highlight");
            }
        }

        public bool IsVisible
        {
            get
            {
                return _selectedCard == null || this._selectedCard.IsVisible;
            }
            set
            {
                if (_selectedCard == null)
                    return;

                _selectedCard.IsVisible = value;
                this.NotifyPropertyChanged("IsVisible");
            }
        }

        public CardValue SelectedCard
        {
            get
            {
                return _selectedCard;
            }
            set
            {
                if (value == null)
                    return;

                _selectedCard = value;

                this.NotifyPropertyChanged("Strength");
                this.NotifyPropertyChanged("IsVisible");
            }
        }

        public Button SelectedButton
        {
            get;
            set;
        }

        public HoldemHandsOptionViewModel()
        {
            var enums = Enum.GetValues(typeof(ColorNames)).Cast<ColorNames>().ToList();

            _supportedColors =
                (from colors
                    in enums
                 orderby colors.ToString()
                 select new HoldemColor(colors)).ToList();
        }

        public HoldemColor SelectedHighlight(Color color)
        {
            var found = _supportedColors.FirstOrDefault(
                x => x.ColorFromName == color);

            return found;
        }

        #region Implementation of INotifyPropertyChanged

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
