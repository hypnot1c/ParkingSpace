using MediatR;
using PS.Web.Api.Model.Output;

namespace PS.Web.Api.Versions.V1
{
  public class ParkingGroupCreateRequest : IRequest<ParkingGroupOutputModel>
  {
    public string Name { get; set; }
  }
}
