using System.Collections.Generic;
using System.Threading.Tasks;
using PS.Web.Api.Model.Output;
using Refit;
using Web.Api.Model;

namespace PS.Web.Api.Client
{
  [HttpInterface]
  public interface IParkingGroupsHttpInterface
  {
    [Get("/v1/parkingGroups")]
    Task<OkApiResponse<IEnumerable<ParkingGroupOutputModel>>> Get();
  }
}
