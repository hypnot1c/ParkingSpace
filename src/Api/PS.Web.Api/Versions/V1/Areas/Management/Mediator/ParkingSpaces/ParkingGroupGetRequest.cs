using MediatR;
using PS.Web.Api.Model.Output.V1.Management;

namespace PS.Web.Api.Versions.V1.Management
{
  public class ParkingGroupGetRequest : IRequest<ParkingGroupOutputModel>
  {
    public ParkingGroupGetRequest(int parkingGroupId)
    {
      this.Id = parkingGroupId;
    }

    public int Id { get; set; }
  }
}
