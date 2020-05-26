using System.Collections.Generic;
using System.Xml.Serialization;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
    public interface IHand : IXmlSerializable
    {
        IList<CardValue> Cards 
        { 
            get; set;
        }
        
        CardName Name 
        { 
            get; set;
        }

		CardValue FindHandByColumn(CardName columnCard);
    }
}