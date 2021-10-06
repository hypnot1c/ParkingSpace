using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Web.Api.Client;
using PS.Web.Api.Model.Input;

namespace ParkingSpace.ViewModels
{
  public class ParkingGroupViewModel : BindableBase, IInitializeAsync
  {
    public ParkingGroupViewModel(
      IParkingSpaceWebApiClient parkingSpaceWebApiClient,
      INavigationService navigationService
      )
    {
      this._parkingSpaceWebApiClient = parkingSpaceWebApiClient;
      this._navigationService = navigationService;

      this.CreateCommand = new DelegateCommand(this.CreateCommandExecuted);
    }

    private readonly IParkingSpaceWebApiClient _parkingSpaceWebApiClient;
    private readonly INavigationService _navigationService;

    private string _name;
    public string Name
    {
      get => _name;
      set => SetProperty(ref _name, value);
    }

    public DelegateCommand CreateCommand { get; }
    private async void CreateCommandExecuted()
    {
      var im = new ParkingGroupInputModel
      {
        Name = this.Name
      };

      var result = await this._parkingSpaceWebApiClient.V1.Management.ParkingGroups.Create(im);

      //var newPath = nameof(ParkingGroupsView);
      //var res = await this._navigationService.NavigateAsync(newPath);
      await this._navigationService.GoBackAsync();
    }

    public async Task InitializeAsync(INavigationParameters parameters)
    {
      await Task.CompletedTask;
    }
  }
}
