using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

using HandGenerator.Phone.Supported;
using HandGenerator.Phone.Supported.Extensions;

using Holdem.Core.Converter;
using Holdem.Interfaces.Card;

namespace HandGenerator.Controls
{
    public sealed class StartingHandConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hand = value as CardValue;

            if (hand == null)
                return ".";
            return Converter.StringToInt(hand.Strength.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Not Implemented");
        }

        #endregion
    }

    public sealed class StartingHandStrengthConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "0";

            var hand = value.ToString();

            if (Converter.IsNumber(hand))
                return hand;

            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "0";

            var hand = value.ToString();

            if (Converter.IsNumber(hand))
                return hand;

            return "0";
        }

        #endregion
    }

    public sealed class StartingHandHighlightConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hand = value as CardValue;

            if (hand == null)
                return new SolidColorBrush(ColorNames.Transparent.FromName());

            return new SolidColorBrush(hand.Highlight);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Not Implemented");
        }

        #endregion
    }

    public sealed class StartingHandHoldemColorConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hand = value as SolidColorBrush;

            if (hand == null)
                return new SolidColorBrush(ColorNames.Transparent.FromName());

            return hand;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Not Implemented");
        }

        #endregion
    }

    public sealed class StartingHandTextHighlightConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hand = value as string;

            if (hand == null)
                return new SolidColorBrush(ColorNames.Transparent.FromName());

            var converted = (Color)ColorConverter.ConvertFromString(hand);

            return new SolidColorBrush(converted);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hand = value as Brush;

            if (hand == null)
                return new SolidColorBrush(ColorNames.Transparent.FromName());

            return hand;
        }

        #endregion
    }

    public sealed class StartingHandBorderHighlightConverter : IValueConverter
    {
        private static readonly Brush _defaultVisibleBrush;
        private static readonly Brush _defaultBrush;

        public Brush VisibleBrush
        {
            get;
            set;
        }

        public Brush DefaultBrush
        {
            get;
            set;
        }

        static StartingHandBorderHighlightConverter()
        {
            _defaultVisibleBrush = new SolidColorBrush(ColorNames.Red.FromName());
            _defaultBrush = new SolidColorBrush(ColorNames.LimeGreen.FromName());
        }

        public StartingHandBorderHighlightConverter()
        {
            this.DefaultBrush = _defaultBrush;
            this.VisibleBrush = _defaultVisibleBrush;
        }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hand = value as CardValue;

            if (hand == null)
                return this.DefaultBrush;

            var found = hand.IsVisible;

            return found ? this.DefaultBrush : this.VisibleBrush;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Not Implemented");
        }

        #endregion
    }

    public sealed class StartingHandVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hand = value as CardValue;

            if (hand == null)
                return Visibility.Collapsed;

            var found = hand.IsVisible;

            return found ? Visibility.Visible : Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Not Implemented");
        }

        #endregion
    }

    public sealed class CardRowConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hand = value as CardValue;

            if (hand == null)
                return 0;

            var row = 12 - (int)hand.Parent;

            //Console.WriteLine("Placing {0} at Row {1}", hand.Name, row);

            return row;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Not Implemented");
        }

        #endregion
    }

    public sealed class CardColumnConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hand = value as CardValue;

            if (hand == null)
                return 0;

            var row = 12 - (int)hand.Name;

            //Console.WriteLine("Placing {0} at Col {1}", hand.Name, row);

            return row;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Not Implemented");
        }

        #endregion
    }
}