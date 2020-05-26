using System;
using TexasHoldemCalculator.Interfaces;

namespace TexasHoldemCalculator.Core.Provider.Ads
{
    public abstract class BaseAdProvider : IAdProvider
    {
        #region Implementation of IAdProvider

        public string AdUnitId { get; private set; }

        public string ApplicationId { get; private set; }

        public int Height { get; private set; }
        public int Width { get; private set; }

        #endregion

        protected BaseAdProvider(string adUnitId, string applicationId, int height, int width)
        {
            if( adUnitId == null )
                throw new ArgumentNullException("adUnitId");
            if( applicationId == null )
                throw new ArgumentNullException("applicationId");
            if( height <= 0 )
                throw new ArgumentException("Height must have a positive size");
            if( width <= 0 )
                throw new ArgumentException("Width must have a positive size");

            this.AdUnitId = adUnitId;
            this.ApplicationId = applicationId;
            this.Width = width;
            this.Height = height;
        }
    }
}