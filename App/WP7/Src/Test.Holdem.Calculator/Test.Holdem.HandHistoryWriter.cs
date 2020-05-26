using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Common.Core.Configuration;
using Common.Core.Configuration.IsolatedStorage;
using Common.Core.Configuration.Service;
using Moq;

using NUnit.Framework;
using TexasHoldemCalculator.Core.Entities.Collections;
using TexasHoldemCalculator.Core.Generator;
using TexasHoldemCalculator.Interfaces.Database;
using TexasHoldemCalculator.Interfaces.HandHistory;
using TexasHoldemCalculator.Interfaces.ReplayEngine;
using TexasHoldemCalculator.ReplayEngine;

namespace Test.Holdem
{
	[TestFixture]
	public class HoldemTestHandHistoryWriter
	{
		[Test]
		public void HoldemHandHistoryWriter_Constructor()
		{
			var configService = new Mock<IPhoneConfiguration>();
			var provider = new Mock<IReplayEngineProvider>();
		    var dataContext = new Mock<IHandHistoryDataContext>();

			Assert.DoesNotThrow(
                () => new HoldemHandHistoryWriter(dataContext.Object, configService.Object, provider.Object));
		}

		[Test]
		public void HoldemHandHistoryWriter_Constructor_Null_Service()
		{
		    try
		    {
		        new HoldemHandHistoryWriter(new Mock<IHandHistoryDataContext>().Object, null,
		                                    new Mock<IReplayEngineProvider>().Object);
                Assert.Fail();
		    }
            catch (ArgumentNullException ane)
		    {
		        
		        throw;
		    }
		}

        //[Test]
        //public void HoldemHandHistoryWriter_AddHandHistory_IList()
        //{
        //    var configService = new Mock<IPhoneConfiguration>();
        //    var holdemConfiguration = new Mock<IPhoneConfiguration>();
        //    var provider = new Mock<IReplayEngineProvider>();
        //    var dataContext = new Mock<IHandHistoryDataContext>();

        //    configService
        //        .Setup(s => s.PersistentConfiguration)
        //        .Returns(holdemConfiguration.Object);

        //    var writer = new HoldemHandHistoryWriter(dataContext.Object, configService.Object, provider.Object);

        //    var game = Game();

        //    var history = new History(game);

        //    writer.AddHandHistory(new List<History> { history });

        //    var expected = writer.History;

        //    Assert.IsNotNull(expected);
        //    Assert.AreEqual(1, expected.ToList().Count);
        //}

        //[Test]
        //public void HoldemHandHistoryWriter_AddHandHistory_Null_IList()
        //{
        //    var configService = new Mock<IPhoneConfiguration>();
        //    var holdemConfiguration = new Mock<IPhoneConfiguration>();
        //    var provider = new Mock<IReplayEngineProvider>();
        //    var dataContext = new Mock<IHandHistoryDataContext>();

        //    configService
        //        .Setup(s => s.PersistentConfiguration)
        //        .Returns(holdemConfiguration.Object);

        //    var writer = new HoldemHandHistoryWriter(dataContext.Object, configService.Object, provider.Object);

        //    writer.AddHandHistory(null);

        //    var expected = writer.History;

        //    Assert.IsNotNull(expected);
        //    Assert.AreEqual(0, expected.ToList().Count);
        //}

        //[Test]
        //public void HoldemHandHistoryWriter_AddHandHistory_Null_IEnumerable()
        //{
        //    var configService = new Mock<IPhoneConfiguration>();
        //    var holdemConfiguration = new Mock<IPhoneConfiguration>();
        //    var provider = new Mock<IReplayEngineProvider>();
        //    var dataContext = new Mock<IHandHistoryDataContext>();

        //    configService
        //        .Setup(s => s.PersistentConfiguration)
        //        .Returns(holdemConfiguration.Object);

        //    var writer = new HoldemHandHistoryWriter(dataContext.Object, configService.Object, provider.Object);

        //    writer.AddHandHistory(null);

        //    var expected = writer.History;

        //    Assert.IsNotNull(expected);
        //    Assert.AreEqual(0, expected.ToList().Count);
        //}

