using System.Threading.Tasks;
using Authorization.Abstractions;
using PS.Xamarin.Authentication;

namespace ParkingSpace.Resources
{
  public class ParkingSpaceWebApiBearerTokenProvider : IBearerTokenProvider
  {
    public ParkingSpaceWebApiBearerTokenProvider(
      IAuthenticationService authenticationService
      )
    {
      this._authenticationService = authenticationService;
    }

    private readonly IAuthenticationService _authenticationService;

    public async Task<string> Get()
    {
      var isTokenExipred = await this._authenticationService.IsTokenExpiredAsync();
      if (isTokenExipred)
      {
        await this._authenticationService.RefreshTokenAsync();
      }

      var account = await this._authenticationService.GetUserAccountAsync();

      return account.Properties["id_token"];
    }
  }
}
