using Prism.Mvvm;
using PS.Web.Api.Client;

namespace ParkingSpace.ViewModels
{
  public class ParkingPlacesViewModel : BindableBase
  {
    public ParkingPlacesViewModel(
      IParkingSpaceWebApiClient parkingSpaceWebApiClient
      )
    {
      this._parkingSpaceWebApiClient = parkingSpaceWebApiClient;
    }

    private readonly IParkingSpaceWebApiClient _parkingSpaceWebApiClient;
  }
}
