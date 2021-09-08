using MediatR;
using PS.Web.Api.Model.Output;

namespace PS.Web.Api.Versions.V1
{
  public class UserGetRequest : IRequest<UserOutputModel>
  {
    public UserGetRequest(int id)
    {
      this.Id = id;
    }

    public int Id { get; set; }
  }
}
