using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
    [XmlRoot("StartingHands")]
    public sealed class StartingHand : IStartingHand
    {
		[XmlAttribute("Id")]
		public int Id { get; set; }

        [XmlAttribute("Title")]
        public string Title
        {
            get;
            set;
        }

        [XmlElement("Description")]
        public string Description
        {
            get;
            set;
        }

        [XmlElement("HandHighlight")]
        public HandHighlight HandHighlight
        {
            get;
            set;
        }

        [XmlElement("Hand")]
        public IList<Hand> Hand
        {
            get;
            set;
        }

        [XmlAttribute("Type")]
        public HandType HandType { get; set; }

        [XmlIgnore]
        public bool IsSelected { get; set; }

        [XmlIgnore]
        public string FileName { get; set; }

        [XmlIgnore]
        public IList<CardValue> AllHands
        {
            get
            {
                var c = from hand in this.Hand
                        from ca in hand.Cards
                        select (CardValue)new StartingHandCardValue
                               {
                                   Highlight = ca.Highlight,
                                   HighlightBrush = new SolidColorBrush(ca.Highlight),
                                   IsVisible = ca.IsVisible,
                                   IsSuited = ca.IsSuited,
                                   Strength = ca.Strength,
                                   Suit = ca.Suit,
                                   Name = ca.Name,
                                   Parent = hand.Name
                               };

                return c.ToList();
            }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public StartingHand()
        {
            this.HandType = HandType.Embedded;
            this.Hand = new List<Hand>();
        }

        public Color FindColorFromStrength(int strength)
        {
            if (this.HandHighlight.HandStrength.ContainsKey(strength))
                return this.HandHighlight.HandStrength[strength].Color;
            return ColorNames.Transparent.FromName();
        }

        public void SetColorFromStrength(CardValue card)
        {
            if (card == null)
                return;

            var highlight = this.FindColorFromStrength(card.Strength);

            card.Highlight = highlight;
        }

        public CardValue FindHandByColumn(CardName rowCard, CardName columnCard)
        {
            var foundRow = this.FindHandByRow(rowCard);

            if (foundRow == null)
                return null;

            return foundRow.FindHandByColumn(columnCard);
        }

        public Hand FindHandByRow(CardName rowCard)
        {
            var foundRow = this.Hand.FirstOrDefault(row => row.Name == rowCard);

            return foundRow;
        }

        public bool HasHand(CardName rowCard, CardName columnCard)
        {
            var found = this.FindHandByRow(rowCard);

            if (found == null)
                return false;

            var foundHand = found.Cards.FirstOrDefault(colCard => colCard.Name == columnCard);

            return foundHand != null;
        }

        public void UpdateLikeStrengths(int oldStrength, int newStrength)
        {
            if (oldStrength == newStrength)
                return;

            var newHightlight = this.FindColorFromStrength(newStrength);

            var hands = from hand in this.Hand
                        from c in hand.Cards
                        where c.Strength == oldStrength
                        select c;

            foreach (var nHand in hands)
            {
                nHand.Strength = newStrength;
                nHand.Highlight = newHightlight;
            }
        }

        public bool ContainsStrength(int strength)
        {
            return this.HandHighlight.HandStrength.ContainsKey(strength);
        }

        /// <summary>
        /// 
        /// Sets the visibility on all cards with the same
        /// hand strength.
        /// 
        /// </summary>
        /// <param name="strength">The hand strength to update.</param>
        /// <param name="visibility">The updated visibility.</param>
        public void UpdateVisibilityToLikeStrength(int strength, bool visibility)
        {
            //Does a card have this strength
            var hands = from hand in this.Hand
                        from c in hand.Cards
                        where c.Strength == strength
                        select c;

            foreach (var nHand in hands)
                nHand.IsVisible = visibility;
        }

        /// <summary>
        /// 
        /// Use this if any of the hand highlight mappings
        /// has been updated or added.
        /// 
        /// </summary>
        public void RefreshHandStrength()
        {
            foreach (var card in this.Hand.SelectMany(hand => hand.Cards))
            {
                this.SetColorFromStrength(card);
            }
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.HasAttributes && reader.MoveToAttribute("Title"))
                this.Title = reader.Value;
			if (reader.HasAttributes && reader.MoveToAttribute("Id"))
				this.Id = int.Parse(reader.Value);
            if (reader.HasAttributes && reader.MoveToAttribute("Type"))
                this.HandType = (HandType)Enum.Parse(typeof(HandType), reader.Value, true);

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {

                    switch (reader.Name)
                    {
						case "Description":
                            reader.Read();
                            this.Description = reader.Value;
                            break;
                        case "HandHighlight":
                            var handHighlight = new HandHighlight();
                            handHighlight.ReadXml(reader);

                            this.HandHighlight = handHighlight;

                            break;
                        case "Hand":
                            var hand = new Hand();
                            hand.ReadXml(reader);

                            this.Hand.Add(hand);

                            break;
                    }
                }
            }

            foreach (var card in this.Hand.SelectMany(hand => hand.Cards))
            {
                this.SetColorFromStrength(card);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Title", this.Title);

			writer.WriteAttributeString("Id", this.Id.ToString(CultureInfo.InvariantCulture));

            writer.WriteAttributeString("Type", this.HandType.ToString());

            writer.WriteElementString("Description", this.Description);

            this.HandHighlight.WriteXml(writer);

            foreach (var hand in this.Hand)
            {
                hand.WriteXml(writer);
            }
        }
    }
}