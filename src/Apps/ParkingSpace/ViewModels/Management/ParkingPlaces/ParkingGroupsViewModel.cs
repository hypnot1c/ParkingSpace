using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Web.Api.Client;
using PS.Web.Api.Model.Output;

namespace ParkingSpace.ViewModels
{
  public class ParkingGroupsViewModel : BindableBase, IInitializeAsync
  {
    public ParkingGroupsViewModel(
      IParkingSpaceWebApiClient parkingSpaceWebApiClient
      )
    {
      this._parkingSpaceWebApiClient = parkingSpaceWebApiClient;

      this.ParkingGroups = new ObservableCollection<ParkingGroupOutputModel>();
    }

    private readonly IParkingSpaceWebApiClient _parkingSpaceWebApiClient;

    public ObservableCollection<ParkingGroupOutputModel> ParkingGroups { get; set; }

    public async Task InitializeAsync(INavigationParameters parameters)
    {
      var pgList = await this._parkingSpaceWebApiClient.ParkingGroups.Get();

      foreach (var item in pgList)
      {
        this.ParkingGroups.Add(item);
      }
    }
  }
}
