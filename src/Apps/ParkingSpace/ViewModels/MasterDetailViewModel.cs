using ParkingSpace.Resources;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Model;

namespace ParkingSpace.ViewModels
{
  public class MasterDetailViewModel : BindableBase
  {
    public MasterDetailViewModel(
      INavigationService navigationService,
      SessionService sessionService
      )
    {
      this._navigationService = navigationService;
      this._sessionService = sessionService;

      this.NavigateCommand = new DelegateCommand<string>(this.NavigateCommandExecuted);
      this.User = this._sessionService.User;
    }

    private readonly INavigationService _navigationService;
    private readonly SessionService _sessionService;

    public DelegateCommand<string> NavigateCommand { get; }

    private UserSessionModel _user;
    public UserSessionModel User
    {
      get => _user;
      set => SetProperty(ref _user, value);
    }

    private async void NavigateCommandExecuted(string path)
    {
      await this._navigationService.NavigateAsync($"NavigationPage/{path}");
    }
  }
}
