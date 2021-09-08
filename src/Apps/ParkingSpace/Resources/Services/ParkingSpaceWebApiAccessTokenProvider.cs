using System.Threading.Tasks;
using PS.Web.Api.Client;
using PS.Xamarin.Authentication;

namespace ParkingSpace.Resources
{
  public class ParkingSpaceWebApiAccessTokenProvider : IAccessTokenProvider
  {
    public ParkingSpaceWebApiAccessTokenProvider(
      IAuthenticationService authenticationService
      )
    {
      this._authenticationService = authenticationService;
    }

    private readonly IAuthenticationService _authenticationService;

    public async Task<string> Get()
    {
      var account = await this._authenticationService.GetUserAccountAsync();
      return account.Properties["access_token"];
    }
  }
}
