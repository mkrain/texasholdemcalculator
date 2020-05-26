using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
//using System.Text;
using System.Xml;
using System.Xml.Serialization;


using Holdem.Interfaces.StartingHands;

namespace HandGenerator.Model
{
    public class StartingHandsGeneratorModel
    {
        private const string RESOURCE_NAME = "Starting.Hands.Template.xml";
        private const string ALL_HANDS_ENABLED = "Starting.Hands.Template.All.Enabled.xml";
        private const string ALL_HANDS_DISABLED = "Starting.Hands.Template.All.Disabled.xml";
        private static readonly XmlReaderSettings _handsReaderSettings;
        private static readonly XmlSerializerNamespaces _removeNamespaces;
        private static readonly XmlSerializer _handSerializer;
        private IStartingHand _startingHandTemplate;
        private IStartingHand _originalStartingHand;

        static StartingHandsGeneratorModel()
        {
            _removeNamespaces = new XmlSerializerNamespaces();

            if (_removeNamespaces.Count == 0)
                _removeNamespaces.Add("", "");

            _handsReaderSettings =
                new XmlReaderSettings
                {
                    CloseInput = true,
                    IgnoreComments = true,
                    IgnoreWhitespace = true
                };

            _handSerializer = new XmlSerializer(typeof(StartingHand));
        }

        public StartingHandsGeneratorModel()
        {
            ImportStartingHandTemplate(RESOURCE_NAME);
        }

        public void ExportToStream(Stream stream, IStartingHand handToExport)
        {
            try
            {
                _handSerializer.Serialize(stream, handToExport);
            }
            catch
            {

            }
        }

        public void ImportFromStream(Stream stream)
        {
            var old = _startingHandTemplate;

            try
            {
                _startingHandTemplate = _handSerializer.Deserialize(stream) as IStartingHand;
            }
            catch
            {
                _startingHandTemplate = old;
            }
        }

        public void ResetHand()
        {
            ImportStartingHandTemplate(RESOURCE_NAME);
        }

        public void ResetAllEnabledHand()
        {
            ImportStartingHandTemplate(ALL_HANDS_ENABLED);
        }

        public void ResetAllDisabledHand()
        {
            ImportStartingHandTemplate(ALL_HANDS_DISABLED);
        }

        #region Private Methods

        private void ImportStartingHandTemplate(string resourceName)
        {
            var resource = this.ReadHandFromResource(resourceName);

            if (resource == null)
                throw new DataException("Could not find the starting hand template: " + resourceName);

            using (var reader = XmlReader.Create(resource, _handsReaderSettings))
            {
                _startingHandTemplate = _handSerializer.Deserialize(reader) as StartingHand;
                _originalStartingHand = _startingHandTemplate;
            }
        }

        private Stream ReadHandFromResource(string resourceName)
        {
            var assembly = Assembly.GetAssembly(this.GetType());
            var resources = assembly.GetManifestResourceNames();

            string found = resources.FirstOrDefault(x => x.Contains(resourceName));

            if (found == null)
                throw new DataException("Could not find the starting hand template: " + resourceName);

            var stream = assembly.GetManifestResourceStream(found);

            return stream;
        }

        #endregion //Private Methods

        public IStartingHand StartingHands
        {
            get
            {
                return _startingHandTemplate;
            }
        }
    }
}