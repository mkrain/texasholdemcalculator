

using TexasHoldemCalculator.Interfaces.Security;

namespace TexasHoldemCalculator.Core.Security
{
    public class SecurityOptionsProvider : ISecurityOptionsProvider
    {
        public string ClientId
        {
            get
            {
                return "REDACTED";
            }
        }

        public string SharedSecret
        {
            get
            {
                return "REDACTED";
            }
        }

        public string ResponseType
        {
            get
            {
                return "code";
            }
        }

        public string Display
        {
            get
            {
                return "touch";
            }
        }

        public string RedirectUri
        {
            get
            {
                return "https://oauth.live.com/desktop";
            }
        }

        public string OAuthAuthorizeUri
        {
            get
            {
                return "https://oauth.live.com";
            }
        }

		public string OAuthMethod
		{
			get
			{
				return "authorize";
			}
		}

        public string OAuthTokenUri
        {
            get
            {
                return "https://oauth.live.com/token";
            }
        }
    }
}
