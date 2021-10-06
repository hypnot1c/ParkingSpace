using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParkingSpace.Resources;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace PS.Web.Api.Client.Http.Refit.IntegrationTests
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddParkingSpaceWebApiClient(builder => {
          builder.ConfigureBase("http://192.168.0.3:5000");
        })
        .AddBearerTokenProvider<ParkingSpaceWebApiBearerTokenProvider>()
        ;
    }

    public void Configure(ILoggerFactory loggerFactory, ITestOutputHelperAccessor accessor)
    {
      loggerFactory.AddProvider(new XunitTestOutputLoggerProvider(accessor));

    }
  }
}
