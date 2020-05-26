using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Media.Imaging;
using TexasHoldemCalculator.Interfaces.Extensions;
using TexasHoldemCalculator.Interfaces.Provider;

namespace TexasHoldemCalculator.Interfaces.SkyDrive
{
    /// <summary>
    /// Helper Class for the SkyDriveRepository.
    /// </summary>
    public class SkyDriveFileInformation : INotifyPropertyChanged
    {
        //Possibly represents sub-folders.
        private readonly List<SkyDriveFileInformation> _subFolders = new List<SkyDriveFileInformation>();

        public IIconProvider IconProvider { get; set; }

        public SkyDriveFileInformation()
        {
            
        }

        public SkyDriveFileInformation(IDictionary<string, object> fileListing)
        {
            this.CreatedTime = 
                Convert.ToDateTime(
                    fileListing
                        .ValueOrDefault("created_time"))
                        .ToString("d", Thread.CurrentThread.CurrentCulture);
            this.FileDescription = "Description";
            this.FileExtension = Path.GetExtension(fileListing.ValueOrDefault("name"));
            this.FileId = fileListing.ValueOrDefault("id");
            this.FileName = fileListing.ValueOrDefault("name");
            this.ParentId = fileListing.ValueOrDefault("parent_id");
            this.Type = fileListing.ValueOrDefault("type");
            this.UploadLocation = fileListing.ValueOrDefault("upload_location");
            this.EntryImage = 
                this.SetIconByExtension(
                    fileListing.ValueOrDefault("name"), 
                    fileListing.ValueOrDefault("picture"));
            this.FileSource = fileListing.ValueOrDefault("source");
            this.FileSharingInformation = string.Empty;
            this.Link = fileListing.ValueOrDefault("link");

            var size = fileListing.ValueOrDefault("size");

            if( !string.IsNullOrEmpty(size) )
            {
                this.FileSizeInformation =
                    string.Format("File Size: {0}", this.FileSizeToString(size));
            }
        }

        public List<SkyDriveFileInformation> Folders
        {
            get { return _subFolders; }
        }

        /// <summary>
        /// The Creation Date-Tim of the Current Element.
        /// </summary>
        private string _createdTime;

        /// <summary>
        /// The File Description for the Current Entry.
        /// </summary>
        private string _fileDescription;

        /// <summary>
        /// The File Extension of the Current Entry.
        /// </summary>
        private string _fileExtension;

        /// <summary>
        /// The Filename of the current entry.
        /// </summary>
        private string _fileName;

        /// <summary>
        /// Information about sharing level of the selected folder.
        /// </summary>
        private string _fileSharingInformation;

        /// <summary>
        /// Save here the file-size and other information.
        /// </summary>
        private string _fileSizeInformation;

        /// <summary>
        /// The source Uri of the file
        /// </summary>  
        private string _fileSource;

        /// <summary>
        /// The icon to show beneath the filename.
        /// </summary>
        private string _icon;

        /// <summary>
        /// The link for the file
        /// </summary>
        private string _link;

        /// <summary>
        /// The Parent Id of the current Element.
        /// </summary>
        private string _parentId;

        /// <summary>
        /// The thumbnail url.
        /// </summary>
        private string _picture;

        /// <summary>
        /// The Type of the Current Entry.
        /// </summary>
        private string _type;

        /// <summary>
        /// The Upload Location of the current entry.
        /// </summary>
        private string _uploadLocation;

        /// <summary>
        /// 
        /// Files the size to string.
        /// 
        /// </summary>
        /// <param name="fileSize">Size of the file.</param>
        /// <returns></returns>
        private string FileSizeToString(object fileSize)
        {
            string suffix;
            decimal value;

            if (fileSize is string && string.IsNullOrEmpty(fileSize as string))
                return string.Empty;
            if (fileSize == null)
                return string.Empty;

            if (Convert.ToDecimal(fileSize) > 1000000.0m)
            {
                value = Convert.ToDecimal(fileSize) / 1000000.0m; //MB
                suffix = "MB";
            }
            else
            {
                value = Convert.ToDecimal(fileSize) / 1000.0m; //KB
                suffix = "KB";
            }

            return string.Format("{0:0.##} {1}", value, suffix);
        }

        /// <summary>
        /// 
        /// Sets the icon by extension.
        /// 
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="imageUri"> </param>
        /// <returns></returns>
        private string SetIconByExtension(string fileName, string imageUri)
        {
            string extension = Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(extension))
                return "funiversal_binary.png";