        [Test]
		public void HoldemHandHistoryWriter_WriteHandHistory_Null_History()
		{
			var configService = new Mock<IPhoneConfiguration>();
			var provider = new Mock<IReplayEngineProvider>();
            var dataContext = new Mock<IHandHistoryDataContext>();
            var writer = new HoldemHandHistoryWriter(dataContext.Object, configService.Object, provider.Object);

			writer.WriteHandHistory((History)null);

			var expected = writer.HandHistory;

			Assert.IsNotNull(expected);
			Assert.AreEqual(0, expected.Count());
		}

        [Test]
		public void HoldemHandHistoryWriter_WriteHandHistory_Null_IEnumerable_History()
		{
			var configService = new Mock<IPhoneConfiguration>();
			var provider = new Mock<IReplayEngineProvider>();
            var dataContext = new Mock<IHandHistoryDataContext>();
            var writer = new HoldemHandHistoryWriter(dataContext.Object, configService.Object, provider.Object);

			writer.WriteHandHistory((History)null);

			var expected = writer.HandHistory;

			Assert.IsNotNull(expected);
			Assert.AreEqual(0, expected.Count());
		}

        [Test]
        public void HoldemHandHistoryWriter_AddHandHistory_IEnumerable()
        {
            var configService = new Mock<IConfigurationService>();
            var holdemConfiguration = new Mock<IPhoneConfiguration>();
            var provider = new Mock<IReplayEngineProvider>();
            var dataContext = new Mock<IHandHistoryDataContext>();

            configService
                .Setup(s => s.PersistentConfiguration)
                .Returns(holdemConfiguration.Object);

            var writer = new HoldemHandHistoryWriter(dataContext.Object, configService.Object.PersistentConfiguration, provider.Object);

            var history = new History(Game());

            writer.WriteHandHistory(new List<History> { history });

            var expected = writer.HandHistory;

            Assert.IsNotNull(expected);
            Assert.AreEqual(1, expected.Count());
        }

        [Test]
		public void HoldemHandHistoryWriter_WriteHandHistory_Load_Existing()
		{
			var configService = new Mock<IPhoneConfiguration>();
			var provider = new Mock<IReplayEngineProvider>();
            var dataContext = new Mock<IHandHistoryDataContext>();

			const string savePath = "test_load_save.xml";
			const string savePath1 = "test_load_save1.xml";

		    var fInfo = new FileInfo(savePath);

			if (!fInfo.Directory.Exists)
				fInfo.Directory.Create();

			var openStream = new FileStreamWrapper(new FileStream(savePath, FileMode.Open, FileAccess.Read, FileShare.Read));
            var saveStream = new FileStreamWrapper(new FileStream(savePath1, FileMode.Create, FileAccess.Write, FileShare.Read));

			provider.Setup(file => file.WriteableStream()).Returns(saveStream);
			provider.Setup(file => file.ReadableStream()).Returns(openStream);

            var writer = new HoldemHandHistoryWriter(dataContext.Object, configService.Object, provider.Object);

			writer.GetHandReplay(provider.Object);

			writer.WriteHandHistory(new History(Game()));
			writer.WriteHandHistory(new History(Game()));

			//There is one stored plus an additional one, i.e. 2.
			Assert.IsNotNull(writer);
			Assert.AreEqual(2, writer.HandHistory.Count());

			if (fInfo.Directory.Exists)
				fInfo.Directory.Delete(true);
		}

        [Test]
		public void HoldemHandHistoryWriter_WriteHandHistory_Load_Existing_Exception()
		{
			var configService = new Mock<IPhoneConfiguration>();
			var provider = new Mock<IReplayEngineProvider>();
            var dataContext = new Mock<IHandHistoryDataContext>();

			const string savePath = "c:\\temp\\test_load_save.xml";
			const string savePath1 = "c:\\temp\\test_load_save1.xml";

		    var fInfo = new FileInfo(savePath);

			if (!fInfo.Directory.Exists)
				fInfo.Directory.Create();

            var saveStream = new FileStreamWrapper(new FileStream(savePath1, FileMode.Create, FileAccess.Write, FileShare.Read));

			provider.Setup(file => file.WriteableStream()).Returns(saveStream);
			provider.Setup(file => file.ReadableStream()).Throws<Exception>();

			var writer = new HoldemHandHistoryWriter(dataContext.Object, configService.Object, provider.Object);

			writer.GetHandReplay(provider.Object);

			writer.WriteHandHistory(new History(Game()));

			//There is one stored
			Assert.IsNotNull(writer);
			Assert.AreEqual(1, writer.HandHistory.Count());
		}

