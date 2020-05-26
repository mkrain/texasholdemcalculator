using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace TexasHoldemCalculator.Interfaces.Extensions
{
    /// <summary>
    /// 
    /// Taken from: http://stackoverflow.com/questions/1415140/c-sharp-enums-can-my-enums-have-friendly-names.
    /// I created this as an extension of Enum by adding "this".
    /// 
    /// </summary>
    public static class EnumHelper
    {
        public static string Description(this Enum value)
        {
            if( value == null )
                return string.Empty;

            if( !Enum.IsDefined(value.GetType(), value) )
                return string.Empty;

            var fieldInfo = value.GetType().GetField(value.ToString());

            if( fieldInfo != null )
            {
                var attributes = 
                    fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

                if( attributes != null && attributes.Length > 0 )
                    return attributes[0].Description;
            }

            return StringHelper.ToFriendlyName(value.ToString());
        }
    }

    public static class StringHelper
    {
        public static bool IsNullOrWhiteSpace(string value)
        {
            return value == null || string.IsNullOrEmpty(value.Trim());
        }

        public static string ToFriendlyName(string value)
        {
            if( value == null )
                return string.Empty;
            if( value.Trim().Length == 0 )
                return string.Empty;

            string result = value;

            result = string.Concat(result.Substring(0, 1).ToUpperInvariant(), result.Substring(1, result.Length - 1));

            const string pattern = @"([A-Z]+(?![a-z])|\d+|[A-Z][a-z]+|(?![A-Z])[a-z]+)+";

            var words = new List<string>();
            var match = Regex.Match(result, pattern);

            if( match.Success )
            {
                words.AddRange(from Capture capture 
                                   in match.Groups[1].Captures
                               select capture.Value);
            }

            return string.Join(" ", words.ToArray());
        }
    }
}
