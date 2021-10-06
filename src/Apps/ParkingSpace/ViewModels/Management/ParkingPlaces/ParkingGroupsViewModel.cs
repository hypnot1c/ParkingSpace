using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ParkingSpace.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Web.Api.Client;
using PS.Web.Api.Model.Output.V1.Management;

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

      this.CreateCommand = new DelegateCommand(this.CreateCommandExecuted);
      this.EditCommand = new DelegateCommand<ParkingGroupOutputModel>(this.EditCommandExecuted, this.CanEdit);
    }

    private readonly IParkingSpaceWebApiClient _parkingSpaceWebApiClient;
    private readonly INavigationService _navigationService;

    public ObservableCollection<ParkingGroupOutputModel> ParkingGroups { get; set; }

    public DelegateCommand CreateCommand { get; }
    public DelegateCommand<ParkingGroupOutputModel> EditCommand { get; }
    private async void CreateCommandExecuted()
    {
      var newPath = nameof(ParkingGroupView);
      var res = await this._navigationService.NavigateAsync($"NavigationPage/{newPath}", null, true);
    }

    private async void EditCommandExecuted(ParkingGroupOutputModel item)
    {
      var newPath = nameof(ParkingGroupEditView);

      var args = new NavigationParameters();
      args.Add("groupId", item.Id);

      var res = await this._navigationService.NavigateAsync($"NavigationPage/{newPath}", args, true);
    }

    bool CanEdit(ParkingGroupOutputModel parameter)
    {
      return true;
    }

    public async Task InitializeAsync(INavigationParameters parameters)
    {
      var pgList = await this._parkingSpaceWebApiClient.V1.Management.ParkingGroups.Get();

      foreach (var item in pgList)
      {
        this.ParkingGroups.Add(item);
      }
    }
  }
}
