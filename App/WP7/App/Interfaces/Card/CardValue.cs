using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TexasHoldemCalculator.Interfaces.Card
{
	[XmlRoot("Card"), XmlInclude(typeof(CardValue))]
	public class CardBase : IXmlSerializable
	{
		private HoldemCard _holdemCard = HoldemCard.Hole1;

		#region Implementation of IXmlSerializable

		public virtual XmlSchema GetSchema()
		{
			return null;
		}

		public virtual void ReadXml(XmlReader reader)
		{
			if( reader.HasAttributes && reader.MoveToAttribute("Card") )
				this.HoldemCard = (HoldemCard)Enum.Parse(typeof(HoldemCard), reader.Value, true);
			if( reader.HasAttributes && reader.MoveToAttribute("Name") )
				this.Name = (CardName)Enum.Parse(typeof(CardName), reader.Value, true);
			if( reader.HasAttributes && reader.MoveToAttribute("Suit") )
				this.Suit = (Suit)Enum.Parse(typeof(Suit), reader.Value, true);
			if( reader.HasAttributes && reader.MoveToAttribute("Strength") )
				this.Strength = int.Parse(reader.Value);
			if( reader.HasAttributes && reader.MoveToAttribute("Suited") )
				this.IsSuited = bool.Parse(reader.Value);
			if( reader.HasAttributes && reader.MoveToAttribute("Visible") )
			{
				this.IsVisible = bool.Parse(reader.Value);
				Visibility = this.IsVisible ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		public virtual void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString("Card", this.HoldemCard.ToString());
			writer.WriteAttributeString("Name", this.Name.ToString());
			writer.WriteAttributeString("Suit", this.Suit.ToString());
			writer.WriteAttributeString("Strength", this.Strength.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("Suited", this.IsSuited.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("Visible", this.IsVisible.ToString(CultureInfo.InvariantCulture));
		}

		#endregion

		#region Implementation of CardValue

        [XmlAttribute("Suit", typeof(Suit))]
        public virtual Suit Suit { get; set; }

		[XmlAttribute("Name", typeof(CardName))]
		public virtual CardName Name
		{
			get;
			set;
		}

		[XmlAttribute("Card", typeof(HoldemCard))]
		public virtual HoldemCard HoldemCard
		{
			get
			{
				return _holdemCard;
			}
			set
			{
				_holdemCard = value;
			}
		}

		[XmlAttribute("Strength")]
		public virtual int Strength
		{
			get;
			set;
		}

		[XmlAttribute("Suited")]
		public virtual bool IsSuited
		{
			get;
			set;
		}

		[XmlAttribute("IsVisible")]
        public virtual bool IsVisible
		{
			get;
			set;
		}

		public Visibility Visibility
		{
			get;
			set;
		}

        public Color Highlight
        {
            get;
            set;
        }

	    public SolidColorBrush HighlightBrush { get; set; }

        public CardName Parent
        {
            get;
            set;
        }

		public int Row
		{
			get
			{
				return 12 - (int)this.Parent;
			}
			set
			{
				this.Parent = (CardName)value;
			}
		}

		public int Column
		{
			get
			{
				return 12 - (int)this.Name;
			}
			set
			{
				this.Name = (CardName)value;
			}
		}

		public int ColumnSpan
		{
			get
			{
				return 1;
			}
		}

		#endregion

		public CardBase()
		{ 
		}

        public CardBase(CardValue card) : this(card.Suit, card.Name, card.HoldemCard)
        {
        }

		public CardBase(Suit suit = Suit.Club, CardName name = CardName.Two, HoldemCard card = HoldemCard.Flop1)
		{
			this.Suit = suit;
			this.Name = name;
			this.HoldemCard = card;
		}

	    public static bool operator ==(CardBase left, CardBase right)
        {
            if( ReferenceEquals(left, right) )
                return true;
            if( Equals(left, null) || Equals(right, null) )
                return false;
            return left.Name == right.Name 
                && left.Suit == right.Suit 
                && left.GameId == right.GameId;
        }

        public static bool operator !=(CardBase left, CardBase right)
        {
            return !( left == right );
        }

        public override bool Equals(object obj)
        {
            var card = obj as CardBase;

            if(card == null)
                return false;

            return this.Name == card.Name 
                && this.Suit == card.Suit 
                && this.GameId == card.GameId;
        }

        //private bool Equals(CardValue obj)
        //{
        //    return this.Name == obj.Name 
        //        && this.Suit == obj.Suit 
        //        && this.GameId == obj.GameId;
        //}

		public override int GetHashCode()
		{
			unchecked
			{
				return ( this.GameId.GetHashCode() * 613 ) ^ ( this.Suit.GetHashCode() * 397 ) ^ this.Name.GetHashCode();
			}
		}

		public override string ToString()
		{
		    string name;

		    switch( Name )
		    {
		            case CardName.Ace:
                    case CardName.King:
                    case CardName.Queen:
                    case CardName.Jack:
		                name = this.Name.ToString().Substring(0, 1);
		            break;
                default:
                        name = ((int)this.Name + 2).ToString(CultureInfo.InvariantCulture);
		            break;
		    }
		    
		    string suit = this.Suit.ToString().Substring(0, 1).ToLower();

			return string.Format("{0}{1}, GameId: {2}", name, suit, GameId);
		}

	    #region Implementation of IPersistable

	    public virtual long Id { get; set; }

        public virtual long GameId { get; set; }

	    #endregion
	}

    [Table(Name = "Cards")]
    public class CardValue : CardBase
    {
        private EntityRef<HandHistory.History> _history;

        [Column(CanBeNull = false, IsPrimaryKey = true)]
        public override long GameId { get; set; }

        [Column(CanBeNull = false, IsPrimaryKey = true)]
        public override CardName Name { get; set; }

        [Column(CanBeNull = false, IsPrimaryKey = true)]
        public override Suit Suit { get; set; }

        [Column(CanBeNull = false)]
        public override int Strength { get; set; }

        [Column(CanBeNull = false)]
        public override bool IsSuited { get; set; }

        [Column(CanBeNull = false)]
        public override bool IsVisible { get; set; }

        [Column(CanBeNull = false)]
        public override HoldemCard HoldemCard { get; set; }

        [Association(Storage = "_history", ThisKey = "GameId", OtherKey = "Id", IsForeignKey = true, IsUnique = true)]
        public HandHistory.History History
        {
            get { return _history.Entity; }
            set
            {
                if( value == null )
                    return;

                _history.Entity = value;
                this.GameId = value.Id;
            }
        }

        public CardValue()
        {
        }

        public CardValue(CardValue card) : base(card)
        {
        }

        public CardValue(Suit suit, CardName name) : base(suit, name)
        {
            //this.Suit = suit;
            //this.Name = name;
        }

        public CardValue(Suit suit, CardName name, HoldemCard card) : base(suit, name, card)
        {
            //this.Suit = suit;
            //this.Name = name;
            //this.HoldemCard = card;
        }
    }
}