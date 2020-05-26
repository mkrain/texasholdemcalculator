
using System.Windows.Media.Imaging;

namespace TexasHoldemCalculator.Interfaces.Provider
{
    public interface IIconProvider
    {
        BitmapImage PlusIcon { get; }

        BitmapImage MinusIcon { get; }

        BitmapImage GetFolderImage(string folderIconName);

        BitmapImage GetFolderImageFromUri(string iconUri);
    }
}