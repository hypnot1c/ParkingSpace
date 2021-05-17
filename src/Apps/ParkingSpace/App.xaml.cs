using System.Threading.Tasks;
using ParkingSpace.ViewModels;
using ParkingSpace.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;

namespace ParkingSpace
{
  public partial class App : PrismApplication
  {
    public App()
    {
    }

    protected override async void OnInitialized()
    {
      InitializeComponent();

      await this.NavigateToRootViewAsync();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.RegisterForNavigation<NavigationPage>();
      containerRegistry.RegisterForNavigation<LoadingView, LoadingViewModel>();
      containerRegistry.RegisterForNavigation<MasterDetailView, MasterDetailViewModel>();
      containerRegistry.RegisterForNavigation<CalendarView, CalendarViewModel>();
      containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
    }

    private async Task NavigateToRootViewAsync()
    {
      await this.NavigationService.NavigateAsync("LoadingView");
    }
  }
}
