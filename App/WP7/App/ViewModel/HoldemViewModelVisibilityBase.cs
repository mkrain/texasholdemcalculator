using System;
using System.Collections.Generic;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TexasHoldemCalculator.ViewModel
{
    public abstract class HoldemViewModelVisibilityBase : ViewModelBase
    {
        private readonly Dictionary<string, CommandVisibilityMapping> 
            _commandVisibilityMappings = new Dictionary<string,CommandVisibilityMapping>();

        private const int MAX_DEFAULTS = 4;

        #region Public Properties

        public RelayCommand VisibilityCommand
        {
            get { return _commandVisibilityMappings["Visibility"].Command; }
        }

        public RelayCommand OutsVisibilityCommand
        {
            get { return _commandVisibilityMappings["OutsVisibility"].Command; }
        }

        public RelayCommand PlayersVisibilityCommand
        {
            get { return _commandVisibilityMappings["PlayersVisibility"].Command; }
        }

        public RelayCommand Visibility1Command
        {
            get { return _commandVisibilityMappings["Visibility1"].Command; }
        }

        public RelayCommand Visibility2Command
        {
            get { return _commandVisibilityMappings["Visibility2"].Command; }
        }

        public RelayCommand Visibility3Command
        {
            get { return _commandVisibilityMappings["Visibility3"].Command; }
        }

        public RelayCommand Visibility4Command
        {
            get { return _commandVisibilityMappings["Visibility4"].Command; }
        }

        public Visibility Visibility
        {
            get { return _commandVisibilityMappings["Visibility"].Visibility; }
            set { _commandVisibilityMappings["Visibility"].Visibility = value; }
        }

        public Visibility Visibility1
        {
            get { return _commandVisibilityMappings["Visibility1"].Visibility; }
            set { _commandVisibilityMappings["Visibility1"].Visibility = value; }
        }

        public Visibility Visibility2
        {
            get { return _commandVisibilityMappings["Visibility2"].Visibility; }
            set { _commandVisibilityMappings["Visibility2"].Visibility = value; }
        }

        public Visibility Visibility3
        {
            get { return _commandVisibilityMappings["Visibility3"].Visibility; }
            set { _commandVisibilityMappings["Visibility3"].Visibility = value; }
        }

        public Visibility Visibility4
        {
            get { return _commandVisibilityMappings["Visibility4"].Visibility; }
            set { _commandVisibilityMappings["Visibility4"].Visibility = value; }
        }

        public Visibility OutsVisibility
        {
            get { return _commandVisibilityMappings["OutsVisibility"].Visibility; }
            set { _commandVisibilityMappings["OutsVisibility"].Visibility = value; }
        }

        public Visibility PlayersVisibility
        {
            get { return _commandVisibilityMappings["PlayersVisibility"].Visibility; }
            set { _commandVisibilityMappings["PlayersVisibility"].Visibility = value; }
        }

        #endregion //Public Properties

        protected HoldemViewModelVisibilityBase()
        {
            _commandVisibilityMappings.Add(
                "Visibility", 
                new CommandVisibilityMapping(
                    Visibility.Collapsed, 
                    new RelayCommand(() => VisibilityChanged("Visibility"))));
            _commandVisibilityMappings.Add(
                "OutsVisibility", 
                new CommandVisibilityMapping(
                    Visibility.Collapsed, 
                    new RelayCommand(() => VisibilityChanged("OutsVisibility"))));
            _commandVisibilityMappings.Add(
                "PlayersVisibility", 
                new CommandVisibilityMapping(
                    Visibility.Collapsed, 
                    new RelayCommand(() => VisibilityChanged("PlayersVisibility"))));

            AddDefaultVisibilityCommands();
        }

        private void AddDefaultVisibilityCommands()
        {
            const string format = "Visibility{0}";

            for( int i = 1; i < MAX_DEFAULTS; i++ )
            {
                string name = string.Format(format, i);

                _commandVisibilityMappings.Add(
                name, 
                new CommandVisibilityMapping(
                    Visibility.Collapsed, 
                    new RelayCommand(() => VisibilityChanged(name))));
            }
        }

        #region Protected Methods

        protected void VisibilityChanged(string property)
        {
            if( !_commandVisibilityMappings.ContainsKey(property) )
                throw new ArgumentException("could not find the specified command key: " + property);

            var mapping = _commandVisibilityMappings[property];

            if (mapping.Visibility == Visibility.Collapsed)
                mapping.Visibility = Visibility.Visible;
            else if (mapping.Visibility == Visibility.Visible)
                mapping.Visibility = Visibility.Collapsed;

            base.RaisePropertyChanged(property);
        }

        #endregion //Public Methods
    }

    internal class CommandVisibilityMapping
    {
        public Visibility Visibility { get; set; }
        public RelayCommand Command { get; set; }

        public CommandVisibilityMapping(Visibility visibility, RelayCommand relayCommand)
        {
            this.Visibility = visibility;
            this.Command = relayCommand;
        }
    }
}
