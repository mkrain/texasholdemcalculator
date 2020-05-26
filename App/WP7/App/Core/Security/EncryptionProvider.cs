using System;
using System.IO;
using System.Linq;
using System.Text;
using Common.Core.Configuration.IsolatedStorage;
using RSA;
using TexasHoldemCalculator.Interfaces.Extensions;
using TexasHoldemCalculator.Interfaces.Security;

namespace TexasHoldemCalculator.Core.Security
{
	public class EncryptionProvider : IEncryptionProvider
	{
		private readonly RSACrypto _cryptography;
        private readonly IFileWrapper _isolatedStorage;
		private const string KEY_PAIR_FILE_NAME = "THC.Key.Pair.xml";
		private const int KEY_LENGTH = 2048;
		private const int ENCRYPT_LENGTH = 214;
		private bool _imported;

		public event EventHandler<KeysGeneratedEventArgs> KeysGeneratedEventHandler;

		public bool IsKeyPairGenerated { get { return _imported; } }

		public EncryptionProvider(RSACrypto cryptography, IFileWrapper isolatedStorage)
		{
			_cryptography = cryptography;
			_isolatedStorage = isolatedStorage;

			_cryptography.PaddingProvider = new RSA.PaddingProviders.NoPadding();
			_cryptography.OnKeysGenerated += this.OnKeysGenerated;

			//Only generate the key pairs once.
			this.ImportSavedKeyPairs();

			if(_imported)
			{
			    return;
			}

			_cryptography.GenerateKeys(KEY_LENGTH);
		}

		private void OnKeysGenerated(object sender)
		{
		    KeysGeneratedEventArgs args = null;

			try
			{
				var kvp = _cryptography.ToXmlString(true);

				using( var stream = _isolatedStorage.OpenFile(KEY_PAIR_FILE_NAME, FileMode.CreateNew) )
				{
					using( var writer = new StreamWriter(stream as Stream) )
					{
						writer.WriteLine((string)kvp);
						_imported = true;
					}
				}

                args = new KeysGeneratedEventArgs();
			}
			catch( Exception e )
			{
				_imported = false;
			}

            if (this.KeysGeneratedEventHandler != null)
                this.KeysGeneratedEventHandler(this, args);
		}

		private void ImportSavedKeyPairs()
		{
            if( !_isolatedStorage.FileExists(KEY_PAIR_FILE_NAME) )
            {
                _imported = false;
                return;
            }

		    KeysGeneratedEventArgs args = null;

		    try
			{
				using( var stream = _isolatedStorage.OpenFile(KEY_PAIR_FILE_NAME, FileMode.Open) )
				{
					using( var reader = new StreamReader(stream as Stream) )
					{
						var xmlString = reader.ReadLine();

						_cryptography.FromXmlString(xmlString);
						_imported = true;
					}
				}

                args = new KeysGeneratedEventArgs();
			}
			catch( Exception e )
			{
				_imported = false;
			}

            if (this.KeysGeneratedEventHandler != null)
                this.KeysGeneratedEventHandler(this, args);
		}

		/// <summary>
		/// 
		/// First Encrypts the specified string and than base64
		/// encodes it.
		/// 
		/// </summary>
		/// <param name="toEncrypt"></param>
		/// <returns></returns>
		public string Encrypt(string toEncrypt)
		{
			if( !_imported )
                throw new ArgumentException("Encryption Keys have not been generated.");

            var chunked = toEncrypt.Chunk(ENCRYPT_LENGTH);
			var encryptedChunks = 
				chunked
					.Select(str => Encoding.UTF8.GetBytes((string)str))
					.Select<byte[], byte[]>(encodedChunk => _cryptography.Encrypt(encodedChunk))
					.Select(Convert.ToBase64String).ToList();

			return string.Join("|", encryptedChunks);
		}

		/// <summary>
		/// 
		/// First Decrypts the specified string and than base64
		/// decodes it.
		/// 
		/// </summary>
		/// <returns></returns>
		public string Decrypt(string toDecrypt)
		{
			if( !_imported )
                throw new ArgumentException("Encryption Keys have not been generated.");

			var chunked = toDecrypt.Split(new [] { "|" }, StringSplitOptions.RemoveEmptyEntries);

			var encryptedChunks = 
				chunked
					.Select(Convert.FromBase64String)
					.Select<byte[], byte[]>(encryptedChunk => _cryptography.Decrypt(encryptedChunk))
					.Select(str => Encoding.UTF8.GetString(str, 0, str.Length).Replace("\0", string.Empty)).ToList();

			return string.Join("", encryptedChunks);
		}

        //private IEnumerable<string> ChunkString(int pieceSize, string chunk)
        //{
        //    var chunked = new List<string>();

        //    for( int i = 0; i < chunk.Length; i += pieceSize )
        //    {
        //        var nextChunk = chunk.Skip(i).Take(pieceSize).ToArray();
        //        chunked.Add(new string(nextChunk));
        //    }

        //    return chunked;
        //}
	}
}