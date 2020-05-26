using System;

namespace TexasHoldemCalculator.Interfaces.Converter
{
	public interface IConverter
	{
		/// <summary>
		/// 
		/// Converts a string to an integer.
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		int StringToWholeValue(string text);

		/// <summary>
		/// 
		/// Converts a string to a double.
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		double StringToRealValue(string text);

		/// <summary>
		/// 
		/// Converts a string to a Boolean.
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		bool StringToChoiceValue(string input);

		/// <summary>
		/// 
		/// Converts the string to an enumeration of type T.
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="input"></param>
		/// <returns></returns>
		T StringToEnum<T>(string input);

		/// <summary>
		/// 
		/// Converts a string to the specified type T.
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="input"></param>
		/// <returns></returns>
		T Parse<T>(string input) where T : IConvertible;
	}
}