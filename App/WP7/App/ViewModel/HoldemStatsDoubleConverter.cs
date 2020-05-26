using System.Windows.Data;
using TexasHoldemCalculator.Core.Converter;

namespace TexasHoldemCalculator.ViewModel
{
    public sealed class HoldemStatsDoubleConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var converted = Converter.Parse<double>(value.ToString());

            return converted;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var converted = Converter.Parse<double>(value.ToString());

            return converted;
        }

        #endregion
    }
}