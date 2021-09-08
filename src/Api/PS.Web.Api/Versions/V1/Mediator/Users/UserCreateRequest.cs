using MediatR;
using PS.Web.Api.Model.Output;

namespace PS.Web.Api.Versions.V1
{
  public class UserCreateRequest : IRequest<UserOutputModel>
  {
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
  }
}
