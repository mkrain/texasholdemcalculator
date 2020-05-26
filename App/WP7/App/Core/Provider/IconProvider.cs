using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using TexasHoldemCalculator.Interfaces.Provider;

namespace TexasHoldemCalculator.Core.Provider
{
    public class IconProvider : IIconProvider
    {
        private const string ICON_BASE = "Provider.Icons";
        private const string ICON_FILES_BASE = "Provider.Icons.Files";
        private const string ICON_FORMAT = "{0}.{1}.{2}";
        private const string PLUS_ICON_NAME = "THC.Add.png";
        private const string MINUS_ICON_NAME = "THC.Minus.png";
        private static string _assemblyName;

        public BitmapImage PlusIcon
        {
            get { return this.GetImage(PLUS_ICON_NAME); }
        }

        public BitmapImage MinusIcon
        {
            get { return this.GetImage(MINUS_ICON_NAME); }
        }

        public IconProvider()
        {
            var name = Assembly.GetExecutingAssembly().FullName;
            var asmName = new AssemblyName(name);

            _assemblyName = asmName.Name;
        }

        public BitmapImage GetFolderImage(string folderIconName)
        {
            return this.GetFileImage(folderIconName);
        }

        public BitmapImage GetFolderImageFromUri(string iconUri)
        {
            //HACK:  Figure out a better way to distinguish between images with URI's
            if( !iconUri.Contains("storage.live.com") )
                return this.GetFolderImage(iconUri);
            return this.GetImageFromUriSource(iconUri);
        }

        /// <summary>
        /// 
        /// Used for icons at the root /Icons folder.
        /// 
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        private BitmapImage GetImage(string imageName)
        {
            var resource = string.Format(ICON_FORMAT, _assemblyName, ICON_BASE, imageName);

            return this.GetImageFromAbsolutePath(resource);
        }

        /// <summary>
        /// 
        /// Used for icons at the /Icons/Files folder.
        /// 
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        private BitmapImage GetFileImage(string imageName)
        {
            var resource = string.Format(ICON_FORMAT, _assemblyName, ICON_FILES_BASE, imageName);

            return this.GetImageFromAbsolutePath(resource);
        }

        private BitmapImage GetImageFromUriSource(string iconUri)
        {
            var bmp = new BitmapImage(new Uri(iconUri, UriKind.RelativeOrAbsolute));

            return bmp;
        }

        private BitmapImage GetImageFromAbsolutePath(string pathName)
        {
            var stream = this.GetAbsoluteStreamResource(pathName);
            var bmp =
                new BitmapImage
                {
                    CreateOptions = BitmapCreateOptions.None
                };

            bmp.SetSource(stream);

            return bmp;
        }

        private Stream GetAbsoluteStreamResource(string resource)
        {
            return this.GetType().Assembly.GetManifestResourceStream(resource);
        }
    }
}