using System.IO;
using System.IO.IsolatedStorage;
using Holdem.Interfaces.Configuration;

namespace TexasHoldemCalculator.Core.Configuration
{
    public class HoldemIsolatedStorageFile : IHoldemIsolatedStorageFile
    {
        private readonly IsolatedStorageFile _file;

        public HoldemIsolatedStorageFile(IsolatedStorageFile file)
        {
            _file = file;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            _file.Dispose();
        }

        #endregion

        #region Implementation of IHoldemIsolatedStorage

        public long Quota
        {
            get
            {
                return _file.Quota;
            }
        }

        public long AvailableFreeSpace
        {
            get
            {
                return _file.AvailableFreeSpace;
            }
        }

        public void Remove()
        {
            _file.Remove();
        }

        public bool IncreaseQuotaTo(long newQuotaSize)
        {
            return _file.IncreaseQuotaTo(newQuotaSize);
        }

        public void DeleteFile(string file)
        {
            _file.DeleteFile(file);
        }

        public bool FileExists(string path)
        {
            return _file.FileExists(path);
        }

        public bool DirectoryExists(string path)
        {
            return _file.DirectoryExists(path);
        }

        public void CreateDirectory(string dir)
        {
            _file.CreateDirectory(dir);
        }

        public void DeleteDirectory(string dir)
        {
            _file.DeleteDirectory(dir);
        }

        public string[] GetFileNames()
        {
            return _file.GetFileNames();
        }

        public string[] GetFileNames(string searchPattern)
        {
            return _file.GetFileNames(searchPattern);
        }

        public string[] GetDirectoryNames()
        {
            return _file.GetDirectoryNames();
        }

        public string[] GetDirectoryNames(string searchPattern)
        {
            return _file.GetDirectoryNames(searchPattern);
        }

        public IHoldemIsolatedStorageFileStream OpenFile(string path, FileMode mode)
        {
            return new HoldemIsolatedStorageFileStream(_file.OpenFile(path, mode));
        }

        public IHoldemIsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access)
        {
            return new HoldemIsolatedStorageFileStream(_file.OpenFile(path, mode, access));
        }

        public IHoldemIsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
        {
            return new HoldemIsolatedStorageFileStream(_file.OpenFile(path, mode, access, share));
        }

        public IHoldemIsolatedStorageFileStream CreateFile(string path)
        {
            return new HoldemIsolatedStorageFileStream(_file.CreateFile(path));
        }

        public IHoldemIsolatedStorageFile GetUserStoreForApplication()
        {
            return new HoldemIsolatedStorageFile(IsolatedStorageFile.GetUserStoreForApplication());
        }

        #endregion
    }
}
