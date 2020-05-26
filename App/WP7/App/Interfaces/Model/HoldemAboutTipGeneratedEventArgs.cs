using System;
using System.Collections.Generic;

namespace TexasHoldemCalculator.Interfaces.Model
{
    public class HoldemAboutTipGeneratedEventArgs : EventArgs
    {
        public IEnumerable<HoldemAboutTip> Tips { get; private set; }

        public HoldemAboutTipGeneratedEventArgs(IEnumerable<HoldemAboutTip> tips)
        {
            this.Tips = tips;
        }
    }
}