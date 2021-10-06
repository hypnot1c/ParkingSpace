using System.Threading.Tasks;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output.V1;
using Refit;

namespace PS.Web.Api.Client.V1
{
  [DefaultFor(typeof(IUsersArea))]
  public class UsersArea : IUsersArea
  {
    public UsersArea(IV1HttpInterface http)
    {
      this._http = http;
    }

    private readonly IV1HttpInterface _http;

    public async Task<UserOutputModel> Get(int id)
    {
      try
      {
        var response = await this._http.User_Get(id);

        return response.Result;
      }
      catch (ApiException ex)
      {
        throw new ParkingSpaceWebApiClientException($"An error occuted on the request. See more details in the inner exception.", ex);
      }
    }

    public async Task<UserOutputModel> Get(string email)
    {
      try
      {
        var response = await this._http.User_Get(email);

        return response.Result;
      }
      catch (ApiException ex)
      {
        throw new ParkingSpaceWebApiClientException($"An error occuted on the request. See more details in the inner exception.", ex);
      }
    }

    public async Task<UserOutputModel> EnsureCreated(UserInputModel im)
    {
      try
      {
        var response = await this._http.User_EnsureCreated(im);

        return response.Result;
      }
      catch (ApiException ex)
      {
        throw new ParkingSpaceWebApiClientException($"An error occuted on the request. See more details in the inner exception.", ex);
      }
    }
  }
}
