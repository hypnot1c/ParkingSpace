using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace PS.Web.Api.Client
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddParkingSpaceWebApiClient(this IServiceCollection services, Action<ParkingSpaceWebApiOptionsBuilder> buildAction)
    {
      var builder = new ParkingSpaceWebApiOptionsBuilder();
      buildAction?.Invoke(builder);
      var opts = builder.Build();

      typeof(AssemblyMarker).Assembly
        .HttpInterfaces()
        .ToList()
        .ForEach(httpType =>
        {
          services
            .AddRefitClient(httpType)
            .ConfigureHttpClient(http =>
            {
              http.BaseAddress = new Uri(opts.BaseUrl);
            })
            .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
            ;
        }
        );

      services.AddScoped<AccessTokenDelegatingHandler>();
      services.AddDefaults();

      return services;
    }

    public static IServiceCollection AddParkingSpaceWebApiAccessTokenProvider<T>(this IServiceCollection services) where T : class, IAccessTokenProvider
    {
      services.AddScoped<IAccessTokenProvider, T>();

      return services;
    }

    private static IServiceCollection AddDefaults(this IServiceCollection services)
    {
      typeof(AssemblyMarker).Assembly
        .GetTypes()
        .Where(t =>
          t.GetCustomAttributes().Any(a => a.GetType() == typeof(DefaultForAttribute))
        )
        .ToList()
        .ForEach(concreteType =>
        {
          var attr = concreteType.GetCustomAttribute(typeof(DefaultForAttribute)) as DefaultForAttribute;

          services.AddScoped(attr.Abstract, concreteType);
          services.AddScoped(concreteType);
        });

      return services;
    }
  }
}
