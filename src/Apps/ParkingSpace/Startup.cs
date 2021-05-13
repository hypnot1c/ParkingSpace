using Microsoft.Extensions.DependencyInjection;
using ParkingSpace.Resources;
using Shiny;
using Shiny.Prism;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

namespace ParkingSpace
{
  public class Startup : PrismStartup
  {
    protected override void ConfigureServices(IServiceCollection services, IPlatform platform)
    {
      services.AddSingleton<ISecureStorage, SecureStorageImplementation>();

      services.AddAuthenticationService(platform);

      services.AddGoogleApiClient();
    }
  }
}
