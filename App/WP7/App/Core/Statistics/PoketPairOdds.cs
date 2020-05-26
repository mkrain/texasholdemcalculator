using System;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Core.Statistics
{
	public sealed class PocketPairOdds : HoldemStatisticsBase, IPocketPairOdds
	{
        #region Properties

        /// <summary>
        /// 
        /// This is used to determine how many significant digits
        /// to round after the decimal for probabilities.  For example:
        /// 
        /// Factor of 10 means 2 significant digits
        /// Factor of 100 means 3 significant digits
        /// Factor of 1000 means 4 significant digits
        /// 
        /// </summary>
        public int RoundFactor
        {
            get;
            set;
        }

        #endregion //Properties

        #region Constructor

        public PocketPairOdds()
        {
            this.RoundFactor = SignificantDigits;
        }

        #endregion //Constructor

        #region IPocketPairOdds Members

        /// <summary>
		/// 
		/// This returns the probability that there is one pair that is larger than yours.
		///		For example, it's for 0.0588 22 and 0.0049 for KK, zero for AA of course.
		///		
		/// So the probability P of a single opponent being dealt a higher pocket pair is:
		///		P = ( ( 14 - r ) * 4 / 50 ) * ( 3 / 49 )
		///		Where r is the rank of the hand, 2-10, J-A = 11-14
		/// 
		///		P reduces to: ( 84 - 6r ) / 1225.
		/// 
		/// For this app the hand rank translates into:
		/// 
		/// 2 - A is actually 0 - 12 so the Rank is ( card value + 2 ).
		///
		///		
		/// The formula was taken from here:
		///		http://en.wikipedia.org/wiki/Poker_probability_%28Texas_hold_%27em%29#Pocket_pairs
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
        public double ProbabilityHandFacesHigherSingleProbability(IHandPocketPairOptions info)
		{
			if( info == null )
				throw new ArgumentNullException("info");

            var probability = this.ComputeSinglePairOdd(info.CardValue);

            var round = Math.Round(100 * probability, 3);

            return round / 100;
		}

		/// <summary>
		/// 
		/// Returns ProbabilityHandFacesHigherSingleProbability as a ratio or 1 / prob.
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
        public double ProbabilityHandFacesHigherSingleRatio(IHandPocketPairOptions info)
		{
            if (info == null)
                throw new ArgumentNullException("info");

            var denominator = this.ComputeSinglePairOdd(info.CardValue);

            if (Math.Abs(denominator - 0) < base.Epsilon)
				return 0;

            if (info.Precision < 0 || info.Precision > this.Precision)
                info.Precision = this.Precision;

            var round = Math.Round(1 / denominator, info.Precision);

            return round;
		}

		/// <summary>
		/// 
		/// Returns ProbabilityHandFacesHigherSingleProbability as a percentage or p * 100.
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
        public double ProbabilityHandFacesHigherSinglePercentage(IHandPocketPairOptions info)
		{
            if (info == null)
                throw new ArgumentNullException("info");

            if (info.Precision < 0 || info.Precision > this.Precision)
                info.Precision = this.Precision;

		    var round = Math.Round(100 * this.ComputeSinglePairOdd(info.CardValue), info.Precision);

            return round;
		}

		/// <summary>
		/// 
		/// This returns the probability that there is multiple pairs that are larger than yours.
		///		For example, for 22 it's 0.0588 and 0.0049 for KK, zero for AA.
		///		
		/// So the probability P of an opponent being dealt a higher pocket pair is:
		///		P = ( ( 14 - r ) * 4 / 50 ) * ( 3 / 49 ) * n - Pma
		///		Where r is the rank of the hand, 2-10, J-A = 11-14, n is the number of
		///		players still in the hand and Pma is the adjusted probability that multiple opponents have higher pocket pairs.
		/// 
		///		P reduces to: ( ( 84 - 6r ) / 1225 ) * n - Pma.
		/// 
		/// For this app the hand rank translates into:
		/// 
		/// 2 - A is actually 0 - 12 so the Rank is ( card value + 2 ).
		///
		///	TODO: Find the correct formula for Pma.
		///		
		/// The formula was taken from here:
		///		http://en.wikipedia.org/wiki/Poker_probability_%28Texas_hold_%27em%29#Pocket_pairs
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
        public double ProbabilityHandFacesHigherMultiProbability(IHandPocketPairOptions info)
		{
            if (info == null)
                throw new ArgumentNullException("info");

            ValidateNumberOfPlayers(info.NumberOfPlayers);

            var probability = this.ComputeSinglePairOdd(info.CardValue) * info.NumberOfPlayers;

            var round = Math.Round(100 * probability, this.Precision);

            return round;
		}

		/// <summary>
		/// 
		/// Returns ProbabilityHandFacesHigherMultiProbability as a ratio or 1 / prob.
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
        public double ProbabilityHandFacesHigherMultiRatio(IHandPocketPairOptions info)
		{
		    if( info == null )
		        throw new ArgumentNullException("info");

		    ValidateNumberOfPlayers(info.NumberOfPlayers);

		    if( info.CardValue == CardName.Ace )
		        return 0.0;

		    var denominator = this.ComputeSinglePairOdd(info.CardValue) * info.NumberOfPlayers;

		    if( info.Precision < 0 || info.Precision > this.Precision )
		        info.Precision = this.Precision;

		    var round = Math.Round(1 / denominator, info.Precision);

		    return round;
		}

	    /// <summary>
		/// 
		/// Returns ProbabilityHandFacesHigherMultiProbability as a percentage or p * 100.
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
        public double ProbabilityHandFacesHigherMultiPercentage(IHandPocketPairOptions info)
		{
            if (info == null)
                throw new ArgumentNullException("info");

            ValidateNumberOfPlayers(info.NumberOfPlayers);

            if (info.Precision < 0 || info.Precision > this.Precision)
                info.Precision = this.Precision;

		    var round = Math.Round(100 * this.ComputeSinglePairOdd(info.CardValue) * info.NumberOfPlayers, info.Precision);

            return round;
		}

		#endregion

	    /// <summary>
	    /// 
	    /// Computes:
	    ///		P = ( ( 84 - 6r ) / 1225 )
	    ///		Where r = ( card value + 2 ), is the rank of the pair.
	    /// 
	    /// Probabilities are always rounded using the internal Precision
	    /// property.
	    /// 
	    /// </summary>
	    ///<param name="cardName"></param>
	    ///<returns></returns>
	    private double ComputeSinglePairOdd(CardName cardName)
		{
            var portion = (6 * ((int)cardName + 2)) / SingleDenominator;

            var round = Math.Round(SingleHandProbability - portion, this.Precision);

            return round;
		}
	}
}