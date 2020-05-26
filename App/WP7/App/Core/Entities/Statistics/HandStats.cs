using System.Globalization;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Core.Entities.Statistics
{
    #region Statistics Entities

    public sealed class HandStatsInfo : IHandStats
    {
        #region IHandStatsInfo Members

        public bool Expectation
        {
            get;
            set;
        }

        public double PotOdds
        {
            get;
            set;
        }

        public double HandOdds
        {
            get;
            set;
        }

        public double MaxBet
        {
            get;
            set;
        }

        public double MakeHandPercent
        {
            get;
            set;
        }

        public string PotOddsText
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "{0:0.00}", this.PotOdds);
            }
        }

        public string HandOddsText
        {
            get
            {
                return string.Format("{0:0.00}", this.HandOdds);
            }
        }

        public string MakeHandText
        {
            get
            {
                return string.Format("{0:0.00}%", this.MakeHandPercent);
            }
        }

        public string MaxBetText
        {
            get
            {
                return this.MaxBet.ToString(CultureInfo.InvariantCulture);
            }
        }
		
        #endregion
    }

    public sealed class FullHandStatsInfo : IFullHandStatsInfo
    {
        public IHandStats FlopHandStats
        {
            get;
            set;
        }

        public IHandStats TurnHandStats
        {
            get;
            set;
        }

        public IHandStats RiverHandStats
        {
            get;
            set;
        }
    }

    public sealed class HandRunnerRunnerStats : IHandRunnerRunnerStats
    {
        #region IHandStatsInfo Members

        public double RunnerRunnerPercent
        {
            get;
            set;
        }

        public double RunnerRunnerRatio
        {
            get;
            set;
        }

        public string RunnerRunnerPercentText
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "{0:0.00}%", this.RunnerRunnerPercent);
            }
        }

        public string RunnerRunnerRatioText
        {
            get
            {
                return string.Format("{0:0.00}:1", this.RunnerRunnerRatio);
            }
        }

        #endregion
    }

    public class HandKickerStats : IHandKickerStats
    {
        public double Probability
        {
            get;
            set;
        }

        public double Ratio
        {
            get;
            set;
        }

        public double Percentage
        {
            get;
            set;
        }

        public string ProbabilityText
        {
            get
            {
                return string.Format("{0:0.00}", this.Probability);
            }
        }

        public string RatioText
        {
            get
            {
                return string.Format("{0:0.00}:1", this.Ratio);
            }
        }

        public string PercentageText
        {
            get
            {
                return string.Format("{0:0.00}%", this.Percentage);
            }
        }
    }

    public sealed class HandPocketPairStats : HandKickerStats, IHandPocketPairStats
    {

    }

    #endregion

    #region Option Entities

    public sealed class HandConfigOptions : IHandOptions
    {
        #region IHandConfigInfo Members

        public CardName CardValue
        {
            get;
            set;
        }

        public int NumberOfPlayers
        {
            get;
            set;
        }

        public int Precision
        {
            get;
            set;
        }

        public int NumberOfOuts
        {
            get;
            set;
        }

        public double MaxBet
        {
            get;
            set;
        }

        public double PotSize
        {
            get;
            set;
        }

        public PotOddsSelection OddsSelection
        {
            get;
            set;
        }

        public HoldemCard HandState
        {
            get;
            set;
        }

        public HandConfigOptions()
        {

        }

        public HandConfigOptions(IHandOptions options)
        {
            if (options == null)
                return;

            this.CardValue = options.CardValue;
            this.NumberOfPlayers = options.NumberOfPlayers;
            this.NumberOfOuts = options.NumberOfOuts;
            this.Precision = options.Precision;
            this.MaxBet = options.MaxBet;
            this.PotSize = options.PotSize;
            this.OddsSelection = options.OddsSelection;
            this.HandState = options.HandState;
        }

        #endregion

        #region Implementation of ICloneable

        public IHandOptions Clone()
        {
            return this.MemberwiseClone() as HandConfigOptions;
        }

        #endregion
    }

    public class HandKickerOptions : IHandKickerOptions
    {
        #region IHandConfigInfo Members

        public CardName CardValue
        {
            get;
            set;
        }

        public int NumberOfPlayers
        {
            get;
            set;
        }

        public int Precision
        {
            get;
            set;
        }

        #endregion
    }

    public sealed class HandPocketPairOptions : HandKickerOptions, IHandPocketPairOptions
    {

    }

    public class HandRunnerRunnerOptions : IHandRunnerRunnerOptions
    {
        public int Precision
        {
            get;
            set;
        }

        public int NumberOfOuts
        {
            get;
            set;
        }
    }

    #endregion
}