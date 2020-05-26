using System;
using System.Runtime.Serialization;
using TexasHoldemCalculator.Interfaces.SkyDrive;

namespace TexasHoldemCalculator.Core.Entities.SkyDrive
{
    [DataContract]
    public class WindowsLiveAuthorizationCode : IWindowsLiveAuthorizationCode
    {
        private readonly string[] _tokens = new [] { "|" };
        private string _expiration;

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [DataMember(Name = "scope")]
        public string Scope { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "expires_in")]
        public string ExpiresIn { get; set; }

        [DataMember(Name = "expiration")]
        public DateTime ExpirationDate
        {
            get
            {
                DateTime expirationDate = DateTime.Now;

                if( string.IsNullOrEmpty(_expiration) )
                {
                    int expiresIn = this.ParseExpires(this.ExpiresIn);

                    if( expiresIn != -1 )
                        expirationDate = DateTime.Now.AddSeconds(expiresIn);
                    _expiration = expirationDate.ToString();
                }
                else
                {
                    expirationDate = this.ParseExpirationDate(_expiration);
                }

                return expirationDate;
            }
            set
            {
                if( value <= this.ExpirationDate )
                    return;

                _expiration = value.ToString();
            }
        }

        public bool IsExpired
        {
            get { return this.ExpirationDate < DateTime.Now; }
        }

        public WindowsLiveAuthorizationCode()
        {
            
        }

        public WindowsLiveAuthorizationCode(WindowsLiveAuthorizationCode copy) : 
            this(
                copy.AccessToken, 
                copy.RefreshToken, 
                copy.Scope, 
                copy.TokenType, 
                copy.ExpiresIn, 
                DateTime.Now.ToString())
        {
            
        }

        public WindowsLiveAuthorizationCode(
            string accessToken, 
            string refreshToken, 
            string scope, 
            string tokenType, 
            string expireIn,
            string expiration)
        {
            this.AccessToken = accessToken ?? string.Empty;
            this.RefreshToken = refreshToken ?? string.Empty;
            this.Scope = scope ?? string.Empty;
            this.TokenType = tokenType ?? string.Empty;
            this.ExpiresIn = expireIn ?? string.Empty;

            int seconds;

            this.ExpirationDate = this.ParseExpirationDate(expiration);

            if( int.TryParse(expireIn, out seconds) && seconds > 0)
                this.ExpirationDate = this.ExpirationDate.AddSeconds(seconds);

            _expiration = this.ExpirationDate.ToString();
        }

        public WindowsLiveAuthorizationCode(string authorization)
        {
            this.ParseAuthToken(authorization);
        }

        private DateTime ParseExpirationDate(string expiration)
        {
            DateTime exp;

            if( DateTime.TryParse(expiration, out exp) )
                return exp;
            return exp;
        }

        private int ParseExpires(string expiresIn)
        {
            int expire = -1;

            if( int.TryParse(expiresIn, out expire))
                return expire;
            return expire;
        }

        private void ParseAuthToken(string token)
        {
            if( string.IsNullOrEmpty(token) )
                throw new ArgumentException("The authorization token must not be nothing");

            var parts = token.Split(_tokens, StringSplitOptions.RemoveEmptyEntries);

            if( parts.Length == 0 )
                throw new ArgumentException("Must have at least one token specified");

            this.AccessToken = parts[0];
            this.RefreshToken = parts[1];
            this.Scope = parts[2];
            this.TokenType = parts[3];
            this.ExpiresIn = parts[4];
            string expiration = parts[5];

            var date = this.ParseExpirationDate(expiration);

            this.ExpirationDate = date;
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                this.AccessToken ?? string.Empty,
                this.RefreshToken ?? string.Empty,
                this.Scope ?? string.Empty,
                this.TokenType ?? string.Empty,
                this.ExpiresIn ?? string.Empty,
                this.ExpirationDate);
        }
    }
}