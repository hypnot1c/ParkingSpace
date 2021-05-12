using ParkingSpace.Services.Abstractions;

namespace ParkingSpace.Services
{
  public class AuthenticationService : IAuthenticationService
  {
    public bool IsAuthenticated { get; }
  }
}
