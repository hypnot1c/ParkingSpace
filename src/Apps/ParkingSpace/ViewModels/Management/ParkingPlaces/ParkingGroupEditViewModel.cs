using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Web.Api.Client;

namespace ParkingSpace.ViewModels
{
  public class ParkingGroupEditViewModel : BindableBase, IInitializeAsync
  {
    public ParkingGroupEditViewModel(
      IParkingSpaceWebApiClient parkingSpaceWebApiClient,
      INavigationService navigationService
      )
    {
      this._parkingSpaceWebApiClient = parkingSpaceWebApiClient;
      this._navigationService = navigationService;
    }

    private readonly IParkingSpaceWebApiClient _parkingSpaceWebApiClient;
    private readonly INavigationService _navigationService;

    public async Task InitializeAsync(INavigationParameters parameters)
    {
      //var result = await this._parkingSpaceWebApiClient.ParkingGroups.Get();
      await Task.CompletedTask;
    }
  }
}
