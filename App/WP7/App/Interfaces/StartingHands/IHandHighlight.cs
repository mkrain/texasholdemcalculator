using System.Collections.Generic;
using System.Xml.Serialization;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
    public interface IHandHighlight : IXmlSerializable
    {
        IDictionary<int, IHandStrength> HandStrength
        {
            get;
            set;
        }
    }
}
