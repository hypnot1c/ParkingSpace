using System;
using MediatR;
using Microsoft.Extensions.Logging;
using ParkingSpace.Mediator;
using ParkingSpace.Resources;
using ParkingSpace.Views;
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
      IMediator mediator,
      GoogleProviderSettings googleProviderSettings,
      ILogger<LoginViewModel> logger
      )
    {
      this.SignInCommand = new DelegateCommand(this.OnSignInButton_Clicked);

      this._navigationService = navigationService;
      this._mediator = mediator;
      this._googleProviderSettings = googleProviderSettings;
      this._logger = logger;
    }
    public DelegateCommand SignInCommand { get; }

    private bool _showLoadingIndicator;
    public bool ShowLoadingIndicator
    {
      get => _showLoadingIndicator;
      set => SetProperty(ref _showLoadingIndicator, value);
    }

    private readonly INavigationService _navigationService;
    private readonly IMediator _mediator;
    private readonly GoogleProviderSettings _googleProviderSettings;
    private readonly ILogger<LoginViewModel> _logger;

    private void OnSignInButton_Clicked()
    {
      this.ShowLoadingIndicator = true;
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
        var authUserRequest = new AuthenticateUserRequest(e.Account);
        await this._mediator.Send(authUserRequest);

        await this._navigationService.NavigateAsync(nameof(FlyoutView));
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

      this._logger.LogError("Authentication error: " + e.Message);
    }
  }
}
