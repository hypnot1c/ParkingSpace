using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PS.Data.Master.Context;
using PS.Data.Master.Model;
using PS.Web.Api.Model.Output.V1.Management;

namespace PS.Web.Api.Versions.V1.Management
{
  public class ParkingGroupCreateRequestHandler
    : BaseRequestHandler, IRequestHandler<ParkingGroupCreateRequest, ParkingGroupOutputModel>
  {
    public ParkingGroupCreateRequestHandler(
      IMediator mediator,
      IMapper mapper,
      MasterContext masterContext
      ) : base(mapper, masterContext)
    {
      this._mediator = mediator;
    }

    private readonly IMediator _mediator;

    public async Task<ParkingGroupOutputModel> Handle(
      ParkingGroupCreateRequest request,
      CancellationToken cancellationToken
      )
    {
      var parkingGroup = this.Mapper.Map<ParkingGroupModel>(request);

      this.MasterContext.ParkingGroups.Add(parkingGroup);

      await this.MasterContext.SaveChangesAsync();

      var pgGetRequest = new ParkingGroupGetRequest(parkingGroup.Id);

      var result = await this._mediator.Send(pgGetRequest);

      return result;
    }
  }
}
