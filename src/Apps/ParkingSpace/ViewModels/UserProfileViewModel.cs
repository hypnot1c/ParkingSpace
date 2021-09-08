using Microsoft.Extensions.Logging;
using ParkingSpace.Resources;
using ParkingSpace.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Model;
using PS.Xamarin.Authentication;

namespace ParkingSpace.ViewModels
{
  public class UserProfileViewModel : BindableBase
  {
    public UserProfileViewModel(
      IAuthenticationService authenticationService,
      SessionService sessionService,
      INavigationService navigationService,
      ILogger<UserProfileViewModel> logger
      )
    {
      this.Logger = logger;
      this._sessionService = sessionService;
      this._authenticationService = authenticationService;
      this._navigationService = navigationService;

      this.User = this._sessionService.User;

      this.SignOutCommand = new DelegateCommand(this.OnSignOutButton_Clicked);
    }

    public ILogger<UserProfileViewModel> Logger { get; }
    public DelegateCommand SignOutCommand { get; }

    private readonly SessionService _sessionService;
    private readonly IAuthenticationService _authenticationService;
    private readonly INavigationService _navigationService;
    private UserSessionModel _user;
    public UserSessionModel User
    {
      get => _user;
      set => SetProperty(ref _user, value);
    }

    private async void OnSignOutButton_Clicked()
    {
      this._authenticationService.SignOut();

      await this._navigationService.NavigateAsync($"/{nameof(LoadingView)}");
    }
  }
}