            switch (extension.ToUpper())
            {
                case ".AAC":
                    return "file_aac.png";
                case ".AI":
                    return "file_ai.png";
                case ".AVI":
                    return "file_avi.png";
                case ".BIN":
                    return "file_bin.png";
                case ".BMP":
                case ".JPG":
                case ".JPEG":
                case ".PNG":
                    return imageUri;
                case ".CUE":
                    return "file_cue.png";
                case ".DIVX":
                    return "file_divx.png";
                case ".DOCX":
                    return "file_doc.png";
                case ".DOC":
                    return "file_doc.png";
                case ".EPS":
                    return "file_eps.png";
                case ".FLAC":
                    return "file_flac.png";
                case ".FLV":
                    return "file_flv.png";
                case ".GIF":
                    return "file_gif.png";
                case ".HTML":
                    return "file_html.png";
                case ".ICAL":
                    return "file_ical.png";
                case ".INDD":
                    return "file_indd.png";
                case ".INX":
                    return "file_inx.png";
                case ".ISO":
                    return "file_iso.png";
                case ".MOV":
                    return "file_mov.png";
                case ".MP3":
                    return "file_aac.mp3";
                case ".MPG":
                    return "file_aac.mpeg";
                case ".PDF":
                    return "file_pdf.png";
                case ".PHP":
                    return "file_php.png";
                case ".PPS":
                    return "file_pps.png";
                case ".PPT":
                    return "file_ppt.png";
                case ".PSD":
                    return "file_psd.png";
                case ".QXD":
                    return "file_qxd.png";
                case ".QXP":
                    return "file_qxp.png";
                case ".RAW":
                    return "file_raw.png";
                case ".RTF":
                    return "file_rtf.png";
                case ".SVG":
                    return "file_svg.png";
                case ".TIF":
                    return "file_tif.png";
                case ".TXT":
                    return "file_txt.png";
                case ".VCF":
                    return "file_vcf.png";
                case ".WAV":
                    return "file_wav.png";
                case ".WMA":
                    return "file_wma.png";
                case ".XLSX":
                    return "file_xls.png";
                case ".XLS":
                    return "file_xls.png";
                case ".XML":
                    return "file_xml.png";
                case ".WMV":
                    return "jewel_case.png";
                case ".ZIP":
                    return "box_zip.png";
                case ".RAR":
                    return "box_rar.png";
                default:
                    return "universal_binary.png";
            }
        }

        #region Notify Property Changed Stuff

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if( this.PropertyChanged != null )
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region PublicMembers

        /// <summary>
        /// The Filename of the current entry.
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if( _fileName != value )
                {
                    _fileName = value;
                    this.OnPropertyChanged("FileName");
                }
            }
        }

        /// <summary>
        /// The FileId of the current entry.
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// The Upload Location of the current entry.
        /// </summary>
        public string UploadLocation
        {
            get { return _uploadLocation; }
            set
            {
                if( _uploadLocation != value )
                {
                    _uploadLocation = value;
                    this.OnPropertyChanged("UploadLocation");
                }
            }
        }

        /// <summary>
        /// The Type of the Current Entry.
        /// </summary>
        public string Type
        {
            get { return _type; }
            set
            {
                if( _type != value )
                {
                    _type = value;
                    this.OnPropertyChanged("Type");
                }
            }
        }

        /// <summary>
        /// The Parent Id of the current Element.
        /// </summary>
        public string ParentId
        {
            get { return _parentId; }
            set
            {
                if( _parentId != value )
                {
                    _parentId = value;
                    this.OnPropertyChanged("ParentId");
                }
            }
        }

        /// <summary>
        /// The Creation Date-Tim of the Current Element.
        /// </summary>
        public string CreatedTime
        {
            get { return _createdTime; }
            set
            {
                if( _createdTime != value )
                {
                    _createdTime = value;
                    this.OnPropertyChanged("CreatedTime");
                }
            }
        }

        /// <summary>
        /// The File Extension of the Current Entry.
        /// </summary>
        public string FileExtension
        {
            get { return _fileExtension; }
            set
            {
                if( _fileExtension != value )
                {
                    _fileExtension = value;
                    this.OnPropertyChanged("FileExtension");
                }
            }
        }

        /// <summary>
        /// The File Description for the Current Entry.
        /// </summary>
        public string FileDescription
        {
            get { return _fileDescription; }
            set
            {
                if( _fileDescription != value )
                {
                    _fileDescription = value;
                    this.OnPropertyChanged("FileDescription");
                }
            }
        }

        /// <summary>
        /// The icon to show beneath the filename.
        /// </summary>
        public string EntryImage
        {
            get { return _icon; }
            set
            {
                if( _icon != value )
                {
                    _icon = value;
                    this.OnPropertyChanged("Icon");
                }
            }
        }

        /// <summary>
        /// The source Uri of the file
        /// </summary>
        public string FileSource
        {
            get { return _fileSource; }
            set
            {
                if( _fileSource != value )
                {
                    _fileSource = value;
                    this.OnPropertyChanged("FileSource");
                }
            }
        }

        /// <summary>
        /// The link for the file
        /// </summary>
        public string Link
        {
            get { return _link; }
            set
            {
                if( _link != value )
                {
                    _link = value;
                    this.OnPropertyChanged("Link");
                }
            }
        }

        /// <summary>
        /// Save here the file-size and other information.
        /// </summary>
        public string FileSizeInformation
        {
            get { return _fileSizeInformation; }
            set
            {
                if( _fileSizeInformation != value )
                {
                    _fileSizeInformation = value;
                    this.OnPropertyChanged("FileSizeInformation");
                }
            }
        }

        /// <summary>
        /// Information about sharing level of the selected folder.
        /// </summary>
        public string FileSharingInformation
        {
            get { return _fileSharingInformation; }
            set
            {
                if( _fileSharingInformation != value )
                {
                    _fileSharingInformation = value;
                    this.OnPropertyChanged("FileSharingInformation");
                }
            }
        }

        /// <summary>
        /// The thumbnail url.
        /// </summary>
        public string Picture
        {
            get { return _picture; }
            set
            {
                if( _picture != value )
                {
                    _picture = value;
                    this.OnPropertyChanged("Picture");
                    this.OnPropertyChanged("PictureIcon");
                }
            }
        }

        public BitmapImage PictureIcon
        {
            get
            {
                return ( this.IconProvider == null || string.IsNullOrEmpty(this.EntryImage) ) ? 
                    null :
                    this.IconProvider.GetFolderImageFromUri(this.EntryImage);
            }
        }

        #endregion
    }
}