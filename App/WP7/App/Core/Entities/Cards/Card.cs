using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using System.Xml.Serialization;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Core.Entities.Cards
{
    public class Card : ICard
    {
        #region Variables

        public static readonly ReadOnlyCollection<string> _cardNames
            = new ReadOnlyCollection<string>(
                new List<string>(13)
				{
					"Two",
					"Three",
					"Four",
					"Five",
					"Six",
					"Seven",
					"Eight",
					"Nine",
					"Ten",
					"Jack",
					"Queen",
					"King",
					"Ace"
				});

        public string CardPath
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public CardName Name
        {
            get;
            set;
        }

        public Image Image
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        [XmlAttribute("suit", typeof(Suit))]
        public Suit CardSuit
        {
            get;
            set;
        }

        #endregion //Variables

        public Card()
        {
        }

        public Card(CardName name, Suit suit)
        {
            this.CardSuit = suit;
            this.Name = name;
        }

        public Card(CardValue cardValue) : this(cardValue.Name, cardValue.Suit)
        {
        }

        #region ICard Members

        //[XmlAttribute("cardid")]
        public long CardId
        {
            get
            {
                return (long)this.Name;
                //return _cardId;
            }
            set
            {
                this.Name = (CardName)value;
            }
        }

        //[XmlAttribute("cardvalue")]
        public long CardValue
        {
            get
            {
                return (long)this.Name;
            }
            set
            {
                this.Name = (CardName)value;
            }
        }

        //[XmlAttribute("cardtext")]
        public string CardText
        {
            get
            {
                if (this.Name < CardName.Jack)
                {
                    var val = (int)this.Name;
                    return (val + 2).ToString(CultureInfo.InvariantCulture);
                }

                return (this.Name.ToString()).Substring(0, 1);
            }
            set
            {
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            var card = obj as Card;

            return (card != null) && this.Equals(card);
        }

        public bool Equals(Card obj)
        {
            return obj.Name == this.Name
                   && obj.CardSuit == this.CardSuit;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = this.CardId.GetHashCode();
                result = (result * 397) ^ this.CardSuit.GetHashCode();
                result = (result * 397) ^ this.Name.GetHashCode();
                return result;
            }
        }
    }
}