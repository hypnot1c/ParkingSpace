using System;
using System.Diagnostics;
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
        var serializedUser = await this._secureStorage.GetAsync(this._options.SecureStorageKeys.UserAccount);
        if (!String.IsNullOrEmpty(serializedUser))
        {
          this._account = Account.Deserialize(serializedUser);
        }
      }

      return !(this._account is null);
    }

    public async Task<bool> IsAccessTokenExpiredAsync()
    {
      if (this._account is null)
      {
        var serializedUser = await this._secureStorage.GetAsync(this._options.SecureStorageKeys.UserAccount);
        if (!String.IsNullOrEmpty(serializedUser))
        {
          this._account = Account.Deserialize(serializedUser);
        }
      }

      var tokenIssueDateString = await this._secureStorage.GetAsync(this._options.SecureStorageKeys.UserTokenIssueDate);

      if(!DateTime.TryParse(tokenIssueDateString, out var tokenIssueDate))
      {
        return true;
      }

      DateTime.SpecifyKind(tokenIssueDate, DateTimeKind.Utc);

      var tokenExpirationDate = tokenIssueDate.AddSeconds(Int32.Parse(this._account.Properties["expires_in"]));

      return (tokenExpirationDate - DateTime.UtcNow).TotalSeconds <= 10;
    }

    public async Task AuthenticateAsync(Account account)
    {
      var serializedUser = account.Serialize();
      await this._secureStorage.SetAsync(this._options.SecureStorageKeys.UserAccount, serializedUser);

      var tokenIssueDate = DateTime.UtcNow;
      await this._secureStorage.SetAsync(this._options.SecureStorageKeys.UserTokenIssueDate, tokenIssueDate.ToString());

      if (account.Properties.ContainsKey("refresh_token"))
      {
        var refreshToken = account.Properties["refresh_token"];
        await this._secureStorage.SetAsync(this._options.SecureStorageKeys.UserRefreshToken, refreshToken);
      }

      this._account = account;
    }

    public void SignOut()
    {
      this._secureStorage.Remove(this._options.SecureStorageKeys.UserAccount);
      this._secureStorage.Remove(this._options.SecureStorageKeys.UserTokenIssueDate);
      this._secureStorage.Remove(this._options.SecureStorageKeys.UserRefreshToken);

      this._account = null;
    }

    public async Task<Account> GetUserAccountAsync()
    {
      if (this._account is null)
      {
        var serializedUser = await this._secureStorage.GetAsync(this._options.SecureStorageKeys.UserAccount);
        if (!String.IsNullOrEmpty(serializedUser))
        {
          this._account = Account.Deserialize(serializedUser);
        }
      }

      return this._account;
    }

    public async Task RefreshAccessTokenAsync()
    {
      var account = await this.GetUserAccountAsync();

      var providerSettings = this._options.GoogleProviderSettings;

      var authenticator = new OAuth2Authenticator(
        providerSettings.ClientId,
        null,
        providerSettings.Scopes,
        new Uri(providerSettings.AuthorizeUrl),
        new Uri(providerSettings.RedirectUri),
        new Uri(providerSettings.AccessTokenUrl),
        null,
        true);

      authenticator.Completed += OnAuthCompleted;
      authenticator.Error += OnAuthError;

      var refreshToken = await this._secureStorage.GetAsync(this._options.SecureStorageKeys.UserRefreshToken);

      await authenticator.RequestRefreshTokenAsync(refreshToken);
    }

    private async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
    {
      var authenticator = sender as OAuth2Authenticator;
      if (authenticator != null)
      {
        authenticator.Completed -= OnAuthCompleted;
        authenticator.Error -= OnAuthError;
      }

      if (e.IsAuthenticated)
      {
        await this.AuthenticateAsync(e.Account);
      }
    }

    private void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
    {
      var authenticator = sender as OAuth2Authenticator;
      if (authenticator != null)
      {
        authenticator.Completed -= OnAuthCompleted;
        authenticator.Error -= OnAuthError;
      }

      Debug.WriteLine("Authentication error: " + e.Message);
    }
  }
}
