using System.Windows.Controls;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Core.Entities.Cards
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

        Suit CardSuit
        {
            get;
            set;
        }

        string CardPath
        {
            get;
            set;
        }

        CardName Name
        {
            get;
            set;
        }

        Image Image
        {
            get;
            set;
        }
    }
}
