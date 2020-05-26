using System;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
    [XmlRoot("Card"), XmlInclude(typeof(CardValue))]
    public class StartingHandCardValue : CardValue
    {
        #region Implementation of IXmlSerializable

        public override void ReadXml(XmlReader reader)
        {
            if (reader.HasAttributes && reader.MoveToAttribute("Name"))
                this.Name = (CardName)Enum.Parse(typeof(CardName), reader.Value, true);
            if (reader.HasAttributes && reader.MoveToAttribute("Strength"))
                this.Strength = int.Parse(reader.Value);
            if (reader.HasAttributes && reader.MoveToAttribute("Suit"))
                this.Suit = (Suit)Enum.Parse(typeof(Suit), reader.Value, true);
            if (reader.HasAttributes && reader.MoveToAttribute("Suited"))
                this.IsSuited = bool.Parse(reader.Value);
            if (reader.HasAttributes && reader.MoveToAttribute("Visible"))
                this.IsVisible = bool.Parse(reader.Value);
            if (reader.HasAttributes && reader.MoveToAttribute("Visible"))
            {
                this.IsVisible = bool.Parse(reader.Value);
                Visibility = this.IsVisible ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Card");
            writer.WriteAttributeString("Name", this.Name.ToString());
            writer.WriteAttributeString("Strength", this.Strength.ToString());
            writer.WriteAttributeString("Suit", this.Suit.ToString());
            writer.WriteAttributeString("Suited", this.IsSuited.ToString());
            writer.WriteAttributeString("Visible", this.IsVisible.ToString());
            writer.WriteEndElement();
        }

        #endregion

        public StartingHandCardValue()
        {
        }

        public StartingHandCardValue(Suit suit, CardName name) : base(suit, name)
        {
        }

        public StartingHandCardValue(Suit suit, CardName name, HoldemCard card) : base(suit, name, card)
        {
        }

        public override string ToString()
        {
            return "Name[" + this.Name + "], Suit[" + this.Suit + "].";
        }

        public override long Id { get; set; }
    }
}