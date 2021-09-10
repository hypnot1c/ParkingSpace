using System.Threading.Tasks;
using Api.Google.Client;
using ParkingSpace.Resources;
using ParkingSpace.Views;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Web.Api.Client;
using PS.Xamarin.Authentication;

namespace ParkingSpace.ViewModels
{
  public class LoadingViewModel : BindableBase, INavigationAware
  {
    public LoadingViewModel(
      INavigationService navigationService,
      SessionService sessionService,
      IAuthenticationService authenticationService,
      IGoogleApiClient googleApiClient,
      IParkingSpaceWebApiClient parkingSpaceWebApiClient
      )
    {
      this._navigationService = navigationService;
      this._sessionService = sessionService;
      this._authenticationService = authenticationService;
      this._googleApiClient = googleApiClient;
      this._parkingSpaceWebApiClient = parkingSpaceWebApiClient;
    }
    private readonly INavigationService _navigationService;
    private readonly SessionService _sessionService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IGoogleApiClient _googleApiClient;
    private readonly IParkingSpaceWebApiClient _parkingSpaceWebApiClient;

    public void OnNavigatedFrom(INavigationParameters parameters)
    {
    }

    public async void OnNavigatedTo(INavigationParameters parameters)
    {
      var targetView = nameof(LoginView);

      var isAuthenticated = await this._authenticationService.IsAuthenticatedAsync();

      if (isAuthenticated)
      {
        var isIdTokenExpired = await this._authenticationService.IsTokenExpiredAsync();
        if (isIdTokenExpired)
        {
          await this._authenticationService.RefreshTokenAsync();
        }

        await this.InitSessionAsync();

        targetView = nameof(FlyoutView);
      }

      await this._navigationService.NavigateAsync(targetView);
    }

    private async Task InitSessionAsync()
    {
      var authUser = await this._googleApiClient.GetUserAsync();

      var user = await this._parkingSpaceWebApiClient.Users.Get(authUser.Email);

      this._sessionService.SetSessionUser(user);
    }
  }
}
