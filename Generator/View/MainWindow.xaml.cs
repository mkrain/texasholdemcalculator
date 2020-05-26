using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

using HandGenerator.Entities.Model;
using HandGenerator.Phone.Supported.Extensions;
using HandGenerator.ViewModel;

using Holdem.Interfaces.Card;

namespace ThcStartingHandsGenerator.View
{
    [Guid("59FA331F-32F8-49F2-886D-3A9E7F578284")]
    public partial class MainWindow
    {
        #region Instance Variables

        private string _previousStrength;
        private Color _previousHighlight;
        private HoldemPhoneColorViewModel _model;
        private HoldemHandsOptionViewModel _opModel;
        private bool _applyToAllColors;

        #endregion //Instance Variables

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            this.StartingHandsGrid.StartingHandButtonClick += this.HandSelectionClick;

            BindViewModel();
        }

        #endregion //Constructor

        #region Control RoutedEvents

        private void HandSelectionClick(object sender, RoutedEventArgs e)
        {
            var button = e.Source as Button;

            if (button == null)
                return;

            var color = ColorConverter.ConvertFromString(button.Background.ToString());

            if (color == null)
                return;

            _previousHighlight = (Color)color;
            _previousStrength = button.Content.ToString();

            var parent = VisualTreeHelper.GetParent(button) as ContentPresenter;

            if (parent == null)
                return;

            _opModel.SelectedButton = button;

            var cardValue = parent.Content as CardValue;

            if (cardValue == null)
                return;

            _opModel.SelectedCard = cardValue;

            //Set the color from the button as the selected color
            SetListBoxColor(cardValue.Highlight);

            //Focus the numeric up down box
            NumericUpDown.Focus();
        }

        private void HandSelectionCancelClick(object sender, RoutedEventArgs e)
        {
            if (_opModel.SelectedButton == null)
                return;

            var previousStrength = int.Parse(_previousStrength);

            //Reset the previous strength value(s)
            if (previousStrength != _opModel.Strength)
                this.NumericUpDown.Value = previousStrength;

            //Reset the previous highlight value(s)
            if (_previousHighlight == _opModel.SelectedCard.Highlight)
                return;

            _opModel.Highlight = _previousHighlight;

            ApplyColor(_applyToAllColors, _previousHighlight);
        }

        private void HandSelectionOkClick(object sender, RoutedEventArgs e)
        {
            ApplyColor(_applyToAllColors, this.SelectedColor().ColorName.FromName());
        }

        //private void ApplyToAllColorsButtonClick(object sender, RoutedEventArgs e)
        //{
        //    ApplyColor(_applyToAllColors, this.SelectedColor().ColorName.FromName());
        //}

        #endregion //Control RoutedEvents

        #region Private Methods

        private void SetListBoxColor(Color selctedColor)
        {
            var selected = _opModel.SelectedHighlight(selctedColor);

            if (selected == null)
                return;

            this.StartingHandColorListBox.ScrollIntoView(selected);
            this.StartingHandColorListBox.SelectedItem = selected;
        }

        private void ApplyColor(bool allLikeColors, Color colorToApply)
        {
            if (_opModel.SelectedButton == null || _opModel.SelectedCard == null)
                return;

            _opModel.SelectedCard.Highlight = colorToApply;

            _model.UpdateHandWithHighlight(_opModel.SelectedCard, allLikeColors);

            _opModel.SelectedButton.Content = _opModel.Strength.ToString();

            //Update the numeric up down control with the new value
            this.NumericUpDown.Value = _opModel.Strength;

            var newColor = _model.FindHightlightFromStrength(_opModel.SelectedCard.Strength);
            //Update the color if it has changed
            SetListBoxColor(newColor);
            _opModel.Highlight = newColor;
        }

        private HoldemColor SelectedColor()
        {
            var listbox = this.StartingHandColorListBox;

            if (listbox.SelectedIndex == -1)
                return null;

            var selected = listbox.SelectedItem as HoldemColor;

            if (selected != null)
                return selected;

            return null;
        }

        private void BindViewModel()
        {
            _model = new HoldemPhoneColorViewModel();
            _opModel = new HoldemHandsOptionViewModel();

            this.DataContext = _model;
            this.HandOptionBorder.DataContext = _opModel;
        }

        private void ExitMenuItemClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoadHandClick(object sender, RoutedEventArgs e)
        {
            var fileDialog =
                new Microsoft.Win32.OpenFileDialog
                {
                    CheckFileExists = true,
                    CheckPathExists = true,
                    DefaultExt = ".xml",
                };

            var result = fileDialog.ShowDialog(this);

            if (result.Value)
            {
                _model.ImportStartingHand(fileDialog.FileName);
            }
        }

        private void SaveHandClick(object sender, RoutedEventArgs e)
        {
            var fileDialog =
                new Microsoft.Win32.SaveFileDialog
                {
                    CheckFileExists = false,
                    CheckPathExists = true,
                    DefaultExt = ".xml",
                    FileName = 
                        string.Format(
                            "StartingHand_{0}_{1}_{2}_{3}.xml", 
                            DateTime.Now.Year, 
                            DateTime.Now.Month,
                            DateTime.Now.Day, 
                            DateTime.Now.Millisecond)
                };

            var result = fileDialog.ShowDialog(this);

            if (result.Value)
            {
                _model.ExportStartingHand(fileDialog.FileName);
            }
        }

        private void ApplyToAllColorsToggleButtonClick(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;

            if (toggleButton == null)
                return;

            if( !toggleButton.IsChecked.HasValue )
                toggleButton.IsChecked = false;

            _applyToAllColors = toggleButton.IsChecked.Value;
        }

        private void NumericUpDownKeyDown(object sender, KeyEventArgs e)
        {
            //var control = sender as NumericBox;

            if (e.Key == Key.Enter)
            {
                ApplyColor(_applyToAllColors, this.SelectedColor().ColorName.FromName());
            }
        }

        #endregion //Private Methods

        private void MenuItemClick(object sender, RoutedEventArgs e)
        {
            var selectedColor = this.SelectedColor();

            var paletteColor = ColorPaletteListBox.SelectedItem as HoldemColor;

            if (paletteColor == null)
            {
                MessageBox.Show(
                    "The color could be changed, please select a palette color.",
                    "No Palette Color Selected",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            var success = _model.UpdateToNewColor(paletteColor, selectedColor);

            if (!success)
            {
                MessageBox.Show(
                    "The color could be changed, choose one that is not already mapped.",
                    "Color Could Not Be Added",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
        }

        private void ColorPaletteListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var paletteColor = ColorPaletteListBox.SelectedItem as HoldemColor;

            if( paletteColor != null )
                SetListBoxColor(paletteColor.ColorFromName);
        }

        private void ResetHandClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;

            if (menuItem == null)
                return;

            switch( menuItem.Header as string )
            {
                case "All _Enabled":
                    _model.ResetToStartingHandsAllEnabled();
                    return;
                case "All _Disabled":
                    _model.ResetToStartingHandsAllDisabled();
                    return;
                default:
                    _model.ResetStartingHand();
                    return;
            }
        }
    }
}
