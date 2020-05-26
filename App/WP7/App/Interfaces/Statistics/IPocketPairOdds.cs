namespace TexasHoldemCalculator.Interfaces.Statistics
{
	public interface IPocketPairOdds
	{
        int Precision
        {
            get;
            set;
        }

        int RoundFactor
        {
            get;
            set;
        }

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
        double ProbabilityHandFacesHigherSingleProbability(IHandPocketPairOptions info);

		/// <summary>
		/// 
		/// Returns ProbabilityHandFacesHigherSingleProbability as a ratio or 1 / prob.
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
        double ProbabilityHandFacesHigherSingleRatio(IHandPocketPairOptions info);

		/// <summary>
		/// 
		/// Returns ProbabilityHandFacesHigherSingleProbability as a percentage or prob * 100.
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
        double ProbabilityHandFacesHigherSinglePercentage(IHandPocketPairOptions info);

		/// <summary>
		/// 
		/// This returns the probability that there is one pair that is larger than yours.
		///		For example, it's for 0.0588 22 and 0.0049 for KK, zero for AA of course.
		///		
		/// So the probability P of a single opponent being dealt a higher pocket pair is:
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
		///	TODO: Find the correct formual for Pma.
		///		
		/// The formula was taken from here:
		///		http://en.wikipedia.org/wiki/Poker_probability_%28Texas_hold_%27em%29#Pocket_pairs
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
        double ProbabilityHandFacesHigherMultiProbability(IHandPocketPairOptions info);

		/// <summary>
		/// 
		/// Returns ProbabilityHandFacesHigherMultiProbability as a ratio or 1 / prob.
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
        double ProbabilityHandFacesHigherMultiRatio(IHandPocketPairOptions info);

		/// <summary>
		/// 
		/// Returns ProbabilityHandFacesHigherMultiProbability as a percentage or prob * 100.
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
        double ProbabilityHandFacesHigherMultiPercentage(IHandPocketPairOptions info);
	}
}