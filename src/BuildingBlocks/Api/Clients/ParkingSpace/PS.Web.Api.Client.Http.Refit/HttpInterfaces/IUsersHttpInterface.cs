using System.Threading.Tasks;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output;
using Refit;
using Web.Api.Model;

namespace PS.Web.Api.Client
{
  [HttpInterface]
  public interface IUsersHttpInterface
  {
    [Get("/v1/users/{id}")]
    Task<OkApiResponse<UserOutputModel>> Get(int id);

    [Post("/v1/users/ensure-created")]
    Task<OkApiResponse<UserOutputModel>> EnsureCreated(UserInputModel im);
  }
}
