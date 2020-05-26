using System;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Interfaces.Model
{
    public class HoldemRunnerRunnerEventArgs : EventArgs
    {
        public IHandRunnerRunnerStats RunnerRunner
        {
            get;
            set;
        }


        public HoldemRunnerRunnerEventArgs(IHandRunnerRunnerStats runnerInfo)
        {
            this.RunnerRunner = runnerInfo;
        }
    }
}
