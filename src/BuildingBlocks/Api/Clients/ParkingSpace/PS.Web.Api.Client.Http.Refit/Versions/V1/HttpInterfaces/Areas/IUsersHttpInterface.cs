using System.Threading.Tasks;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output.V1;
using Refit;
using Web.Api.Model;

namespace PS.Web.Api.Client.V1
{
  public partial interface IV1HttpInterface
  {
    [Get("/v1/users/{id}")]
    Task<OkApiResponse<UserOutputModel>> User_Get(int id);

    [Get("/v1/users/{email}")]
    Task<OkApiResponse<UserOutputModel>> User_Get(string email);

    [Post("/v1/users/ensure-created")]
    Task<OkApiResponse<UserOutputModel>> User_EnsureCreated(UserInputModel im);
  }
}
