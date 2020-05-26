using System.Collections.Generic;

namespace TexasHoldemCalculator.Interfaces.SkyDrive
{
    public class SkyDriveDirectoryInformation
    {
        private readonly IEnumerable<SkyDriveFileInformation> _files;
        private readonly IEnumerable<SkyDriveDirectoryInformation> _directories;

        public IEnumerable<SkyDriveFileInformation> Files
        {
            get { return _files; }
        }

        public IEnumerable<SkyDriveDirectoryInformation> Directories
        {
            get { return _directories; }
        }

        public SkyDriveDirectoryInformation()
        {
            _files = new List<SkyDriveFileInformation>();
            _directories = new LinkedList<SkyDriveDirectoryInformation>();
        }

        public SkyDriveDirectoryInformation(IEnumerable<SkyDriveFileInformation> filesInDirectory)
        {
            _files = new List<SkyDriveFileInformation>(filesInDirectory);
        }

        public SkyDriveDirectoryInformation(IEnumerable<SkyDriveDirectoryInformation> directories)
        {
            _directories = new List<SkyDriveDirectoryInformation>(directories);
        }

        public SkyDriveDirectoryInformation(
            IEnumerable<SkyDriveFileInformation> files,
            IEnumerable<SkyDriveDirectoryInformation> directories)
        {
            _files = new List<SkyDriveFileInformation>(files);
            _directories = new LinkedList<SkyDriveDirectoryInformation>(directories);
        }
    }
}