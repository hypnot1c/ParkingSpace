using MediatR;
using PS.Web.Api.Model.Output.V1.Management;

namespace PS.Web.Api.Versions.V1.Management
{
  public class ParkingGroupCreateRequest : IRequest<ParkingGroupOutputModel>
  {
    public string Name { get; set; }
  }
}
