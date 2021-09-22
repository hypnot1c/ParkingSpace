using Microsoft.Extensions.DependencyInjection;

namespace PS.Web.Api.Client
{
  public class ParkingSpaceWebApiClientBuilder : IParkingSpaceWebApiClientBuilder
  {
    public ParkingSpaceWebApiClientBuilder(IServiceCollection services)
    {
      this.Services = services;
    }

    public IServiceCollection Services { get; }
  }
}
