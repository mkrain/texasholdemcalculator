namespace TexasHoldemCalculator.Core.Provider.Ads
{
    public class LargeAdProvider : BaseAdProvider
    {
        public LargeAdProvider(string adUnitId, string applicationId) : this(adUnitId, applicationId, 80, 480)
        {
            
        }

        private LargeAdProvider(
            string adUnitId, 
            string applicationId, 
            int height = 80, 
            int width = 480) : base(adUnitId, applicationId, height, width)
        {
        }
    }
}