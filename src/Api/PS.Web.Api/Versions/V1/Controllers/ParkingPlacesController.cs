using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PS.Web.Api.Versions.V1
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
    [AllowAnonymous]
    public async Task<ActionResult> Get()
    {
      var psListRequest = new ParkingPlacesGetRequest();

      var result = await this.Mediator.Send(psListRequest);

      return Ok(result);
    }
  }
}
