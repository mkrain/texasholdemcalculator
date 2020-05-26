
namespace TexasHoldemCalculator.Interfaces.Security
{
    public interface ISecurityOptionsProvider
    {
        string ClientId
        {
            get;
        }

        string SharedSecret
        {
            get;
        }

        string ResponseType
        {
            get;
        }

        string Display
        {
            get;
        }

        string RedirectUri
        {
            get;
        }

        string OAuthAuthorizeUri
        {
            get;
        }

        string OAuthTokenUri
        {
            get;
        }
    }
}
