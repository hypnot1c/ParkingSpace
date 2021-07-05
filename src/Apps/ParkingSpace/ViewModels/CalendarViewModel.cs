using Microsoft.Extensions.Logging;
using ParkingSpace.Resources;
using ParkingSpace.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Model;

namespace ParkingSpace.ViewModels
{
  public class CalendarViewModel : BindableBase
  {
    public CalendarViewModel(
      ILogger<CalendarViewModel> logger,
      SessionService sessionService,
      INavigationService navigationService
      )
    {
      this.Logger = logger;
      this._sessionService = sessionService;
      this._navigationService = navigationService;

      this.User = this._sessionService.User;

      this.AvatarTapCommand = new DelegateCommand(this.OnAvatar_Clicked);
    }

    public ILogger<CalendarViewModel> Logger { get; }
    public DelegateCommand AvatarTapCommand { get; }

    private readonly SessionService _sessionService;
    private readonly INavigationService _navigationService;
    private UserSessionModel _user;
    public UserSessionModel User
    {
      get => _user;
      set => SetProperty(ref _user, value);
    }

    private async void OnAvatar_Clicked()
    {
      await this._navigationService.NavigateAsync(nameof(UserProfileView));
    }
  }
}
