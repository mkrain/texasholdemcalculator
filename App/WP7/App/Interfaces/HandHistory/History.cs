using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Extensions;

namespace TexasHoldemCalculator.Interfaces.HandHistory
{
    [XmlRoot("Game"), XmlInclude(typeof(CardValue)), Table(Name = "HandHistoryGame")]
    public class History : IHandHistory, IXmlSerializable
    {
        private IEnumerable<CardValue> _bestHand;
        private string _bestHandString;

        private EntitySet<CardValue> _hand = new EntitySet<CardValue>(); 
        private HandRank _rank = HandRank.Undefined;

        #region Implementation of IHandHistory

        public CardValue HoleCardOne
        {
            get
            {
                return this.CurrentCard(HoldemCard.Hole1);
            }
            set
            {
                AssertKeyExists(HoldemCard.Hole1, value);
            }
        }

        public CardValue HoleCardTwo
        {
            get
            {
                return this.CurrentCard(HoldemCard.Hole2);
            }
            set
            {
                AssertKeyExists(HoldemCard.Hole2, value);
            }
        }

        public CardValue FlopCardOne
        {
            get
            {
                return this.CurrentCard(HoldemCard.Flop1);
            }
            set
            {
                AssertKeyExists(HoldemCard.Flop1, value);
            }
        }

        public CardValue FlopCardTwo
        {
            get
            {
                return this.CurrentCard(HoldemCard.Flop2);
            }
            set
            {
                AssertKeyExists(HoldemCard.Flop2, value);
            }
        }

        public CardValue FlopCardThree
        {
            get
            {
                return this.CurrentCard(HoldemCard.Flop3);
            }
            set
            {
                AssertKeyExists(HoldemCard.Flop3, value);
            }
        }

        public CardValue TurnCard
        {
            get
            {
                return this.CurrentCard(HoldemCard.Turn);
            }
            set
            {
                AssertKeyExists(HoldemCard.Turn, value);
            }
        }

        public CardValue RiverCard
        {
            get
            {
                return this.CurrentCard(HoldemCard.River);
            }
            set
            {
                AssertKeyExists(HoldemCard.River, value);
            }
        }

        private CardValue CurrentCard(HoldemCard key)
        {
            return _hand.Count == 0 || Enumerable.All<CardValue>(_hand, c => c.HoldemCard != key) ? null : Enumerable.FirstOrDefault<CardValue>(_hand, c => c.HoldemCard == key);            
        }

        private void AssertKeyExists(HoldemCard cardKey, CardValue card)
        {
            if( _hand == null )
                throw new InvalidOperationException("The EntitySet _hand was not initialized.");
            if (card == null)
                throw new InvalidOperationException("The CardValue for the card was not initialized.");

            card.History = this;

            if( !_hand.Contains(card) )
                _hand.Add(card);
            else
                _hand[(int)cardKey] = card;
        }

        public IEnumerable<CardValue> Cards
        {
            get { return Enumerable.Select<CardValue, CardValue>(_hand, x => x).ToList(); }
        }

