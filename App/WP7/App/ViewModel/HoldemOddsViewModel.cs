using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using TexasHoldemCalculator.Interfaces.Resource;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.ViewModel
{
    public class HoldemOddsViewModel : ViewModelBase
    {
        private readonly IHoldemResource _resource;
        

        public IEnumerable<IHoleOdds> HoleOdds
        {
            get
            {
                return _resource.HoleOdds();
            }
        }

        public HoldemOddsViewModel(IHoldemResource resource)
        {
            if (resource == null)
                throw new ArgumentNullException("resource");

            _resource = resource;
        }
    }
}