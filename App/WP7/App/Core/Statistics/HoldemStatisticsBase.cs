using System;

namespace TexasHoldemCalculator.Core.Statistics
{
    public class HoldemStatisticsBase
    {
        /// <summary>
        ///   Example from this site: http://www.texasholdem-poker.com/examples/22
        /// 
        ///   Hand [As, Ac] - Flop [Js, Qs, Ad, Kd]
        /// 
        ///   Possible hands:
        /// 
        ///   1.  [Ah]: Last A for the quad.
        ///   2.  [Ts]: Royal flush.
        ///   3.  [Th, Td, Tc]: Nut straight.
        ///   4.  [8 spades (10s was already counted)]: Nut flush.
        ///   5.  [3 J/Q/K] Nut full house.
        /// 
        ///   1 + 1 + 3 + 8 + 3 + 3 + 3 = 22 outs.
        /// </summary>
        private const int MAX_OUTS = 22;

        /// <summary>
        /// 
        /// 0 outs produces 0 for all odds which is trivial.
        /// 
        /// </summary>
        private const int MIN_OUTS = 1;

        /// <summary>
        /// 
        /// There are 5 cards shown, 3 flop, 1 turn and 1 river.  For each
        /// round 1 card is burned or 3 burn cards.  52 card deck - ( 3 + 5 )
        /// divided by 2 cards per player is:
        /// 
        /// 44 / 2 = 22 Players maximum.  Realistically 12 players are seated but
        /// this theoretical maximum is used for calculation.
        /// 
        /// </summary>
        private const int MAX_PLAYERS = 22;

        /// <summary>
        /// 
        /// You can't play a game with yourself!
        /// 
        /// </summary>
        private const int MIN_PLAYERS = 2;

        private const double SINGLE_DENOM = 1225;
        private const double SINGLE_HAND_PROB = 84 / SINGLE_DENOM;
        private const int PRECISION = 8;
        private const int MIN_PRECISION = 1;
        private const int SIGNIFICANT_DIGITS = 1000;
        private const int CARDS_LEFT_AT_FLOP = 47;
        private const int CARDS_LEFT_AT_TURN = 46;
        private const double RIVER_RATIO = 1.0 / 46.0;
        private const double TURN_RATIO = 1.0 / 47.0;

        private const double EPSILON = 0.00005;

        public static double SingleHandProbability
        {
            get
            {
                return SINGLE_HAND_PROB;
            }
        }

        public static int SignificantDigits
        {
            get
            {
                return SIGNIFICANT_DIGITS;
            }
        }

        public static double SingleDenominator
        {
            get
            {
                return SINGLE_DENOM;
            }
        }

        public static int MaxOuts
        {
            get
            {
                return MAX_OUTS;
            }
        }

        public static int MinOuts
        {
            get
            {
                return MIN_OUTS;
            }
        }

        public static int MaxPlayers
        {
            get
            {
                return MAX_PLAYERS;
            }
        }

        public static int MinPlayers
        {
            get
            {
                return MIN_PLAYERS;
            }
        }

        public static int CardsLeftAtFlop
        {
            get
            {
                return CARDS_LEFT_AT_FLOP;
            }
        }

        public static int CardsLeftAtTurn
        {
            get
            {
                return CARDS_LEFT_AT_TURN;
            }
        }

        public static double RiverRatio
        {
            get
            {
                return RIVER_RATIO;
            }
        }

        public static double TurnRatio
        {
            get
            {
                return TURN_RATIO;
            }
        }

        public static double MaxPrecision
        {
            get { return PRECISION; }
        }

        public static double MinPrecision
        {
            get { return MIN_PRECISION; }
        }

        public double Epsilon { get; set; }

        public int Precision
        {
            get;
            set;
        }

        protected HoldemStatisticsBase()
        {
            this.Precision = PRECISION;
            this.Epsilon = EPSILON;
        }

        protected virtual void ValidateNumberOfOuts(int numberOfOuts)
        {
            if( numberOfOuts > MAX_OUTS )
                throw new ArgumentException("Number of outs cannot be greater than: " + MAX_OUTS);
            if( numberOfOuts < 0 )
                throw new ArgumentException("Number must be greater than: " + MAX_OUTS);
        }

        protected virtual void ValidateNumberOfPlayers(int players)
        {
            if( players > MAX_PLAYERS )
                throw new ArgumentException("There can be no more than: " + MAX_PLAYERS + " players.");
            if (players < 2)
                throw new ArgumentException("There must be at least 2 players.");
        }

    }
}