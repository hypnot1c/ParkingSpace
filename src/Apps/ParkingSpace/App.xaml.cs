using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ParkingSpace.ViewModels;
using ParkingSpace.Views;
using Prism;
using Prism.Events;
using Prism.Ioc;
using Xamarin.Forms;

namespace ParkingSpace
{
  public partial class App : PrismApplication
  {
    public App()
    {
    }

    private ILogger<App> _logger;

    protected override async void OnInitialized()
    {
      InitializeComponent();

      this._logger = this.Container.Resolve<ILogger<App>>();

      await this.NavigateToRootViewAsync();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.RegisterForNavigation<FlyoutView, FlyoutViewModel>();
      containerRegistry.RegisterForNavigation<NavigationPage>();
      containerRegistry.RegisterForNavigation<LoadingView, LoadingViewModel>();
      containerRegistry.RegisterForNavigation<CalendarView, CalendarViewModel>();
      containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
      containerRegistry.RegisterForNavigation<UserProfileView, UserProfileViewModel>();

      containerRegistry.RegisterForNavigation<ManagementView, ManagementViewModel>();
      containerRegistry.RegisterForNavigation<ParkingPlacesView, ParkingPlacesViewModel>();
      containerRegistry.RegisterForNavigation<ParkingGroupsView, ParkingGroupsViewModel>();
    }

    private async Task NavigateToRootViewAsync()
    {
      await this.NavigationService.NavigateAsync(nameof(LoadingView));
    }

    protected override void OnNavigationError(INavigationError navigationError)
    {
      base.OnNavigationError(navigationError);
      this._logger.LogError("Navigation error occured {0}", navigationError.Exception.Message);
    }
  }
}
