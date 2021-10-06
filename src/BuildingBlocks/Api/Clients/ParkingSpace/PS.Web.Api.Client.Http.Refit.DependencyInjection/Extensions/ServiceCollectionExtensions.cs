using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace PS.Web.Api.Client
{
  public static class ServiceCollectionExtensions
  {
    public static IParkingSpaceWebApiClientBuilder AddParkingSpaceWebApiClient(this IServiceCollection services, Action<ParkingSpaceWebApiOptionsBuilder> buildAction)
    {
      var optsBuilder = new ParkingSpaceWebApiOptionsBuilder();
      buildAction?.Invoke(optsBuilder);
      var opts = optsBuilder.Build();

      var t = typeof(AssemblyMarker).Assembly
        .HttpInterfaces()
        .ToList();

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
            .AddHttpMessageHandler<BearerTokenDelegatingHandler>()
            ;
        }
        );

      services.AddScoped<BearerTokenDelegatingHandler>();
      services.AddDefaults();

      var builder = new ParkingSpaceWebApiClientBuilder(services);

      return builder;
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
