using System.Collections.Generic;
using MediatR;
using PS.Web.Api.Model.Output.V1.Management;

namespace PS.Web.Api.Versions.V1.Management
{
  public class ParkingGroupsGetRequest : IRequest<IEnumerable<ParkingGroupOutputModel>>
  {

  }
}
