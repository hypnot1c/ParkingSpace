using System.Threading.Tasks;
using Api.Google.Client.Model.Output;

namespace Api.Google.Client
{
  public interface IGoogleApiClient
  {
    Task<UserOutputModel> GetUserAsync();
  }
}
