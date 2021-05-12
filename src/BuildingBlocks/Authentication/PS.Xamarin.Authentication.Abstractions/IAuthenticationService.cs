using System.Threading.Tasks;

namespace PS.Xamarin.Authentication
{
  public interface IAuthenticationService
  {
    Task<bool> IsAuthenticatedAsync();
  }
}
