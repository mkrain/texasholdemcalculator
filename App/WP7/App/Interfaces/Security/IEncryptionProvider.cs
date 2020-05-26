using System;

namespace TexasHoldemCalculator.Interfaces.Security
{
	public interface IEncryptionProvider
	{
		bool IsKeyPairGenerated { get; }

		string Encrypt(string toEncrypt);

		string Decrypt(string toDecrypt);

		event EventHandler<KeysGeneratedEventArgs> KeysGeneratedEventHandler;
	}
}
