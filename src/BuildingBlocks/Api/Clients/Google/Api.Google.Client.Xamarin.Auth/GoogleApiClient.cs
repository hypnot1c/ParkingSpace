using System;
using System.Threading.Tasks;
using Api.Google.Client.Model.Output;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xamarin.Auth;

namespace Api.Google.Client
{
  public class GoogleApiClient : IGoogleApiClient
  {
    public GoogleApiClient(
      ILogger<GoogleApiClient> logger,
      Func<Task<Account>> accountProvider
      )
    {
      this.Logger = logger;
      this._accountProvider = accountProvider;
    }

    protected ILogger<GoogleApiClient> Logger { get; }

    private readonly Func<Task<Account>> _accountProvider;
    public async Task<UserOutputModel> GetUserAsync()
    {
      var account = await this._accountProvider();
      if (account is null)
      {
        throw new ArgumentNullException(nameof(account), "Account property is not set");
      }
      var request = new OAuth2Request("GET", new Uri(Urls.USER_INFO), null, account);
      var response = await request.GetResponseAsync();
      UserOutputModel user = null;

      if (response is null)
      {
        this.Logger.LogError("There is no response from google api {0}", Urls.USER_INFO);
        return user;
      }

      // Deserialize the data and store it in the account store
      // The users email address will be used to identify data in SimpleDB
      string userJson = await response.GetResponseTextAsync();
      user = JsonConvert.DeserializeObject<UserOutputModel>(userJson);

      return user;
    }
  }
}
