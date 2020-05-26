using System;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Core.Statistics
{
    public class PokerHandOdds : HoldemStatisticsBase, IPokerHandOdds
    {
        #region IPokerHandOdds Members

        /// <summary>
        /// 
        /// This returns the probability that a player with an Ax hand,
        ///		where x is a 2 to K, is facing a larger kicker, single opponents.
        ///		
        /// If there are n opponents than the probability is:
        ///		1 - [( 1 - p )^n], where n is the number of players and p
        ///		is the probability of one opponent as given above.
        ///
        /// Where p = 159 - 12x / 1225, x is the ran of the hand.
        /// 
        /// For this app the hand rank translates into:
        /// 
        /// 2 - A is actually 0 - 12 so the Rank is ( card value + 2 ).
        ///		
        /// The formula was taken from here:
        ///		http://en.wikipedia.org/wiki/Poker_probability_(Texas_hold_%27em)#Hands_with_one_ace
        /// 
        /// </summary>
        ///<param name="info"></param>
        ///<returns></returns>
        public double HandWithBiggerAceAsProbability(IHandKickerOptions info)
        {
            if (info.Precision < 0 || info.Precision > this.Precision)
                info.Precision = this.Precision;

            //If we are out of range or the kicker is an ace return 0.
            if ((int)info.CardValue > Int32.MaxValue || (CardName.Ace == info.CardValue))
                return 0;

            var num = (159 - (12 * ((int)info.CardValue + 2)));
            var single = (num / SingleDenominator);

            //This is for single opponent
            if (info.NumberOfPlayers == 2)
                return Math.Round(100 * Math.Round(single, base.Precision), info.Precision) / 100;

            // 1 - ( 1 - p ) ^ N
            single = 1 - Math.Pow(1 - single, info.NumberOfPlayers);

            return Math.Round(100 * Math.Round(single, base.Precision), info.Precision) / 100;
        }

        /// <summary>
        /// 
        /// This returns the probability as a ratio, i.e. X to Y.
        /// 
        /// For an AJ, probability = 0.02204, the ratio is ( 1 / p ) - 1
        ///		44.37 with a round of two.
        /// 
        /// </summary>
        ///<param name="info"></param>
        ///<returns></returns>
        public double HandWithBiggerAceAsRatio(IHandKickerOptions info)
        {
            if (info.Precision < 0 || info.Precision > this.Precision)
                info.Precision = this.Precision;

            var prob = this.HandWithBiggerAceAsProbability(info);

            if (prob <= 0)
                return 0;

            prob = 1 / prob;

            return Math.Round(Math.Round(prob, 8) - 1, info.Precision);
        }

        /// <summary>
        /// 
        /// This returns the probability as a percentage, i.e. p * 100 %
        /// 
        /// For an AJ, probability = 0.02204 * 100 = 2.20% with a round of two.
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public double HandWithBiggerAceAsPercentage(IHandKickerOptions info)
        {
            if (info.Precision < 0 || info.Precision > this.Precision)
                info.Precision = this.Precision;

            var prob = this.HandWithBiggerAceAsProbability(info);

            return Math.Round(100 * prob, info.Precision);
        }

        #endregion
    }
}