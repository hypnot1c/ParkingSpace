using System.Threading.Tasks;
using ParkingSpace.ViewModels;
using ParkingSpace.Views;
using Prism;
using Prism.Ioc;
using PS.Xamarin.Authentication;
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
      containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
      containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
    }

    private async Task NavigateToRootViewAsync()
    {
      var auth = this.Container.Resolve<IAuthenticationService>();

      var startViewName = (await auth.IsAuthenticatedAsync()) ? "MainView" : "LoginView";

      await this.NavigationService.NavigateAsync(startViewName);
    }
  }
}
