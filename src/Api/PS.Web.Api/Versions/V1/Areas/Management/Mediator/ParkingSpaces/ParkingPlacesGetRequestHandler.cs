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
  public class ParkingPlacesGetRequestHandler
    : BaseRequestHandler, IRequestHandler<ParkingPlacesGetRequest, IEnumerable<ParkingPlaceOutputModel>>
  {
    public ParkingPlacesGetRequestHandler(
      IMapper mapper,
      MasterContext masterContext
      ) : base(mapper, masterContext)
    {
    }

    public async Task<IEnumerable<ParkingPlaceOutputModel>> Handle(
      ParkingPlacesGetRequest request,
      CancellationToken cancellationToken
      )
    {
      var psList = await this.Mapper.ProjectTo<ParkingPlaceOutputModel>(
          this.MasterContext.ParkingSpaces.AsQueryable()
        )
        .ToListAsync()
        ;

      return psList;
    }
  }
}
