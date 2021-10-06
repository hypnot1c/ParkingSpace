using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PS.Data.Master.Context;
using PS.Web.Api.Model.Output.V1.Management;

namespace PS.Web.Api.Versions.V1.Management
{
  public class ParkingGroupsGetRequestHandler
    : BaseRequestHandler, IRequestHandler<ParkingGroupsGetRequest, IEnumerable<ParkingGroupOutputModel>>
  {
    public ParkingGroupsGetRequestHandler(
      IMapper mapper,
      MasterContext masterContext
      ) : base(mapper, masterContext)
    {
    }

    public async Task<IEnumerable<ParkingGroupOutputModel>> Handle(
      ParkingGroupsGetRequest request,
      CancellationToken cancellationToken
      )
    {
      var pgList = await this.Mapper.ProjectTo<ParkingGroupOutputModel>(
          this.MasterContext.ParkingGroups.AsQueryable()
        )
        .ToListAsync()
        ;

      return pgList;
    }
  }
}
