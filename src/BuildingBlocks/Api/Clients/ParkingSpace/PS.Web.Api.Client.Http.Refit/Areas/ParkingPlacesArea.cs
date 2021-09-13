using System.Collections.Generic;
using System.Threading.Tasks;
using PS.Web.Api.Model.Output;
using Refit;

namespace PS.Web.Api.Client
{
  [DefaultFor(typeof(IParkingPlacesArea))]
  public class ParkingPlacesArea : IParkingPlacesArea
  {
    public ParkingPlacesArea(string baseUrl)
    {
      this._http = RestService.For<IParkingPlacesHttpInterface>(baseUrl);
    }

    public ParkingPlacesArea(IParkingPlacesHttpInterface http)
    {
      this._http = http;
    }

    private readonly IParkingPlacesHttpInterface _http;

    public async Task<IEnumerable<ParkingPlaceOutputModel>> Get()
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
  }
}
