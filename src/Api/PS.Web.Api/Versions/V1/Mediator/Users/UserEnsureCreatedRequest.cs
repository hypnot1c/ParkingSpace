using MediatR;
using PS.Web.Api.Model.Output.V1;

namespace PS.Web.Api.Versions.V1
{
  public class UserEnsureCreatedRequest : IRequest<UserOutputModel>
  {
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string AvatarUrl { get; set; }
  }
}
