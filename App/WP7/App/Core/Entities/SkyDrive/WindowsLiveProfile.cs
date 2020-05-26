using System.Runtime.Serialization;
using TexasHoldemCalculator.Interfaces.SkyDrive;

namespace TexasHoldemCalculator.Core.Entities.SkyDrive
{
    [DataContract]
    public class WindowsLiveProfile : IWindowsLiveProfile
    {
        [DataMember(Name = "id")]
        public string Id
        {
            get;
            set;
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }
    }
}
