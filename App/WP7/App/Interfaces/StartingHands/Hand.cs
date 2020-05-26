using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
    public class Hand// : IHand
    {
        [XmlAttribute("Name")]
        public CardName Name
        {
            get;
            set;
        }
		
    	[XmlElement("Card")]
        public List<CardValue> Cards
        {
            get;
            set;
        }

        public Hand()
        {
            this.Cards = new List<CardValue>();
        }

		public CardValue FindHandByColumn(CardName columnCard)
		{
			var hand = this.Cards.FirstOrDefault(card => card.Name == columnCard);

			return hand;
		}

        public virtual XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
           reader.ReadAttributeValue();

            if( reader.HasAttributes && reader.MoveToAttribute("Name") )
            {
                this.Name = (CardName)Enum.Parse(typeof(CardName), reader.Value, true);
            }

            while( reader.Read() )
            {
                if( reader.IsStartElement() &&  string.Compare(reader.Name, "Card") == 0 )
                {
                    var card = new StartingHandCardValue();

                    card.ReadXml(reader);

                    this.Cards.Add(card);
                }

                if( reader.NodeType == XmlNodeType.EndElement )
                    break;
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Hand");

            writer.WriteAttributeString("Name", this.Name.ToString());

            foreach( var card in this.Cards )
            {
                card.WriteXml(writer);
            }

            writer.WriteEndElement();
        }
    }
}