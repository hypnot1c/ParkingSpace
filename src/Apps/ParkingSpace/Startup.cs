using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
      services.AddLogging();

      services.AddSingleton<ISecureStorage, SecureStorageImplementation>();

      services.AddAuthenticationService();

      services.AddGoogleApiClient();

      services.AddParkingSpaceWebApiClient();

      services.AddTinyMapper();

      services.AddMediatR(typeof(Startup));

      services.AddSingleton<SessionService>();
    }

    protected override void ConfigureLogging(ILoggingBuilder builder, IPlatform platform)
    {
      base.ConfigureLogging(builder, platform);

      #if DEBUG
      builder.AddDebug();
      #endif
    }
  }
}
