using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TexasHoldemCalculator.Core.Converter
{
	public static class Converter
	{
		private const RegexOptions REGEX_OPTIONS =
			RegexOptions.Compiled | RegexOptions.CultureInvariant |
			RegexOptions.IgnoreCase | RegexOptions.Singleline;

		private static readonly Regex _isRealNumber = new Regex(@"^-?\d+(\.\d+)?$", REGEX_OPTIONS);
		private static readonly Regex _isWholeNumber = new Regex(@"^\d+$", REGEX_OPTIONS);

		public static bool IsNumber(string number)
		{
			if( string.IsNullOrEmpty(number) )
				return false;

			return _isWholeNumber.IsMatch(number) || _isRealNumber.IsMatch(number);
		}

		/// <summary>
		/// 
		/// Converts the specified string to a whole number, or zero
		/// if it's not a number.
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static int StringToInt(string text)
		{
			if( string.IsNullOrEmpty(text) )
				return 0;

			return _isWholeNumber.IsMatch(text.Trim()) ? int.Parse(text) : 0;
		}

		/// <summary>
		/// 
		/// Converts the specified string to a real number, or zero
		/// if it's not a number.
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static double StringToDouble(string text)
		{
			if( string.IsNullOrEmpty(text) )
				return 0;

			return _isRealNumber.IsMatch(text.Trim()) ? double.Parse(text) : 0.0;
		}

		/// <summary>
		/// 
		/// Converts the specified string to a boolean value, or 
		/// false if it is not a boolean.
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static bool StringToBool(string input)
		{
			return !string.IsNullOrEmpty(input)
			       && string.Compare("true", input.Trim(), StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>
		/// 
		/// Converts the specified string into the given T enum value.
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="input"></param>
		/// <returns></returns>
		public static T StringToEnum<T>(string input)
		{
		    if( Enum.IsDefined(typeof(T), input) )
		    {
		        var selected = Enum.Parse(typeof(T), input, true);

		        return (T)selected;
		    }

		    return default( T );
		}

	    /// <summary>
		/// 
		/// Converts the specified string into the given T value.
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="input"></param>
		/// <returns></returns>
		public static T Parse<T>(string input) where T : IConvertible
		{
		    if( string.IsNullOrEmpty(input) )
				return default( T );

			try
			{
				return (T)Convert.ChangeType(
				          	input,
				          	typeof(T), CultureInfo.InvariantCulture);
			}
			catch
			{
				return default( T );
			}
		}
	}
}