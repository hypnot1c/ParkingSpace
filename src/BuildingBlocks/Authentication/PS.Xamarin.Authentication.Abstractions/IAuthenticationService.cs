using System.Threading.Tasks;
using Xamarin.Auth;

namespace PS.Xamarin.Authentication
{
  public interface IAuthenticationService
  {
    Task<bool> IsAuthenticatedAsync();
    Task<bool> IsTokenExpiredAsync();
    Task AuthenticateAsync(Account account);
    void SignOut();
    Task<Account> GetUserAccountAsync();
    Task RefreshTokenAsync();
  }
}
