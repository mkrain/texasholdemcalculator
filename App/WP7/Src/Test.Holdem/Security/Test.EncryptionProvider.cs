



//using NUnit.Framework;


//namespace Test.Holdem.Security
//{
//    [TestFixture]
//    public class Test
//    {
//        //[Test, Ignore]
//        //public void EncryptionProvider_EncryptString()
//        //{
//        //    var storage = new Mock<IHoldemIsolatedStorageFile>();

//        //    storage
//        //        .Setup(s => s.OpenFile(It.IsAny<string>(), It.IsAny<FileMode>()))
//        //        .Returns(new HoldemIsolatedStorageFileStream(new MemoryStream()));

//        //    var provider = new EncryptionProvider(new RSACrypto(256), storage.Object);

//        //    var evt = new AutoResetEvent(false);

//        //    provider.KeysGeneratedEventHandler += (o, e) => evt.Set();

//        //    var randomString = new RNGCryptoServiceProvider();

//        //    var bytes = new byte[20];

//        //    randomString.GetBytes(bytes);

//        //    var decodedString = Encoding.Unicode.GetString(bytes, 0, bytes.Length);

//        //    evt.WaitOne();

//        //    var encryptString = provider.Encrypt(decodedString);

//        //    decodedString.Should().NotContainEquivalentOf(encryptString);

//        //    var decryptedString = provider.Decrypt(encryptString);

//        //    decryptedString.Should().BeEquivalentTo(decodedString);
//        //}
//    }
//}