		[Test]
        public void HoldemHandHistoryWriter_WriteHandHistory_Save_Existing()
        {
            var holdemService = new Mock<IConfigurationService>();
            var provider = new Mock<IReplayEngineProvider>();
            var mockStorage = new Mock<IPhoneConfiguration>();
            var isolatedStorageFile = new Mock<IFileWrapper>();
		    var dataContext = new Mock<IHandHistoryDataContext>();

            const string savePath = "test_save.xml";

            var fInfo = new FileInfo(savePath);

            if (!fInfo.Directory.Exists)
                fInfo.Directory.Create();

            var history =
                new HandHistoryWriterCollection(
                    new List<History>
			        {
			            new History(Game())
			        });

            using (var saveStream = new FileStreamWrapper(new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read)))
            {
                using (var loadStream = new FileStreamWrapper(ConvertHandToStream(history)))
                {
                    isolatedStorageFile
                        .Setup(x => x.FileExists(It.IsAny<string>()))
                        .Returns(true);
                    isolatedStorageFile
                        .Setup(x => x.DirectoryExists(It.IsAny<string>()))
                        .Returns(true);
                    //Load Existing File
                    isolatedStorageFile
                        .Setup(x => x.OpenFile(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.Read))
                        .Returns(loadStream);
                    //Save New File
                    isolatedStorageFile
                        .Setup(x => x.OpenFile(It.IsAny<string>(), FileMode.Create, FileAccess.Write, FileShare.Read))
                        .Returns(saveStream);
                    holdemService
                        .Setup(x => x.PersistentConfiguration)
                        .Returns(mockStorage.Object);
                    holdemService
                        .Setup(x => x.IsolatedStorage)
                        .Returns(isolatedStorageFile.Object);

                    var writer = new HoldemHandHistoryWriter(dataContext.Object, holdemService.Object.PersistentConfiguration, provider.Object);

                    writer.WriteHandHistory(new History(Game()));

                    var expected = writer.HandHistory;

                    Assert.IsNotNull(expected);
                    Assert.AreEqual(2, expected.Count());
                }
            }
        }

		[Test]
		public void HoldemHandHistoryWriter_WriteHandHistory_GetHandReplay()
		{
            var holdemService = new Mock<IConfigurationService>();
            var provider = new Mock<IReplayEngineProvider>();
            var mockStorage = new Mock<IPhoneConfiguration>();
            var isolatedStorageFile = new Mock<IFileWrapper>();
            var dataContext = new Mock<IHandHistoryDataContext>();

            const string savePath = "c:\\temp\\test_save.xml";

            var fInfo = new FileInfo(savePath);

            if (!fInfo.Directory.Exists)
                fInfo.Directory.Create();

            var history =
                new HandHistoryWriterCollection(
                    new List<History>
			        {
			            new History(Game())
			        });

            using (var saveStream =
                new FileStreamWrapper(
                    new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read)))
            {
                using (var loadStream = new FileStreamWrapper(ConvertHandToStream(history)))
                {
                    isolatedStorageFile
                        .Setup(x => x.FileExists(It.IsAny<string>()))
                        .Returns(true);
                    isolatedStorageFile
                        .Setup(x => x.DirectoryExists(It.IsAny<string>()))
                        .Returns(true);
                    //Load Existing File
                    isolatedStorageFile
                        .Setup(x => x.OpenFile(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.Read))
                        .Returns(loadStream);
                    //Save New File
                    isolatedStorageFile
                        .Setup(x => x.OpenFile(It.IsAny<string>(), FileMode.Create, FileAccess.Write, FileShare.Read))
                        .Returns(saveStream);
                    holdemService
                        .Setup(x => x.PersistentConfiguration)
                        .Returns(mockStorage.Object);
                    holdemService
                        .Setup(x => x.IsolatedStorage)
                        .Returns(isolatedStorageFile.Object);
                    dataContext.Setup(ctx => ctx.GetAllHandHistory())
                        .Returns(
                            new List<History> { new History() });

                    var writer = new HoldemHandHistoryWriter(dataContext.Object, holdemService.Object.PersistentConfiguration, provider.Object);

                    var expected = writer.GetHandReplay(provider.Object);

                    Assert.IsNotNull(expected);
                    Assert.AreEqual(1, expected.Count());
                }
            }

