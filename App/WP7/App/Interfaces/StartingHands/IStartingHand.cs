using System.Collections.Generic;
using System.Windows.Media;
using System.Xml.Serialization;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
    public interface IStartingHand : IXmlSerializable
    {
        string Title { get; set; }

        string Description { get; set; }

        HandType HandType { get; set; }

        HandHighlight HandHighlight { get; set; }

        IList<Hand> Hand { get; set; }

        IList<CardValue> AllHands { get; }

        bool IsSelected { get; set; }

        string FileName { get; set; }

        Color FindColorFromStrength(int strength);

        Hand FindHandByRow(CardName rowCard);

        CardValue FindHandByColumn(CardName rowCard, CardName columnCard);

        void SetColorFromStrength(CardValue card);

        bool HasHand(CardName rowCard, CardName columnCard);

        void UpdateVisibilityToLikeStrength(int strength, bool isVisible);

        void UpdateLikeStrengths(int oldStrength, int strength);

        bool ContainsStrength(int strength);

        void RefreshHandStrength();
    }

    public enum HandType
    {
        Undefined = 0,
        Custom,
        Embedded
    }
}
