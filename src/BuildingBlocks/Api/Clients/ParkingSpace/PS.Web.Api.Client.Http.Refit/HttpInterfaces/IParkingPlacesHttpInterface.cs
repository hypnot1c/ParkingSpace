using System.Collections.Generic;
using System.Threading.Tasks;
using PS.Web.Api.Model.Output;
using Refit;
using Web.Api.Model;

namespace PS.Web.Api.Client
{
  [HttpInterface]
  public interface IParkingPlacesHttpInterface
  {
    [Get("/v1/parkingPlaces")]
    Task<OkApiResponse<IEnumerable<ParkingPlaceOutputModel>>> Get();
  }
}
