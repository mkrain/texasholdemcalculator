using System;
using System.Collections.Generic;
using TexasHoldemCalculator.Interfaces.Resource;
using TexasHoldemCalculator.Interfaces.Statistics;

namespace TexasHoldemCalculator.Model
{
    public class HoldemHoleOddsModel
    {
    	private readonly IHoldemResource _resource;

        public IList<IHoleOdds> HoleOdds
        {
            get
            {
				return _resource.HoleOdds();
            }
        }


        public HoldemHoleOddsModel(IHoldemResource resource)
        {
			if (resource == null)
				throw new ArgumentNullException("resource");

        	_resource = resource;
        }
    }
}