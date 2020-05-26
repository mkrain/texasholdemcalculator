namespace TexasHoldemCalculator.Interfaces.Statistics
{
	public interface IPokerHandOdds
	{
        double HandWithBiggerAceAsProbability(IHandKickerOptions info);
        double HandWithBiggerAceAsRatio(IHandKickerOptions info);
        double HandWithBiggerAceAsPercentage(IHandKickerOptions info);
	}
}