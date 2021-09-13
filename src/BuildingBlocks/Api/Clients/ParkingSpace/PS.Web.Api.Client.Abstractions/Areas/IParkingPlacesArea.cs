using System.Collections.Generic;
using System.Threading.Tasks;
using PS.Web.Api.Model.Output;

namespace PS.Web.Api.Client
{
  public interface IParkingPlacesArea
  {
    Task<IEnumerable<ParkingPlaceOutputModel>> Get();
  }
}
