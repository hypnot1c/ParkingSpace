using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using ParkingSpace.Resources;
using ParkingSpace.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace ParkingSpace.ViewModels
{
  public class MasterDetailViewModel : BindableBase
  {
    public MasterDetailViewModel(
      ILogger<MasterDetailViewModel> logger,
      INavigationService navigationService
      )
    {
      this._navigationService = navigationService;
      this.Logger = logger;

      this.MenuItems = new ObservableCollection<MenuItem>();

      MenuItems.Add(new MenuItem()
      {
        Icon = "ic_viewa",
        PageName = nameof(LoginView),
        Title = "Login"
      });

      MenuItems.Add(new MenuItem()
      {
        Icon = "ic_viewb",
        PageName = nameof(CalendarView),
        Title = "Calendar"
      });

      this.NavigateCommand = new DelegateCommand(this.NavigateCommandExecuted);
      this.SelectedMenuItem = this.MenuItems[1];
      this.NavigateCommandExecuted();
    }

    private readonly INavigationService _navigationService;

    public ILogger<MasterDetailViewModel> Logger { get; }
    public ObservableCollection<MenuItem> MenuItems { get; set; }

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
      await this._navigationService.NavigateAsync(newPath);
    }
  }
}
