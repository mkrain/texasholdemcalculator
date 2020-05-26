using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TexasHoldemCalculator.Core.Entities.Collections
{
    [XmlRoot(ElementName = "History")]
    public sealed class HandHistoryWriterCollection : HoldemBaseCollection<Interfaces.HandHistory.History>, IXmlSerializable
    {
        public HandHistoryWriterCollection(IEnumerable<Interfaces.HandHistory.History> handHistory) : base(handHistory)
        {
        }

        public HandHistoryWriterCollection()
        {

        }

        public void AddRange(IList<Interfaces.HandHistory.History> games)
        {
            base.AddRange(games);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.Read();

            var date = DateTime.Now;

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                if (reader.HasAttributes && reader.MoveToAttribute("Date"))
                    date = DateTime.Parse(reader.Value);

                reader.ReadStartElement("Game");

                var history =
                    new Interfaces.HandHistory.History 
                    {
                        Date = date
                    };

                history.ReadXml(reader);

                this.Add(history);

                reader.ReadEndElement();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (var game in this)
            {
                writer.WriteStartElement("Game");

                game.WriteXml(writer);

                writer.WriteEndElement();
            }
        }
    }
}