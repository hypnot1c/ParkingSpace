using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.Web.Api.Model.Output.V1.Management;

namespace PS.Web.Api.Versions.V1.Management
{
  public class ParkingPlacesController : ApiBaseController
  {
    public ParkingPlacesController(
      IMapper mapper,
      IMediator mediator,
      ILogger<ParkingPlacesController> logger
      ) : base(mapper, mediator, logger)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParkingPlaceOutputModel>>> Get()
    {
      var psListRequest = new ParkingPlacesGetRequest();

      var result = await this.Mediator.Send(psListRequest);

      return Ok(result);
    }
  }
}
