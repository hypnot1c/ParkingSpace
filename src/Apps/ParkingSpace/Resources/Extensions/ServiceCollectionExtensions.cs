using System;
using System.Threading.Tasks;
using Api.Google.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParkingSpace.MappingProfiles;
using PS.Web.Api.Client;
using PS.Xamarin.Authentication;
using Xamarin.Auth;
using Xamarin.Forms;

namespace ParkingSpace.Resources
{
  static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
    {
      services.AddSingleton<GoogleProviderSettings>(sp =>
      {
        var clientId = String.Empty;
        var redirectUri = String.Empty;

        if (Device.RuntimePlatform == Device.Android)
        {
          clientId = "51862517676-v7j9degm2ail3gldk889n798ng5cku3m.apps.googleusercontent.com";
          redirectUri = "com.googleusercontent.apps.51862517676-v7j9degm2ail3gldk889n798ng5cku3m:/oauth2redirect";
        }

        var settings = new GoogleProviderSettings
        {
          ClientId = clientId,
          Scopes = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile openid",
          RedirectUri = redirectUri,
          AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth",
          AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token",
          UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo"
        };

        return settings;
      });

      services.AddSingleton<AuthenticationServiceOptions>(sp =>
      {
        var googleProviderSettings = sp.GetRequiredService<GoogleProviderSettings>();

        var opts = new AuthenticationServiceOptions(googleProviderSettings)
        {
          SecureStorageKeys =
          {
            UserAccount = "UserAccount",
            UserTokenIssueDate = "UserTokenIssueDate",
            UserRefreshToken = "UserRefreshToken"
          }
        };

        return opts;
      });

      services.AddSingleton<IAuthenticationService, AuthenticationService>();

      return services;
    }

    public static IServiceCollection AddGoogleApiClient(this IServiceCollection services)
    {
      services.AddScoped<IGoogleApiClient, GoogleApiClient>(sp =>
      {
        var authService = sp.GetRequiredService<IAuthenticationService>();
        var logger = sp.GetRequiredService<ILogger<GoogleApiClient>>();

        Func<Task<Account>> accountProvider = async () =>
        {
          var isAccessTokenExpired = await authService.IsAccessTokenExpiredAsync();

          if (isAccessTokenExpired)
          {
            await authService.RefreshAccessTokenAsync();
          }

          var account = await authService.GetUserAccountAsync();

          return account;
        };


        var client = new GoogleApiClient(logger, accountProvider);

        return client;
      });

      return services;
    }

    public static IServiceCollection AddParkingSpaceWebApiClient(this IServiceCollection services)
    {
      services
        .AddParkingSpaceWebApiClient(builder => {
          builder.ConfigureBase("http://192.168.0.3:5000");
        })
        .AddParkingSpaceWebApiAccessTokenProvider<ParkingSpaceWebApiAccessTokenProvider>()
        ;

      return services;
    }

    public static IServiceCollection AddTinyMapper(this IServiceCollection services)
    {
      UserMappingProfile.Init();

      return services;
    }
  }
}
