using MediatR;
using PS.Web.Api.Model.Output;

namespace PS.Web.Api.Versions.V1
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
