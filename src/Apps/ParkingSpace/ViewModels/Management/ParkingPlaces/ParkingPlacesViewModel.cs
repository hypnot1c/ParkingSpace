using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ParkingSpace.Resources;
using ParkingSpace.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Web.Api.Client;
using PS.Web.Api.Model.Output;

namespace ParkingSpace.ViewModels
{
  public class ParkingPlacesViewModel : BindableBase, IInitializeAsync
  {
    public ParkingPlacesViewModel(
      IParkingSpaceWebApiClient parkingSpaceWebApiClient,
      INavigationService navigationService
      )
    {
      this._parkingSpaceWebApiClient = parkingSpaceWebApiClient;
      this._navigationService = navigationService;

      this.GroupedParkingPlaces = new ObservableCollection<Grouping<string, ParkingPlaceOutputModel>>();

      this.NavigateCommand = new DelegateCommand(this.NavigateCommandExecuted);
    }

    private readonly IParkingSpaceWebApiClient _parkingSpaceWebApiClient;
    private readonly INavigationService _navigationService;

    public ObservableCollection<Grouping<string, ParkingPlaceOutputModel>> GroupedParkingPlaces { get; set; }

    public DelegateCommand NavigateCommand { get; }
    private async void NavigateCommandExecuted()
    {
      var newPath = nameof(ParkingGroupsView);
      var res = await this._navigationService.NavigateAsync(newPath);
    }

    public async Task InitializeAsync(INavigationParameters parameters)
    {
      var psList = await this._parkingSpaceWebApiClient.ParkingPlaces.Get();

      var grouped = psList
        .GroupBy(k => k.Group.Name)
        .Select(g => new Grouping<string, ParkingPlaceOutputModel>(g.Key, g))
        ;

      foreach (var item in grouped)
      {
        this.GroupedParkingPlaces.Add(item);
      }
    }
  }
}
