using System;
using System.Diagnostics;
using ParkingSpace.Resources;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PS.Xamarin.Authentication;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;

namespace ParkingSpace.ViewModels
{
  public class LoginViewModel : BindableBase
  {
    public LoginViewModel(
      INavigationService navigationService,
      IAuthenticationService authenticationService,
      GoogleProviderSettings googleProviderSettings
      )
    {
      this.SignInCommand = new DelegateCommand(this.OnSignInButton_Clicked);

      this._navigationService = navigationService;
      this._authenticationService = authenticationService;
      this._googleProviderSettings = googleProviderSettings;
    }
    public DelegateCommand SignInCommand { get; }

    private readonly INavigationService _navigationService;
    private readonly IAuthenticationService _authenticationService;
    private readonly GoogleProviderSettings _googleProviderSettings;

    private void OnSignInButton_Clicked()
    {
      string clientId = this._googleProviderSettings.ClientId;
      string redirectUri = this._googleProviderSettings.RedirectUri;

      var authenticator = new OAuth2Authenticator(
        clientId,
        null,
        this._googleProviderSettings.Scopes,
        new Uri(this._googleProviderSettings.AuthorizeUrl),
        new Uri(redirectUri),
        new Uri(this._googleProviderSettings.AccessTokenUrl),
        null,
        true
        );

      authenticator.Completed += OnAuthCompleted;
      authenticator.Error += OnAuthError;

      AuthenticationState.Authenticator = authenticator;

      var presenter = new OAuthLoginPresenter();

      presenter.Login(authenticator);
    }

    async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
    {
      var authenticator = sender as OAuth2Authenticator;
      if (authenticator != null)
      {
        authenticator.Completed -= OnAuthCompleted;
        authenticator.Error -= OnAuthError;
      }

      if (e.IsAuthenticated)
      {
        await this._authenticationService.AuthenticateAsync(e.Account);

        await this._navigationService.NavigateAsync("MainView");
      }
    }

    void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
    {
      var authenticator = sender as OAuth2Authenticator;
      if (authenticator != null)
      {
        authenticator.Completed -= OnAuthCompleted;
        authenticator.Error -= OnAuthError;
      }

      Debug.WriteLine("Authentication error: " + e.Message);
    }
  }
}
