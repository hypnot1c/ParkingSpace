using ParkingSpace.Services.Abstractions;
using Prism.Mvvm;
using Prism.Navigation;

namespace ParkingSpace.ViewModels
{
  public class MainViewModel : BindableBase, INavigationAware
  {
    public MainViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
    {
      this._navigationService = navigationService;
      this._authenticationService = authenticationService;
    }
    private string _message;
    private readonly INavigationService _navigationService;
    private readonly IAuthenticationService _authenticationService;

    public string Message
    {
      get => _message;
      set => SetProperty(ref _message, value);
    }

    public void OnNavigatedFrom(INavigationParameters parameters)
    {

    }

    public async void OnNavigatedTo(INavigationParameters parameters)
    {
      if (!this._authenticationService.IsAuthenticated)
      {
        await this._navigationService.NavigateAsync("LoginView");
      }
    }
  }
}
