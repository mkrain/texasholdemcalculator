using System.Runtime.Serialization;

namespace TexasHoldemCalculator.Interfaces.SkyDrive
{
    public interface IWindowsLiveProfile
    {
        [DataMember(Name = "id")]
        string Id
        {
            get;
            set;
        }

        [DataMember(Name = "name")]
        string Name
        {
            get;
            set;
        }
    }
}
