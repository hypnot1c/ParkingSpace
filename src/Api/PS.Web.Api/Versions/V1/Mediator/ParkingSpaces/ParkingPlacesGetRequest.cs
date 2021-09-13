using System.Collections.Generic;
using MediatR;
using PS.Web.Api.Model.Output;

namespace PS.Web.Api.Versions.V1
{
  public class ParkingPlacesGetRequest : IRequest<IEnumerable<ParkingPlaceOutputModel>>
  {

  }
}
