using System.Threading.Tasks;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output;

namespace PS.Web.Api.Client
{
  public interface IUsersArea
  {
    Task<UserOutputModel> Get(int id);
    Task<UserOutputModel> Get(string email);
    Task<UserOutputModel> EnsureCreated(UserInputModel user);
  }
}
