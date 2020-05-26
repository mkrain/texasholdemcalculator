using System;
using System.Runtime.Serialization;

namespace TexasHoldemCalculator.Interfaces.SkyDrive
{
    public interface IWindowsLiveAuthorizationCode
    {
        [DataMember(Name = "access_token")]
        string AccessToken { get; set; }

        [DataMember(Name = "refresh_token")]
        string RefreshToken { get; set; }

        [DataMember(Name = "scope")]
        string Scope { get; set; }

        [DataMember(Name = "token_type")]
        string TokenType { get; set; }

        [DataMember(Name = "expires_in")]
        string ExpiresIn { get; set; }

        [DataMember(Name = "expiration")]
        DateTime ExpirationDate { get; set; }

        [DataMember(Name = "IsExpired")]
        bool IsExpired { get; }
    }
}