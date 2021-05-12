using System;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Essentials.Interfaces;

namespace PS.Xamarin.Authentication
{
  public class AuthenticationService : IAuthenticationService
  {
    public AuthenticationService(ISecureStorage secureStorage, AuthenticationServiceOptions options)
    {
      this._options = options;
      this._secureStorage = secureStorage;
    }

    private readonly AuthenticationServiceOptions _options;
    private readonly ISecureStorage _secureStorage;
    private Account _account;
    public async Task<bool> IsAuthenticatedAsync()
    {
      if (this._account is null)
      {
        var serializedUser = await this._secureStorage.GetAsync(this._options.UserAccountKey);
        if (!String.IsNullOrEmpty(serializedUser))
        {
          this._account = Account.Deserialize(serializedUser);
        }
      }

      return !(this._account is null);
    }
  }
}
