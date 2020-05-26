using System;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Generator;
using TexasHoldemCalculator.Interfaces.HandHistory;

namespace TexasHoldemCalculator.Core.Generator
{
	public class DeckGenerator : IDeckGenerator
	{
		#region Properties

		private readonly ICardGenerator _cardGenerator;
		private readonly IPrimitiveGenerator _primitiveGenerator;

		#endregion Properties

		public DeckGenerator(ICardGenerator generator, IPrimitiveGenerator primitiveGenerator)
		{
			if( generator == null )
				throw new ArgumentNullException("generator");
			if( primitiveGenerator == null )
				throw new ArgumentNullException("primitiveGenerator");

			_cardGenerator = generator;
			_primitiveGenerator = primitiveGenerator;
		}

		#region Public Methods

		/// <summary>
		/// 
		/// Cards are 1-indexed rather than zero indexed.
		/// 
		/// Unfortunately due to the small bit size on the device
		/// it is not possible to generate all 52! hands.  There may
		/// be some hands that repeat, although hopefully using the
		/// underlying shuffle will randomize them sufficient such
		/// that duplicate hands show only so often.
		/// 
		/// </summary>
		/// <param name="players"></param>
		/// <returns></returns>
		public History GenerateHoldemDeck(int players)
		{
            if (players == 0)
                throw new ArgumentException("The number of players must be > 1");
			if( (uint)players > 22 )
				throw new ArgumentException("complex reason", "players");

			//Generate 52 cards in random order
			var randomized = _primitiveGenerator.GenerateList(1, 52);

            //Check for dupes in the generated numbers
#if HACK
            var dupes = ( from d in randomized
                        group d by d
                        into g
                        where g.Count() > 1
                        select g.Key ).ToList();

            var hasDupes = dupes.Count > 1;

            if (dupes.Count > 1)
                throw new ArgumentException("Duplicated value generated: " + dupes.First());
            
            while( hasDupes )
            {
                randomized = _primitiveGenerator.GenerateList(1, 52);

                dupes = (from d in randomized
                         group d by d
                             into g
                             where g.Count() > 1
                             select g.Key).ToList();

                hasDupes = dupes.Count > 1;
            }

#endif

            if ( randomized.Length < 52 )
				throw new ArgumentException("array length less than 52");

			var baseIndex = 2 * players;

            var history =
                new History
		        {
                    //Save the generated cards, 2 * i + 6 where i is the number of players.
		            HoleCardOne = this.IntializeCard(randomized[0], HoldemCard.Hole1),
		            //This corresponds to the 2 * ith card with i is the number of players
                    HoleCardTwo = this.IntializeCard(randomized[players], HoldemCard.Hole2),
                    //The flop starts at 2 * i + 1, since the 2 * i + 0 spot is the burn card
		            FlopCardOne = this.IntializeCard(randomized[baseIndex + 1], HoldemCard.Flop1),
		            FlopCardTwo = this.IntializeCard(randomized[baseIndex + 2], HoldemCard.Flop2),
		            FlopCardThree = this.IntializeCard(randomized[baseIndex + 3], HoldemCard.Flop3),
                    //The turn starts at 2 * i + 5, since the 2 * i + 4 spot is the burn card
		            TurnCard = this.IntializeCard(randomized[baseIndex + 5], HoldemCard.Turn),
                    //The river starts at 2 * i + 7, since the 2 * i + 6 spot is the burn card
		            RiverCard = this.IntializeCard(randomized[baseIndex + 7], HoldemCard.River)
		        };

		    history.GameDescription = "Auto generated hand THC";
               
#if HACK
            var duplicates = ( from c in history.Cards
		                         group c by new { c.Name, c.Suit }
		                         into g
		                         where g.Count() > 1
		                         select g.Key ).ToList();

            if (duplicates.Count > 0)
            {
                var findDupes = from c in history.Cards
                                from dup in duplicates
                                where c.Name == dup.Name
                                      && c.Suit == dup.Suit
                                select c;

                var testEachCard =
                    new List<ICardValue>
                    {
                        IntializeCard(randomized[0], HoldemCard.Hole1),
                        IntializeCard(randomized[players], HoldemCard.Hole2),
                        IntializeCard(randomized[baseIndex + 1], HoldemCard.Flop1),
                        IntializeCard(randomized[baseIndex + 2], HoldemCard.Flop2),
                        IntializeCard(randomized[baseIndex + 3], HoldemCard.Flop3),
                        IntializeCard(randomized[baseIndex + 5], HoldemCard.Turn),
                        IntializeCard(randomized[baseIndex + 7], HoldemCard.River)
                    };

                foreach (var card in findDupes)
                {
                    Console.WriteLine("Duplicate card: " + card);
                }

                throw new InvalidOperationException("A duplicate card was generated: " + duplicates.First());
            }
#endif

            return history;
		}

		#endregion Public Methods

		#region Private Methods

		private CardValue IntializeCard(int cardId, HoldemCard round)
		{
			var card = _cardGenerator.GetCard(cardId);
			card.HoldemCard = round;

			return card;
		}

		#endregion //Private Methods
	}
}