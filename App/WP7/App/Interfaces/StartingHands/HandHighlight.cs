using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
    public class HandHighlight : IHandHighlight
    {
        [XmlElement("Strength")]
        public IDictionary<int, IHandStrength> HandStrength
        {
            get;
            set;
        }

        public HandHighlight()
        {
            this.HandStrength = new Dictionary<int, IHandStrength>();
        }

        public virtual XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement()
                    && string.Compare(reader.Name, "Strength") == 0)
                {
                    var handStrength = new HandStrength();

                    handStrength.ReadXml(reader);

                    this.HandStrength.Add(handStrength.Id, handStrength);
                }

                if (reader.NodeType == XmlNodeType.EndElement)
                    break;
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("HandHighlight");

            foreach (var strength in this.HandStrength.Keys)
            {
                this.HandStrength[strength].WriteXml(writer);
            }

            writer.WriteEndElement();
        }
    }
}
