using System.ComponentModel;
using GalaSoft.MvvmLight.Command;

namespace TexasHoldemCalculator.Interfaces.Model
{
    public class StartingHandContext : IStartingHandsContext, INotifyPropertyChanged
    {
        private string _startingHand;

        public string Title
        {
            get
            {
                return _startingHand;
            }
            set
            {
                if (_startingHand == value)
                    return;

                _startingHand = value;
                OnPropertyChanged("Title");
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected == value)
                    return;

                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public RelayCommand<string> DeleteHandCommand
        {
            get; private set; 
        }

        public RelayCommand<StartingHandContext> SelectedHandCommand
        {
            get;
            private set;
        }

        public StartingHandContextType HandType { get; set; }

        public StartingHandContext(string title, bool isSelected)
        {
            this.HandType = StartingHandContextType.Regular;
            this.Title = title;
            this.IsSelected = isSelected;
        }

        public StartingHandContext(string title, bool isSelected, RelayCommand<string> deleteHandCommand)
        {
            this.Title = title;
            this.IsSelected = isSelected;
            this.DeleteHandCommand = deleteHandCommand;
        }

        public StartingHandContext(string title, bool isSelected, RelayCommand<string> deleteHandCommand, RelayCommand<StartingHandContext> selectHandCommand)
        {
            this.Title = title;
            this.IsSelected = isSelected;
            this.DeleteHandCommand = deleteHandCommand;
            this.SelectedHandCommand = selectHandCommand;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}