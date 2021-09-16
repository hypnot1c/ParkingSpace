using System.Collections.Generic;
using System.Threading.Tasks;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output;
using Refit;

namespace PS.Web.Api.Client
{
  [DefaultFor(typeof(IParkingGroupsArea))]
  public class ParkingGroupsArea : IParkingGroupsArea
  {
    public ParkingGroupsArea(string baseUrl)
    {
      this._http = RestService.For<IParkingGroupsHttpInterface>(baseUrl);
    }

    public ParkingGroupsArea(IParkingGroupsHttpInterface http)
    {
      this._http = http;
    }

    private readonly IParkingGroupsHttpInterface _http;

    public async Task<IEnumerable<ParkingGroupOutputModel>> Get()
    {
      try
      {
        var response = await this._http.Get();

        return response.Result;
      }
      catch (ApiException ex)
      {
        throw new ParkingSpaceWebApiClientException($"An error occuted on the request. See more details in the inner exception.", ex);
      }
    }

    public async Task<ParkingGroupOutputModel> Create(ParkingGroupInputModel im)
    {
      try
      {
        var response = await this._http.Create(im);

        return response.Result;
      }
      catch (ApiException ex)
      {
        throw new ParkingSpaceWebApiClientException($"An error occuted on the request. See more details in the inner exception.", ex);
      }
    }
  }
}
