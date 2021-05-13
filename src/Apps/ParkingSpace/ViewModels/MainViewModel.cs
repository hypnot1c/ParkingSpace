using Api.Google.Client;
using Prism.Mvvm;
using Prism.Navigation;

namespace ParkingSpace.ViewModels
{
  public class MainViewModel : BindableBase, INavigationAware
  {
    public MainViewModel(IGoogleApiClient googleApiClient)
    {
      this._googleApiClient = googleApiClient;
    }
    private string _message;
    private readonly IGoogleApiClient _googleApiClient;

    public string Message
    {
      get => _message;
      set => SetProperty(ref _message, value);
    }

    public void OnNavigatedFrom(INavigationParameters parameters)
    {
      //throw new System.NotImplementedException();
    }

    public async void OnNavigatedTo(INavigationParameters parameters)
    {
      var user = await this._googleApiClient.GetUserAsync();
    }
  }
}
