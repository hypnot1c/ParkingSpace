using System.Collections.Generic;
using System.Threading.Tasks;
using PS.Web.Api.Model.Output.V1.Management;
using Refit;

namespace PS.Web.Api.Client.V1.Management
{
  [DefaultFor(typeof(IParkingPlacesArea))]
  public class ParkingPlacesArea : IParkingPlacesArea
  {
    public ParkingPlacesArea(IV1HttpInterface http)
    {
      this._http = http;
    }

    private readonly IV1HttpInterface _http;

    public async Task<IEnumerable<ParkingPlaceOutputModel>> Get()
    {
      try
      {
        var response = await this._http.Management_ParkingPlace_Get();

        return response.Result;
      }
      catch (ApiException ex)
      {
        throw new ParkingSpaceWebApiClientException($"An error occuted on the request. See more details in the inner exception.", ex);
      }
    }
  }
}
