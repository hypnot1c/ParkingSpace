using MediatR;
using Xamarin.Auth;

namespace ParkingSpace.Mediator
{
  public class AuthenticateUserRequest : IRequest
  {
    public AuthenticateUserRequest(Account account)
    {
      Account = account;
    }

    public Account Account { get; }
  }
}
