using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ParkingSpace.Resources;
using ParkingSpace.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace ParkingSpace.ViewModels
{
  public class ManagementViewModel : BindableBase, IInitializeAsync
  {
    public ManagementViewModel(
      INavigationService navigationService,
      ILogger<ManagementViewModel> logger
      )
    {
      this._navigationService = navigationService;
      this.Logger = logger;

      this.MenuItems = new ObservableCollection<MenuItem>();

      this.NavigateCommand = new DelegateCommand(this.NavigateCommandExecuted);
    }

    public ObservableCollection<MenuItem> MenuItems { get; set; }
    public ILogger<ManagementViewModel> Logger { get; }

    private readonly INavigationService _navigationService;

    private MenuItem _selectedMenuItem;
    public MenuItem SelectedMenuItem
    {
      get => _selectedMenuItem;
      set => SetProperty(ref _selectedMenuItem, value);
    }

    public DelegateCommand NavigateCommand { get; }

    private async void NavigateCommandExecuted()
    {
      var newPath = SelectedMenuItem.PageName;
      var res = await this._navigationService.NavigateAsync(newPath);
    }

    private void BuildMenu()
    {
      this.MenuItems.Add(new MenuItem()
      {
        Icon = "ic_viewa",
        PageName = nameof(ParkingPlacesView),
        Title = "Parking places"
      });
    }

    public async Task InitializeAsync(INavigationParameters parameters)
    {
      this.BuildMenu();
      await Task.CompletedTask;
    }
  }
}
