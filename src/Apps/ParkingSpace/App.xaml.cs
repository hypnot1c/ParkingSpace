using ParkingSpace.Services.Abstractions;
using ParkingSpace.ViewModels;
using ParkingSpace.Views;
using Prism.Ioc;
using Xamarin.Forms;

namespace ParkingSpace
{
  public partial class App
  {
    public App()
    {
    }

    protected override async void OnInitialized()
    {
      InitializeComponent();
      var auth = Container.Resolve<IAuthenticationService>();
      var result = await NavigationService.NavigateAsync("MainView");
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.RegisterForNavigation<NavigationPage>();
      containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
      containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
    }
  }
}
