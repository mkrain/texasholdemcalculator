using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Xml;
using System.Xml.Serialization;
using Common.Core.Configuration.IsolatedStorage;
using GalaSoft.MvvmLight.Messaging;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Exceptions;
using TexasHoldemCalculator.Interfaces.Messages;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace TexasHoldemCalculator.Core.Provider
{
    public class StartingHandsManager : IStartingHandsManager
    {
        #region Variables

        private const string RESOURCE_DIRECTORY = "Core.Provider.StartingHands";
        private const string HAND_RESOURCE = "{0}.{1}.{2}.xml";
        private const string HAND_DIRECTORY = @"StartingHands";
        private const string CUSTOM_HAND_ID = " *C";
        private static readonly string _assemblyName;
        private readonly IDictionary<string, StartingHand> _startingHands;
        private static readonly XmlReaderSettings _handsReaderSettings;
        private static readonly XmlSerializerNamespaces _removeNamespaces;
        private static readonly XmlSerializer _handSerializer;
        private readonly IFileWrapper _isoloatedStorage;

        #endregion //Variables

        #region Constructors

        static StartingHandsManager()
        {
            var name = Assembly.GetExecutingAssembly().FullName;
            _assemblyName = new AssemblyName(name).Name;

            _removeNamespaces = new XmlSerializerNamespaces();
            _removeNamespaces.Add("", "");

            //_handsWriterSettings =
            //    new XmlWriterSettings
            //    {
            //        Indent = true,
            //        CloseOutput = true,
            //        OmitXmlDeclaration = true,
            //        Encoding = Encoding.UTF8
            //    };

            _handsReaderSettings =
                new XmlReaderSettings
                    {
                        CloseInput = true,
                        IgnoreComments = true,
                        IgnoreWhitespace = true
                    };

            _handSerializer = new XmlSerializer(typeof(StartingHand));
        }

        public StartingHandsManager(IFileWrapper isoloatedStorage)
        {
            _isoloatedStorage = isoloatedStorage;

            if(!_isoloatedStorage.DirectoryExists(HAND_DIRECTORY))
            {
                _isoloatedStorage.CreateDirectory(HAND_DIRECTORY);
            }

            _startingHands = new Dictionary<string, StartingHand>();

            //These are embedded resources
            this.LoadStartingHandsFromResources();

            //These are custom downloaded hands
            this.LoadStartingHandsFromFolder();
        }

        #endregion //Constructors

        #region IStartingHandManager Implementation

        /// <summary>
        ///   Returns a list of starting hand titles.  Use <see cref = "GetHandFromTitle" /> to get
        ///   the actual starting hand, IStartingHand.
        /// </summary>
        public IList<string> AvailableStartingHandsByTitle
        {
            get
            {
                var titles = this.AvailableStartingHands.Select(h => h.Title);

                return titles.ToList();
            }
        }

        public IList<StartingHand> AvailableStartingHands
        {
            get
            {
                var embeddedHands =
                    (from hand in _startingHands.Values
                     orderby hand.Title
                     select hand).ToList();

                return embeddedHands;
            }
        }

        /// <summary>
        ///   Returns the starting hand, IStartingHand, based on the resource name.
        /// </summary>
        /// <param name = "resourceName">Embedded resource name.</param>
        /// <exception cref = "MissingManifestResourceException">Thrown if the resource is not an embedded resource.</exception>
        /// <returns></returns>
        public StartingHand GetHandFromResource(string resourceName)
        {
            if(_startingHands.ContainsKey(resourceName))
            {
                return _startingHands[resourceName];
            }

            var foundResource = this.FindStartingHandFromResource(resourceName);

            if(foundResource != null)
            {
                _startingHands.Add(resourceName, foundResource);
            }

            return foundResource;
        }

        /// <summary>
        ///   Returns the staring hand, IStartingHand, based on the resource name.
        /// </summary>
        /// <param name = "title">Title for the starting hand, i.e. Skylanksy Basic Strategy.</param>
        /// <returns>IStartinghand if found otherwise null.</returns>
        public StartingHand GetHandFromTitle(string title)
        {
            if(string.IsNullOrEmpty(title) || !_startingHands.ContainsKey(title))
            {
                return _startingHands.Values.FirstOrDefault();
            }

            var foundHand = _startingHands[title];

            return foundHand;
        }

        #endregion //IStartingHandManager Implementation

        #region Private Methods

        private void LoadStartingHandsFromResources()
        {
            var resources = this.GetType().Assembly.GetManifestResourceNames();

            if(resources.Length <= 0)
            {
                throw new InvalidOperationException("There are no resources for Holdem.Provider");
            }

            var res = resources.Where(x => x.Contains(string.Format("{0}.{1}.", _assemblyName, RESOURCE_DIRECTORY))).ToList();

            foreach(var x in res)
            {
                if(_startingHands.ContainsKey(x))
                {
                    continue;
                }

                var hand = this.FindStartingHandFromReliativeResource(x);

                if(hand != null)
                {
                    _startingHands.Add(hand.Title, hand);
                }
            }
        }

        /// <summary>
        /// 
        /// To distinguish custom hands from embedded resources
        /// custom hands will use the file name as a key rather
        /// than the embedded resource name.
        /// 
        /// </summary>
        private void LoadStartingHandsFromFolder()
        {
            var files = this.GetStoredCustomHandFiles();

            if(files == null || files.Count == 0)
            {
                return;
            }

            foreach(var file in files)
            {
                try
                {
                    var customHand = this.GetHandFromIsolatedStorage(file);

                    if(customHand == null)
                    {
                        continue;
                    }

                    //Distinguish between embedded and custom hands.
                    this.UpdateStartingHandCustomTitle(customHand);

                    if(_startingHands.ContainsKey(customHand.Title))
                    {
                        continue;
                    }

                    customHand.FileName = file;
                    _startingHands.Add(customHand.Title, customHand);
                }
                catch(Exception e)
                {

                }
            }
        }

        private void AddUniqueCustomStartingHand(StartingHand startingHand)
        {
            this.UpdateStartingHandCustomTitle(startingHand);

            this.AddUniqueStartingHand(startingHand);
        }

        private void AddUniqueStartingHand(StartingHand startingHand)
        {
            if(_startingHands.ContainsKey(startingHand.Title))
            {
                throw new DuplicateHandException();
            }

            _startingHands.Add(startingHand.Title, startingHand);
        }

        private StartingHand FindStartingHandFromReliativeResource(string resourceName)
        {
            var resource = this.GetType().Assembly.GetManifestResourceStream(resourceName);

            if(resource == null)
            {
                throw new MissingManifestResourceException("Could not find requested resource: " + resourceName);
            }

            using(var reader = XmlReader.Create(resource, _handsReaderSettings))
            {
                var hand = _handSerializer.Deserialize(reader) as StartingHand;

                return hand;
            }
        }

        private StartingHand FindStartingHandFromResource(string resourceName)
        {
            var format = HandFormatString(resourceName);

            return this.FindStartingHandFromReliativeResource(format);
        }

        private StartingHand GetStartingHandFromStream(Stream startingHand)
        {
            using(var reader = XmlReader.Create(startingHand, _handsReaderSettings))
            {
                var hand = _handSerializer.Deserialize(reader) as StartingHand;

                return hand;
            }
        }

        private StartingHand GetHandFromIsolatedStorage(string absoluteFileName)
        {
            using(
                var stream =
                    (Stream)_isoloatedStorage.OpenFile(absoluteFileName, FileMode.Open, FileAccess.Read, FileShare.None)
                )
            {
                //Corrupt file, delete
                if(stream.Length == 0)
                {
                    stream.Close();

                    this.DeleteFileFromIsolatedStorage(absoluteFileName);
                    return null;
                }

                return this.GetStartingHandFromStream(stream);
            }
        }

        private static string HandFormatString(string resourceName)
        {
            return string.Format(HAND_RESOURCE, _assemblyName, RESOURCE_DIRECTORY, resourceName);
        }

        private static string CustomHandFormatString(StartingHand startingHand)
        {
            return StartingHandFormatString(startingHand.FileName);
        }

        private static string StartingHandFormatString(string fileName)
        {
            return HAND_DIRECTORY + "/" + fileName;
        }

        private void UpdateStartingHandCustomTitle(StartingHand customStartingHand)
        {
            var title = customStartingHand.Title;

            customStartingHand.Title = title + CUSTOM_HAND_ID;
            customStartingHand.HandType = HandType.Custom;
        }

        private List<string> GetStoredCustomHandFiles()
        {
            var files = (from f in _isoloatedStorage.GetFileNames(HAND_DIRECTORY + "/*.xml")
                         select HAND_DIRECTORY + "/" + f).ToList();

            return files;
        }

        /// <summary>
        /// 
        /// Writes the file in the contexts stream to a file
        /// using the file name.
        /// 
        /// </summary>
        /// <param name="context">Context that contains a stream representation of a file and its file name.</param>
        public FileSaveStatus WriteResourceToFile(StartingHandSaveContext context)
        {
            if(_startingHands.ContainsKey(context.FileName))
            {
                return FileSaveStatus.FailedExisting;
            }

            var handToSave = StartingHandFormatString(context.FileName);

            try
            {
                using(var stream = (Stream)_isoloatedStorage.CreateFile(handToSave))
                {
                    const int length = 256;
                    var buffer = new Byte[length];

                    int bytesRead = context.StartingHandStream.Read(buffer, 0, length);

                    while(bytesRead > 0)
                    {
                        stream.Write(buffer, 0, bytesRead);
                        bytesRead = context.StartingHandStream.Read(buffer, 0, length);
                    }

                    context.StartingHandStream.Close();
                }

                using(
                    var readStream = _isoloatedStorage.OpenFile(
                        handToSave, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    var startingHand = this.GetStartingHandFromStream(readStream as Stream);

                    if(startingHand == null)
                    {
                        return FileSaveStatus.FailedRead;
                    }

                    startingHand.FileName = handToSave;

                    this.AddUniqueCustomStartingHand(startingHand);
                }
            }
            catch(Exception)
            {
                return FileSaveStatus.FailedOther;
            }

            return FileSaveStatus.Succeeded;
        }

        public void DeleteCustomStartingHand(StartingHand customHand)
        {
            try
            {
                if(!_isoloatedStorage.FileExists(customHand.FileName))
                {
                    this.NotifyObserversFileDeleted(customHand.Title, FileDeleteStatus.FailedMissing);
                    return;
                }

                if(this.DeleteFileFromIsolatedStorage(customHand.FileName))
                {
                    _startingHands.Remove(customHand.Title);
                    this.NotifyObserversFileDeleted(customHand.Title, FileDeleteStatus.Succeeded);
                    return;
                }
            }
            catch(Exception)
            {
                this.NotifyObserversFileDeleted(customHand.Title, FileDeleteStatus.FailedStorageError);
                return;
            }

            this.NotifyObserversFileDeleted(customHand.Title, FileDeleteStatus.FailedOther);
        }

        private bool DeleteFileFromIsolatedStorage(string absoluteFileName)
        {
            try
            {
                _isoloatedStorage.DeleteFile(absoluteFileName);
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        private void NotifyObserversFileDeleted(string title, FileDeleteStatus status)
        {
            Messenger.Default.Send(
                new DeletedCustomHandMessage(
                    new StartingHandDeletedDescription
                        {
                            Title = title,
                            Status = status
                        }));
        }

        #endregion //Private Methods
    }
}