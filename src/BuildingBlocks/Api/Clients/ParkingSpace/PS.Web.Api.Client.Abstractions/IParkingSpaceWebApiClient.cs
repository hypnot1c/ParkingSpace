using System.Threading.Tasks;
using PS.Web.Api.Client.Model.Input;
using PS.Web.Api.Client.Model.Output;

namespace PS.Web.Api.Client.Abstractions
{
  public interface IParkingSpaceWebApiClient
  {
    Task<UserOutputModel> User_EnsureCreated(UserInputModel user);
  }
}