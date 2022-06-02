using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Web.Api.Client;
using PS.Web.Api.Model.Input;

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

      this.SaveCommand = new DelegateCommand(this.SaveCommandExecuted);
    }

    private readonly IParkingSpaceWebApiClient _parkingSpaceWebApiClient;
    private readonly INavigationService _navigationService;

    private int id;

    private string _name;
    public string Name
    {
      get => _name;
      set => SetProperty(ref _name, value);
    }
    public DelegateCommand SaveCommand { get; }

    private async void SaveCommandExecuted()
    {
      var im = new ParkingGroupInputModel
      {
        Id = this.id,
        Name = this.Name
      };

      //var result = await this._parkingSpaceWebApiClient.V1.Management.ParkingGroups.Create(im);

      //var newPath = nameof(ParkingGroupsView);
      //var res = await this._navigationService.NavigateAsync(newPath);
      await this._navigationService.GoBackAsync();
    }

    public async Task InitializeAsync(INavigationParameters parameters)
    {
      var groupId = Int32.Parse(parameters["groupId"].ToString());

      var result = await this._parkingSpaceWebApiClient.V1.Management.ParkingGroups.Get(groupId);

      this.Name = result.Name;
    }
  }
}
