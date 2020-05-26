using System.ComponentModel;

namespace TexasHoldemCalculator.Interfaces.Card
{
	public interface ICard
	{
		long CardId
		{
			get;
			set;
		}

		long CardValue
		{
			get;
			set;
		}

		string CardText
		{
			get;
			set;
		}
	}

	public enum HoldemCard : int
	{
        [Description("Hole Card One")]
		Hole1 = 0,
        [Description("Hole Card Two")]
		Hole2 = 1,
        [Description("Flop Card One")]
		Flop1 = 2,
        [Description("Flop Card Two")]
		Flop2 = 3,
        [Description("Flop Card Three")]
		Flop3 = 4,
        [Description("Turn Card")]
		Turn = 5,
        [Description("Rive Card")]
		River = 6
	};
}