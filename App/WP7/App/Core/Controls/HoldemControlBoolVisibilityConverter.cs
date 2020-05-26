using System.Windows;
using System.Windows.Data;

namespace TexasHoldemCalculator.Core.Controls
{
    public sealed class HoldemControlBoolVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
                return ((bool)value) ? Visibility.Collapsed : Visibility.Visible;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
                return ((bool)value) ? Visibility.Collapsed : Visibility.Visible;
            return Visibility.Visible;
        }

        #endregion
    }
}
