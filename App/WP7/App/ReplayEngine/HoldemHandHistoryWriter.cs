using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Common.Core.Configuration;
using TexasHoldemCalculator.Core.Entities.Collections;
using TexasHoldemCalculator.Interfaces.Database;
using TexasHoldemCalculator.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.ReplayEngine
{
	public class HoldemHandHistoryWriter : HoldemHandHistoryWriterBase
	{
		private readonly XmlSerializer _serializer;
		private readonly XmlWriterSettings _handHistoryWriterSettings;
		private readonly XmlSerializerNamespaces _removeNamespaces;

        public HoldemHandHistoryWriter(
            IHandHistoryDataContext dataContext,
            IPhoneConfiguration configuration,
            IReplayEngineProvider replayEngineProvider)
            : base(dataContext, configuration, replayEngineProvider)
        {

            _handHistoryWriterSettings =
                new XmlWriterSettings
                {
                    Indent = true,
                    CloseOutput = true,
                    OmitXmlDeclaration = true,
                    Encoding = Encoding.UTF8
                };

            _removeNamespaces = new XmlSerializerNamespaces();

            if(_removeNamespaces.Count == 0)
            {
                _removeNamespaces.Add("", "");
            }

            _serializer = new XmlSerializer(typeof(HandHistoryWriterCollection));
        }

	    #region Implementation of IHandHistoryWriter

		public override string DisplayName
		{
			get
			{
				return "THC Hand History";
			}
		}

		protected override HandHistoryWriterCollection LoadExistingHandHistory(Stream fileStream)
		{
			var collection = new HandHistoryWriterCollection();

			try
			{
				using (var reader = new StreamReader(fileStream))
				{
					collection = _serializer.Deserialize(reader.BaseStream) as HandHistoryWriterCollection;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return collection;
		}

		protected override void SaveExistingHandHistory(Stream fileStream)
		{
			try
			{
				using (var writer = XmlWriter.Create(fileStream, _handHistoryWriterSettings))
				{
					_serializer.Serialize(writer, this.HandHistory);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		#endregion
	}
}