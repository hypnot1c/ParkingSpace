using System;
using System.Threading.Tasks;
using Api.Google.Client.Model.Output;
using Newtonsoft.Json;
using Xamarin.Auth;

namespace Api.Google.Client
{
  public class GoogleApiClient : IGoogleApiClient
  {
    public GoogleApiClient(Func<Task<Account>> accountProvider)
    {
      this._accountProvider = accountProvider;
    }

    private readonly Func<Task<Account>> _accountProvider;
    public async Task<UserOutputModel> GetUserAsync()
    {
      var account = await this._accountProvider();
      if(account is null)
      {
        throw new ArgumentNullException(nameof(account), "Account property is not set");
      }
      var request = new OAuth2Request("GET", new Uri(Urls.USER_INFO), null, account);
      var response = await request.GetResponseAsync();
      UserOutputModel user = null;
      if (response != null)
      {
        // Deserialize the data and store it in the account store
        // The users email address will be used to identify data in SimpleDB
        string userJson = await response.GetResponseTextAsync();
        user = JsonConvert.DeserializeObject<UserOutputModel>(userJson);
      }

      return user;
    }
  }
}
