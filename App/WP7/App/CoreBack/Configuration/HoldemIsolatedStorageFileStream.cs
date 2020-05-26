using System;
using System.IO;
using Holdem.Interfaces.Configuration;

namespace TexasHoldemCalculator.Core.Configuration
{
    public class HoldemIsolatedStorageFileStream : Stream, IHoldemIsolatedStorageFileStream
    {
        private readonly Stream _stream;

        public HoldemIsolatedStorageFileStream(Stream stream) 
        {
            _stream = stream;
        }

        #region Implementation of IHoldemIsolatedStorageFileStream

        public override bool CanRead
        {
            get
            {
                return _stream.CanRead;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return _stream.CanWrite;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return _stream.CanSeek;
            }
        }

        public override long Length
        {
            get
            {
                return _stream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return _stream.Position;
            }
            set
            {
                _stream.Position = value;
            }
        }

        public string Name
        {
            get
            {
                return _stream is FileStream ? ((FileStream)_stream).Name : string.Empty;
            }
        }

        public new void Dispose(bool disposing)
        {
            if( disposing )
                _stream.Dispose();
            base.Dispose(disposing);
        }

        public new void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override void Close()
        {
            _stream.Close();
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override int ReadByte()
        {
            return _stream.ReadByte();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        public override void WriteByte(byte value)
        {
            _stream.WriteByte(value);
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
        {
            return _stream.BeginRead(buffer, offset, numBytes, userCallback, stateObject);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            return _stream.EndRead(asyncResult);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
        {
            return _stream.BeginWrite(buffer, offset, numBytes, userCallback, stateObject);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            _stream.EndWrite(asyncResult);
        }

        #endregion
    }
}