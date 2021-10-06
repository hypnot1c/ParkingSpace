using System.Collections.Generic;
using System.Threading.Tasks;
using PS.Web.Api.Model.Output.V1.Management;
using Refit;
using Web.Api.Model;

namespace PS.Web.Api.Client.V1
{
  public partial interface IV1HttpInterface
  {
    [Get("/v1/management/parkingPlaces")]
    Task<OkApiResponse<IEnumerable<ParkingPlaceOutputModel>>> Management_ParkingPlace_Get();
  }
}
