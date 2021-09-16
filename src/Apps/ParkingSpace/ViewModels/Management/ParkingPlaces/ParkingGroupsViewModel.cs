using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ParkingSpace.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Web.Api.Client;
using PS.Web.Api.Model.Output;

namespace ParkingSpace.ViewModels
{
  public class ParkingGroupsViewModel : BindableBase, IInitializeAsync
  {
    public ParkingGroupsViewModel(
      IParkingSpaceWebApiClient parkingSpaceWebApiClient,
      INavigationService navigationService
      )
    {
      this._parkingSpaceWebApiClient = parkingSpaceWebApiClient;
      this._navigationService = navigationService;

      this.ParkingGroups = new ObservableCollection<ParkingGroupOutputModel>();

      this.NavigateCommand = new DelegateCommand(this.NavigateCommandExecuted);
    }

    private readonly IParkingSpaceWebApiClient _parkingSpaceWebApiClient;
    private readonly INavigationService _navigationService;

    public ObservableCollection<ParkingGroupOutputModel> ParkingGroups { get; set; }

    public DelegateCommand NavigateCommand { get; }
    private async void NavigateCommandExecuted()
    {
      var newPath = nameof(ParkingGroupView);
      var res = await this._navigationService.NavigateAsync(newPath);
    }

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
