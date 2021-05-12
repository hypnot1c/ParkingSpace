using Microsoft.Extensions.DependencyInjection;
using ParkingSpace.Services;
using ParkingSpace.Services.Abstractions;
using Shiny;
using Shiny.Prism;

namespace ParkingSpace
{
  public class Startup : PrismStartup
  {
    protected override void ConfigureServices(IServiceCollection services, IPlatform platform)
    {
      // Register Stuff
      services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
  }
}
