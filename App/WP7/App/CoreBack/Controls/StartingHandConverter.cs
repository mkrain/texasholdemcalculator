using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Holdem.Interfaces.Card;
using Holdem.Interfaces.StartingHands;
using TexasHoldemCalculator.Core.Entities.Cards;

namespace TexasHoldemCalculator.Core.Controls
{
	public sealed class StartingHandConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
            if( value == null )
                return -1;

			var hand = value as CardValue;

			if( hand == null )
				return -1;
			return hand.Strength;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("Not Implemented");
		}

		#endregion
	}

	public sealed class StartingHandHighlightConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var hand = value as CardValue;

			var color = hand == null ? ColorNames.Transparent.FromName() : hand.Highlight;

			return new SolidColorBrush(color);
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
            if( value == null )
				return Visibility.Collapsed;

			var hand = value as CardValue;

			if( hand == null )
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
            if( value == null )
				return 0;

			var hand = value as CardValue;

			if( hand == null )
				return 0;

			var row = 13 - (int)hand.Parent;

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
            if( value == null )
				return 0;

			var hand = value as CardValue;

			if( hand == null )
				return 0;

			var row = 13 - (int)hand.Name;

			return row;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("Not Implemented");
		}

		#endregion
	}

	public sealed class KickerColumnConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var hand = value as CardKicker;

			if( hand == null )
				return 0;

			if( hand.CardName == CardName.Ace )
				return 1;

			int col = (int)hand.CardName % 4;

			return col;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("Not Implemented");
		}

		#endregion
	}

	public sealed class KickerRowConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var hand = value as CardKicker;

			if( hand == null )
				return 0;

			if( hand.CardName == CardName.Ace )
				return 3;
			if( hand.CardName <= CardName.Five )
				return 0;
			if( hand.CardName <= CardName.Nine )
				return 1;
			if( hand.CardName <= CardName.King )
				return 2;

			return -1;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("Not Implemented");
		}

		#endregion
	}

    public sealed class HoldemStatsIntConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converted = Converter.Converter.Parse<double>(value.ToString());

            return (int)converted;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converted = Converter.Converter.Parse<double>(value.ToString());

            return (int)converted;
        }

		#endregion
	}
}