        [Column(CanBeNull = false, IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long Id { get; set; }

        public string HandValuation { get { return this.DetermineBestPossibleHand(); } }
        
        [Column(CanBeNull = false)]
        public HandRank HandRank
        {
            get
            {
                if( _rank == HandRank.Undefined )
                    _rank = this.GetHandRank();
                return _rank;
            }
            set { _rank = value; }
        }

        #endregion

        #region Implementation of IHandHistoryGameInfo

        [Column]
        public string TournamentId { get; set; }

        [Column]
        public string TableId { get; set; }

        [Column]
        public bool WonHand { get; set; }

        [Column]
        public string WonPotAmount { get; set; }

        [Column]
        public string TotalPotAmount { get; set; }

        [Column]
        public string GameDescription { get; set; }

        #endregion

        public History()
        {
            this.Date = DateTime.Now;
        }

        public History(History oldHistory)
        {
            if( oldHistory.HandSet != null )
                this.HandSet = oldHistory.HandSet;

            this.Date = oldHistory.Date;

            this.ValidateHand();
        }

        public History(IEnumerable<CardValue> cards)
        {
            if( cards == null )
                throw new ArgumentNullException("cards");

            var list = cards.ToList();

            if (list.Count < 7)
                throw new ArgumentException("There must be seven cards", "cards");

            var hole1 = list.FirstOrDefault(c => c.HoldemCard == HoldemCard.Hole1);
            var hole2 = list.FirstOrDefault(c => c.HoldemCard == HoldemCard.Hole2);
            var flop1 = list.FirstOrDefault(c => c.HoldemCard == HoldemCard.Flop1);
            var flop2 = list.FirstOrDefault(c => c.HoldemCard == HoldemCard.Flop2);
            var flop3 = list.FirstOrDefault(c => c.HoldemCard == HoldemCard.Flop3);
            var turn = list.FirstOrDefault(c => c.HoldemCard == HoldemCard.Turn);
            var river = list.FirstOrDefault(c => c.HoldemCard == HoldemCard.River);

            this.HoleCardOne = hole1;
            this.HoleCardTwo = hole2;
            this.FlopCardOne = flop1;
            this.FlopCardTwo = flop2;
            this.FlopCardThree = flop3;
            this.TurnCard = turn;
            this.RiverCard = river;
        }

        [Association(Storage = "_hand", ThisKey = "Id", OtherKey = "GameId", IsUnique = true)]
        public EntitySet<CardValue> HandSet
        {
            get { return _hand; }
            set { _hand.Assign(value); }
        }

        #region IHandHistory Members

        [XmlAttribute("Date"), Column(CanBeNull = false)]
        public DateTime Date { get; set; }

        public string DetermineBestPossibleHand()
        {
            if( _bestHand == null )
            {
                _bestHand = this.GetBestHand();
                _bestHandString = this.HandRank.Description();
            }

            return _bestHandString;
        }

        public IEnumerable<CardValue> GetBestHand()
        {
            return GetBestHand(this.Cards);
        }

        public bool IsRoyalFlush()
        {
            return IsRoyalFlush(this.Cards);
        }

        public bool IsStraightFlush()
        {
            return IsStraightFlush(this.Cards);
        }

        public bool IsFourOfAKind()
        {
            return IsFourOfAKind(this.Cards);
        }

        public bool IsFullHouse()
        {
            return ( this.IsThreeOfAKind() && this.IsPair() );
        }

        public bool IsFlush()
        {
            return IsFlush(this.Cards);
        }

        public bool IsStraight()
        {
            return IsStraight(this.Cards);
        }

        public bool IsThreeOfAKind()
        {
            return IsThreeOfAKind(this.Cards);
        }

        public bool IsTwoPair()
        {
            return IsTwoPair(this.Cards);
        }

        public bool IsPair()
        {
            return IsPair(this.Cards);
        }

        public IEnumerable<CardValue> GetRoyalFlush()
        {
            return GetRoyalFlush(this.Cards);
        }

        public IEnumerable<CardValue> GetStraightFlush()
        {
            return GetStraightFlush(this.Cards);
        }

        public IEnumerable<CardValue> GetFourOfAKind()
        {
            return GetFourOfAKind(this.Cards);
        }

        public IEnumerable<CardValue> GetFullHouse()
        {
            return GetFullHouse(this.Cards);
        }

        public IEnumerable<CardValue> GetFlush()
        {
            return GetFlush(this.Cards);
        }

        public IEnumerable<CardValue> GetStraight()
        {
            return GetStraight(this.Cards);
        }

        public IEnumerable<CardValue> GetThreeOfAKind()
        {
            return GetThreeOfAKind(this.Cards);
        }

        public IEnumerable<CardValue> GetTwoPair()
        {
            return GetTwoPair(this.Cards);
        }

        public IEnumerable<CardValue> GetPair()
        {
            return GetPair(this.Cards);
        }

        public IEnumerable<CardValue> GetHighCard()
        {
            return GetHighCard(this.Cards);
        }

        #endregion

        private HandRank GetHandRank()
        {
            return GetHandRank(this.Cards);
        }

        private Suit GetFlushSuit()
        {
            return GetFlushSuit(this.Cards);
        }

        private void ValidateHand()
        {
            if( this.HoleCardOne == null )
                throw new ArgumentException("A hand must include the first hole card");
            if( this.HoleCardTwo == null )
                throw new ArgumentException("A hand must include the second hole card");

            if( this.TurnCard == null && this.RiverCard == null )
                return;

            if( this.FlopCardOne == null )
                throw new ArgumentException("A hand must include the first flop card");
            if( this.FlopCardTwo == null )
                throw new ArgumentException("A hand must include the second flop card");
            if( this.FlopCardThree == null )
                throw new ArgumentException("A hand must include the third flop card");
        }

        #region Static Version Of Hand Ranks

        private static Suit GetFlushSuit(IEnumerable<CardValue> hand)
        {
            IEnumerable<Suit> query = ( from card in hand
                                        group card by card.Suit
                                        into k where k.Count() >= 5 select k.Key );

            return query.First();
        }

        public static bool IsPair(IEnumerable<CardValue> hand)
        {
            return GetPair(hand).Any();
        }

        public static bool IsTwoPair(IEnumerable<CardValue> hand)
        {
            return GetTwoPair(hand).Any();
        }

        public static bool IsThreeOfAKind(IEnumerable<CardValue> hand)
        {
            return GetThreeOfAKind(hand).Any();
        }

        public static bool IsStraight(IEnumerable<CardValue> hand)
        {
            return GetStraight(hand).Any();
        }

        public static bool IsFlush(IEnumerable<CardValue> hand)
        {
            return GetFlush(hand).Any();
        }

        public static bool IsFullHouse(IEnumerable<CardValue> hand)
        {
            return GetFullHouse(hand).Any();
        }

        public static bool IsFourOfAKind(IEnumerable<CardValue> hand)
        {
            return GetFourOfAKind(hand).Any();
        }

        public static bool IsStraightFlush(IEnumerable<CardValue> hand)
        {
            return GetStraightFlush(hand).Any();
        }

        public static bool IsRoyalFlush(IEnumerable<CardValue> hand)
        {
            var list = hand.ToList();

            if( !IsFlush(list) )
                return false;

            var suit = GetFlushSuit(list);

            var straight = from card in list
                           where ( card.Name == CardName.Ace || card.Name == CardName.King || card.Name == CardName.Queen || card.Name == CardName.Jack || card.Name == CardName.Ten ) && card.Suit == suit
                           select card;

            return straight.Count() == 5;
        }

        /// <summary>
        /// 
        /// Returns the cards sorted from high to low.
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public static IEnumerable<CardValue> GetHighCard(IEnumerable<CardValue> hand)
        {
            if( hand == null )
                return new List<CardValue>();

            var list = hand.ToList();

            if( list.Count < 7 )
                return new List<CardValue>();

            var query = from card in list
                        orderby card.Name descending
                        select card;

            return query;
        }

        /// <summary>
        /// 
        /// Returns the cards with highest pair first.
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public static IEnumerable<CardValue> GetPair(IEnumerable<CardValue> hand)
        {
            List<CardValue> list = hand.ToList();

            if( hand == null || list.Count < 2 )
                return new List<CardValue>();

            if( list.Count < 2 )
                return new List<CardValue>();

            IEnumerable<CardName> query = ( from card in list
                                            group card by card.Name
                                            into k where k.Count() == 2 select k.Key );

            if( !query.Any() )
                return new List<CardValue>();

            return GetMultipleCards(list, 2, 1, 2);
        }

        /// <summary>
        /// 
        /// Returns the cards with highest two pairs first.
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public static IEnumerable<CardValue> GetTwoPair(IEnumerable<CardValue> hand)
        {
            List<CardValue> list = hand.ToList();

            if( hand == null || list.Count < 4 )
                return new List<CardValue>();

            IEnumerable<CardName> query = ( from card in list
                                            group card by card.Name
                                            into k where k.Count() == 2 select k.Key );

            if( query.Count() < 2 )
                return new List<CardValue>();

            return GetMultipleCards(list, 2, 2, 4);
        }

        /// <summary>
        /// 
        /// Returns the cards with highest three of a kind first.
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public static IEnumerable<CardValue> GetThreeOfAKind(IEnumerable<CardValue> hand)
        {
            if( hand == null )
                return new List<CardValue>();

            List<CardValue> list = hand.ToList();

            if( list.Count < 3 )
                return new List<CardValue>();

            IEnumerable<CardName> query = ( from card in list
                                            group card by card.Name
                                            into k where k.Count() == 3 select k.Key );

            if( !query.Any() )
                return new List<CardValue>();

            return GetMultipleCards(list, 3, 1, 3);
        }

        /// <summary>
        /// 
        /// Returns the largest straight
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CardValue> GetStraight(IEnumerable<CardValue> hand)
        {
            List<CardValue> dummy = hand.ToList();

            if( hand == null || dummy.Count() < 5 )
                return new List<CardValue>();

            List<CardValue> list = ( from card in dummy
                                     orderby card.Name descending
                                     select card ).ToList();

            if( list.Count < 5 )
                return new List<CardValue>();

            //Is this a low straight?
            List<CardValue> lowStraight = ( from c in list
                                            orderby c.Name
                                            where c.Name == CardName.Ace || c.Name == CardName.Two || c.Name == CardName.Three || c.Name == CardName.Four || c.Name == CardName.Five
                                            select c ).Unique(c => c.Name).ToList();

            //Because A = 11, it would be sorted last, this
            //is a special case.
            if( lowStraight.Count == 5 )
            {
                List<CardValue> lstraight = lowStraight;
                var ace = new List<CardValue> {
                    lowStraight.First(c => c.Name == CardName.Ace)
                };
                IEnumerable<CardValue> original = list.Except(lstraight);

                return ace.Union(lstraight.Except(ace)).Union(original);
            }

            IEnumerable<int> ranges = ( from c in list
                                        orderby c.Name
                                        select (int)c.Name ).Distinct();

            IEnumerable<List<int>> partition = ranges.PartitionSequential();

            List<int> found = partition.FirstOrDefault(c => c.Count >= 5);

            if( found == null )
                return new List<CardValue>();

            int length = found.Count;

            List<CardValue> straight = ( from p in list
                                         from q in found.Skip(length - 5).Take(5)
                                         where (int)p.Name == q
                                         select p ).Unique(c => c.Name).ToList();

            IEnumerable<CardValue> difference = list.Except(straight);

            return straight.Union(difference);
        }

        /// <summary>
        /// 
        /// Returns the largest flush.
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CardValue> GetFlush(IEnumerable<CardValue> hand)
        {
            var dummy = new List<CardValue>();

            if( hand == null )
                return dummy;

            List<CardValue> list = hand.ToList();

            if( list.Count() < 5 )
                return dummy;

            IEnumerable<int> query = ( from card in list
                                       group card by card.Suit
                                       into k where k.Count() >= 5 select k.Count() );

            if( !query.Any() )
                return dummy;

            Suit flushSuit = GetFlushSuit(list);

            IOrderedEnumerable<CardValue> flush = from card in list
                                                  where card.Suit == flushSuit
                                                  orderby card.Name descending
                                                  select card;

            List<CardValue> found = flush.ToList();
            IEnumerable<CardValue> difference = list.Except(found);

            return found.Union(difference);
        }

        /// <summary>
        /// 
        /// Returns a full house, where the 3 cards
        /// are ordered before the highest pair.
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public static IEnumerable<CardValue> GetFullHouse(IEnumerable<CardValue> hand)
        {
            var dummy = new List<CardValue>();

            if( hand == null )
                return dummy;

            var list = hand.ToList();

            if( !( IsThreeOfAKind(list) && IsPair(list) ) )
                return dummy;

            //Get the highest three of a kind
            var trips = GetThreeOfAKind(list).Take(3).ToList();

            //Get the highest two of a kind
            var pair = GetPair(list).Take(2).ToList();

            var difference = list.Except(trips).Except(pair);

            return trips.Union(pair).Union(difference);
        }

        /// <summary>
        /// 
        /// Returns the cards with highest four of a kind first.
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public static IEnumerable<CardValue> GetFourOfAKind(IEnumerable<CardValue> hand)
        {
            var dummy = new List<CardValue>();

            if( hand == null )
                return dummy;

            var list = hand.ToList();

            if (list.Count < 4)
                return dummy;

            var query = ( from card in list
                                            group card by card.Name
                                            into k where k.Count() == 4 select k.Key );

            if( !query.Any() )
                return dummy;

            return GetMultipleCards(list, 4, 1, 4);
        }

        public static IEnumerable<CardValue> GetStraightFlush(IEnumerable<CardValue> hand)
        {
            var dummy = new List<CardValue>();

            if( hand == null )
                return dummy;

            var list = hand.ToList();

            var flush = GetFlush(list).ToList();

            if( flush.Count < 5 )
                return dummy;

            var straight = GetStraight(flush.Take(5)).ToList();

            if( straight.Count == 0 )
                return dummy;

            var difference = list.Except(straight);

            return straight.Union(difference);
        }

        public static IEnumerable<CardValue> GetRoyalFlush(IEnumerable<CardValue> hand)
        {
            var dummy = new List<CardValue>();

            if( hand == null )
                return dummy;

            var flush = GetFlush(hand).ToList();

            if( flush.Count == 0 )
                return dummy;

            var suit = GetFlushSuit(flush);

            var straight = ( from card in flush
                             where ( card.Name == CardName.Ace || card.Name == CardName.King || card.Name == CardName.Queen || card.Name == CardName.Jack || card.Name == CardName.Ten ) && card.Suit == suit
                             select card ).ToList();

            if( straight.Count() < 5 )
                return dummy;

            var royal = straight.Take(5).ToList();
            var difference = flush.Except(royal);

            return royal.Union(difference);
        }

        public static HandRank GetHandRank(IEnumerable<CardValue> hand)
        {
            var list = hand.ToList();

            if( IsRoyalFlush(list) )
                return HandRank.RoyalFlush;
            if( IsStraightFlush(list) )
                return HandRank.StraightFlush;
            if( IsFourOfAKind(list) )
                return HandRank.FourOfAKind;
            if( IsFullHouse(list) )
                return HandRank.FullHouse;
            if( IsFlush(list) )
                return HandRank.Flush;
            if( IsStraight(list) )
                return HandRank.Straight;
            if( IsThreeOfAKind(list) )
                return HandRank.ThreeOfAKind;
            if( IsTwoPair(list) )
                return HandRank.TwoPair;
            if( IsPair(list) )
                return HandRank.Pair;
            return HandRank.HighCard;
        }

        public static IEnumerable<CardValue> GetBestHand(IEnumerable<CardValue> hand)
        {
            var list = hand.ToList();

            if( IsRoyalFlush(list) )
                return GetRoyalFlush(list);
            if( IsStraightFlush(list) )
                return GetStraightFlush(list);
            if( IsFourOfAKind(list) )
                return GetFourOfAKind(list);
            if( IsFullHouse(list) )
                return GetFullHouse(list);
            if( IsFlush(list) )
                return GetFlush(list);
            if( IsStraight(list) )
                return GetStraight(list);
            if( IsThreeOfAKind(list) )
                return GetThreeOfAKind(list);
            if( IsTwoPair(list) )
                return GetTwoPair(list);
            if( IsPair(list) )
                return GetPair(list);
            return GetHighCard(list);
        }

        /// <summary>
        /// 
        /// Returns (listTake amount number of cards) from the hand list having group() == groupCount,
        /// taking the groupCount from the grouping.  
        /// 
        /// Used for:
        /// 
        /// * Pair
        /// * Two Pair
        /// * Three of a Kind
        /// * Four of a Kind
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="groupCount"></param>
        /// <param name="groupTake"></param>
        /// <param name="listTake"></param>
        /// <returns></returns>
        private static IEnumerable<CardValue> GetMultipleCards(IEnumerable<CardValue> hand, int groupCount, int groupTake, int listTake)
        {
            List<CardValue> list = ( from card in hand
                                     orderby card.Name descending
                                     select card ).ToList();

            IEnumerable<CardName> query = ( from card in list
                                            group card by card.Name
                                            into k where k.Count() == groupCount select k.Key ).OrderByDescending(c => c).Take(groupTake);

            List<CardValue> found = list.Where(c => query.Contains(c.Name)).Take(listTake).ToList();

            IEnumerable<CardValue> difference = list.Except(found);

            return found.Union(difference);
        }

        #endregion

        #region Implementation of IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while( reader.NodeType != XmlNodeType.EndElement )
            {
                var value = new CardValue();

                value.ReadXml(reader);

                this.SetHand(value);

                reader.Read();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Date", this.Date.ToString(CultureInfo.InvariantCulture));

            this.WriteHand(writer, this.HoleCardOne);
            this.WriteHand(writer, this.HoleCardTwo);
            this.WriteHand(writer, this.FlopCardOne);
            this.WriteHand(writer, this.FlopCardTwo);
            this.WriteHand(writer, this.FlopCardThree);
            this.WriteHand(writer, this.TurnCard);
            this.WriteHand(writer, this.RiverCard);
        }

        private void WriteHand(XmlWriter writer, CardValue card)
        {
            writer.WriteStartElement("Card");

            var val = (CardValue)card;
            val.WriteXml(writer);

            writer.WriteEndElement();
        }

        private void SetHand(CardValue card)
        {
            switch( card.HoldemCard )
            {
                case HoldemCard.Hole1:
                    this.HoleCardOne = card;
                    break;
                case HoldemCard.Hole2:
                    this.HoleCardTwo = card;
                    break;
                case HoldemCard.Flop1:
                    this.FlopCardOne = card;
                    break;
                case HoldemCard.Flop2:
                    this.FlopCardTwo = card;
                    break;
                case HoldemCard.Flop3:
                    this.FlopCardThree = card;
                    break;
                case HoldemCard.Turn:
                    this.TurnCard = card;
                    break;
                case HoldemCard.River:
                    this.RiverCard = card;
                    break;
            }
        }

        #endregion
    }
}