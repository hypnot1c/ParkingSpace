using Microsoft.Extensions.DependencyInjection;
using PS.Xamarin.Authentication;

namespace ParkingSpace.Resources
{
  static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
    {
      services.AddSingleton<AuthenticationServiceOptions>(sp =>
      {
        var opts = new AuthenticationServiceOptions
        {
          UserAccountKey = "UserAccount"
        };

        return opts;
      });
      services.AddSingleton<IAuthenticationService, AuthenticationService>();
      return services;
    }
  }
}
