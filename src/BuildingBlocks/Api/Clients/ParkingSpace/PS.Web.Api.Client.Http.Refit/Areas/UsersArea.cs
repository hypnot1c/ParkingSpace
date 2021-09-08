using System.Threading.Tasks;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output;
using Refit;

namespace PS.Web.Api.Client
{
  [DefaultFor(typeof(IUsersArea))]
  public class UsersArea : IUsersArea
  {
    public UsersArea(string baseUrl)
    {
      this._http = RestService.For<IUsersHttpInterface>(baseUrl);
    }

    public UsersArea(IUsersHttpInterface http)
    {
      this._http = http;
    }

    private readonly IUsersHttpInterface _http;

    public async Task<UserOutputModel> Get(int id)
    {
      try
      {
        var response = await this._http.Get(id);

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
        var response = await this._http.EnsureCreated(im);

        return response.Result;
      }
      catch (ApiException ex)
      {
        throw new ParkingSpaceWebApiClientException($"An error occuted on the request. See more details in the inner exception.", ex);
      }
    }
  }
}
