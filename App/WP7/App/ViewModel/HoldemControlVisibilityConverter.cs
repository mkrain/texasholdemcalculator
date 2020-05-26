using System.Windows;
using System.Windows.Data;
using TexasHoldemCalculator.Core.Converter;

namespace TexasHoldemCalculator.ViewModel
{
    public sealed class HoldemControlVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var converted = Converter.Parse<bool>(value.ToString());

            return converted ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var converted = Converter.Parse<bool>(value.ToString());

            return converted ? Visibility.Collapsed : Visibility.Visible;
        }

        #endregion
    }

    
}