using System.Threading.Tasks;

namespace Authorization.Abstractions
{
  public interface IBearerTokenProvider
  {
    Task<string> Get();
  }
}
