using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PS.Data.Master.Context;
using PS.Web.Api.Model.Output.V1.Management;

namespace PS.Web.Api.Versions.V1.Management
{
  public class ParkingGroupGetRequestHandler
    : BaseRequestHandler, IRequestHandler<ParkingGroupGetRequest, ParkingGroupOutputModel>
  {
    public ParkingGroupGetRequestHandler(
      IMapper mapper,
      MasterContext masterContext
      ) : base(mapper, masterContext)
    {
    }

    public async Task<ParkingGroupOutputModel> Handle(
      ParkingGroupGetRequest request,
      CancellationToken cancellationToken
      )
    {
      var pg = await this.Mapper.ProjectTo<ParkingGroupOutputModel>(
          this.MasterContext.ParkingGroups
            .Where(pg => pg.Id == request.Id)
        )
        .SingleOrDefaultAsync()
        ;

      return pg;
    }
  }
}
