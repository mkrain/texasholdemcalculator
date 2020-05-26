
using System.Windows.Media;

namespace HandGenerator.Phone.Supported.Extensions
{
    public static class ColorNamesExtensions
    {
        // thanks to Jabb...
        // http://silverlight.net/forums/t/13225.aspx
        public static Color FromName(this ColorNames color)
        {
            var value = (uint)color;

            return Color.FromArgb(
                (byte)(value >> 24),
                (byte)(value >> 16),
                (byte)(value >> 8),
                (byte)value);
        }
    }
}
