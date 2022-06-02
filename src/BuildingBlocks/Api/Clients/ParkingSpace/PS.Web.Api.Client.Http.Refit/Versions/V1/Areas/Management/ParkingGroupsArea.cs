using System.Collections.Generic;
using System.Threading.Tasks;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output.V1.Management;
using Refit;

namespace PS.Web.Api.Client.V1.Management
{
  [DefaultFor(typeof(IParkingGroupsArea))]
  public class ParkingGroupsArea : IParkingGroupsArea
  {
    public ParkingGroupsArea(IV1HttpInterface http)
    {
      this._http = http;
    }

    private readonly IV1HttpInterface _http;

    public async Task<ParkingGroupOutputModel> Get(int id)
    {
      try
      {
        var response = await this._http.Management_ParkingGroup_Get(id);

        return response.Result;
      }
      catch (ApiException ex)
      {
        throw new ParkingSpaceWebApiClientException($"An error occuted on the request. See more details in the inner exception.", ex);
      }
    }

    public async Task<IEnumerable<ParkingGroupOutputModel>> Get()
    {
      try
      {
        var response = await this._http.Management_ParkingGroup_Get();

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
        var response = await this._http.Management_ParkingGroup_Create(im);

        return response.Result;
      }
      catch (ApiException ex)
      {
        throw new ParkingSpaceWebApiClientException($"An error occuted on the request. See more details in the inner exception.", ex);
      }
    }
  }
}
