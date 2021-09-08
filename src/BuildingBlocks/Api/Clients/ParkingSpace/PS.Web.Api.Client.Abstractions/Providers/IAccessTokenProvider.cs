using System.Threading.Tasks;

namespace PS.Web.Api.Client
{
  public interface IAccessTokenProvider
  {
    Task<string> Get();
  }
}
