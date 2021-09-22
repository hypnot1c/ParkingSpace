using Microsoft.Extensions.DependencyInjection;

namespace PS.Web.Api.Client
{
  public interface IParkingSpaceWebApiClientBuilder
  {
    IServiceCollection Services { get; }
  }
}
