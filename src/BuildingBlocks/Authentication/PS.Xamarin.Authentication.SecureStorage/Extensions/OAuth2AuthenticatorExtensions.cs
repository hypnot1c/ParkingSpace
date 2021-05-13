using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace PS.Xamarin.Authentication
{
  public static class OAuth2AuthenticatorExtensions
  {
    /// <summary>
    /// Method that requests a new access token based on an initial refresh token
    /// </summary>
    /// <param name="refreshToken">Refresh token, typically from the <see cref="AccountStore"/>'s refresh_token property</param>
    /// <returns>Time in seconds the refresh token expires in</returns>
    public static async Task<int> RequestRefreshTokenAsync(this OAuth2Authenticator authenticator, string refreshToken)
    {
      var queryValues = new Dictionary<string, string>
      {
        {"refresh_token", refreshToken},
        {"client_id", authenticator.ClientId},
        {"grant_type", "refresh_token"}
      };

      if (!String.IsNullOrEmpty(authenticator.ClientSecret))
      {
        queryValues["client_secret"] = authenticator.ClientSecret;
      }

      var accessToken = await authenticator.RequestAccessTokenAsync(queryValues);
      var accountProperties = accessToken;

      authenticator.OnRetrievedAccountProperties(accountProperties);

      return Int32.Parse(accountProperties["expires_in"]);
    }
  }
}
