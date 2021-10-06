using System.Threading.Tasks;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output.V1;

namespace PS.Web.Api.Client.V1
{
  public interface IUsersArea
  {
    Task<UserOutputModel> Get(int id);
    Task<UserOutputModel> Get(string email);
    Task<UserOutputModel> EnsureCreated(UserInputModel user);
  }
}
