using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.Web.Api.Model.Output;

namespace PS.Web.Api.Versions.V1
{
  public class ParkingGroupsController : ApiBaseController
  {
    public ParkingGroupsController(
      IMapper mapper,
      IMediator mediator,
      ILogger<ParkingGroupsController> logger
      ) : base(mapper, mediator, logger)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParkingGroupOutputModel>>> Get()
    {
      var pgListRequest = new ParkingGroupsGetRequest();

      var result = await this.Mediator.Send(pgListRequest);

      return Ok(result);
    }
  }
}
