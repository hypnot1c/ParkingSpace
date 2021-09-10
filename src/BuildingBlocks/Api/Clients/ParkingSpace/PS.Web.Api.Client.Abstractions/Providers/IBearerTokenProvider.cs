using System.Threading.Tasks;

namespace PS.Web.Api.Client
{
  public interface IBearerTokenProvider
  {
    Task<string> Get();
  }
}
