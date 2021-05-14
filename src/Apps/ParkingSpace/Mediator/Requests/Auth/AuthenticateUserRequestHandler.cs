using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PS.Xamarin.Authentication;

namespace ParkingSpace.Mediator
{
  public class AuthenticateUserRequestHandler : IRequestHandler<AuthenticateUserRequest>
  {
    public AuthenticateUserRequestHandler(
      IMediator mediator,
      IAuthenticationService authenticationService
      )
    {
      this._mediator = mediator;
      this._authenticationService = authenticationService;
    }

    private readonly IMediator _mediator;
    private readonly IAuthenticationService _authenticationService;

    public async Task<Unit> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
    {
      var account = request.Account;
      await this._authenticationService.AuthenticateAsync(account);

      var userAuthNotification = new UserAuthenticatedNotification();
      await this._mediator.Publish(userAuthNotification);

      return Unit.Value;
    }
  }
}
