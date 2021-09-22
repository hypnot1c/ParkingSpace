using System;
using Authorization.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace PS.Web.Api.Client
{
  public static class ParkingSpaceWebApiClientBuilderExtensions
  {
    public static IParkingSpaceWebApiClientBuilder AddBearerTokenProvider<T>(this IParkingSpaceWebApiClientBuilder builder) where T : class, IBearerTokenProvider
    {
      builder.Services.AddScoped<IBearerTokenProvider, T>();

      return builder;
    }

    public static IParkingSpaceWebApiClientBuilder AddBearerTokenProvider<T>(this IParkingSpaceWebApiClientBuilder builder, Func<IServiceProvider, T> implementationFactory) where T : class, IBearerTokenProvider
    {
      builder.Services.AddScoped<IBearerTokenProvider, T>(implementationFactory);

      return builder;
    }
  }
}
