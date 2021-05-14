using System.Threading.Tasks;
using PS.Web.Api.Client.Abstractions;
using PS.Web.Api.Client.Model.Input;
using PS.Web.Api.Client.Model.Output;

namespace PS.Web.Api.Client
{
  public class ParkingSpaceWebApiClient : IParkingSpaceWebApiClient
  {
    public async Task<UserOutputModel> User_EnsureCreated(UserInputModel user)
    {
      var result = new UserOutputModel
      {
        Id = 1,
        Firstname = user.Firstname,
        Lastname = user.Lastname,
        Email = user.Email,
        AvatarUrl = user.AvatarUrl
      };

      var res = await Task.FromResult(result);

      return res;
    }
  }
}
