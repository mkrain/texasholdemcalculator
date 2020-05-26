using System;
using System.IO;
using Common.Core.Configuration.Service;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.ReplayEngine
{
    public class HandHistoryReplayEngineProvider : IReplayEngineProvider
    {
        private const string DEFAULT_HISTORY_SAVE_DIRECTORY = "/HoldemHandHistory";
        private readonly string _generateFileName = "HandHistory_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xml";
        private readonly IConfigurationService _configuration;

        private string HandHistorySaveDirectory
        {
            get;
            set;
        }

        public HandHistoryReplayEngineProvider(IConfigurationService configuration)
        {
            _configuration = configuration;

            if(!_configuration.PersistentConfiguration.ContainsKey(ConfigKey.HandHistorySaveDirectory))
            {
                _configuration.PersistentConfiguration.Add(
                    ConfigKey.HandHistorySaveDirectory, DEFAULT_HISTORY_SAVE_DIRECTORY);
            }

            var historySaveDirectory =
                _configuration.PersistentConfiguration.Get<string>(ConfigKey.HandHistorySaveDirectory);

            var saveDir = historySaveDirectory ?? DEFAULT_HISTORY_SAVE_DIRECTORY;

            //Re-save the directory as a precaution.
            this.HandHistorySaveDirectory = saveDir;

            if(!_configuration.IsolatedStorage.DirectoryExists(this.HandHistorySaveDirectory))
            {
                _configuration.IsolatedStorage.CreateDirectory(this.HandHistorySaveDirectory);
            }
        }

        #region Implementation of IReplayEngineProvider

        public Stream WriteableStream()
        {
            var saveFileName = this.HandHistorySaveDirectory + "/" + _generateFileName;

            var stream = 
                _configuration.IsolatedStorage.OpenFile(
                    saveFileName, FileMode.Create, FileAccess.Write);

            return stream as Stream;
        }

        public Stream ReadableStream()
        {
            var saveFileName = this.HandHistorySaveDirectory + "/" + _generateFileName;

            var stream =
                _configuration.IsolatedStorage.OpenFile(
                    saveFileName, FileMode.OpenOrCreate, FileAccess.Read);

            return stream as Stream;
        }

        #endregion
    }
}