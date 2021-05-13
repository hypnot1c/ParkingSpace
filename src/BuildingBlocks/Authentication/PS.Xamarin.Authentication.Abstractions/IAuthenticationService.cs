using System.Threading.Tasks;
using Xamarin.Auth;

namespace PS.Xamarin.Authentication
{
  public interface IAuthenticationService
  {
    Task<bool> IsAuthenticatedAsync();
    Task<bool> IsAccessTokenExpiredAsync();
    Task AuthenticateAsync(Account account);
    Task<Account> GetUserAccountAsync();
    Task RefreshAccessTokenAsync();
  }
}
