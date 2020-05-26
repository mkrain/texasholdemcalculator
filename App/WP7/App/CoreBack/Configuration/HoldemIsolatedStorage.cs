using System.IO;
using System.IO.IsolatedStorage;
using Holdem.Interfaces.Configuration;

namespace TexasHoldemCalculator.Core.Configuration
{
    public class HoldemIsolatedStorage : IHoldemIsolatedStorage
    {
        private readonly IsolatedStorageFile _storage;

        public HoldemIsolatedStorage(IsolatedStorageFile storage )
        {
            _storage = storage;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            _storage.Dispose();
        }

        #endregion

        #region Implementation of IHoldemIsolatedStorage

        public long Quota
        {
            get
            {
                return _storage.Quota;
            }
        }

        public long AvailableFreeSpace
        {
            get
            {
                return _storage.AvailableFreeSpace;
            }
        }

        public void Remove()
        {
            _storage.Remove();
        }

        public bool IncreaseQuotaTo(long newQuotaSize)
        {
            return _storage.IncreaseQuotaTo(newQuotaSize);
        }

        public void DeleteFile(string file)
        {
            _storage.DeleteFile(file);
        }

        public bool FileExists(string path)
        {
            return _storage.FileExists(path);
        }

        public bool DirectoryExists(string path)
        {
            return _storage.DirectoryExists(path);
        }

        public void CreateDirectory(string dir)
        {
            _storage.CreateDirectory(dir);
        }

        public void DeleteDirectory(string dir)
        {
            _storage.DeleteDirectory(dir);
        }

        public string[] GetFileNames()
        {
            return _storage.GetFileNames();
        }

        public string[] GetFileNames(string searchPattern)
        {
            return _storage.GetFileNames(searchPattern);
        }

        public string[] GetDirectoryNames()
        {
            return _storage.GetDirectoryNames();
        }

        public string[] GetDirectoryNames(string searchPattern)
        {
            return _storage.GetDirectoryNames(searchPattern);
        }

        public IHoldemIsolatedStorageFileStream OpenFile(string path, FileMode mode)
        {
            return new HoldemIsolatedStorageFileStream(_storage.OpenFile(path, mode));
        }

        public IHoldemIsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access)
        {
            return new HoldemIsolatedStorageFileStream(_storage.OpenFile(path, mode, access));
        }

        public IHoldemIsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
        {
            return new HoldemIsolatedStorageFileStream(_storage.OpenFile(path, mode, access, share));
        }

        public IHoldemIsolatedStorageFileStream CreateFile(string path)
        {
            return new HoldemIsolatedStorageFileStream(_storage.CreateFile(path));
        }

        public IHoldemIsolatedStorageFile GetUserStoreForApplication()
        {
            return new HoldemIsolatedStorageFile(IsolatedStorageFile.GetUserStoreForApplication());
        }

        #endregion
    }
}
