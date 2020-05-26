using System;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Core.Statistics
{
    /// <summary>
    /// 
    /// Statics are implemented as found here: http://en.wikipedia.org/wiki/Poker_probability_(Texas_hold_%27em)
    /// 
    /// Specifically these sections:
    /// 
    /// 1.  http://en.wikipedia.org/wiki/Poker_probability_(Texas_hold_%27em)#After_the_flop_-_outs
    /// 2.  http://en.wikipedia.org/wiki/Poker_probability_(Texas_hold_%27em)#Runner-runner_outs
    /// 
    /// Internally a precision of 8 is maintained but the user is allowed
    /// to specify a larger/smaller precision when a round happens on the
    /// internal calculations.  I.e. Math.Round(Math.Round(value, 8), userDefinedPrecision).
    /// 
    /// </summary>
    public class PotOdds : HoldemStatisticsBase, IPotOdds
    {
        #region Probabilities

        /// <summary>
        /// 
        /// Calculates the probability that you will hit one of your outs
        /// on the flop.
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public double RiverProbability(IHandOptions options)
        {
            ValidateNumberOfOuts(options.NumberOfOuts);

            return this.RoundWithScale(options.NumberOfOuts * RiverRatio, options.Precision);
            //return Math.Round(options.NumberOfOuts * RiverRatio, options.Precision);
        }

        /// <summary>
        /// 
        /// Calculates the probability that you will hit one of your outs
        /// on the river.
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public double TurnProbability(IHandOptions options)
        {
            ValidateNumberOfOuts(options.NumberOfOuts);

            return this.RoundWithScale(options.NumberOfOuts * TurnRatio, options.Precision);
            //return Math.Round(options.NumberOfOuts * TurnRatio, options.Precision);
        }

        /// <summary>
        /// 
        /// Calculate the probability that you will hit on either the turn or the river.
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public double TurnOrRiverProbability(IHandOptions options)
        {
            ValidateNumberOfOuts(options.NumberOfOuts);

            var flopToTurn = (CardsLeftAtFlop - options.NumberOfOuts) / (double)CardsLeftAtFlop;
            var turnToRiver = (CardsLeftAtTurn - options.NumberOfOuts) / (double)CardsLeftAtTurn;

            var value = flopToTurn * turnToRiver;
            value = 1 - value;

            return this.RoundWithScale(value, options.Precision);
        }

        /// <summary>
        /// 
        ///  This assumes common sets of cards, i.e. suits or same value
        ///		i.e player has As-Kh flop is Jh-7h-9c, needs Xh on the Turn and Yh on the river.
        ///  Player has 2c-2h, the flop is 7c-9h-As, needs 2h on the turn 2s on the river.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public double RunnerRunnerProbability(IHandRunnerRunnerOptions info)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            ValidateNumberOfOuts(info.NumberOfOuts);

            if (info.NumberOfOuts == 0)
                return 0;

            var runnerOne = info.NumberOfOuts / (double)CardsLeftAtFlop;
            var runnerTwo = (info.NumberOfOuts - 1) / (double)CardsLeftAtTurn;

            return this.RoundWithScale(runnerOne * runnerTwo, this.Precision);

            //return Math.Round(runnerOne * runnerTwo, this.Precision);
        }

        /// <summary>
        /// 
        ///  If the runner-runner probability of a flush is: 0.04163
        ///		this will return 4.163 with a round of info.Precision.
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public double RunnerRunnerAsPercentage(IHandRunnerRunnerOptions info)
        {
            return Math.Round(100 * this.RunnerRunnerProbability(info), info.Precision);
        }

        /// <summary>
        /// 
        ///  If the runner-runner probability of a flush is: 0.04163
        ///		this will return 1 / 0.04163 - 1 = 23.02 with a round of info.Precision.
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public double RunnerRunnerAsRatio(IHandRunnerRunnerOptions info)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            ValidateNumberOfOuts(info.NumberOfOuts);

            var ratio = this.RunnerRunnerProbability(info);

            if (Math.Abs(ratio - 0) < base.Epsilon)
                return 0;

            ratio = 1 / ratio;

            return this.RoundWithScale(ratio - 1, info.Precision);

            //return Math.Round(ratio - 1, info.Precision);
        }

        /// <summary>
        /// 
        /// This turns your probability into an odds ratio for example:
        /// 
        ///		( 1 / .24 over card draw ) - 1 = 3.17 or rounded, 3.2 to 1 odd.
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public double CalculatePokerOddsAsRatio(IHandOptions info)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            var value = 0.0;

            switch (info.OddsSelection)
            {
                case PotOddsSelection.River:
                    value = this.RiverProbability(info);
                    break;
                case PotOddsSelection.Turn:
                    value = this.TurnProbability(info);
                    break;
                case PotOddsSelection.Combined:
                    value = this.TurnOrRiverProbability(info);
                    break;
            }

            return Math.Abs(value - 0.0) < base.Epsilon ? 0 : this.RoundWithScale(this.RoundWithScale(1 / value, this.Precision) - 1, info.Precision);
            //return Math.Abs(value - 0.0) < base.Epsilon ? 0 : Math.Round(Math.Round(1 / value, this.Precision) - 1, info.Precision);
        }

        /// <summary>
        /// 
        ///  This returns the ratios as a percentage, for example:
        ///		if the ratio is .25 the method returns 25 which is 25%.
        /// 
        ///		The Hand ratio is :
        ///		( 1 / probability ) - 1 * 100, size a ratio of N:1 is really
        ///		N + 1 / 100.
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public double CalculatePokerOddsAsPercent(IHandOptions info)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            switch (info.OddsSelection)
            {
                case PotOddsSelection.River:
                    return Math.Round(100 * this.RiverProbability(info), info.Precision);
                case PotOddsSelection.Turn:
                    return Math.Round(100 * this.TurnProbability(info), info.Precision);
                case PotOddsSelection.Combined:
                    return Math.Round(100 * this.TurnOrRiverProbability(info), info.Precision);
                default:
                    return 0.0;
            }
        }

        /// <summary>
        /// 
        ///  This returns the probability number for example with a flush the probability on the
        ///		turn is .1967.
        /// With a round of 2 this becomes .20.
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public double CalculatePokerOddsAsProbability(IHandOptions info)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            switch (info.OddsSelection)
            {
                case PotOddsSelection.River:
                    return this.RiverProbability(info);
                case PotOddsSelection.Turn:
                    return this.TurnProbability(info);
                case PotOddsSelection.Combined:
                    return this.TurnOrRiverProbability(info);
                default:
                    return 0.0;
            }
        }

        #endregion //Probabilities

        #region PotOdds

        /// <summary>
        /// 
        ///  This is simple a ratio of Pot Amount to Bet Amount.  For example,
        /// 
        ///		Pot Amount = 100
        ///		Bet Amount = 10
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public double CalculatePotOddsAsRatio(IHandOptions info)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            if (info.PotSize < 0)
                throw new ArgumentException("PotSize cannot be less than zero");

            return Math.Abs(info.MaxBet - 0) < base.Epsilon ? 0 : this.RoundWithScale(info.PotSize / info.MaxBet, info.Precision);
            //return Math.Abs(info.MaxBet - 0) < base.Epsilon ? 0 : Math.Round(info.PotSize / info.MaxBet, info.Precision);
        }

        /// <summary>
        /// 
        ///  This is simple a ratio of bet amount to the bet + pot amount
        /// 
        ///		Pot Amount = 100
        ///		Bet Amount = 10
        /// 
        ///		Pot Percent (decimal) = 10 / ( 100 + 10 ) = 0.090909____
        ///		or 9%.
        /// 
        /// </summary>
        /// <returns></returns>
        public double CalculatePotOddsAsPercentage(IHandOptions info)
        {
            var ratio = this.CalculatePotOddsAsRatio(info);

            if (Math.Abs(ratio - 0) < base.Epsilon)
                return 0;

            var rounded = Math.Round(1 / ratio, info.Precision);

            return rounded;// / 100;
        }

        /// <summary>
        /// 
        ///  This is simple a ratio of bet amount to the bet + pot amount
        /// 
        ///		Pot Amount = 100
        ///		Bet Amount = 10
        /// 
        ///		Pot Percent = ( 1 / Pot Odd Ratio ) * 100 = betAmount / potAmount * 100
        ///		..
        ///		10 / 100 = .1 * 100 = 10%
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public double CalculatePotOddsPercent(IHandOptions info)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            if (info.PotSize < 0)
                throw new ArgumentException("PotSize cannot be less than zero");

            var ratio = this.CalculatePotOddsAsRatio(info);

            return Math.Round(100 * ratio, info.Precision);
        }

        /// <summary>
        /// 
        ///  This returns the maximum bet that you should call given your hand odds and the pot size.
        ///		This assumes the previous player placed a bet, which is part of the pot.
        /// 
        ///		Max = pot_size * probability
        ///           ----------------------
        ///              1 - probability
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public double CalculateMaxCallableBet(IHandOptions info)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            if (info.PotSize <= 0)
                return 0;

            var prob = this.CalculatePokerOddsAsProbability(info);

            //The bet should only be a positive number.  Floor insures
            //the the player cannot bet more than is in the pot.
            var maxBet = Math.Floor(Math.Round(info.PotSize * prob, info.Precision));

            //The max bet cannot be greater than the pot amount, there isn't enough in the pot!
            return maxBet > info.PotSize ? info.PotSize : maxBet;
        }

        #endregion //PotOdds

        #region Expectations

        /// <summary>
        /// 
        ///  This returns true if the Hand odds are greater than the Pot Odds.
        ///  That is to say when you will win your hand more often than the ratio
        ///  of the bet to call to the pot size, you have a statistical positive expectancy.
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool IsPositiveExpectation(IHandOptions info)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            var pokerOdds = this.CalculatePokerOddsAsProbability(info);
            var potOdds = this.CalculatePotOddsAsPercentage(info);

            return pokerOdds >= potOdds;
        }

        #endregion //Expectations

        /// <summary>
        /// 
        /// Rounding decimal values without scaling loses precision.
        /// Instead scaling the value a factor of the precision
        /// rounds to that decimal value.
        /// 
        /// </summary>
        /// <param name="value">Value to round</param>
        /// <param name="precision">Scale size base 10.</param>
        /// <returns></returns>
        public double RoundWithScale(double value, int precision)
        {
            //Rounding straight off eliminates all significant digits.
            //Scale the value to 10^precision and than lop of the excess
            var scale = Math.Pow(10, precision);

            return Math.Round(value * scale, precision) / scale;
        }
    }
}