            if (fInfo.Directory.Exists)
                fInfo.Directory.Delete(true);
		}

		[Test]
		//TODO: Fix This.
		public void HoldemHandHistoryWriter_WriteHandHistory_GetHandReplay_Default()
		{
			//var holdemService = new Mock<IHoldemService>();
			//var provider = new Mock<IReplayEngineProvider>();
			//var mockStorage = new Mock<IHoldemConfiguration<ConfigKey, object>>();
			//var isolatedStorageFile = new Mock<IHoldemIsolatedStorageFile>();

			//const string savePath = "c:\\temp\\test_save.xml";

			//var fInfo = new FileInfo(savePath);

			//if (!fInfo.Directory.Exists)
			//    fInfo.Directory.Create();

			//var history =
			//    new HandHistoryWriterCollection(
			//        new List<IHandHistory>
			//        {
			//            new History(Game())
			//        });

			//using (var saveStream =
			//    new FileStreamWrapper(
			//        new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read)))
			//{
			//    using (var loadStream = new FileStreamWrapper(ConvertHandToStream(history)))
			//    {
			//        isolatedStorageFile
			//            .Setup(x => x.FileExists(It.IsAny<string>()))
			//            .Returns(true);
			//        isolatedStorageFile
			//            .Setup(x => x.DirectoryExists(It.IsAny<string>()))
			//            .Returns(true);
			//        //Load Existing File
			//        isolatedStorageFile
			//            .Setup(x => x.OpenFile(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.Read))
			//            .Returns(loadStream);
			//        //Save New File
			//        isolatedStorageFile
			//            .Setup(x => x.OpenFile(It.IsAny<string>(), FileMode.Create, FileAccess.Write, FileShare.Read))
			//            .Returns(saveStream);
			//        holdemService
			//            .Setup(x => x.PersistentConfiguration)
			//            .Returns(mockStorage.Object);
			//        holdemService
			//            .Setup(x => x.IsolatedStorage)
			//            .Returns(isolatedStorageFile.Object);

			//        var writer = new HoldemHandHistoryWriter(dataContext.Object, holdemService.Object, provider.Object);

			//        var expected = writer.GetHandReplay();

			//        Assert.IsNotNull(expected);
			//        Assert.AreEqual(1, expected.ToList().Count);
			//    }
			//}

			//if (fInfo.Directory.Exists)
			//    fInfo.Directory.Delete(true);
		}

		[Test]
		//TODO: Fix This.
		public void HoldemHandHistoryWriter_WriteHandHistory_GetHandReplay_Loaded()
		{
			//var holdemService = new Mock<IHoldemService>();
			//var provider = new Mock<IReplayEngineProvider>();
			//var mockStorage = new Mock<IHoldemConfiguration<ConfigKey, object>>();
			//var isolatedStorageFile = new Mock<IHoldemIsolatedStorageFile>();

			//const string savePath = "c:\\temp\\test_save.xml";

			//var fInfo = new FileInfo(savePath);

			//if (!fInfo.Directory.Exists)
			//    fInfo.Directory.Create();

			//var history =
			//    new HandHistoryWriterCollection(
			//        new List<IHandHistory>
			//        {
			//            new History(Game())
			//        });

			//using (var saveStream =
			//    new FileStreamWrapper(
			//        new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read)))
			//{
			//    using (var loadStream = new FileStreamWrapper(ConvertHandToStream(history)))
			//    {
			//        isolatedStorageFile
			//            .Setup(x => x.FileExists(It.IsAny<string>()))
			//            .Returns(true);
			//        isolatedStorageFile
			//            .Setup(x => x.DirectoryExists(It.IsAny<string>()))
			//            .Returns(true);
			//        //Load Existing File
			//        isolatedStorageFile
			//            .Setup(x => x.OpenFile(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.Read))
			//            .Returns(loadStream);
			//        //Save New File
			//        isolatedStorageFile
			//            .Setup(x => x.OpenFile(It.IsAny<string>(), FileMode.Create, FileAccess.Write, FileShare.Read))
			//            .Returns(saveStream);
			//        holdemService
			//            .Setup(x => x.PersistentConfiguration)
			//            .Returns(mockStorage.Object);
			//        holdemService
			//            .Setup(x => x.IsolatedStorage)
			//            .Returns(isolatedStorageFile.Object);

			//        var writer = new HoldemHandHistoryWriter(dataContext.Object, holdemService.Object, provider.Object);

			//        writer.WriteHandHistory(new History(Game()));

			//        var expected = writer.GetHandReplay();

			//        Assert.IsNotNull(expected);
			//        Assert.AreEqual(2, expected.ToList().Count);
			//    }
			//}

			//if (fInfo.Directory.Exists)
			//    fInfo.Directory.Delete(true);
		}

		[Test]
		//TODO: Fix This.
		public void HoldemHandHistoryWriter_WriteHandHistory_Save_Existing_Exception_Base()
		{
			//var holdemService = new Mock<IHoldemService>();
			//var provider = new Mock<IReplayEngineProvider>();
			//var mockStorage = new Mock<IHoldemConfiguration<ConfigKey, object>>();
			//var isolatedStorageFile = new Mock<IHoldemIsolatedStorageFile>();

			//const string savePath = "c:\\temp\\test_save.xml";

			//var fInfo = new FileInfo(savePath);

			//var history =
			//    new HandHistoryWriterCollection(
			//        new List<IHandHistory>
			//        {
			//            new History(Game())
			//        });

			//using (var loadStream = new FileStreamWrapper(ConvertHandToStream(history)))
			//{
			//    isolatedStorageFile
			//        .Setup(x => x.FileExists(It.IsAny<string>()))
			//        .Returns(true);
			//    isolatedStorageFile
			//        .Setup(x => x.DirectoryExists(It.IsAny<string>()))
			//        .Returns(true);
			//    //Load Existing File
			//    isolatedStorageFile
			//        .Setup(x => x.OpenFile(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.Read))
			//        .Returns(loadStream);
			//    //Save New File
			//    isolatedStorageFile
			//        .Setup(x => x.OpenFile(It.IsAny<string>(), FileMode.Create, FileAccess.Write, FileShare.Read))
			//        .Throws(new Exception("My exception."));
			//    holdemService
			//        .Setup(x => x.PersistentConfiguration)
			//        .Returns(mockStorage.Object);
			//    holdemService
			//        .Setup(x => x.IsolatedStorage)
			//        .Returns(isolatedStorageFile.Object);

			//    var writer = new HoldemHandHistoryWriter(dataContext.Object, holdemService.Object, provider.Object);

			//    writer.WriteHandHistory(new History(Game()));

			//    var expected = writer.History;

			//    Assert.IsNotNull(expected);
			//    Assert.AreEqual(2, expected.ToList().Count);
			//}

			//if (fInfo.Directory.Exists)
			//    fInfo.Directory.Delete(true);
		}

        [Test]
        public void HoldemHandHistoryWriter_DeleteHandHistory()
		{
			var configService = new Mock<IPhoneConfiguration>();
			var provider = new Mock<IReplayEngineProvider>();
            var dataContext = new Mock<IHandHistoryDataContext>();
          
			var writer = new HoldemHandHistoryWriter(dataContext.Object, configService.Object, provider.Object);

			var history = new History(Game());

			writer.WriteHandHistory(new List<History> { history });

			var expected = writer.HandHistory;

			Assert.IsNotNull(expected);
			Assert.AreEqual(1, expected.Count());

			writer.DeleteHandHistory();

			Assert.AreEqual(0, writer.HandHistory.Count());
		}

		[Test]
		//TODO: Fix This.
		public void HoldemHandHistoryWriter_FlushHandHistory_Exception()
		{
			//var holdemService = new Mock<IHoldemService>();
			//var provider = new Mock<IReplayEngineProvider>();
			//var mockStorage = new Mock<IHoldemConfiguration<ConfigKey, object>>();
			//var isolatedStorageFile = new Mock<IHoldemIsolatedStorageFile>();

			//isolatedStorageFile
			//    .Setup(x => x.DirectoryExists(It.IsAny<string>()))
			//    .Throws(new Exception("Failed flush."));
			//holdemService
			//    .Setup(x => x.PersistentConfiguration)
			//    .Returns(mockStorage.Object);
			//holdemService
			//    .Setup(x => x.IsolatedStorage)
			//    .Returns(isolatedStorageFile.Object);

			//var writer = new HoldemHandHistoryWriter(dataContext.Object, holdemService.Object, provider.Object);

			//var history = new History(Game());

			//writer.AddHandHistory((IEnumerable<IHandHistory>)new List<IHandHistory> { history });

			//var expected = writer.History;

			//writer.FlushHandHistory();

			//Assert.AreEqual(1, expected.ToList().Count);
		}

		[Test]
		//TODO: Fix This.
		public void HoldemHandHistoryWriter_WriteHandHistory_Flush_Create_Directory()
		{
			//var holdemService = new Mock<IHoldemService>();
			//var provider = new Mock<IReplayEngineProvider>();
			//var mockStorage = new Mock<IHoldemConfiguration<ConfigKey, object>>();
			//var isolatedStorageFile = new Mock<IHoldemIsolatedStorageFile>();

			//const string savePath = "c:\\temp\\test_save.xml";

			//var fInfo = new FileInfo(savePath);

			//if (!fInfo.Directory.Exists)
			//    fInfo.Directory.Create();

			//var history =
			//    new HandHistoryWriterCollection(
			//        new List<IHandHistory>
			//        {
			//            new History(Game())
			//        });

			//using (var saveStream = new FileStreamWrapper(new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read)))
			//{
			//    using (var loadStream = new FileStreamWrapper(ConvertHandToStream(history)))
			//    {
			//        isolatedStorageFile
			//            .Setup(x => x.FileExists(It.IsAny<string>()))
			//            .Returns(true);
			//        isolatedStorageFile
			//            .Setup(x => x.DirectoryExists(It.IsAny<string>()))
			//            .Returns(false);
			//        //Load Existing File
			//        isolatedStorageFile
			//            .Setup(x => x.OpenFile(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.Read))
			//            .Returns(loadStream);
			//        //Save New File
			//        isolatedStorageFile
			//            .Setup(x => x.OpenFile(It.IsAny<string>(), FileMode.Create, FileAccess.Write, FileShare.Read))
			//            .Returns(saveStream);
			//        holdemService
			//            .Setup(x => x.PersistentConfiguration)
			//            .Returns(mockStorage.Object);
			//        holdemService
			//            .Setup(x => x.IsolatedStorage)
			//            .Returns(isolatedStorageFile.Object);

			//        var writer = new HoldemHandHistoryWriter(dataContext.Object, holdemService.Object, provider.Object);

			//        writer.WriteHandHistory(new History(Game()));

			//        var expected = writer.History;

			//        Assert.IsNotNull(expected);
			//        Assert.AreEqual(2, expected.ToList().Count);
			//    }
			//}

			//if (fInfo.Directory.Exists)
			//    fInfo.Directory.Delete(true);
		}

		#region Helper Methods

		private static History Game()
		{
			var game = new History();

			var cardGen = new CardGenerator(new NumberGenerator());

		    game.HoleCardOne = cardGen.GetCard(new Random(Environment.TickCount).Next(0, 53));
            game.HoleCardTwo = cardGen.GetCard(new Random(Environment.TickCount).Next(0, 53));
            game.FlopCardOne = cardGen.GetCard(new Random(Environment.TickCount).Next(0, 53));
            game.FlopCardTwo = cardGen.GetCard(new Random(Environment.TickCount).Next(0, 53));
            game.FlopCardThree = cardGen.GetCard(new Random(Environment.TickCount).Next(0, 53));
            game.TurnCard = cardGen.GetCard(new Random(Environment.TickCount).Next(0, 53));
            game.RiverCard = cardGen.GetCard(new Random(Environment.TickCount).Next(0, 53));

			return game;
		}

		private static FileStream ConvertHandToStream(HandHistoryWriterCollection games)
		{
			var serializer = new XmlSerializer(typeof(HandHistoryWriterCollection));

			const string savePath = "test_load.xml";

			var dInfo = new FileInfo(savePath).Directory;

			if (!dInfo.Exists)
				dInfo.Create();

			using (var fStream = new FileStream(savePath, FileMode.Create))
			{
				serializer.Serialize(fStream, games);
			}

			return File.Open("c:\\temp\\test_load.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		#endregion  //Helper Methods
	}
}