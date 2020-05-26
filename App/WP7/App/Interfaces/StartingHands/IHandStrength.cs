using System.Windows.Media;
using System.Xml.Serialization;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
    public interface IHandStrength : IXmlSerializable
    {
        int Id
        {
            get;
            set;
        }

        Color Color
        {
            get;
            set;
        }

        string Description
        {
            get;
            set;
        }
    }
}
