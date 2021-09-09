using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ParkingSpace.Resources;
using ParkingSpace.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Web.Api.Client;

namespace ParkingSpace.ViewModels
{
  public class FlyoutViewModel : BindableBase, IInitializeAsync
  {
    public FlyoutViewModel(
      ILogger<FlyoutViewModel> logger,
      IParkingSpaceWebApiClient parkingSpaceWebApiClient,
      INavigationService navigationService
      )
    {
      this._navigationService = navigationService;
      this.Logger = logger;
      this._parkingSpaceWebApiClient = parkingSpaceWebApiClient;

      this.MenuItems = new ObservableCollection<MenuItem>();

      this.MenuItems.Add(new MenuItem()
      {
        Icon = "ic_viewb",
        PageName = nameof(CalendarView),
        Title = "Calendar"
      });

      this.NavigateCommand = new DelegateCommand(this.NavigateCommandExecuted);

      this.SelectedMenuItem = this.MenuItems[0];
      this.NavigateCommandExecuted();
    }

    public ObservableCollection<MenuItem> MenuItems { get; set; }
    public ILogger<FlyoutViewModel> Logger { get; }

    private readonly INavigationService _navigationService;
    private readonly IParkingSpaceWebApiClient _parkingSpaceWebApiClient;

    private MenuItem _selectedMenuItem;
    public MenuItem SelectedMenuItem
    {
      get => _selectedMenuItem;
      set => SetProperty(ref _selectedMenuItem, value);
    }

    public DelegateCommand NavigateCommand { get; }

    private async void NavigateCommandExecuted()
    {
      var newPath = nameof(Xamarin.Forms.NavigationPage) + "/" + SelectedMenuItem.PageName;
      var res = await this._navigationService.NavigateAsync(newPath);
    }

    private void BuildMenu()
    {
      this.MenuItems.Add(new MenuItem()
      {
        Icon = "ic_viewa",
        PageName = nameof(LoginView),
        Title = "Login"
      });
    }

    public async Task InitializeAsync(INavigationParameters parameters)
    {
      this.BuildMenu();
      await Task.CompletedTask;
    }
  }
}
